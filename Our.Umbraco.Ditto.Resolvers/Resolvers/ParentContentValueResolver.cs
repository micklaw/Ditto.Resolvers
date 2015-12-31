using System.ComponentModel;
using System.Globalization;
using Umbraco.Core.Models;

namespace Our.Umbraco.Ditto.Resolvers.Resolvers
{
    public class ParentContentValueResolver : DittoValueResolver
    {
        public ParentContentValueResolver()
        {
            
        }

        protected ParentContentValueResolver(DittoValueResolverAttribute attribute, IPublishedContent content, DittoValueResolverContext context)
        {
            Attribute = attribute;
            Content = content;
            Context = context;
            Culture = new CultureInfo("en-GB");
        }

        protected virtual bool IsValid(IPublishedContent content, PropertyDescriptor descriptor)
        {
            if (content != null && descriptor != null && content.Parent != null)
            {
                // [ML] - As we actually have stuff to do now, generate a list please if required

                return true;
            }

            return false;
        }

        public override object ResolveValue()
        {
            var content = Context.Instance as IPublishedContent;
            var descriptor = Context.PropertyDescriptor;

            if (IsValid(content, descriptor))
            {
                return content.Parent.As(descriptor.PropertyType);
            }

            return null;
        }
    }
}