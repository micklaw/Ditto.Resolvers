using System;

namespace Our.Umbraco.Ditto.Resolvers.Utils
{
    public static class PreConditions
    {
        public static T NotNull<T>(this T value) where T : class
        {
            if (value == null)
            {
                throw new ArgumentNullException();
            }

            return value;
        }
    }
}
