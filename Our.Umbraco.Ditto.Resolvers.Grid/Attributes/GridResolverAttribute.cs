using Our.Umbraco.Ditto.Resolvers.Grid.Resolvers;
using System;

namespace Our.Umbraco.Ditto.Resolvers.Grid.Attributes {
    public class GridResolverAttribute : DittoValueResolverAttribute {
        public GridResolverAttribute(Type resolverType)
            : base(resolverType) {
        }

        public GridResolverAttribute(string propertyAlias = null, string framework = null)
            : base(typeof(GridValueResolver)) {
            PropertyAlias = propertyAlias;
            Framework = framework;
        }

        public string PropertyAlias { get; private set; }
        public string Framework { get; private set; }

        public bool HasFramework {
            get { return string.IsNullOrEmpty(Framework) == false; }
        }
    }
}
