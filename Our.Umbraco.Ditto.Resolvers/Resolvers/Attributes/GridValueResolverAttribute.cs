using System;
using Our.Umbraco.Ditto.Resolvers.Models.Grid;

namespace Our.Umbraco.Ditto.Resolvers.Resolvers.Attributes
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
