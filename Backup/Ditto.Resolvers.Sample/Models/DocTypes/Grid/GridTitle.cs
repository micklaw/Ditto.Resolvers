using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Models;

namespace Ditto.Resolvers.Sample.Models.DocTypes.Grid
{
    public class GridTitle : Base.Grid
    {
        public GridTitle(IPublishedContent content) : base(content)
        {
        }

        public string Title { get; set; }
    }
}