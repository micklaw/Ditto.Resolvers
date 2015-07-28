using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Archetype.Models;
using Our.Umbraco.Ditto.Resolvers.Archetype.Attributes;
using Our.Umbraco.Ditto.Resolvers.Shared.Internal;
using Our.Umbraco.Ditto.Resolvers.Shared.Services;
using Our.Umbraco.Ditto.Resolvers.Shared.Services.Abstract;
using Umbraco.Core;
using Umbraco.Core.Models;

namespace Our.Umbraco.Ditto.Resolvers.Archetype.Extensions
{
    public static class ArchetypeModelExtensions
    {
        /// <summary>
        /// The cache for storing constructor parameter information.
        /// </summary>
        private static readonly ConcurrentDictionary<string, Type> _archetypeCache
            = new ConcurrentDictionary<string, Type>();

        /// <summary>
        /// The cache for storing type property information.
        /// </summary>
        private static readonly ConcurrentDictionary<Type, PropertyInfo[]> _propertyCache
            = new ConcurrentDictionary<Type, PropertyInfo[]>();

        /// <summary>
        /// Add properties of type to the cache
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static void AddToPropertyCache(Type type)
        {
            PropertyInfo[] properties;
            _propertyCache.TryGetValue(type, out properties);

            if (properties == null)
            {
                properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                        .Where(x => x.CanWrite)
                        .ToArray();

                _propertyCache.TryAdd(type, properties);
            }
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
        /// <returns></returns>
        private static object GetTypedArchetype(ArchetypeModel archetype, Type entityType, CultureInfo culture = null, IPublishedContent content = null)
        {
            // [ML] - Hurry up Elvis operator ?. !!

            if (archetype != null && (archetype.Fieldsets != null && archetype.Fieldsets.Any()))
            {
                // [ML] - Identify type

                var isGenericList = entityType.IsGenericType &&
                                    (entityType.GetGenericTypeDefinition() == typeof (IList<>) ||
                                     entityType.GetGenericTypeDefinition() == typeof (List<>));
                var propertyType = isGenericList ? entityType.GetGenericArguments().FirstOrDefault() : entityType;

                if (propertyType == null)
                {
                    throw new NullReferenceException(string.Format("The type ({0}) can not be inferred?",
                        entityType.Name));
                }

                // [ML] - Build a generic list from the type found above

                var constructedListType = typeof (List<>).MakeGenericType(propertyType);
                var list = (IList) Activator.CreateInstance(constructedListType);

                // [ML] - We have work to do, so get the service for resolving properties

                var service =
                    DependencyResolver.Current.GetService<PropertyValueService>() ??
                    DependencyResolver.Current.GetService<DittoValueService>();

                if (service == null)
                {
                    throw new NullReferenceException(
                        string.Format("No service found which implements '{0}'.",
                            typeof (PropertyValueService).FullName));
                }

                foreach (var fieldset in archetype.Fieldsets)
                {
                    if (fieldset.Properties != null && fieldset.Properties.Any())
                    {
                        Type instanceType;

                        _archetypeCache.TryGetValue(fieldset.Alias, out instanceType);

                        if (instanceType == null)
                        {
                            // [ML] - find the first class which matches name and base type

                            instanceType =
                                TypeHelper.GetTypeByName<ArchetypeFieldsetModel>(fieldset.Alias).FirstOrDefault();

                            if (instanceType != null)
                            {
                                _archetypeCache.TryAdd(fieldset.Alias, instanceType);

                                AddToPropertyCache(instanceType);
                            }
                        }

                        if (instanceType != null)
                        {
                            // [ML] - Create an instance for each archetype object

                            var instance = Activator.CreateInstance(instanceType) as ArchetypeFieldsetModel;

                            if (instance != null)
                            {
                                instance.Properties = fieldset.Properties;
                                instance.Disabled = fieldset.Disabled;
                                instance.Alias = fieldset.Alias;

                                PropertyInfo[] properties;
                                _propertyCache.TryGetValue(instanceType, out properties);

                                if (properties != null && properties.Any())
                                {
                                    foreach (var propertyInfo in properties)
                                    {
                                        // [ML] - Get the alias for the property incase any child items are Archetypes

                                        var alias = propertyInfo.Name;
                                        var attribute =
                                            propertyInfo.GetCustomAttributes(typeof (ArchetypeResolverAttribute))
                                                .FirstOrDefault() as ArchetypeResolverAttribute;

                                        if (attribute != null && !string.IsNullOrWhiteSpace(attribute.PropertyAlias))
                                        {
                                            alias = attribute.PropertyAlias;
                                        }

                                        // [ML] - Find any matching properties on the content

                                        var property =
                                            fieldset.Properties.FirstOrDefault(i => i.Alias.ToLower() == alias.ToLower());

                                        if (property != null)
                                        {
                                            var childArchetype = property.GetValue<ArchetypeModel>();

                                            // [ML] - If this is an archetype, then kick off ths process again

                                            if (childArchetype != null)
                                            {
                                                propertyInfo.SetValue(instance,
                                                    childArchetype.As(propertyInfo.PropertyType, culture, content));
                                            }
                                            else
                                            {
                                                propertyInfo.SetValue(instance,
                                                    service.Set(content, culture, propertyInfo, property.Value,
                                                        instance));
                                            }
                                        }
                                    }

                                    // [ML] - If this is not a generic type, then return the first item

                                    if (!isGenericList)
                                    {
                                        return instance;
                                    }

                                    list.Add(instance);
                                }
                            }
                        }
                    }
                }

                return isGenericList ? list : null;
            }

            return null;
        }

        /// <summary>
        /// Returns the given instance of <see cref="ArchetypeModel"/> as the specified type.
        /// </summary>
        /// <param name="archetype">
        /// The <see cref="ArchetypeModel"/> to convert.
        /// </param>
        /// <param name="content">
        /// 
        /// </param>
        /// <param name="culture">
        /// The <see cref="CultureInfo"/>
        /// </param>
        /// <typeparam name="T">
        /// The <see cref="Type"/> of items to return.
        /// </typeparam>
        /// <returns>
        /// The resolved <see cref="T"/>.
        /// </returns>
        public static T As<T>(
            this ArchetypeModel archetype,
            IPublishedContent content = null,
            CultureInfo culture = null)
            where T : class
        {
            return As(archetype, typeof(T), culture, content) as T;
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
        /// <returns>
        /// The converted <see cref="Object"/> as the given type.
        /// </returns>
        public static object As(
            this ArchetypeModel archetype,
            Type type,
            CultureInfo culture = null,
            IPublishedContent content = null)
        {
            if (archetype == null)
            {
                return null;
            }

            using (DisposableTimer.DebugDuration(type, string.Format("ArchetypeModel As ({0})", type.Name), "Complete"))
            {
                return GetTypedArchetype(archetype, type, culture, content);
            }
        }
    }
}
