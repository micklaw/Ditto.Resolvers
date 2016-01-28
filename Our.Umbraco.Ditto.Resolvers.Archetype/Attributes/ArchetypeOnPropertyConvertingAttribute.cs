using System;

namespace Our.Umbraco.Ditto.Resolvers.Archetype.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ArchetypeOnPropertyConvertingAttribute : Attribute, IPropertyName
    {
        public string PropertyName { get; private set; }

        public ArchetypeOnPropertyConvertingAttribute(string propertyName) 
        {
            PropertyName = propertyName;
        }
    }
}
