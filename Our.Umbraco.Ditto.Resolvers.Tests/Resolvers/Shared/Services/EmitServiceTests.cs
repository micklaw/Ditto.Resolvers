using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Archetype.Models;
using NUnit.Framework;
using Our.Umbraco.Ditto.Resolvers.Models.Grid;
using Our.Umbraco.Ditto.Resolvers.Resolvers.Attributes;
using Our.Umbraco.Ditto.Resolvers.Shared.Services;

namespace Our.Umbraco.Ditto.Resolvers.Tests.Resolvers.Shared.Services
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

        [TestCase("ConvertedValue", typeof(GridValueResolverAttribute), new [] { typeof(string) }, new object [] { "newAlias"}, Result = 1, TestName = "Proxy Made, Property Overriden, Attribute Added,With Param")]
        public object OverrideProperty(string propertyName, Type attribute, Type[] constructorParams, object[] constructorValues)
        {
            var instance = _emitter.OverrideProperty<Control>(propertyName, attribute, constructorParams, constructorValues);

           instance.ConvertedValue = 1;

            var propertyAttribute = instance.GetType().GetProperty(propertyName).GetCustomAttributes(attribute).FirstOrDefault();

            Assert.IsNotNull(propertyAttribute);

            var propertyValue = propertyAttribute.GetType().GetProperty("Alias").GetValue(propertyAttribute);

            Assert.AreEqual(propertyValue, constructorValues[0]);

            return instance.ConvertedValue;
        }

        public interface IWidget
        {
            
        }

        public class MyModel : ArchetypeFieldsetModel, IWidget
        {
             
        }

        public class TypeModelA : MyModel
        {

        }

        public class TypeModelB : MyModel
        {

        }

        [Test]
        public void SomeTest()
        {
            var type = typeof (IWidget);

            var constructedListType = typeof(List<>).MakeGenericType(type);
            var list = (IList)Activator.CreateInstance(constructedListType);

            var item = Activator.CreateInstance(typeof(TypeModelA));
            var item1 = Activator.CreateInstance(typeof(TypeModelB));

            list.Add(item);
            list.Add(item1);

            Assert.IsNotEmpty(list);
            Assert.IsTrue(list.Count == 2);
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
