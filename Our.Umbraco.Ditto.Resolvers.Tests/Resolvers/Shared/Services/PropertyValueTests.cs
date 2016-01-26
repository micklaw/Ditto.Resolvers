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
using Umbraco.Core.Models;
using System.Globalization;
using Our.Umbraco.Ditto.Resolvers.Tests.Helpers;
using System.Collections.ObjectModel;
using Our.Umbraco.Ditto.Resolvers.Tests.Fakes;
using Our.Umbraco.Ditto.Resolvers.Tests.Archetype.Models.Ditto;
using Our.Umbraco.Ditto.Resolvers.Shared.Services.Abstract;
using Moq;

namespace Our.Umbraco.Ditto.Resolvers.Tests.Resolvers.Shared.Services
{
    [TestFixture]
    public class PropertyValueTests
    {
        private DittoValueService _valueService { get; set; }

        private ArchetypeModel _archetype;
        private IPublishedContent _content;
        private readonly CultureInfo _culture = new CultureInfo("en-GB");
        private PropertyDescriptor _propertyDescriptor;
        private DittoValueResolverContext _context;

        [SetUp]
        public void SetUp()
        {
            _valueService = new DittoValueService();

            _archetype = ContentHelpers.Archetype;

            var content = ContentHelpers.FakeContent(123, "Fake Node 1", properties: new Collection<IPublishedProperty>
            {
                new FakePublishedProperty("myArchetypeProperty", _archetype, true)
            });

            _content = new FakeModel(content);
            _propertyDescriptor = TypeDescriptor.GetProperties(_content)["TextString"];

            _context = new FakeDittoValueResolverContext(_content, _propertyDescriptor);
        }
      
        [Test]
        public void DittoPropertyValue_Set_DoesNotThrowTargetInvocationError()
        {
            Assert.DoesNotThrow(() => _valueService.Set(_content, _culture, typeof(FakeModel).GetProperty("TextString"), null, _content, _context));
        }

        [TearDown]
        public void TearDown()
        {
            _archetype = null;
            _content = null;
            _propertyDescriptor = null;
            _context = null;
        }

    }
}
