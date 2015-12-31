using System;
using System.Collections.Concurrent;
using Our.Umbraco.Ditto.Resolvers.Container.Abstract;

namespace Our.Umbraco.Ditto.Resolvers.Container
{
    public class DittoAliasLocator : IAliasLocator
    {
        private static readonly Lazy<ConcurrentDictionary<Type, Func<Type, string>>> _lazyCache = new Lazy<ConcurrentDictionary<Type, Func<Type, string>>>(() => new ConcurrentDictionary<Type, Func<Type, string>>());

        private static ConcurrentDictionary<Type, Func<Type, string>> _cache => _lazyCache.Value;

        public Func<Type, string> Resolve<T>()
        {
            return Resolve(typeof(T));
        }

        public Func<Type, string> Resolve(Type type)
        {
            if (type == null)
            {
                return null;
            }

            Func<Type, string> func;
            _cache.TryGetValue(type, out func);

            return func;
        }

        public static bool Register(Type type, Func<Type, string> method)
        {
            if (type == null)
            {
                throw new ArgumentException("alias");
            }

            if (method == null)
            {
                throw new ArgumentException("method");
            }

            return _cache.TryAdd(type, method);
        }

        public static bool Register<T>(Func<Type, string> method)
        {
            return Register(typeof(T), method);
        }

        public void ClearContainer()
        {
            _cache.Clear();
        }
    }
}
