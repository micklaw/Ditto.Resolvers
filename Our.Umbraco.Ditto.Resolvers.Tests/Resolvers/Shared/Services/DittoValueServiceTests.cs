using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Web;
using Archetype.Models;
using Moq;
using NUnit.Framework;
using Our.Umbraco.Ditto.Resolvers.Archetype.Services;
using Our.Umbraco.Ditto.Resolvers.Archetype.Services.Abstract;
using Our.Umbraco.Ditto.Resolvers.Container;
using Our.Umbraco.Ditto.Resolvers.Shared.Services;
using Our.Umbraco.Ditto.Resolvers.Shared.Services.Abstract;
using Our.Umbraco.Ditto.Resolvers.Tests.Archetype.Models.Archetype;
using Our.Umbraco.Ditto.Resolvers.Tests.Fakes;
using Our.Umbraco.Ditto.Resolvers.Tests.Fakes.Ditto;
using Our.Umbraco.Ditto.Resolvers.Tests.Helpers;
using Umbraco.Core.Models;

namespace Our.Umbraco.Ditto.Resolvers.Tests.Resolvers.Shared.Services
{
    [TestFixture]
    public class DittoValueServiceTests
    {
        private DittoValueService _sut;

        private IPublishedContent _content;
        private readonly CultureInfo _culture = new CultureInfo("en-GB");
        private PropertyDescriptor _propertyDescriptor;
        private ArchetypeModel _archetype;
        private DittoValueResolverContext _context;
        private PropertyInfo _propertyInfo;

        [SetUp]
        public void SetUp()
        {
            _sut = new DittoValueService();
            _archetype = ContentHelpers.Archetype;

            var content = ContentHelpers.FakeContent(123, "Fake Node 1", properties: new Collection<IPublishedProperty>
            {
                new FakePublishedProperty("myArchetypeProperty", _archetype, true)
            });

            _content = new FakeModel(content);
            _propertyDescriptor = TypeDescriptor.GetProperties(_content)["Callout"];
            _propertyInfo = typeof (FakeModel).GetProperty("Callout");
            _context = new FakeDittoValueResolverContext(_content, _propertyDescriptor);

            
        }

        [TearDown]
        public void TearDown()
        {
            _sut = null;
            _content = null;
            _propertyDescriptor = null;
            _propertyInfo = null;
            _context = null;
        }

        [Test]
        public void DittoValueService_Set_DoesNotThrowTargetInvocation()
        {
            Assert.DoesNotThrow(() => _sut.Set(_content, _culture, _propertyInfo, null, _content, _context));
        }
    }
}
