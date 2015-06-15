using Our.Umbraco.Ditto.Resolvers.Grid.Attributes;
using System.ComponentModel;
using System.Globalization;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace Our.Umbraco.Ditto.Resolvers.Grid.Resolvers {
    public class GridValueResolver : DittoValueResolver<GridResolverAttribute> {
        public override object ResolveValue(ITypeDescriptorContext context, GridResolverAttribute attribute, CultureInfo culture) {
            var content = context.Instance as IPublishedContent;
            var descriptor = context.PropertyDescriptor;

            if (content != null && descriptor != null) {
                var alias = attribute.PropertyAlias ?? descriptor.DisplayName;

                if (content.HasValue(alias)) {
                    var output = attribute.HasFramework
                        ? content.GetGridHtml(alias, attribute.Framework)
                        : content.GetGridHtml(alias);

                    return output;
                }
            }

            return null;
        }
    }
}
