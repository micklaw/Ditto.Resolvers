using Archetype.Models;
using Ditto.Resolvers.Sample.Models.Archetypes.Abstract;
using Our.Umbraco.Ditto;
using Our.Umbraco.Ditto.Resolvers.Archetype.Attributes;
using Our.Umbraco.Ditto.Resolvers.Archetype.Models.Abstract;

namespace Ditto.Resolvers.Sample.Models.Archetypes
{
    [ArchetypeContent]
    public class Number : IMulti, IFieldset
    {
        public int Main { get; set; }

        [ArchetypeOnPropertyConverting(nameof(Main))]
        internal void MainOnConverting(DittoConversionHandlerContext context, ArchetypeFieldsetModel fieldset)
        {
            var x = context;
        }

        [ArchetypeOnPropertyConverted(nameof(Main))]
        internal void MainOnConverted(DittoConversionHandlerContext context, ArchetypeFieldsetModel fieldset)
        {
            var x = context;
        }

        public string Alias { get; set; }

        public bool Disabled { get; set; }

        [DittoOnConverting]
        internal void OnConverting(DittoConversionHandlerContext context, ArchetypeFieldsetModel fieldset)
        {
            var x = context;
        }

        [DittoOnConverted]
        internal void OnConverted(DittoConversionHandlerContext context, ArchetypeFieldsetModel fieldset)
        {
            var x = context;
        }
    }
}