using System.ComponentModel;
using System.Globalization;
using Archetype.Models;
using Newtonsoft.Json;
using Our.Umbraco.Ditto.Resolvers.Archetype.Attributes;
using Our.Umbraco.Ditto.Resolvers.Archetype.Extensions;
using Umbraco.Core.Models;
using Umbraco.Core.Models.PublishedContent;

namespace Our.Umbraco.Ditto.Resolvers.Archetype.Resolvers
{
    public class ArchetypeValueResolver : DittoValueResolver<ArchetypeResolverAttribute>
    {
        public override object ResolveValue(ITypeDescriptorContext context, ArchetypeResolverAttribute attribute, CultureInfo culture)
        {
            var content = context.Instance as IPublishedContent;
            var descriptor = context.PropertyDescriptor;

            if (content != null && descriptor != null)
            {
                var alias = attribute.PropertyAlias ?? descriptor.DisplayName;
                var property = content.GetProperty(alias);

                if (property.HasValue)
                {
                    var archetype = property.Value as ArchetypeModel;

                    if (archetype != null)
                    {
                        return archetype.As(descriptor.PropertyType, culture, content);
                    }
                }
            }

            return null;
        }
    }
}
