using System;

namespace Our.Umbraco.Ditto.Resolvers.Archetype.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ArchetypeOnPropertyConvertedAttribute : Attribute, IPropertyName
    {
        public string PropertyName { get; }

        public ArchetypeOnPropertyConvertedAttribute(string propertyName)
        {
            PropertyName = propertyName;
        }
    }
}
