using Ditto.Resolvers.Sample.Models.Archetypes.Abstract;
using Our.Umbraco.Ditto.Resolvers.Archetype.Attributes;
using Our.Umbraco.Ditto.Resolvers.Archetype.Models.Abstract;

namespace Ditto.Resolvers.Sample.Models.Archetypes
{
    [ArchetypeContent]
    public class Number : IMulti, IFieldset
    {
        public int Main { get; set; }

        public string Alias { get; set; }

        public bool Disabled { get; set; }
    }
}