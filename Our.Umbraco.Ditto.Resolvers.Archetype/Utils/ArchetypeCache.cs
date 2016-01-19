using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;
using Our.Umbraco.Ditto.Resolvers.Utils;

namespace Our.Umbraco.Ditto.Resolvers.Archetype.Utils
{
    public class ArchetypeCache
    {
        /// <summary>
        /// The cache for storing constructor parameter information.
        /// </summary>
        private static readonly ConcurrentDictionary<string, Type> _archetypeCache = new ConcurrentDictionary<string, Type>();

        /// <summary>
        /// Get the type for a given alias
        /// </summary>
        /// <param name="alias"></param>
        /// <returns></returns>
        public static Type GetTypeFromAlias(string alias)
        {
            alias = alias.NotNull();

            Type type;

            _archetypeCache.TryGetValue(alias, out type);

            return type;
        }

        /// <summary>
        /// Add a Type with alias key to the archetype cache
        /// </summary>
        /// <param name="alias"></param>
        /// <param name="instanceType"></param>
        public static void AddTypeByAlias(string alias, Type instanceType)
        {
            alias = alias.NotNull();
            instanceType = instanceType.NotNull();

            _archetypeCache.TryAdd(alias, instanceType);
        }
    
        /// <summary>
        /// The cache for storing type property information.
        /// </summary>
        private static readonly ConcurrentDictionary<Type, PropertyInfo[]> _propertyCache = new ConcurrentDictionary<Type, PropertyInfo[]>();

        /// <summary>
        /// Get the properties for a given type
        /// </summary>
        /// <param name="instanceType"></param>
        /// <returns></returns>
        public static PropertyInfo[] GetPropertiesFromType(Type instanceType)
        {
            instanceType = instanceType.NotNull();

            PropertyInfo[] type;

            _propertyCache.TryGetValue(instanceType, out type);

            return type;
        }

        /// <summary>
        /// Add properties of type to the cache
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static void AddToPropertyCache(Type type)
        {
            PropertyInfo[] properties;
            _propertyCache.TryGetValue(type, out properties);

            if (properties == null)
            {
                properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(x => x.CanWrite).ToArray();

                _propertyCache.TryAdd(type, properties);
            }
        }
    }
}
