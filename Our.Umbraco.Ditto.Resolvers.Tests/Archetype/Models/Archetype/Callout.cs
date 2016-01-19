using System.ComponentModel;
using System.Web;
using Our.Umbraco.Ditto.Resolvers.Archetype.Attributes;
using Our.Umbraco.Ditto.Resolvers.Archetype.Models.Abstract;
using umbraco.DataLayer.Utility.Table;

namespace Our.Umbraco.Ditto.Resolvers.Tests.Archetype.Models.Archetype
{
    public interface IWidget
    {
        
    }

    [ArchetypeContent("calloutAlias")]
    public class Callout : IWidget, IFieldset
    {
        [TypeConverter(typeof(DittoHtmlStringConverter))]
        public IHtmlString Summary { get; set; }

        public string Header { get; set; }

        public string Link { get; set; }

        public string Alias { get; set; }

        public bool Disabled { get; set; }
    }
}