using Our.Umbraco.Ditto.Resolvers.Resolvers;
using Our.Umbraco.Ditto.Resolvers.Resolvers.Attributes;
using Umbraco.Core.Models;

namespace Our.Umbraco.Ditto.Resolvers.Tests.Fakes
{
    public class GridHtmlValueResolverShim : GridHtmlValueResolver
    {
        public GridHtmlValueResolverShim(GridHtmlValueResolverAttribute attribute, IPublishedContent content, DittoValueResolverContext context) : base(attribute, content, context)
        {
            
        }
    }
}
