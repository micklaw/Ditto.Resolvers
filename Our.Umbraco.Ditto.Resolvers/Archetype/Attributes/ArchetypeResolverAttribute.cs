using System;
using Our.Umbraco.Ditto.Resolvers.Archetype.Resolvers;

namespace Our.Umbraco.Ditto.Resolvers.Archetype.Attributes
{
    public class ArchetypeResolverAttribute : DittoValueResolverAttribute
    {
        public ArchetypeResolverAttribute(Type resolverType) : base(resolverType)
        {

        }

        public ArchetypeResolverAttribute(string propertyAlias = null)
            : base(typeof(ArchetypeValueResolver))
        {
            PropertyAlias = propertyAlias;
        }

        public string PropertyAlias { get; private set; }
    }
}
