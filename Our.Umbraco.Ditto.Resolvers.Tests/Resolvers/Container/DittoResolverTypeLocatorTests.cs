using System;
using NUnit.Framework;
using Our.Umbraco.Ditto.Resolvers.Container;
using Our.Umbraco.Ditto.Resolvers.Container.Abstract;

namespace Our.Umbraco.Ditto.Resolvers.Tests.Resolvers.Container
{
    [TestFixture]
    public class DittoResolverTypeLocatorTests
    {
        public IResolverLocator _resolver { get; set; }

        [SetUp]
        public void SetUp()
        {
            _resolver = new DittoResolverTypeLocator();

            DittoResolverTypeLocator.Register<DittoHtmlStringConverter>("htmlString");
        }

        [TestCase(typeof(DittoContentPickerConverter), "contentPicker", Result = true, TestName = "Type registered")]
        [TestCase(typeof(DittoContentPickerConverter), null, ExpectedException = typeof(ArgumentException), TestName = "No alias throws exception")]
        [TestCase(null, "contentPicker", ExpectedException = typeof(ArgumentException), TestName = "No Type throws exception")]
        [TestCase(typeof(DittoContentPickerConverter), "htmlString", Result = false, TestName = "Existing Type not registered")]
        public bool Register(Type type, string alias)
        {
            return DittoResolverTypeLocator.Register(alias, type);
        }

        [TestCase("contentPicker", Result = null, TestName = "Unknown alias, Type not found")]
        [TestCase("", Result = null, TestName = "Empty alias, Type not found")]
        [TestCase(null, Result = null, TestName = "Null alias, Type not found")]
        [TestCase("htmlString", Result = typeof(DittoHtmlStringConverter), TestName = "Existing alias, Type found")]
        public Type Resolve(string alias)
        {
            return _resolver.Resolve(alias);
        }

        [TearDown]
        public void TearDown()
        {
            _resolver.ClearContainer();
        }
    }
}
