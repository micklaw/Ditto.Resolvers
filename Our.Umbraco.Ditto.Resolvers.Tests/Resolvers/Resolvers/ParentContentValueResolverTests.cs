using System.Collections.ObjectModel;
using System.ComponentModel;
using NUnit.Framework;
using Our.Umbraco.Ditto.Resolvers.Resolvers;
using Our.Umbraco.Ditto.Resolvers.Tests.Fakes;
using Our.Umbraco.Ditto.Resolvers.Tests.Fakes.Ditto;
using Our.Umbraco.Ditto.Resolvers.Tests.Helpers;
using Umbraco.Core.Models;

namespace Our.Umbraco.Ditto.Resolvers.Tests.Resolvers.Resolvers
{
    [TestFixture]
    public class ParentContentValueResolverTests
    {
        private ParentContentValueResolverShim _sut;
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

            _content = new FakeModel(content);

            _propertyDescriptor = TypeDescriptor.GetProperties(_content)["TextString"];
            _context = new FakeDittoValueResolverContext(_content, _propertyDescriptor);
        }

        [Test]
        public void ParentContentValueResolver_IsValid_TrueWithAllFields()
        {
            _sut = new ParentContentValueResolverShim(new DittoValueResolverAttribute(typeof(ParentContentValueResolver)), _content, _context);

            Assert.IsTrue(_sut.IsValid());
        }

        [Test]
        public void ParentContentValueResolver_IsValid_FalseWhenNoContextContentInstance()
        {
            _context = new FakeDittoValueResolverContext(null, _propertyDescriptor);

            _sut = new ParentContentValueResolverShim(new DittoValueResolverAttribute(typeof(ParentContentValueResolver)), _content, _context);

            Assert.IsFalse(_sut.IsValid());
        }

        [Test]
        public void ParentContentValueResolver_IsValid_FalseWhenNoPropertyDescriptor()
        {
            _context = new FakeDittoValueResolverContext(_content, null);

            _sut = new ParentContentValueResolverShim(new DittoValueResolverAttribute(typeof(ParentContentValueResolver)), _content, _context);

            Assert.IsFalse(_sut.IsValid());
        }

        [Test]
        public void ParentContentValueResolver_IsValid_FalseWhenNoContentParent()
        {
            var content = ContentHelpers.FakeContent(123, "Fake Node 1", properties: new Collection<IPublishedProperty>
            {
                new FakePublishedProperty("myArchetypeProperty", ContentHelpers.Archetype, true)
            });

            _content = new FakeModel(content);

            _context = new FakeDittoValueResolverContext(_content, null);

            _sut = new ParentContentValueResolverShim(new DittoValueResolverAttribute(typeof(ParentContentValueResolver)), _content, _context);

            Assert.IsFalse(_sut.IsValid());
        }
    }
}
