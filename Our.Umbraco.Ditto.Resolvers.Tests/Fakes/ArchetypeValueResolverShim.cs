using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Our.Umbraco.Ditto.Resolvers.Archetype.Attributes;
using Our.Umbraco.Ditto.Resolvers.Archetype.Resolvers;
using Umbraco.Core.Models;

namespace Our.Umbraco.Ditto.Resolvers.Tests.Fakes
{
    public class ArchetypeValueResolverShim : ArchetypeValueResolver
    {
        public ArchetypeValueResolverShim(DittoValueResolverContext context, CultureInfo culture, ArchetypeValueResolverAttribute attribute) : base(context)
        {
            base.Context = context;
            base.Culture = culture;
            base.Content = context.Instance as IPublishedContent;
            base.Attribute = attribute;
        }
    }
}
