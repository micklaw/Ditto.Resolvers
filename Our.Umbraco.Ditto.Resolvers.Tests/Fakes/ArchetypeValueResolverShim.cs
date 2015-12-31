using System.Globalization;
using Our.Umbraco.Ditto.Resolvers.Archetype.Attributes;
using Our.Umbraco.Ditto.Resolvers.Archetype.Resolvers;
using Our.Umbraco.Ditto.Resolvers.Archetype.Services;
using Umbraco.Core.Models;

namespace Our.Umbraco.Ditto.Resolvers.Tests.Fakes
{
    public class ArchetypeValueResolverShim : ArchetypeValueResolver
    {
        public ArchetypeValueResolverShim(DittoValueResolverContext context, CultureInfo culture, ArchetypeValueResolverAttribute attribute) : base(context, new ArchetypeBindingService())
        {
            base.Context = context;
            base.Culture = culture;
            base.Content = context.Instance as IPublishedContent;
            base.Attribute = attribute;
        }
    }
}
