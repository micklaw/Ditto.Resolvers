using System;

namespace Our.Umbraco.Ditto.Resolvers.Container.Abstract
{
    /// <summary>
    /// Resolve 
    /// </summary>
    public interface IAliasLocator
    {
        Func<Type, string> Resolve<T>();

        Func<Type, string> Resolve(Type type);

        void ClearContainer();
    }
}