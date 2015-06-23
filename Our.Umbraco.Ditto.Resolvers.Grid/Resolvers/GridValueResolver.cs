using System.ComponentModel;
using System.Globalization;
using Newtonsoft.Json;
using Our.Umbraco.Ditto.Resolvers.Grid.Attributes;
using Our.Umbraco.Ditto.Resolvers.Grid.Converters;
using Our.Umbraco.Ditto.Resolvers.Grid.Models;
using Our.Umbraco.Ditto.Resolvers.Shared.Services;
using Umbraco.Core.Models;
using Umbraco.Core.PropertyEditors.ValueConverters;

namespace Our.Umbraco.Ditto.Resolvers.Grid.Resolvers
{
    public class GridValueResolver<T> : DittoValueResolver<GridResolverAttribute> where T : Control
    {
        public override object ResolveValue(ITypeDescriptorContext context, GridResolverAttribute attribute, CultureInfo culture)
        {
            var content = context.Instance as IPublishedContent;
            var descriptor = context.PropertyDescriptor;

            GridModel grid = null;

            if (content != null && descriptor != null)
            {
                var alias = attribute.PropertyAlias ?? descriptor.DisplayName;
                var property = content.GetProperty(alias);

                if (property.HasValue)
                {
                    var gridHtml = property.Value.ToString();

                    if (!string.IsNullOrWhiteSpace(gridHtml))
                    {
                        var settings = new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                            PreserveReferencesHandling = PreserveReferencesHandling.None
                        };

                        settings.Converters.Add(new ControlConverter<T>(content, culture));

                        grid = JsonConvert.DeserializeObject<GridModel>(gridHtml, settings);
                    }
                }
            }

            return grid;
        }
    }
}
