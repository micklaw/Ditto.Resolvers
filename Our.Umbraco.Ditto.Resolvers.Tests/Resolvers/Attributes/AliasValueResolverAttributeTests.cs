using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Our.Umbraco.Ditto.Resolvers.Archetype.Attributes;
using Our.Umbraco.Ditto.Resolvers.Archetype.Resolvers;
using Our.Umbraco.Ditto.Resolvers.Resolvers.Attributes;

namespace Our.Umbraco.Ditto.Resolvers.Tests.Resolvers.Attributes
{
    [TestFixture]
    public class AliasValueResolverAttributeTests
    {
        private AliasValueResolverAttribute _sut;

        [Test]
        public void AliasValueResolverAttribute_ConstructorWithType_PropertiesInitialised()
        {
            var resolverType = typeof (ArchetypeValueResolver);

            _sut = new AliasValueResolverAttribute(resolverType);

            Assert.IsNullOrEmpty(_sut.Alias);
            Assert.AreEqual(_sut.ResolverType, resolverType);
            Assert.AreEqual(_sut.TypeId, typeof(AliasValueResolverAttribute));
        }

        [Test]
        public void AliasValueResolverAttribute_ConstructorWithTypeAndAlias_PropertiesInitialised()
        {
            var resolverType = typeof(ArchetypeValueResolver);
            var alias = "MyAlias";

            _sut = new AliasValueResolverAttribute(resolverType, alias);

            Assert.AreEqual(_sut.Alias, alias);
            Assert.AreEqual(_sut.ResolverType, resolverType);
            Assert.AreEqual(_sut.TypeId, typeof(AliasValueResolverAttribute));
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void AliasValueResolverAttribute_ConstructorWithNullTypeHasAlias_ThrowsException()
        {
            var alias = "MyAlias";

            _sut = new AliasValueResolverAttribute(null, alias);
        }

        [Test]
        public void AliasValueResolverAttribute_ConstructorWithTypeNullAlias_PropertiesinitialisedNullAlias()
        {
            var resolverType = typeof(ArchetypeValueResolver);

            _sut = new AliasValueResolverAttribute(resolverType, null);

            Assert.IsNull(_sut.Alias);
            Assert.AreEqual(_sut.ResolverType, resolverType);
            Assert.AreEqual(_sut.TypeId, typeof(AliasValueResolverAttribute));
        }

        [Test]
        public void AliasValueResolverAttribute_ConstructorWithTypeEmptyAlias_PropertiesinitialisedEmptyAlias()
        {
            var resolverType = typeof(ArchetypeValueResolver);

            _sut = new AliasValueResolverAttribute(resolverType, "");

            Assert.AreEqual(_sut.Alias, "");
            Assert.AreEqual(_sut.ResolverType, resolverType);
            Assert.AreEqual(_sut.TypeId, typeof(AliasValueResolverAttribute));
        }
    }
}
