using System;
using Our.Umbraco.Ditto.Resolvers.Archetype.Resolvers;
using Our.Umbraco.Ditto.Resolvers.Resolvers.Attributes;

namespace Our.Umbraco.Ditto.Resolvers.Archetype.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public class ArchetypeValueResolverAttribute : AliasValueResolverAttribute
    {
        public ArchetypeValueResolverAttribute() : this(typeof(ArchetypeValueResolver))
        {
        }

        public ArchetypeValueResolverAttribute(Type resolverType) : base(resolverType)
        {
        }

        public ArchetypeValueResolverAttribute(string alias) : this(typeof(ArchetypeValueResolver), alias)
        {
        }

        public ArchetypeValueResolverAttribute(Type resolverType, string alias) : base(resolverType, alias)
        {
        }
    }
}
