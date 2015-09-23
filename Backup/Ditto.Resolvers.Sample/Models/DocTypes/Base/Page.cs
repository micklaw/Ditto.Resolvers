using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Models;
using Umbraco.Core.Models.PublishedContent;

namespace Ditto.Resolvers.Sample.Models.DocTypes.Base
{
    public class Page : Content
    {
        public Page(IPublishedContent content) : base(content)
        {
        }
    }
}