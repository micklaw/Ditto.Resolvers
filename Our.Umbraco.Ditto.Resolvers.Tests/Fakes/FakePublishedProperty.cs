using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;

namespace Our.Umbraco.Ditto.Resolvers.Tests.Fakes
{
    public class FakePublishedProperty : IPublishedProperty
    {
        public FakePublishedProperty()
        {
            HasValue = true;
            Alias = "alias";
            Value = null;
        }

        public FakePublishedProperty(string alias, object value, bool hasValue)
        {
            HasValue = hasValue;
            Alias = alias;
            Value = value;
        }

        public string PropertyTypeAlias { get; set; }

        public bool HasValue { get; set; }

        public object DataValue { get; set; }

        public object Value { get; set; }

        public object XPathValue { get; set; }

        public string Alias { get; set; }

        public Guid Version { get; set; }
    }
}
