using System;

namespace Our.Umbraco.Ditto.Resolvers.Shared.Services.Abstract
{
    /// <summary>
    /// Service used to proxy a class and override virtual properties with new properties on proxy class
    /// </summary>
    public abstract class EmitterService
    {
        /// <summary>
        /// Override a virtual property and inject an attribute to it
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName"></param>
        /// <param name="attribute"></param>
        /// <param name="constructorParams"></param>
        /// <param name="constructorValues"></param>
        /// <returns></returns>
        public abstract T OverrideProperty<T>(string propertyName, Type attribute = null, Type[] constructorParams = null, object[] constructorValues = null);
    }
}
