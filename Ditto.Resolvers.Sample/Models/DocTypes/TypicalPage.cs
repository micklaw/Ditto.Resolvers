using System.Collections.Generic;
using Ditto.Resolvers.Sample.Models.Archetypes.Abstract;
using Ditto.Resolvers.Sample.Models.DocTypes.Composition;
using Our.Umbraco.Ditto;
using Our.Umbraco.Ditto.Resolvers.Archetype.Attributes;

namespace Ditto.Resolvers.Sample.Models.DocTypes
{
    public class TypicalPage
    {
        [CurrentContentAs]
        public Typical Typical { get; set; }

        [ArchetypeValueResolver]
        public List<IMulti> UnknownBadBoy { get; set; }
    }
}