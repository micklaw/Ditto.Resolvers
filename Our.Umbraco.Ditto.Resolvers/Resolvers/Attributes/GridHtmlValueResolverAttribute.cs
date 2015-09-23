using System;

namespace Our.Umbraco.Ditto.Resolvers.Resolvers.Attributes
{
    public class GridHtmlValueResolverAttribute : DittoValueResolverAttribute
    {
        public string Alias { get; }

        public string Framework { get; }

        public bool HasFramework => !string.IsNullOrWhiteSpace(Framework);

        public GridHtmlValueResolverAttribute(Type resolverType) : base(resolverType)
        {
        }

        public GridHtmlValueResolverAttribute(string alias = null, string framework = null) : this(typeof(GridHtmlValueResolver), alias, framework)
        {
        }

        public GridHtmlValueResolverAttribute(Type resolverType, string alias) : base(resolverType)
        {
            Alias = alias;
        }

        public GridHtmlValueResolverAttribute(Type resolverType, string alias, string framework) : base(resolverType)
        {
            Alias = alias;
            Framework = framework;
        }
    }
}
