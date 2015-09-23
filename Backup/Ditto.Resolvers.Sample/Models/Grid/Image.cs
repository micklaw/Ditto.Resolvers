using Ditto.Resolvers.Sample.Models.Ditto.TypeConverters;
using Umbraco.Core.Models;

namespace Ditto.Resolvers.Sample.Models.Grid
{
    public class Image
    {
        public IPublishedContent Media { get; set; }

        public Focalpoint focalPoint { get; set; }

        public int id { get; set; }

        public string image { get; set; }
    }
}