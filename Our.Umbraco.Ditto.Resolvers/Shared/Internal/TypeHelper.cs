using System;
using System.CodeDom;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Umbraco.Core;

namespace Our.Umbraco.Ditto.Resolvers.Shared.Internal
{
    /// <summary>
    /// Extensions methods for <see cref="T:System.Type"/> for inferring type properties.
    /// Most of this code was adapted from the Entity Framework
    /// </summary>
    public static class TypeHelper
    {
        private static ConcurrentDictionary<Type, Dictionary<string, Type>> _typeCache = new ConcurrentDictionary<Type, Dictionary<string, Type>>();
        private static ConcurrentDictionary<Type, Dictionary<string, Type>> _typeByAttributeCache = new ConcurrentDictionary<Type, Dictionary<string, Type>>();

        /// <summary>
        /// Gets all Type instances matching the specified class name with just non-namespace qualified class name.
        /// </summary>
        /// <param name="className">Name of the class sought.</param>
        /// <param name="getAtributeName"></param>
        /// <returns>Types that have the class name specified. They may not be in the same namespace.</returns>
        public static Type GetTypeByName<T>(string className, Func<Type, string> getAtributeName = null)
        {
            var mainType = typeof (T);

            Dictionary<string, Type> types;
            _typeCache.TryGetValue(mainType, out types);

            if (types == null)
            {
                types = new Dictionary<string, Type>();

                var foundTypes = PluginManager.Current.ResolveTypes<T>();

                if (foundTypes != null)
                {
                    foreach (var foundType in foundTypes)
                    {
                        var alias = foundType.Name;

                        if (getAtributeName != null)
                        {
                            alias = getAtributeName(foundType) ?? alias;
                        }

                        types.Add(alias.ToLower(), foundType);
                    }

                    _typeCache.TryAdd(mainType, types);
                }
            }

            Type returnType;
            types.TryGetValue(className.ToLower(), out returnType);

            return returnType;
        }

        public static Type GetTypeByAtttribute<T>(string archetypeAlias, Func<Type, string> getAtributeName = null) where T : Attribute
        {
            var mainType = typeof(T);

            Dictionary<string, Type> types;
            _typeByAttributeCache.TryGetValue(mainType, out types);

            if (types == null)
            {
                types = new Dictionary<string, Type>();

                var foundTypes = PluginManager.Current.ResolveAttributedTypes<T>();

                if (foundTypes != null)
                {
                    foreach (var foundType in foundTypes)
                    {
                        var alias = foundType.Name;

                        if (getAtributeName != null)
                        {
                            alias = getAtributeName(foundType) ?? alias;
                        }

                        types.Add(alias.ToLower(), foundType);
                    }

                    _typeByAttributeCache.TryAdd(mainType, types);
                }
            }

            Type returnType;
            types.TryGetValue(archetypeAlias.ToLower(), out returnType);

            return returnType;
        }
    }
}