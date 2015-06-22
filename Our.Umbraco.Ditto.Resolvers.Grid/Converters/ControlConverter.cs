using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Our.Umbraco.Ditto.Resolvers.Container;
using Our.Umbraco.Ditto.Resolvers.Container.Abstract;
using Our.Umbraco.Ditto.Resolvers.Grid.Attributes;
using Our.Umbraco.Ditto.Resolvers.Grid.Models;
using Our.Umbraco.Ditto.Resolvers.Shared.Services;
using Our.Umbraco.Ditto.Resolvers.Shared.Services.Abstract;
using Umbraco.Core.Models;

namespace Our.Umbraco.Ditto.Resolvers.Grid.Converters
{
    internal class ControlConverter : Newtonsoft.Json.Converters.CustomCreationConverter<Control>
    {
        private EmitterService _emitter { get; set; }
        private PropertyValueService _valueService { get; set; }
        private IResolverLocator _resolver { get; set; }

        private IPublishedContent _content { get; set; }
        private CultureInfo _culture { get; set; }

        public ControlConverter(IPublishedContent content, CultureInfo culture = null)
        {
            _resolver = DependencyResolver.Current.GetService<IResolverLocator>() ?? DependencyResolver.Current.GetService<DittoResolverTypeLocator>();
            _valueService = DependencyResolver.Current.GetService<PropertyValueService>() ??  DependencyResolver.Current.GetService<DittoValueService>();
            _emitter = DependencyResolver.Current.GetService<EmitterService>() ?? DependencyResolver.Current.GetService<DittoEmitterService>();

            _content = content;
            _culture = culture;
        }

        public Control Create(JObject jObject)
        {
            // [ML] - Resolve original type

            var control = JsonConvert.DeserializeObject<Control>(jObject.ToString());

            // [ML] - If we have an alias (which we should, get a typeconverter and proxy the class
 
            if (control != null && !string.IsNullOrWhiteSpace(control.editor.alias))
            {
                var typeConverterAttribute = _resolver.Resolve(control.editor.alias);
                var isResolved = typeConverterAttribute != null;

                var attribute = isResolved ? typeof (TypeConverterAttribute) : null;
                var ctorParams = isResolved ? new[] {typeof (Type)} : Type.EmptyTypes;
                var ctorValues = isResolved ? new object[] { typeConverterAttribute } : null;

                // [ML] - Override the virtual propery with our own

                var proxy = _emitter.OverrideProperty<Control>("ConvertedValue", attribute, ctorParams, ctorValues);

                proxy.editor = control.editor;
                proxy.value = control.value;
                proxy.ConvertedValue = control.ConvertedValue;

                var property = proxy.GetType().GetProperty("ConvertedValue");

                // [ML] - Set the value using Ditto

                proxy.ConvertedValue = _valueService.Set(_content, _culture, property, proxy.value, proxy);

                return proxy;
            }

            return control;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Control target = null;

            if (reader != null)
            {
                var jObject = JObject.Load(reader);

                target = Create(jObject);
            }

            return target;
        }

        public override Control Create(Type objectType)
        {
            throw new NotImplementedException();
        }
    }
}
