using Umbraco.Core.Models;
using Umbraco.Core.Models.PublishedContent;

namespace Ditto.Resolvers.Sample.Models.DocTypes.Base
{
    public class Content : PublishedContentModel
    {
        public Content(IPublishedContent content) : base(content)
        {
        }
    }
}