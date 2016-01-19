using Umbraco.Core.Models;

namespace Ditto.Resolvers.Sample.Models.DocTypes.Base
{
    public class Page : Content
    {
        public Page(IPublishedContent content) : base(content)
        {
        }
    }
}