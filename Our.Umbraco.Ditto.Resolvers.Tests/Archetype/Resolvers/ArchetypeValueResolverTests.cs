using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Our.Umbraco.Ditto.Resolvers.Archetype.Resolvers;
using Our.Umbraco.Ditto.Resolvers.Tests.Fakes;
using Umbraco.Core.Models;
using Moq;
using Our.Umbraco.Ditto.Resolvers.Archetype.Attributes;

namespace Our.Umbraco.Ditto.Resolvers.Tests.Archetype.Resolvers
{
    [TestFixture]
    public class ArchetypeValueResolverTests
    {
        private Mock<DittoValueResolverContext> _mockContext;
        private ArchetypeValueResolver _archetypeResolver;

        public class FakeModel
        {
            public string TextString { get; set; }
        }

        [SetUp]
        public void SetUp()
        {
            _mockContext = new Mock<DittoValueResolverContext>();
        }

        [Test]
        public void Resolver_ResolvesTextString_WherePublishedContentHasField()
        {
            string value = "1234";

            _mockContext
                .Setup(i => i.Instance)
                .Returns(new FakePublishedContentModel(123, "Fake Node 1", null, new Collection<IPublishedProperty>
                {
                    new FakePublishedProperty("myArchetype", value, true)
                }));

            _archetypeResolver = new ArchetypeValueResolverShim(_mockContext.Object, new CultureInfo("en-GB"), new ArchetypeValueResolverAttribute());

            var resolveValue = _archetypeResolver.ResolveValue() as string;

            Assert.AreEqual(resolveValue, value);
        }
    }
}
