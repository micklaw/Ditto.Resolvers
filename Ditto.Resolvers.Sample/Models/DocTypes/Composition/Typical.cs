using System.Collections.Generic;
using System.Linq;
using Ditto.Resolvers.Sample.Models.Archetypes.Abstract;
using Our.Umbraco.Ditto.Resolvers.Archetype.Attributes;

namespace Ditto.Resolvers.Sample.Models.DocTypes.Composition
{
    public class Typical
    {
        private List<IWidget> _priceList { get; set; }

        [ArchetypeValueResolver("priceList")]
        public List<IWidget> PriceList
        {
            get
            {
                return _priceList ?? new List<IWidget>();
            }
            set
            {
                if (value == null)
                {
                    value = new List<IWidget>();
                }

                _priceList = value.Take(1).ToList();
            }
        }
    }
}