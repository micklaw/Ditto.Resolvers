using Our.Umbraco.Ditto.Resolvers.Archetype.Attributes;
using Our.Umbraco.Ditto.Resolvers.Tests.Archetype.Models.Archetype;
using Our.Umbraco.Ditto.Resolvers.Tests.Archetype.Services;
using Umbraco.Core.Models;
using Umbraco.Core.Models.PublishedContent;

namespace Our.Umbraco.Ditto.Resolvers.Tests.Archetype.Models.Ditto
{
    public class FakeModel : PublishedContentModel
    {
        public FakeModel(IPublishedContent content) : base(content)
        {
        }

        [ArchetypeProperty("myArchetypeProperty")]
        [ArchetypeValueResolver]
        public Callout TextString { get; set; }
    }
}