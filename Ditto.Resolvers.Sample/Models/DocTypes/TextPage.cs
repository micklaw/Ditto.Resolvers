using System.Web;
using Ditto.Resolvers.Sample.Models.DocTypes.Base;
using Ditto.Resolvers.Sample.Models.Grid;
using Our.Umbraco.Ditto.Resolvers.Models.Grid;
using Our.Umbraco.Ditto.Resolvers.Resolvers.Attributes;
using Umbraco.Core.Models;

namespace Ditto.Resolvers.Sample.Models.DocTypes
{
    public class TextPage : Page
    {
        public TextPage(IPublishedContent content) : base(content)
        {
        }

        public string Title { get; set; }

        public HtmlString Body { get; set; }

        [GridValueResolver]
        public GridModel Grid { get; set; }
    }
}