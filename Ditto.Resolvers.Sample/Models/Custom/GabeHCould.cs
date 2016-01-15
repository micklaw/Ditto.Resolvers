using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ditto.Resolvers.Sample.Models.Custom
{
    public class GabeHCoud
    {
        public GabeHCoud(string name, decimal douchBaggeryLevel)
        {
            Name = name;
            DouchBaggeryLevel = douchBaggeryLevel;
        }

        public string Name { get; }

        public decimal DouchBaggeryLevel { get; }
    }
}