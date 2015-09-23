using Ditto.Resolvers.Sample.Models.DocTypes.Base;

namespace Ditto.Resolvers.Sample.Models
{
    public class ViewModel<T> where T : Content
    {
        public T Content { get; set; }
    }
}