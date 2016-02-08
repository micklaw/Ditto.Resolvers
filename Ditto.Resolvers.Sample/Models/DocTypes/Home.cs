using System.Collections.Generic;
using System.Web;
using Ditto.Resolvers.Sample.Models.Archetypes;
using Ditto.Resolvers.Sample.Models.Archetypes.Abstract;
using Ditto.Resolvers.Sample.Models.DocTypes.Base;
using Our.Umbraco.Ditto.Resolvers.Archetype.Attributes;
using Umbraco.Core.Models;

namespace Ditto.Resolvers.Sample.Models.DocTypes
{
    public class Home : Page
    {
        public Home(IPublishedContent content) : base(content)
        {
        }

        public string Title { get; set; }

        public HtmlString Body { get; set; }

        [ArchetypeProperty("priceList"), ArchetypeValueResolver]
        public List<PriceList> PriceList { get; set; }

        [ArchetypeValueResolver]
        public List<IMulti> Multi { get; set; }

        [ArchetypeValueResolver]
        public List<IMulti> UnknownBadBoy { get; set; }
    }
}