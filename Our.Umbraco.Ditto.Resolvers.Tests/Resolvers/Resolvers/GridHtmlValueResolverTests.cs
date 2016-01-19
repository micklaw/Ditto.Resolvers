using System.Collections.ObjectModel;
using System.ComponentModel;
using NUnit.Framework;
using Our.Umbraco.Ditto.Resolvers.Resolvers;
using Our.Umbraco.Ditto.Resolvers.Resolvers.Attributes;
using Our.Umbraco.Ditto.Resolvers.Tests.Fakes;
using Our.Umbraco.Ditto.Resolvers.Tests.Fakes.Ditto;
using Our.Umbraco.Ditto.Resolvers.Tests.Helpers;
using Umbraco.Core.Models;

namespace Our.Umbraco.Ditto.Resolvers.Tests.Resolvers.Resolvers
{
    [TestFixture]
    public class GridHtmlValueResolverTests
    {
        private GridHtmlValueResolverShim _sut;
        private FakeModel _content;
        private FakeDittoValueResolverContext _context;
        private PropertyDescriptor _propertyDescriptor;

        [SetUp]
        public void SetUp()
        {
            var content = ContentHelpers.FakeContent(123, "Fake Node 1", properties: new Collection<IPublishedProperty>());

            _content = new FakeModel(content);

            _propertyDescriptor = TypeDescriptor.GetProperties(_content)["GridHtml"];
            _context = new FakeDittoValueResolverContext(_content, _propertyDescriptor);
        }

        [Test]
        public void GridHtmlValueResolver_ResolveValue_NoAttributeReturnsNull()
        {
            _sut = new GridHtmlValueResolverShim(new GridHtmlValueResolverAttribute(typeof(GridHtmlValueResolver)), _content, _context);

            Assert.AreEqual(_sut.ResolveValue(), null);
        }
    }
}
