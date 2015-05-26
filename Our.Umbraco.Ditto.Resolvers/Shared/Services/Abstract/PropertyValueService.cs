using System;
using System.Globalization;
using System.Reflection;
using Umbraco.Core.Models;

namespace Our.Umbraco.Ditto.Resolvers.Shared.Services.Abstract
{
    public abstract class PropertyValueService
    {
        /// <summary>
        /// SGets the converted value from the Umbraco property value
        /// </summary>
        /// <param name="content"></param>
        /// <param name="culture"></param>
        /// <param name="propertyInfo"></param>
        /// <param name="propertyValue"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        public abstract object Set(IPublishedContent content, CultureInfo culture, PropertyInfo propertyInfo, object propertyValue, object instance);
    }
}
