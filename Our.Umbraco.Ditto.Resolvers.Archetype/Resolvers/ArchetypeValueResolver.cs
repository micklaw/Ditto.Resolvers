using System;
using System.Linq;
using System.Web.Mvc;
using Archetype.Models;
using Our.Umbraco.Ditto.Resolvers.Archetype.Attributes;
using Our.Umbraco.Ditto.Resolvers.Archetype.Services;
using Our.Umbraco.Ditto.Resolvers.Archetype.Services.Abstract;
using Umbraco.Core.Models;

namespace Our.Umbraco.Ditto.Resolvers.Archetype.Resolvers
{
    /// <summary>
    /// Resolver class fired by Ditto to complete conversion of Archetype to strong type
    /// </summary>
    public class ArchetypeValueResolver : DittoValueResolver<DittoValueResolverContext, ArchetypeValueResolverAttribute>
    {
        /// <summary>
        /// Binding service used to convert ArchetypeModel to POCO
        /// </summary>
        private IArchetypeBindingService _bindingService;

        public ArchetypeValueResolver()
        {
            
        }

        protected ArchetypeValueResolver(DittoValueResolverContext context, IArchetypeBindingService bindingService)
        {
            Context = context;

            _bindingService = bindingService;
        }

        public override object ResolveValue()
        {
            // [ML] - Use service passed from constructor or grab from the IoC container if used, or just grab the default

            _bindingService = _bindingService ?? DependencyResolver.Current.GetService<IArchetypeBindingService>() ?? DependencyResolver.Current.GetService<ArchetypeBindingService>();

            var content = Context.Instance as IPublishedContent;
            var descriptor = Context.PropertyDescriptor;

            if (content != null && descriptor != null)
            {
                // [ML] - Set the default alias as the property name, or use the valueresolver alias, or finally use the propertyattribute alias

                var alias = descriptor.DisplayName;

                if (!string.IsNullOrWhiteSpace(Attribute?.Alias))
                {
                    alias = Attribute.Alias;
                }

                var propertyAttribute = descriptor.Attributes.OfType<ArchetypePropertyAttribute>().FirstOrDefault();

                if (!string.IsNullOrWhiteSpace(propertyAttribute?.Alias))
                {
                    alias = propertyAttribute.Alias;
                }

                var property = content.GetProperty(alias);

                // [ML] - If the property has an alias and its of ArchetypeModel, then do some shizzle

                var archetype = property != null && property.HasValue ? property.Value as ArchetypeModel : null;

                if (archetype != null)
                {
                    return _bindingService.As(archetype, descriptor.PropertyType, Culture, content, Context);
                }
            }

            return null;
        }
    }
}
