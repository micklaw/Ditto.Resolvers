using System.Collections.Generic;
using System.Web;
using Our.Umbraco.Ditto.Resolvers.Archetype.Attributes;
using Our.Umbraco.Ditto.Resolvers.Resolvers.Attributes;
using Our.Umbraco.Ditto.Resolvers.Tests.Archetype.Models.Archetype;
using Umbraco.Core.Models;
using Umbraco.Core.Models.PublishedContent;

namespace Our.Umbraco.Ditto.Resolvers.Tests.Fakes.Ditto
{
    public class FakeModel : PublishedContentModel
    {
        public FakeModel(IPublishedContent content) : base(content)
        {
        }

        [ArchetypeProperty("myArchetypeProperty")]
        [ArchetypeValueResolver]
        public Callout Callout { get; set; }

        [ArchetypeValueResolver]
        public IList<IWidget> WidgetList { get; set; }

        [GridHtmlValueResolver]
        public IHtmlString GridHtml { get; set; }
    }
}