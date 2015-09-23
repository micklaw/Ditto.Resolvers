using System.ComponentModel;
using System.Globalization;
using Archetype.Models;
using Newtonsoft.Json;
using Our.Umbraco.Ditto.Resolvers.Archetype.Attributes;
using Our.Umbraco.Ditto.Resolvers.Archetype.Extensions;
using Our.Umbraco.Ditto.Resolvers.Resolvers.Attributes;
using Umbraco.Core.Models;
using Umbraco.Core.Models.PublishedContent;

namespace Our.Umbraco.Ditto.Resolvers.Archetype.Resolvers
{
    public class ArchetypeValueResolver : DittoValueResolver<DittoValueResolverContext, ArchetypeValueResolverAttribute>
    {
        public override object ResolveValue()
        {
            var content = Context.Instance as IPublishedContent;
            var descriptor = Context.PropertyDescriptor;

            if (content != null && descriptor != null)
            {
                var alias = Attribute.Alias ?? descriptor.DisplayName;
                var property = content.GetProperty(alias);

                if (property.HasValue)
                {
                    var archetype = property.Value as ArchetypeModel;

                    if (archetype != null)
                    {
                        return archetype.As(descriptor.PropertyType, Culture, content, Context);
                    }
                }
            }

            return null;
        }
    }
}
