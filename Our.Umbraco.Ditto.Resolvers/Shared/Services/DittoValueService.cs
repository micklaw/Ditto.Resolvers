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
            object result = null;

            var dynMethod = _getTypedValue.Value;

            if (dynMethod != null)
            {
                result = dynMethod.Invoke(this, new [] { content, culture, propertyInfo, propertyValue, instance });
            }

            return result;
        }
    }
}
