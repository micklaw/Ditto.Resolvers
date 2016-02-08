using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Archetype.Models;
using Our.Umbraco.Ditto.Resolvers.Archetype.Attributes;
using Our.Umbraco.Ditto.Resolvers.Archetype.Models.Abstract;
using Our.Umbraco.Ditto.Resolvers.Archetype.Services.Abstract;
using Our.Umbraco.Ditto.Resolvers.Archetype.Utils;
using Our.Umbraco.Ditto.Resolvers.Container;
using Our.Umbraco.Ditto.Resolvers.Container.Abstract;
using Our.Umbraco.Ditto.Resolvers.Shared.Internal;
using Our.Umbraco.Ditto.Resolvers.Shared.Services;
using Our.Umbraco.Ditto.Resolvers.Shared.Services.Abstract;
using Umbraco.Core.Models;

namespace Our.Umbraco.Ditto.Resolvers.Archetype.Services
{
    public class ArchetypeBindingService : IArchetypeBindingService
    {
        private PropertyValueService _valueService { get; }
        private IAliasLocator _aliasLocator { get; }

        public ArchetypeBindingService()
        {
            
        }

        public ArchetypeBindingService(PropertyValueService valueService, IAliasLocator aliasLocator)
        {
            _valueService = valueService;
            _aliasLocator = aliasLocator;
        }

        /// <summary>
        /// Method used to convert ArchetypeModel to a stronly typed Model
        /// </summary>
        /// <param name="archetype">
        /// 
        /// </param>
        /// <param name="entityType">
        /// 
        /// </param>
        /// <param name="culture">
        /// 
        /// </param>
        /// <param name="content">
        /// 
        /// </param>
        /// <param name="context"></param>
        /// <returns></returns>
        private object GetTypedArchetype(ArchetypeModel archetype, Type entityType, CultureInfo culture = null, IPublishedContent content = null, DittoValueResolverContext context = null)
        {
            // [ML] - Identify type

            var isGenericList = entityType.IsGenericType && (entityType.GetGenericTypeDefinition() == typeof(IList<>) || entityType.GetGenericTypeDefinition() == typeof(List<>));
            var propertyType = isGenericList ? entityType.GetGenericArguments().FirstOrDefault() : entityType;

            if (propertyType == null)
            {
                throw new NullReferenceException($"The type ({entityType.Name}) can not be inferred?");
            }

            // [ML] - Build a generic list from the type found above

            var constructedListType = typeof(List<>).MakeGenericType(propertyType);
            var list = (IList)Activator.CreateInstance(constructedListType);

            if (archetype?.Fieldsets != null && archetype.Fieldsets.Any())
            {
                // [ML] - We have work to do, so if the service isnt populated get the default implementations

                var service = _valueService ?? DependencyResolver.Current.GetService<DittoValueService>();
                var aliasLocator = _aliasLocator ?? DependencyResolver.Current.GetService<DittoAliasLocator>();

                foreach (var fieldset in archetype.Fieldsets)
                {
                    if (fieldset.Properties != null && fieldset.Properties.Any())
                    {
                        var instanceType = ArchetypeCache.GetTypeFromAlias(fieldset.Alias);

                        if (instanceType == null)
                        {
                            // [ML] - find the first class which matches name and base type

                            var aliasMethod = aliasLocator.Resolve<ArchetypeContentAttribute>();

                            if (aliasMethod == null)
                            {
                                aliasMethod = (mainType) =>
                                {
                                    var attribute = mainType.GetCustomAttribute<ArchetypeContentAttribute>();

                                    if (!string.IsNullOrWhiteSpace(attribute?.Alias))
                                    {
                                        return attribute.Alias;
                                    }

                                    return null;
                                };
                            }

                            instanceType = TypeHelper.GetTypeByAtttribute<ArchetypeContentAttribute>(fieldset.Alias, aliasMethod);

                            if (instanceType != null)
                            {
                                ArchetypeCache.AddTypeByAlias(fieldset.Alias, instanceType);
                            }
                        }

                        // [ML] - If we cant find a mappping at all, then use the property type, but don't cache this as were not provided wth an alias

                        if (instanceType == null)
                        {
                            if (propertyType.IsAbstract)
                            {
                                throw new InvalidCastException($"Property of type ({propertyType.Name}) is abstract, you may be missing an ArchetypeContentAttribute to describe you POCO.");
                            }

                            instanceType = propertyType;
                        }

                        if (instanceType != null)
                        {
                            // [ML] - Cache the properties so were not reflecting them every time then get them again

                            var properties = ArchetypeCache.GetPropertiesFromType(instanceType);

                            if (properties == null)
                            {
                                ArchetypeCache.AddToPropertyCache(instanceType);

                                properties = ArchetypeCache.GetPropertiesFromType(instanceType);
                            }

                            // [ML] - Create an instance for each archetype object

                            var instance = Activator.CreateInstance(instanceType);
                            
                            if (instance is IFieldset)
                            {
                                instanceType.GetProperty("Disabled").SetValue(instance, fieldset.Disabled);
                                instanceType.GetProperty("Alias").SetValue(instance, fieldset.Alias);

                                properties = properties?.Where(i => !(new[] { "alias", "disabled" }.Contains(i.Name.ToLower()))).ToArray();
                            }

                            if (properties != null)
                            {
                                var conversionCtx = new DittoConversionHandlerContext
                                {
                                    Content = content,
                                    ModelType = instanceType,
                                    Model = instance
                                };

                                OnConvert<DittoOnConvertingAttribute>(instanceType, instance, conversionCtx, fieldset, null);

                                foreach (var propertyInfo in properties)
                                {
                                    // [ML] - Default alias to property name, then potentilly get the value resolver alias, then override this with property attribute alias if available

                                    var alias = propertyInfo.Name;
                                    var resolverAttribute = propertyInfo.GetCustomAttributes(typeof (ArchetypeValueResolverAttribute)).FirstOrDefault() as ArchetypeValueResolverAttribute;
                                    var propertyAttribute =propertyInfo.GetCustomAttributes(typeof (ArchetypePropertyAttribute)).FirstOrDefault() as ArchetypePropertyAttribute;

                                    if (!string.IsNullOrWhiteSpace(resolverAttribute?.Alias))
                                    {
                                        alias = resolverAttribute.Alias;
                                    }

                                    if (!string.IsNullOrWhiteSpace(propertyAttribute?.Alias))
                                    {
                                        alias = propertyAttribute.Alias;
                                    }

                                    // [ML] - Find any matching properties on the content from this alias

                                    var property = fieldset.Properties.FirstOrDefault(i => i.Alias.ToLower() == alias.ToLower());

                                    // [ML] - If this is expecting an archetype, then kick off this process again

                                    var childArchetype = property?.GetValue<ArchetypeModel>();

                                    OnConvert<ArchetypeOnPropertyConvertingAttribute>(instanceType, instance, conversionCtx, fieldset, propertyInfo.Name);

                                    if (resolverAttribute != null && childArchetype != null)
                                    {
                                        propertyInfo.SetValue(instance, As(childArchetype, propertyInfo.PropertyType, culture, content, context));
                                    }
                                    else
                                    {
                                        if (resolverAttribute == null)
                                        {
                                            propertyInfo.SetValue(instance, service.Set(content, culture, propertyInfo, property?.Value, instance, context));
                                        }
                                        else
                                        {
                                            throw new InvalidOperationException($"Property '{alias}' on type '{instanceType.Name}' has an ArchetypeValueResolverAttribute but does the not appear to be an Archetype in the generated JSON.");
                                        }
                                    }

                                    OnConvert<ArchetypeOnPropertyConvertedAttribute>(instanceType, instance, conversionCtx, fieldset,propertyInfo.Name);
                                }

                                // [ML] - If this is not a generic type, then return the first item

                                if (!isGenericList)
                                {
                                    OnConvert<DittoOnConvertedAttribute>(instanceType, instance, conversionCtx, fieldset, null);

                                    return instance;
                                }

                                OnConvert<DittoOnConvertedAttribute>(instanceType, instance, conversionCtx, fieldset, null);

                                list.Add(instance);
                            }
                        }
                    }
                }
            }

            return list;
        }
        
        /// <summary>
        /// Invoke the found method
        /// </summary>
        /// <param name="method"></param>
        /// <param name="instance"></param>
        /// <param name="context"></param>
        /// <param name="fieldset"></param>
        private void InvokeConvert(MethodInfo method, object instance, DittoConversionHandlerContext context, ArchetypeFieldsetModel fieldset)
        {
            var p = method.GetParameters();

            if (p.Length == 2 && p[0].ParameterType == typeof(DittoConversionHandlerContext) && p[1].ParameterType == typeof(ArchetypeFieldsetModel))
            {
                method.Invoke(instance, new object[] { context, fieldset });
            }
        }

        /// <summary>
        /// On Converting Do some stuff
        /// </summary>
        /// <param name="instanceType"></param>
        /// <param name="instance"></param>
        /// <param name="context"></param>
        /// <param name="fieldset"></param>
        /// <param name="propertyName"></param>
        private void OnConvert<T>(Type instanceType, object instance, DittoConversionHandlerContext context, ArchetypeFieldsetModel fieldset, string propertyName) where T : Attribute
        {
            Func<MethodInfo, bool> predicate = x => x.GetCustomAttribute<T>() != null;

            if (typeof(IPropertyName).IsAssignableFrom(typeof(T)))
            {
                predicate = x => x.GetCustomAttributes<T>().OfType<IPropertyName>().Count(i => i.PropertyName == propertyName) > 0;
            }
                
            var method = instanceType.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Where(predicate).FirstOrDefault();

            if (method != null)
            {
                InvokeConvert(method, instance, context, fieldset);
            }
        }

        /// <summary>
        /// Returns an object representing the given <see cref="Type"/>.
        /// </summary>
        /// <param name="archetype">
        /// The <see cref="ArchetypeModel"/> to convert.
        /// </param>
        /// <param name="type">
        /// The <see cref="Type"/> of items to return.
        /// </param>
        /// <param name="content">
        /// 
        /// </param>
        /// <param name="culture">
        /// The <see cref="CultureInfo"/>
        /// </param>
        /// <param name="context"></param>
        /// <returns>
        /// The converted <see cref="Object"/> as the given type.
        /// </returns>
        public object As(
            ArchetypeModel archetype,
            Type type,
            CultureInfo culture = null,
            IPublishedContent content = null,
            DittoValueResolverContext context = null)
        {
            return GetTypedArchetype(archetype, type, culture, content, context);
        }
    }
}
