using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Our.Umbraco.Ditto.Resolvers.Shared.Services.Abstract;

namespace Our.Umbraco.Ditto.Resolvers.Shared.Services
{
    public class DittoEmitterService : EmitterService
    {
        public override T OverrideProperty<T>(string propertyName, Type attribute = null, Type [] constructorParams = null, object [] constructorValues = null)
        {
            // [ML] - Default to an empty instance 

            constructorParams = constructorParams ?? Type.EmptyTypes;
            constructorValues = constructorValues ?? new object[] { null };

            var type = typeof(T);

            // [ML] - Proxy the class

            var proxyNamespace = new AssemblyName(this.GetType().Namespace + ".Proxied");
            var assembly = AppDomain.CurrentDomain.DefineDynamicAssembly(proxyNamespace, AssemblyBuilderAccess.RunAndSave);
            var module = assembly.DefineDynamicModule(proxyNamespace.Name);
            var proxyType = module.DefineType(type.Name + "Proxy", TypeAttributes.Public | TypeAttributes.BeforeFieldInit, type);

            // [ML] - Override the virtual converted value

            var oldProp = type.GetProperty(propertyName);

            if (oldProp != null && oldProp.GetGetMethod().IsVirtual)
            {
                var newProp = proxyType.DefineProperty(oldProp.Name, oldProp.Attributes, oldProp.PropertyType, null);
                var backingField = proxyType.DefineField(string.Format("<{0}>k__BackingField", propertyName), oldProp.PropertyType, FieldAttributes.Private);

                // [ML] - Add the custom attribute
                if (attribute != null)
                {
                    var ctor = attribute.GetConstructor(constructorParams);

                    if (ctor != null)
                    {
                        newProp.SetCustomAttribute(new CustomAttributeBuilder(ctor, constructorValues, new FieldInfo[0], new object[0]));
                    }
                }

                // [ML] - Define a placeholder for a getter and setter of the new override property on the proxy class

                var attributes = MethodAttributes.Virtual | MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig;

                var pGet = proxyType.DefineMethod("get_" + oldProp.Name, attributes, oldProp.PropertyType, Type.EmptyTypes);
                var pSet = proxyType.DefineMethod("set_" + oldProp.Name, attributes, null, new[] {oldProp.PropertyType});

                // [ML] - Give the getter a method body

                ILGenerator pILGet = pGet.GetILGenerator();

                
                pILGet.Emit(OpCodes.Ldarg_0);
                pILGet.Emit(OpCodes.Ldfld, backingField);
                pILGet.Emit(OpCodes.Ret);

                // [ML] - Give the setter a method body

                ILGenerator pILSet = pSet.GetILGenerator();

                pILSet.Emit(OpCodes.Ldarg_0);
                pILSet.Emit(OpCodes.Ldarg_1);
                pILSet.Emit(OpCodes.Stfld, backingField);
                pILSet.Emit(OpCodes.Ret);

                newProp.SetSetMethod(pSet);
                newProp.SetGetMethod(pGet);
            }

            // [ML] - Create and return a an instance

            var newType = proxyType.CreateType();

            return (T)Activator.CreateInstance(newType);
        }
    }
}
