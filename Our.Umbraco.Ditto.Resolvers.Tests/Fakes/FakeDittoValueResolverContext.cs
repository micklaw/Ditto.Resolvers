using System.ComponentModel;
using System.Reflection;
using Umbraco.Core.Models;

namespace Our.Umbraco.Ditto.Resolvers.Tests.Fakes
{
    public class FakeDittoValueResolverContext : DittoValueResolverContext
    {
        public FakeDittoValueResolverContext(IPublishedContent content, PropertyDescriptor propertyDescriptor)
        {
            // [ML] - Pretty messed up having to do this just to complete some tests!! May ask guys if they can make the setter protected?

            var instanceProperty = GetType().GetProperty("Instance", BindingFlags.Instance | BindingFlags.Public);
            var descriptorProperty = GetType().GetProperty("PropertyDescriptor", BindingFlags.Instance | BindingFlags.Public);

            instanceProperty.SetValue(this, content);
            descriptorProperty.SetValue(this, propertyDescriptor);
        }
    }
}
