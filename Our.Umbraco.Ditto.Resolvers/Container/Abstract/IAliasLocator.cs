using System;
using System.ComponentModel;

namespace Our.Umbraco.Ditto.Resolvers.Container.Abstract
{
    public interface IAliasLocator
    {
        Func<Type, string> Resolve<T>();

        Func<Type, string> Resolve(Type type);

        void ClearContainer();
    }
}