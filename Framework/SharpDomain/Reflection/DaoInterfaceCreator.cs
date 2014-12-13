using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace SharpDomain.Reflection
{
    /// <summary>
    /// This class allows creating concrete classes for simple dao interfaces.
    /// Dao interfaces can only have properties with getter and setter
    /// </summary>
    public class DaoInterfaceProxyBuilder
    {
        private const string DefaultAssemblyName = "DaoInterfaceProxyBuilderAssembly";
        private AssemblyBuilder _assembly;
        private ModuleBuilder _module;
        private ConcurrentDictionary<Type, Type> ConcreteTypesCache;


        public DaoInterfaceProxyBuilder()
            : this(DefaultAssemblyName)
        {
        }

        public DaoInterfaceProxyBuilder(string assemblyName)
        {
            ConcreteTypesCache = new ConcurrentDictionary<Type, Type>();
            _assembly = AppDomain.CurrentDomain.DefineDynamicAssembly(
                new AssemblyName(assemblyName),
                AssemblyBuilderAccess.Run);
            _module = _assembly.DefineDynamicModule(assemblyName);
        }

        public Type GetConcreteType(Type interfaceType)
        {
            if (interfaceType == null) throw new ArgumentNullException("interfaceType");

            var type = ConcreteTypesCache.GetOrAdd(interfaceType, CreateConcreteType);

            return type;
        }

        private Type CreateConcreteType(Type interfaceType)
        {
            if (!interfaceType.IsInterface) throw new ArgumentException("Not an interface: " + interfaceType.FullName);

            string typeName = interfaceType.FullName + "Impl";
            var builder = _module.DefineType(typeName,
                TypeAttributes.Serializable |
                TypeAttributes.Class |
                TypeAttributes.Public,
                //TypeAttributes.Sealed,
                typeof(object));

            builder.DefineDefaultConstructor(MethodAttributes.Public);

            var allProperties = GetAllProperties(interfaceType).ToList();

            foreach (var property in allProperties)
            {
                var propertyBuilder = CreateProperty(builder, property);
            }

            var proxyAttr =
                new CustomAttributeBuilder(typeof (DaoInterfaceProxyForAttribute).GetConstructor(new[] {typeof (Type)}),
                    new object[] {interfaceType});
            builder.SetCustomAttribute(proxyAttr);

            builder.AddInterfaceImplementation(interfaceType);

            var type = builder.CreateType();

            return type;
        }

        private PropertyBuilder CreateProperty(TypeBuilder builder, PropertyInfo property)
        {
            var field = BuildField(builder, property);
            var getMethod = BuildGetMethod(builder, property, field);
            var setMethod = BuildSetMethod(builder, property, field);

            var propBuilder = builder.DefineProperty(property.Name,
                property.Attributes | PropertyAttributes.HasDefault,
                property.PropertyType,
                null);

            foreach (var customAttribute in property.GetCustomAttributes(true))
            {
                var attrBuilder = BuildAttribute(customAttribute);
                if (attrBuilder != null)
                    propBuilder.SetCustomAttribute(attrBuilder);
            }

            propBuilder.SetGetMethod(getMethod);
            propBuilder.SetSetMethod(setMethod);

            return propBuilder;
        }

        private static MethodBuilder BuildGetMethod(TypeBuilder builder, PropertyInfo property, FieldBuilder field)
        {
            var method = builder.DefineMethod(
                "get_" + property.Name
                , MethodAttributes.Public 
                | MethodAttributes.SpecialName 
                | MethodAttributes.HideBySig 
                | MethodAttributes.Final 
                | MethodAttributes.Virtual 
                | MethodAttributes.VtableLayoutMask
                , property.PropertyType
                , Type.EmptyTypes);

            var il = method.GetILGenerator();
            //var local = il.DeclareLocal(property.PropertyType);
            // return this._Property
            // - LoadArg this (0)
            // - LoadField _Property
            // - Ret
            il.Emit(OpCodes.Nop);
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldfld, field);
            //il.Emit(OpCodes.Stloc, local);
            //il.Emit(OpCodes.Ldloc, local);
            il.Emit(OpCodes.Ret);

            return method;
        }
        private static MethodBuilder BuildSetMethod(TypeBuilder builder, PropertyInfo property, FieldBuilder field)
        {
            var method = builder.DefineMethod(
                "set_" + property.Name
                , MethodAttributes.Public
                | MethodAttributes.SpecialName
                | MethodAttributes.HideBySig
                | MethodAttributes.Final
                | MethodAttributes.Virtual
                | MethodAttributes.VtableLayoutMask
                , returnType: null
                , parameterTypes: new[] { property.PropertyType });

            var il = method.GetILGenerator();
            // this._Property = value
            // - LoadArg this (0)
            // - LoadArg value (1)
            // - SetField _Property
            // - Ret
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldarg_1);
            il.Emit(OpCodes.Stfld, field);
            il.Emit(OpCodes.Ret);

            return method;
        }
        private static FieldBuilder BuildField(TypeBuilder builder, PropertyInfo property)
        {
            return builder.DefineField(
                "_" + property.Name,
                property.PropertyType,
                FieldAttributes.Private);
        }
        private CustomAttributeBuilder BuildAttribute(object customAttribute)
        {
            var customAttributeType = customAttribute.GetType();
            var constructor = customAttributeType
                .GetConstructors()
                .Select(c => new { Info = c, Parameters = c.GetParameters() })
                .OrderBy(c => c.Parameters.Length)
                .First();

            var constructorArguments = new object[constructor.Parameters.Length];
            for (int i = 0; i < constructor.Parameters.Length; i++)
            {
                var parameter = constructor.Parameters[i];
                object argument = null;
                var done = false;
                var propertyForParameter = customAttributeType.GetProperty(parameter.Name,
                    BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

                if (propertyForParameter != null)
                {
                    argument = propertyForParameter.GetValue(customAttribute);
                    if (parameter.GetType().IsInstanceOfType(argument))
                        done = true;
                }

                if (!done)
                {
                    var fieldForParameter = customAttributeType.GetField(parameter.Name,
                    BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

                    if (fieldForParameter != null)
                    {
                        argument = fieldForParameter.GetValue(customAttribute);
                        if (parameter.GetType().IsInstanceOfType(argument))
                            done = true;
                    }

                    if (!done)
                    {
                        if (parameter.ParameterType.IsValueType)
                        {
                            argument = Activator.CreateInstance(parameter.ParameterType);
                            done = true;
                        }
                        else
                        {
                            argument = null;
                            done = true;
                        }
                    }
                }

                constructorArguments[i] = argument;
            }

            var properties = new List<PropertyInfo>();
            var values = new List<object>();
            foreach (var property in customAttributeType.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.CanWrite))
            {
                var defaultAttr = property.GetCustomAttribute<DefaultValueAttribute>(true);
                var defaultValue = defaultAttr != null ? defaultAttr.Value : null;
                var value = property.GetValue(customAttribute);
                if (!Equals(value, defaultValue))
                {
                    properties.Add(property);
                    values.Add(value);
                }
            }

            var attrBuilder = new CustomAttributeBuilder(
                constructor.Info, 
                constructorArguments, 
                properties.ToArray(),
                values.ToArray());

            return attrBuilder;
        }


        private IEnumerable<PropertyInfo> GetAllProperties(Type interfaceType)
        {
            return GetAllProperties(interfaceType, new HashSet<string>());
        }
        private IEnumerable<PropertyInfo> GetAllProperties(Type interfaceType, HashSet<string> names)
        {
            foreach (var property in interfaceType.GetProperties())
            {
                if (names.Contains(property.Name)) continue;
                yield return property;
                names.Add(property.Name);
            }

            foreach (var @interface in interfaceType.GetInterfaces())
            {
                foreach (var property in GetAllProperties(@interface, names))
                {
                    yield return property;
                }
            }
        }
    }

    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class DaoInterfaceProxyForAttribute : Attribute
    {
        public DaoInterfaceProxyForAttribute(Type daoInterfaceType)
        {
            DaoInterfaceType = daoInterfaceType;
        }

        public Type DaoInterfaceType { get; private set; }  
    }

}
