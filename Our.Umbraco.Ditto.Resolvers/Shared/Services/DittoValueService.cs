using System;
using System.Globalization;
using System.Reflection;
using Our.Umbraco.Ditto.Resolvers.Shared.Services.Abstract;
using Umbraco.Core.Models;

namespace Our.Umbraco.Ditto.Resolvers.Shared.Services
{
    /// <summary>
    /// Class used to hook in to the Ditto GetTypedValue method, this uses reflection, so careful now as things change quickly! 
    /// </summary>
    public class DittoValueService : PropertyValueService
    {
        private readonly Lazy<MethodInfo> _getResolvedValue = new Lazy<MethodInfo>(() => typeof(PublishedContentExtensions).GetMethod("GetResolvedValue", BindingFlags.NonPublic | BindingFlags.Static));
        private readonly Lazy<MethodInfo> _getTypedValue = new Lazy<MethodInfo>(() => typeof (PublishedContentExtensions).GetMethod("GetConvertedValue", BindingFlags.NonPublic | BindingFlags.Static));

        /// <summary>
        /// Get the value using the Ditto GetTypedValue method on PublishedContentExtensions via reflection (breakable)
        /// </summary>
        /// <param name="content"></param>
        /// <param name="culture"></param>
        /// <param name="propertyInfo"></param>
        /// <param name="propertyValue"></param>
        /// <param name="instance"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object Set(IPublishedContent content, CultureInfo culture, PropertyInfo propertyInfo, object propertyValue, object instance, DittoValueResolverContext context)
        {
            object resolverValue = null;

            var typedResolvedMethod = _getResolvedValue.Value;
            if (typedResolvedMethod != null)
            {
                resolverValue = typedResolvedMethod.Invoke(this, new[] { content, culture, propertyInfo, instance, new [] { context }});
            }

            object result = null;

            var typedValueMethod = _getTypedValue.Value;
            if (typedValueMethod != null)
            {
                result = typedValueMethod.Invoke(this, new [] { content, culture, propertyInfo, resolverValue ?? propertyValue, instance });
            }

            return result;
        }
    }
}
