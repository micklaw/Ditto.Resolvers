using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucene.Net.Util.Cache;
using Our.Umbraco.Ditto.Resolvers.Container.Abstract;

namespace Our.Umbraco.Ditto.Resolvers.Container
{
    public class DittoResolverTypeLocator : IResolverLocator
    {
        private static readonly Lazy<ConcurrentDictionary<string, Type>> _lazyCache = new Lazy<ConcurrentDictionary<string, Type>>(() => new ConcurrentDictionary<string, Type>());

        private static ConcurrentDictionary<string, Type> _cache
        {
            get
            {
                return _lazyCache.Value;
            }
        }

        public Type Resolve(string alias)
        {
            if (string.IsNullOrWhiteSpace(alias))
            {
                return null;
            }

            Type type;
            _cache.TryGetValue(alias.ToLower(), out type);

            return type;
        }

        public static bool Register(string alias, Type type)
        {
            if (string.IsNullOrWhiteSpace(alias))
            {
                throw new ArgumentException("alias");
            }

            if (type == null)
            {
                throw new ArgumentException("type");
            }

            return _cache.TryAdd(alias.ToLower(), type);
        }

        public static bool Register<T>(string alias) where T : TypeConverter
        {
            return Register(alias, typeof(T));
        }

        public void ClearContainer()
        {
            _cache.Clear();
        }
    }
}
