using System.ComponentModel;
using Ditto.Resolvers.Sample.Models.DocTypes.Base;
using Our.Umbraco.Ditto;

namespace Ditto.Resolvers.Sample.Models.Archetypes
{
    public class SameProperty
    {
        public string Title { get; set; }

        [TypeConverter(typeof(DittoPickerConverter))]
        public Content Link { get; set; }
    }
}