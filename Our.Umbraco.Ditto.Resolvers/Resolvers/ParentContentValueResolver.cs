using Umbraco.Core.Models;

namespace Our.Umbraco.Ditto.Resolvers.Resolvers
{
    public class ParentContentValueResolver : DittoValueResolver
    {
        public override object ResolveValue()
        {
            var content = Context.Instance as IPublishedContent;
            var descriptor = Context.PropertyDescriptor;

            if (content != null && descriptor != null && content.Parent != null)
            {
                // [ML] - As we actually have stuff to do now, generate a list please if required

                return content.Parent.As(descriptor.PropertyType);
            }

            return null;
        }
    }
}