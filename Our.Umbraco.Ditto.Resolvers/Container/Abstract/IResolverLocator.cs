using System;
using System.ComponentModel;

namespace Our.Umbraco.Ditto.Resolvers.Container.Abstract
{
    public interface IResolverLocator
    {
        Type Resolve(string alias);

        void ClearContainer();
    }
}