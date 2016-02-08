using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web;
using Archetype.Models;
using Moq;
using NUnit.Framework;
using Our.Umbraco.Ditto.Resolvers.Archetype.Services;
using Our.Umbraco.Ditto.Resolvers.Archetype.Services.Abstract;
using Our.Umbraco.Ditto.Resolvers.Container;
using Our.Umbraco.Ditto.Resolvers.Shared.Services.Abstract;
using Our.Umbraco.Ditto.Resolvers.Tests.Archetype.Models.Archetype;
using Our.Umbraco.Ditto.Resolvers.Tests.Fakes;
using Our.Umbraco.Ditto.Resolvers.Tests.Fakes.Ditto;
using Our.Umbraco.Ditto.Resolvers.Tests.Helpers;
using Umbraco.Core.Models;

namespace Our.Umbraco.Ditto.Resolvers.Tests.Archetype.Services
{
    [TestFixture]
    public class ArchetypeBindingServiceTests
    {
        private IArchetypeBindingService _sut;

        private ArchetypeModel _archetype;
        private ArchetypeModel _multiArchetype;
        private IPublishedContent _content;
        private readonly CultureInfo _culture = new CultureInfo("en-GB");
        private PropertyDescriptor _propertyDescriptor;
        private DittoValueResolverContext _context;

        [SetUp]
        public void SetUp()
        {
            _archetype = ContentHelpers.Archetype;
            _multiArchetype = ContentHelpers.MultiArchetype;

            var content = ContentHelpers.FakeContent(123, "Fake Node 1", properties: new Collection<IPublishedProperty>
            {
                new FakePublishedProperty("myArchetypeProperty", _archetype, true),
                new FakePublishedProperty("justText", _multiArchetype, true)
            });

            _content = new FakeModel(content);
        }

        [TearDown]
        public void TearDown()
        {
            _sut = null;
            _archetype = null;
            _content = null;
            _propertyDescriptor = null;
            _context = null;
        }

        [Test]
        public void ArchetypeBindingService_GetTypedArchetype_NoFieldsetsRetunsNull()
        {
            _propertyDescriptor = TypeDescriptor.GetProperties(_content)["Callout"];
            _context = new FakeDittoValueResolverContext(_content, _propertyDescriptor);

            var mockedPropertyService = new Mock<PropertyValueService>();

            mockedPropertyService.SetupSequence(
                i =>
                    i.Set(It.IsAny<IPublishedContent>(), It.IsAny<CultureInfo>(), It.IsAny<PropertyInfo>(),
                        It.IsAny<object>(), It.IsAny<object>(), It.IsAny<DittoValueResolverContext>()))
                        .Returns(new HtmlString("<p>This is the <strong>summary</strong> text.</p>"))
                        .Returns("Ready to Enroll?")
                        .Returns("{}");

            _sut = new ArchetypeBindingService(mockedPropertyService.Object, new DittoAliasLocator());

            _archetype.Fieldsets = new BindingList<ArchetypeFieldsetModel>();

            var resolveValue = _sut.As(_archetype, _propertyDescriptor.PropertyType, _culture, _content, _context);
            
            Assert.IsNotNull(resolveValue);
        }

        [Test]
        public void ArchetypeBindingService_GetTypedArchetype_FullyPopulatedPropertySingleItem()
        {
            _propertyDescriptor = TypeDescriptor.GetProperties(_content)["Callout"];
            _context = new FakeDittoValueResolverContext(_content, _propertyDescriptor);

            var mockedPropertyService = new Mock<PropertyValueService>();

            mockedPropertyService.SetupSequence(
                i =>
                    i.Set(It.IsAny<IPublishedContent>(), It.IsAny<CultureInfo>(), It.IsAny<PropertyInfo>(),
                        It.IsAny<object>(), It.IsAny<object>(), It.IsAny<DittoValueResolverContext>()))
                        .Returns(new HtmlString("<p>This is the <strong>summary</strong> text.</p>"))
                        .Returns("Ready to Enroll?")
                        .Returns("{}");

            _sut = new ArchetypeBindingService(mockedPropertyService.Object, new DittoAliasLocator());

            var resolveValue = _sut.As(_archetype, _propertyDescriptor.PropertyType, _culture, _content, _context) as Callout;

            Assert.IsNotNull(resolveValue);
            Assert.AreEqual(resolveValue.Summary.ToString(), "<p>This is the <strong>summary</strong> text.</p>");
            Assert.AreEqual(resolveValue.Header, "Ready to Enroll?");
            Assert.AreEqual(resolveValue.Link, "{}");
            Assert.AreEqual(resolveValue.Alias, "calloutAlias");
            Assert.IsTrue(resolveValue.Disabled);
        }

        [Test]
        public void ArchetypeBindingService_GetTypedArchetype_FullyPopulatedPropertyListItemWithInterface()
        {
            _propertyDescriptor = TypeDescriptor.GetProperties(_content)["WidgetList"];
            _context = new FakeDittoValueResolverContext(_content, _propertyDescriptor);

            var mockedPropertyService = new Mock<PropertyValueService>();

            mockedPropertyService.SetupSequence(
                i =>
                    i.Set(It.IsAny<IPublishedContent>(), It.IsAny<CultureInfo>(), It.IsAny<PropertyInfo>(),
                        It.IsAny<object>(), It.IsAny<object>(), It.IsAny<DittoValueResolverContext>()))
                        .Returns(new HtmlString("<p>This is the <strong>summary</strong> text.</p>"))
                        .Returns("Ready to Enroll?")
                        .Returns("{}")
                        .Returns("Loadsa text");

            _sut = new ArchetypeBindingService(mockedPropertyService.Object, new DittoAliasLocator());

            var resolveValue = _sut.As(_multiArchetype, _propertyDescriptor.PropertyType, _culture, _content, _context) as List<IWidget>;

            Assert.IsNotNull(resolveValue);

            var first = resolveValue.First() as Callout;
            var second = resolveValue.Skip(1).First() as JustText;

            Assert.IsNotNull(first);
            Assert.AreEqual(first.Summary.ToString(), "<p>This is the <strong>summary</strong> text.</p>");
            Assert.AreEqual(first.Header, "Ready to Enroll?");
            Assert.AreEqual(first.Link, "{}");
            Assert.AreEqual(first.Alias, "calloutAlias");
            Assert.IsTrue(first.Disabled);

            Assert.IsNotNull(second);
            Assert.AreEqual(second.Text, "Loadsa text");
        }
    }
}
