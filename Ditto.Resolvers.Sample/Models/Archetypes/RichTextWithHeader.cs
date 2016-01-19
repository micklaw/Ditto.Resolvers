using System.Collections.Generic;
using System.ComponentModel;
using System.Web;
using Ditto.Resolvers.Sample.Models.Archetypes.Abstract;
using Our.Umbraco.Ditto;
using Our.Umbraco.Ditto.Resolvers.Archetype.Attributes;
using Our.Umbraco.Ditto.Resolvers.Archetype.Models.Abstract;

namespace Ditto.Resolvers.Sample.Models.Archetypes
{
    [ArchetypeContent]
    public class RichTextWithHeader : IMulti, IFieldset
    {
        public string Title { get; set; }

        [TypeConverter(typeof(DittoHtmlStringConverter))]
        public HtmlString Body { get; set; }

        [ArchetypeProperty("priceList"), ArchetypeValueResolver]
        public List<PriceList> PriceList { get; set; }

        public string Alias { get; set; }

        public bool Disabled { get; set; }
    }
}