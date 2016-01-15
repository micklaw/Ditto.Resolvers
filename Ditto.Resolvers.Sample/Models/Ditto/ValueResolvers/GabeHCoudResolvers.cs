using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ditto.Resolvers.Sample.Models.Custom;
using Our.Umbraco.Ditto;

namespace Ditto.Resolvers.Sample.Models.Ditto.ValueResolvers
{
    public class GabeHCoudResolvers : DittoValueResolver<DittoValueResolverContext>
    {
        public override object ResolveValue()
        {
            return new[]
            {
                new GabeHCoud("Katy Hopkins", 10),
                new GabeHCoud("Donald Trump", 100),
            };
        }
    }
}