using System;
using Our.Umbraco.Ditto.Resolvers.Archetype.Resolvers;

namespace Our.Umbraco.Ditto.Resolvers.Archetype.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.Class)]
    public class ArchetypeContentAttribute : Attribute
    {
        public ArchetypeContentAttribute(string alias = null) 
        {
            Alias = alias;
        }

        public string Alias { get; private set; }
    }
}
