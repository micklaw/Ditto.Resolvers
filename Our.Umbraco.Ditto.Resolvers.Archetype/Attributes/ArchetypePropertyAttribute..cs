using System;

namespace Our.Umbraco.Ditto.Resolvers.Archetype.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ArchetypePropertyAttribute : Attribute
    {
        public ArchetypePropertyAttribute(string alias = null) 
        {
            Alias = alias;
        }

        public string Alias { get; private set; }
    }
}
