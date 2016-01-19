using Ditto.Resolvers.Sample.Models.DocTypes.Composition;
using Our.Umbraco.Ditto;

namespace Ditto.Resolvers.Sample.Models.DocTypes
{
    public class TypicalPage
    {
        [CurrentContentAs]
        public Typical Typical { get; set; }
    }
}