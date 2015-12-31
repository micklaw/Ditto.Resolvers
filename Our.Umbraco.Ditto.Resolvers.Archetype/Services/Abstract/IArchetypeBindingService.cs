using System;
using System.Globalization;
using Archetype.Models;
using Umbraco.Core.Models;

namespace Our.Umbraco.Ditto.Resolvers.Archetype.Services.Abstract
{
    public interface IArchetypeBindingService
    {
        /// <summary>
        /// Returns an object representing the given <see cref="Type"/>.
        /// </summary>
        /// <param name="archetype">
        /// The <see cref="ArchetypeModel"/> to convert.
        /// </param>
        /// <param name="type">
        /// The <see cref="Type"/> of items to return.
        /// </param>
        /// <param name="content">
        /// 
        /// </param>
        /// <param name="culture">
        /// The <see cref="CultureInfo"/>
        /// </param>
        /// <param name="context"></param>
        /// <returns>
        /// The converted <see cref="Object"/> as the given type.
        /// </returns>
        object As(
            ArchetypeModel archetype,
            Type type,
            CultureInfo culture = null,
            IPublishedContent content = null,
            DittoValueResolverContext context = null);
    }
}