using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core;

namespace Our.Umbraco.Ditto.Resolvers.Shared.Internal
{
    /// <summary>
    /// Extensions methods for <see cref="T:System.Type"/> for inferring type properties.
    /// Most of this code was adapted from the Entity Framework
    /// </summary>
    public static class TypeHelper
    {
        /// <summary>
        /// Gets all Type instances matching the specified class name with just non-namespace qualified class name.
        /// </summary>
        /// <param name="className">Name of the class sought.</param>
        /// <returns>Types that have the class name specified. They may not be in the same namespace.</returns>
        public static IEnumerable<Type> GetTypeByName<T>(string className)
        {
            var types = PluginManager.Current.ResolveTypes<T>();

            return types.Where(type => type.Name.ToLower() == className.ToLower());
        }
    }
}