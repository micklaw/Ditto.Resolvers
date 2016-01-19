using System.Globalization;
using Our.Umbraco.Ditto.Resolvers.Archetype.Attributes;
using Our.Umbraco.Ditto.Resolvers.Archetype.Resolvers;
using Our.Umbraco.Ditto.Resolvers.Archetype.Services;
using Our.Umbraco.Ditto.Resolvers.Archetype.Services.Abstract;
using Umbraco.Core.Models;

namespace Our.Umbraco.Ditto.Resolvers.Tests.Archetype.Fakes
{
    public class ArchetypeValueResolverShim : ArchetypeValueResolver
    {
        public ArchetypeValueResolverShim(DittoValueResolverContext context, CultureInfo culture, ArchetypeValueResolverAttribute attribute, IArchetypeBindingService bindingService = null) : base(context, bindingService ?? new ArchetypeBindingService())
        {
            base.Context = context;
            base.Culture = culture;
            base.Content = context.Instance as IPublishedContent;
            base.Attribute = attribute;
        }
    }
}
