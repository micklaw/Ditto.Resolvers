using Newtonsoft.Json;
using Our.Umbraco.Ditto.Resolvers.Grid.Attributes;
using Our.Umbraco.Ditto.Resolvers.Grid.Converters;
using Our.Umbraco.Ditto.Resolvers.Grid.Models;
using Umbraco.Core.Models;

namespace Our.Umbraco.Ditto.Resolvers.Grid.Resolvers
{
    public class GridValueResolver<T> : DittoValueResolver<DittoValueResolverContext, GridValueResolverAttribute> where T : Control
    {
        public override object ResolveValue()
        {
            var content = Context.Instance as IPublishedContent;
            var descriptor = Context.PropertyDescriptor;

            GridModel grid = null;

            if (content != null && descriptor != null)
            {
                var alias = Attribute.Alias ?? descriptor.DisplayName;
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

                        settings.Converters.Add(new ControlConverter<T>(content, Culture));

                        grid = JsonConvert.DeserializeObject<GridModel>(gridHtml, settings);
                    }
                }
            }

            return grid;
        }
    }
}
