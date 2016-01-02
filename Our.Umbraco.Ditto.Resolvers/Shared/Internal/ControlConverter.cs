using System;
using System.ComponentModel;
using System.Globalization;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Our.Umbraco.Ditto.Resolvers.Container;
using Our.Umbraco.Ditto.Resolvers.Container.Abstract;
using Our.Umbraco.Ditto.Resolvers.Models.Grid;
using Our.Umbraco.Ditto.Resolvers.Shared.Services;
using Our.Umbraco.Ditto.Resolvers.Shared.Services.Abstract;
using Umbraco.Core.Models;

namespace Our.Umbraco.Ditto.Resolvers.Shared.Internal
{
    internal class ControlConverter<T> : Newtonsoft.Json.Converters.CustomCreationConverter<Control> where T : Control
    {
        private EmitterService _emitter { get; set; }
        private PropertyValueService _valueService { get; set; }
        private IResolverLocator _resolver { get; set; }

        private IPublishedContent _content { get; set; }
        private CultureInfo _culture { get; set; }
        public DittoValueResolverContext _context { get; set; }

        public ControlConverter(IPublishedContent content, CultureInfo culture = null, DittoValueResolverContext context = null)
        {
            _resolver = DependencyResolver.Current.GetService<IResolverLocator>() ?? DependencyResolver.Current.GetService<DittoResolverTypeLocator>();
            _valueService = DependencyResolver.Current.GetService<PropertyValueService>() ??  DependencyResolver.Current.GetService<DittoValueService>();
            _emitter = DependencyResolver.Current.GetService<EmitterService>() ?? DependencyResolver.Current.GetService<DittoEmitterService>();

            _content = content;
            _culture = culture;
            _context = context;
        }

        public Control Create(JObject jObject)
        {
            // [ML] - Resolve original type

            var json = jObject.ToString();

            var control = JsonConvert.DeserializeObject<T>(json);

            // [ML] - If we have an alias (which we should, get a typeconverter and proxy the class
 
            if (control != null && !string.IsNullOrWhiteSpace(control.editor.alias))
            {
                var typeConverterAttribute = _resolver.Resolve(control.editor.alias);
                var isResolved = typeConverterAttribute != null;

                var attribute = isResolved ? typeof (TypeConverterAttribute) : null;
                var ctorParams = isResolved ? new[] {typeof (Type)} : Type.EmptyTypes;
                var ctorValues = isResolved ? new object[] { typeConverterAttribute } : null;

                // [ML] - Override the virtual propery with our own

                var proxy = _emitter.OverrideProperty<T>("ConvertedValue", attribute, ctorParams, ctorValues);
                var proxyType = proxy.GetType();

                var proxyObject = JsonConvert.DeserializeObject(json, proxyType) as T;
                var convertProperyInfo = proxyObject.GetType().GetProperty("ConvertedValue");

                // [ML] - Set the value using Ditto

                proxyObject.ConvertedValue = _valueService.Set(_content, _culture, convertProperyInfo, proxyObject.value, proxyObject, _context);

                return proxyObject;
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
