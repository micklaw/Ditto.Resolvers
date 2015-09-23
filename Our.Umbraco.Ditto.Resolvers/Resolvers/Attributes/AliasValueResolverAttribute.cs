using System;

namespace Our.Umbraco.Ditto.Resolvers.Resolvers.Attributes
{
    public class AliasValueResolverAttribute : DittoValueResolverAttribute
    {
        public string Alias { get; }

        public AliasValueResolverAttribute(Type resolverType) : base(resolverType)
        {
        }

        public AliasValueResolverAttribute(Type resolverType, string alias) : base(resolverType)
        {
            Alias = alias;
        }
    }
}
