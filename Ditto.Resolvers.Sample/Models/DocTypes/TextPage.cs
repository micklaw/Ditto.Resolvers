using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using Ditto.Resolvers.Sample.Models.Archetypes;
using Ditto.Resolvers.Sample.Models.DocTypes.Base;
using Ditto.Resolvers.Sample.Models.Grid;
using Our.Umbraco.Ditto.Resolvers.Archetype.Attributes;
using Our.Umbraco.Ditto.Resolvers.Grid.Attributes;
using Our.Umbraco.Ditto.Resolvers.Grid.Models;
using Umbraco.Core.Models;
using Umbraco.Core.Models.PublishedContent;

namespace Ditto.Resolvers.Sample.Models.DocTypes
{
    public class TextPage : Page
    {
        public TextPage(IPublishedContent content) : base(content)
        {
        }

        public string Title { get; set; }

        public HtmlString Body { get; set; }

        [GridResolver]
        public GridModel Grid { get; set; }
    }
}