using System.ComponentModel;
using System.Web;

namespace Our.Umbraco.Ditto.Resolvers.Tests.Archetype.Models.Archetype
{
    public class Callout
    {
        [TypeConverter(typeof(DittoHtmlStringConverter))]
        public IHtmlString Summary { get; set; }

        public string Header { get; set; }

        public string Link { get; set; }
    }
}