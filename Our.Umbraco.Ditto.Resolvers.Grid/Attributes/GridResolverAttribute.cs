using System;
using Our.Umbraco.Ditto.Resolvers.Grid.Resolvers;

namespace Our.Umbraco.Ditto.Resolvers.Grid.Attributes
{
    public class GridResolverAttribute : DittoValueResolverAttribute
    {
        public GridResolverAttribute(Type resolverType) : base(resolverType)
        {

        }

        public GridResolverAttribute(string propertyAlias = null)
            : base(typeof(GridValueResolver))
        {
            PropertyAlias = propertyAlias;
        }

        public string PropertyAlias { get; private set; }
    }
}
