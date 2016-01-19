using Our.Umbraco.Ditto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ditto.Resolvers.Sample.Models.DocTypes.Composition
{
    public class TestClass
    {
        [CurrentContentAs]
        public Home Homepage { get; set; }
    }
}