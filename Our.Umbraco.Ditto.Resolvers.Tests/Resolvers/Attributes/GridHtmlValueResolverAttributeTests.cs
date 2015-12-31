using System;
using NUnit.Framework;
using Our.Umbraco.Ditto.Resolvers.Resolvers;
using Our.Umbraco.Ditto.Resolvers.Resolvers.Attributes;

namespace Our.Umbraco.Ditto.Resolvers.Tests.Resolvers.Attributes
{
    [TestFixture]
    public class GridHtmlValueResolverAttributeTests
    {
        private GridHtmlValueResolverAttribute _sut;

        private readonly Type _resolverType = typeof(GridHtmlValueResolver);
        private readonly Type _attributeType = typeof(GridHtmlValueResolverAttribute);

        [Test]
        public void GridHtmlValueResolverAttribute_ConstructorWithType_PropertiesInitialised()
        {
            _sut = new GridHtmlValueResolverAttribute(_resolverType);

            Assert.IsNull(_sut.Alias);
            Assert.IsNull(_sut.Framework);
            Assert.AreEqual(_sut.HasFramework, false);
            Assert.AreEqual(_sut.ResolverType, _resolverType);
            Assert.AreEqual(_sut.TypeId, _attributeType);
        }

        [Test]
        public void GridHtmlValueResolverAttribute_ConstructorWithTypeAndAlias_PropertiesInitialised()
        {
            var alias = "MyAlias";

            _sut = new GridHtmlValueResolverAttribute(_resolverType, alias);

            Assert.AreEqual(_sut.Alias, alias);
            Assert.IsNull(_sut.Framework);
            Assert.AreEqual(_sut.HasFramework, false);
            Assert.AreEqual(_sut.ResolverType, _resolverType);
            Assert.AreEqual(_sut.TypeId, _attributeType);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void GridHtmlValueResolverAttribute_ConstructorWithNullTypeHasAlias_ThrowsException()
        {
            Type resolverType = null;
            var alias = "MyAlias";

            _sut = new GridHtmlValueResolverAttribute(resolverType, alias);
        }

        [Test]
        public void GridHtmlValueResolverAttribute_ConstructorNullAliasNullFramework_PropertiesinitialisedNullAliasNullFramework()
        {
            string alias = null;
            string framework = null;

            _sut = new GridHtmlValueResolverAttribute(alias, framework);

            Assert.IsNull(_sut.Alias);
            Assert.IsNull(_sut.Framework);
            Assert.AreEqual(_sut.HasFramework, false);
            Assert.AreEqual(_sut.ResolverType, _resolverType);
            Assert.AreEqual(_sut.TypeId, _attributeType);
        }

        [Test]
        public void GridHtmlValueResolverAttribute_ConstructorNullAliasNullFramework_PropertiesinitialisedEmptyAliasEmptyFramework()
        {
            string alias = "";
            string framework = "";

            _sut = new GridHtmlValueResolverAttribute(alias, framework);

            Assert.AreEqual(_sut.Alias, "");
            Assert.AreEqual(_sut.Framework, "");
            Assert.AreEqual(_sut.HasFramework, false);
            Assert.AreEqual(_sut.ResolverType, _resolverType);
            Assert.AreEqual(_sut.TypeId, _attributeType);
        }

        [Test]
        public void GridHtmlValueResolverAttribute_ConstructorWithAliasWithFramework_Propertiesinitialised()
        {
            string alias = "myAlias";
            string framework = "bootstrap3";

            _sut = new GridHtmlValueResolverAttribute(alias, framework);

            Assert.AreEqual(_sut.Alias, alias);
            Assert.AreEqual(_sut.Framework, framework);
            Assert.AreEqual(_sut.HasFramework, true);
            Assert.AreEqual(_sut.ResolverType, _resolverType);
            Assert.AreEqual(_sut.TypeId, _attributeType);
        }

        [Test]
        public void GridHtmlValueResolverAttribute_ConstructorWithTypeNullAlias_PropertiesinitialisedNullAlias()
        {
            _sut = new GridHtmlValueResolverAttribute(_resolverType, null);

            Assert.IsNull(_sut.Alias);
            Assert.IsNull(_sut.Framework);
            Assert.AreEqual(_sut.HasFramework, false);
            Assert.AreEqual(_sut.ResolverType, _resolverType);
            Assert.AreEqual(_sut.TypeId, _attributeType);
        }

        [Test]
        public void GridHtmlValueResolverAttribute_ConstructorWithTypeEmptyAlias_PropertiesinitialisedEmptyAlias()
        {
            _sut = new GridHtmlValueResolverAttribute(_resolverType, "");

            Assert.AreEqual(_sut.Alias, "");
            Assert.IsNull(_sut.Framework);
            Assert.AreEqual(_sut.HasFramework, false);
            Assert.AreEqual(_sut.ResolverType, _resolverType);
            Assert.AreEqual(_sut.TypeId, _attributeType);
        }
    }
}
