using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Web;
using Ditto.Resolvers.Sample.Models.Grid;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Our.Umbraco.Ditto;
using Umbraco.Web;

namespace Ditto.Resolvers.Sample.Models.Ditto.TypeConverters
{
    public class GridImageConverter : DittoConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            // We can pass null here.
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (sourceType == null || sourceType == typeof(string) || sourceType == typeof(JObject))
            {
                return true;
            }

            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            var helper = new UmbracoHelper(UmbracoContext.Current);

            if (value is JObject)
            {
                var model = JsonConvert.DeserializeObject<Image>(value.ToString());

                if (model.id > 0)
                {
                    model.Media = helper.TypedMedia(model.id);

                    return model;
                }
            }

            return null;
        }
    }
}