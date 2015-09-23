using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Our.Umbraco.Ditto.Resolvers.Archetype.Resolvers;
using Our.Umbraco.Ditto.Resolvers.Resolvers.Attributes;

namespace Our.Umbraco.Ditto.Resolvers.Archetype.Attributes
{
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
