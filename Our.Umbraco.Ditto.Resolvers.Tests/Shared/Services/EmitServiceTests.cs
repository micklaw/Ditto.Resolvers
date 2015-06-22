using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using NUnit.Framework;
using Our.Umbraco.Ditto.Resolvers.Grid.Attributes;
using Our.Umbraco.Ditto.Resolvers.Grid.Models;
using Our.Umbraco.Ditto.Resolvers.Shared.Services;
using Our.Umbraco.Ditto.Resolvers.Shared.Services.Abstract;
using Rhino.Mocks;

namespace Our.Umbraco.Ditto.Resolvers.Tests.Shared.Services
{
    public class Assignable
    {
        public Control Value { get; set; }
    }

    [TestFixture]
    public class EmitServiceTests
    {
        private DittoEmitterService _emitter { get; set; }

        [SetUp]
        public void SetUp()
        {
            _emitter = new DittoEmitterService();
        }        

        [TestCase("ConvertedValue", typeof(GridResolverAttribute), new [] { typeof(string) }, new object [] { "newAlias"}, Result = 1, TestName = "Proxy Made, Property Overriden, Attribute Added,With Param")]
        public object OverrideProperty(string propertyName, Type attribute, Type[] constructorParams, object[] constructorValues)
        {
            var instance = _emitter.OverrideProperty<Control>(propertyName, attribute, constructorParams, constructorValues);

           instance.ConvertedValue = 1;

            var propertyAttribute = instance.GetType().GetProperty(propertyName).GetCustomAttributes(attribute).FirstOrDefault();

            Assert.IsNotNull(propertyAttribute);

            var propertyValue = attribute.GetProperty("PropertyAlias").GetValue(propertyAttribute);

            Assert.AreEqual(propertyValue, constructorValues[0]);

            return instance.ConvertedValue;
        }

        [TestCase("ConvertedValue", typeof(TypeConverterAttribute), new[] { typeof(Type) }, new object[] { typeof(DittoHtmlStringConverter) }, Result = true, TestName = "Proxy Made, Property Overriden, Attribute Added with Costructor Param")]
        public bool OverridePropertyAssignAttributeConstructor(string propertyName, Type attribute, Type[] constructorParams, object[] constructorValues)
        {
            var instance = _emitter.OverrideProperty<Control>("ConvertedValue", attribute, constructorParams, constructorValues);

            var assignable = new Assignable()
            {
                Value = instance
            };

            var property = assignable.Value.GetType().GetProperty("ConvertedValue");
            var propertyAttribute = property.GetCustomAttribute(attribute);

            return propertyAttribute != null;
        }


        [TearDown]
        public void TearDown()
        {
            _emitter = null;
        }
    }
}
