using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ditto.Resolvers.Sample.Models.Archetypes;
using Ditto.Resolvers.Sample.Models.Archetypes.Abstract;
using Ditto.Resolvers.Sample.Models.DocTypes.Base;
using Our.Umbraco.Ditto.Resolvers.Archetype.Attributes;
using Our.Umbraco.Ditto.Resolvers.Archetype.Resolvers;
using Umbraco.Core.Models;
using Umbraco.Core.Models.PublishedContent;

namespace Ditto.Resolvers.Sample.Models.DocTypes
{
    public class Home : Page
    {
        public Home(IPublishedContent content) : base(content)
        {
        }

        public string Title { get; set; }

        public HtmlString Body { get; set; }

        [ArchetypeValueResolver]
        public List<PriceList> PriceList { get; set; }

        [ArchetypeValueResolver]
        public List<IMulti> Multi { get; set; }
    }
}