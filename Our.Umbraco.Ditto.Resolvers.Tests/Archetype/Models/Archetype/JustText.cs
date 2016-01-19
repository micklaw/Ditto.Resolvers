
using Our.Umbraco.Ditto.Resolvers.Archetype.Attributes;

namespace Our.Umbraco.Ditto.Resolvers.Tests.Archetype.Models.Archetype
{
    [ArchetypeContent]
    public class JustText : IWidget
    {
        public string Text { get; set; }
    }
}