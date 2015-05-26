using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using Archetype.Models;
using Ditto.Resolvers.Sample.Models.DocTypes.Base;
using Our.Umbraco.Ditto;

namespace Ditto.Resolvers.Sample.Models.Archetypes
{
    public class PriceList : ArchetypeFieldsetModel
    {
        public string Title { get; set; }

        public int Quantity { get; set; }

        public string Price { get; set; }

        [TypeConverter(typeof(DittoContentPickerConverter))]
        public Content AssociatedPage { get; set; }
    }
}