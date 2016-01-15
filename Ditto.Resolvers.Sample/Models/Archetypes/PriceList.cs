﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using Archetype.Models;
using Ditto.Resolvers.Sample.Models.Custom;
using Ditto.Resolvers.Sample.Models.Ditto.ValueResolvers;
using Ditto.Resolvers.Sample.Models.DocTypes.Base;
using Our.Umbraco.Ditto;
using Our.Umbraco.Ditto.Resolvers.Archetype.Attributes;
using Our.Umbraco.Ditto.Resolvers.Archetype.Models.Abstract;

namespace Ditto.Resolvers.Sample.Models.Archetypes
{
    public class PriceList : IFieldset
    {
        public string Title { get; set; }

        public int Quantity { get; set; }

        public string Price { get; set; }

        [TypeConverter(typeof(DittoContentPickerConverter))]
        public Content AssociatedPage { get; set; }

        public string Alias { get; set; }

        public bool Disabled { get; set; }

        [ArchetypeProperty("randomAlias")]
        public string AnotherText { get; set; }

        public string TestStringField { get; set; }

        [DittoValueResolver(typeof(GabeHCoudResolvers))]
        public IEnumerable<GabeHCoud> Persons { get; set; } 
    }
}