using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
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
        private IPublishedContent _content;
        private readonly CultureInfo _culture = new CultureInfo("en-GB");
        private PropertyDescriptor _propertyDescriptor;
        private DittoValueResolverContext _context;

        [SetUp]
        public void SetUp()
        {
            _archetype = ContentHelpers.Archetype;

            var content = ContentHelpers.FakeContent(123, "Fake Node 1", properties: new Collection<IPublishedProperty>
            {
                new FakePublishedProperty("myArchetypeProperty", _archetype, true)
            });

            _content = new FakeModel(content);
            _propertyDescriptor = TypeDescriptor.GetProperties(_content)["TextString"];

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
            _archetype.Fieldsets = new BindingList<ArchetypeFieldsetModel>();

            var resolveValue = _sut.As(_archetype, _propertyDescriptor.PropertyType, _culture, _content, _context);
            
            Assert.AreEqual(resolveValue, null);
        }

        [Test]
        public void ArchetypeBindingService_GetTypedArchetype_FullyPopulatedProperty()
        {
            var resolveValue = _sut.As(_archetype, _propertyDescriptor.PropertyType, _culture, _content, _context) as Callout;

            Assert.IsNotNull(resolveValue);
            Assert.AreEqual(resolveValue.Summary.ToString(), "<p>This is the <strong>summary</strong> text.</p>");
            Assert.AreEqual(resolveValue.Header, "Ready to Enroll?");
            Assert.AreEqual(resolveValue.Link, "{}");
        }
    }
}
