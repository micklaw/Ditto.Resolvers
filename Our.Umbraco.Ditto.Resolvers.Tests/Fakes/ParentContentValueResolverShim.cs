using Our.Umbraco.Ditto.Resolvers.Resolvers;
using Umbraco.Core.Models;

namespace Our.Umbraco.Ditto.Resolvers.Tests.Fakes
{
    public class ParentContentValueResolverShim : ParentContentValueResolver
    {
        public ParentContentValueResolverShim(DittoValueResolverAttribute attribute, IPublishedContent content, DittoValueResolverContext context) : base(attribute, content, context)
        {
            
        }

        public bool IsValid()
        {
            var content = Context.Instance as IPublishedContent;
            var descriptor = Context.PropertyDescriptor;

            return base.IsValid(content, descriptor);
        }
    }
}
