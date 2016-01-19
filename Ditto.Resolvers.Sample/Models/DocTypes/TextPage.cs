using System.Web;
using Ditto.Resolvers.Sample.Models.DocTypes.Base;
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
    }
}