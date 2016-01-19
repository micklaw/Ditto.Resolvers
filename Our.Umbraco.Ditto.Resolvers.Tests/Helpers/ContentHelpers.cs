using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Archetype.Models;
using Newtonsoft.Json;
using Our.Umbraco.Ditto.Resolvers.Tests.Fakes;
using Umbraco.Core.Models;

namespace Our.Umbraco.Ditto.Resolvers.Tests.Helpers
{
    public class ContentHelpers
    {
        public const string ArchetypeJson = @"{" +
                                   "\"fieldsets\":[" +
                                      "{" +
                                         "\"properties\":[" +
                                            "{" +
                                               "\"alias\":\"summary\"," +
                                               "\"value\":\"<p>This is the <strong>summary</strong> text.</p>\"" +
                                            "}," +
                                            "{" +
                                               "\"alias\":\"header\"," +
                                               "\"value\":\"Ready to Enroll?\"" +
                                            "}," +
                                            "{" +
                                               "\"alias\":\"link\"," +
                                               "\"value\":\"{}\"" +
                                            "}" +
                                         "]," +
                                         "\"alias\":\"calloutAlias\"," +
                                         "\"disabled\":true," +
                                         "\"id\":\"7e02489f-c165-4d43-8b6b-9d8b80c3d488\"" +
                                      "}" +
                                   "]" +
                                "}";

        public const string MultiArchetypeJson = @"{" +
                                   "\"fieldsets\":[" +
                                      "{" +
                                         "\"properties\":[" +
                                            "{" +
                                               "\"alias\":\"summary\"," +
                                               "\"value\":\"<p>This is the <strong>summary</strong> text.</p>\"" +
                                            "}," +
                                            "{" +
                                               "\"alias\":\"header\"," +
                                               "\"value\":\"Ready to Enroll?\"" +
                                            "}," +
                                            "{" +
                                               "\"alias\":\"link\"," +
                                               "\"value\":\"{}\"" +
                                            "}" +
                                         "]," +
                                         "\"alias\":\"calloutAlias\"," +
                                         "\"disabled\":true," +
                                         "\"id\":\"7e02489f-c165-4d43-8b6b-9d8b80c3d488\"" +
                                      "}," +
                                      "{" +
                                         "\"properties\":[" +
                                            "{" +
                                               "\"alias\":\"text\"," +
                                               "\"value\":\"Loadsa text\"" +
                                            "}" +
                                         "]," +
                                         "\"alias\":\"justText\"," +
                                         "\"disabled\":false," +
                                         "\"id\":\"7e02499f-c165-4d43-8b6b-9d8b80c3d488\"" +
                                      "}," +
                                   "]" +
                                "}";

        public static ArchetypeModel Archetype => JsonConvert.DeserializeObject<ArchetypeModel>(ArchetypeJson);

        public static ArchetypeModel MultiArchetype => JsonConvert.DeserializeObject<ArchetypeModel>(MultiArchetypeJson);

        public static IPublishedContent FakeContent(int id, string name, IEnumerable<IPublishedContent> children = null, Collection<IPublishedProperty> properties = null, IPublishedContent parent = null)
        {
            return new FakePublishedContentModel(id, name, children ?? Enumerable.Empty<IPublishedContent>(), properties ?? new Collection<IPublishedProperty>(), parent);
        }
    }
}
