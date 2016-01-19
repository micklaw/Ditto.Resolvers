using System.Globalization;
using Our.Umbraco.Ditto.Resolvers.Resolvers.Attributes;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace Our.Umbraco.Ditto.Resolvers.Resolvers
{
    public class GridHtmlValueResolver : DittoValueResolver<DittoValueResolverContext, GridHtmlValueResolverAttribute>
    {
        protected GridHtmlValueResolver(GridHtmlValueResolverAttribute attribute, IPublishedContent content, DittoValueResolverContext context)
        {
            Attribute = attribute;
            Content = content;
            Context = context;
            Culture = new CultureInfo("en-GB");
        }

        public override object ResolveValue()
        {
            var content = Context.Instance as IPublishedContent;
            var descriptor = Context.PropertyDescriptor;

            if (content != null && descriptor != null)
            {
                var alias = Attribute.Alias ?? descriptor.DisplayName;

                if (content.HasValue(alias))
                {
                    var output = Attribute.HasFramework ? content.GetGridHtml(alias, Attribute.Framework) : content.GetGridHtml(alias);

                    return output;
                }
            }

            return null;
        }
    }
}