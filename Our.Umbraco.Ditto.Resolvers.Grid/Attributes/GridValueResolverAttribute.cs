using System;
using Our.Umbraco.Ditto.Resolvers.Grid.Models;
using Our.Umbraco.Ditto.Resolvers.Grid.Resolvers;
using Our.Umbraco.Ditto.Resolvers.Resolvers.Attributes;

namespace Our.Umbraco.Ditto.Resolvers.Grid.Attributes
{
    public class GridValueResolverAttribute : AliasValueResolverAttribute
    {
        public GridValueResolverAttribute(Type resolverType) : base(resolverType)
        {
        }

        public GridValueResolverAttribute(string alias = null) : this(typeof(GridValueResolver<Control>), alias)
        {
        }

        public GridValueResolverAttribute(Type resolverType, string alias) : base(resolverType, alias)
        {
        }
    }
}
