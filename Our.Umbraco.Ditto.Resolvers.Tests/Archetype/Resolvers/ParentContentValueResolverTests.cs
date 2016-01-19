using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using Archetype.Models;
using Moq;
using NUnit.Framework;
using Our.Umbraco.Ditto.Resolvers.Archetype.Attributes;
using Our.Umbraco.Ditto.Resolvers.Archetype.Resolvers;
using Our.Umbraco.Ditto.Resolvers.Archetype.Services.Abstract;
using Our.Umbraco.Ditto.Resolvers.Resolvers;
using Our.Umbraco.Ditto.Resolvers.Tests.Archetype.Fakes;
using Our.Umbraco.Ditto.Resolvers.Tests.Archetype.Models.Archetype;
using Our.Umbraco.Ditto.Resolvers.Tests.Archetype.Models.Ditto;
using Our.Umbraco.Ditto.Resolvers.Tests.Fakes;
using Our.Umbraco.Ditto.Resolvers.Tests.Helpers;
using Umbraco.Core.Models;

namespace Our.Umbraco.Ditto.Resolvers.Tests.Archetype.Resolvers
{
    [TestFixture]
    public class ArchetypeValueResolverTests
    {
        private ArchetypeValueResolver _sut;
        private Mock<IArchetypeBindingService> _bindingServiceMock;
        private ArchetypeValueResolverAttribute _attribute;
        private CultureInfo _culture;
        private FakeModel _content;
        private FakeDittoValueResolverContext _context;
        private PropertyDescriptor _propertyDescriptor;

        [SetUp]
        public void SetUp()
        {
            var content = ContentHelpers.FakeContent(123, "Fake Node 1", properties: new Collection<IPublishedProperty>
            {
                new FakePublishedProperty("myArchetypeProperty", ContentHelpers.Archetype, true)
            }, 
            parent: ContentHelpers.FakeContent(1234, "Fake Parent Node"));

            _bindingServiceMock = new Mock<IArchetypeBindingService>();
            _bindingServiceMock.Setup(
                i =>
                    i.As(It.IsAny<ArchetypeModel>(), It.IsAny<Type>(), It.IsAny<CultureInfo>(),
                        It.IsAny<IPublishedContent>(), It.IsAny<DittoValueResolverContext>()))
                               .Returns(new Callout());

            _culture = new CultureInfo("en-GB");
            _content = new FakeModel(content);

            _propertyDescriptor = TypeDescriptor.GetProperties(_content)["TextString"];
            _context = new FakeDittoValueResolverContext(_content, _propertyDescriptor);

            _attribute = typeof(FakeModel).GetProperty("TextString").GetCustomAttribute< ArchetypeValueResolverAttribute>();
        }

        [Test]
        public void ArchetypeValueResolver_ResolveValue_EmptyCtorDoesNotThrow()
        {
            Assert.DoesNotThrow(() => new ArchetypeValueResolver());
        }

        [Test]
        public void ArchetypeValueResolver_ResolveValue_ReturnsObjectWithProperlySetup()
        {
            _sut = new ArchetypeValueResolverShim(_context, _culture, _attribute, _bindingServiceMock.Object);

            Assert.IsNotNull(_sut.ResolveValue());
        }
    }
}
