﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace SmartDose.Core
{
    public class ClassBuilderProperty
    {
        public string Name { get; set; }

        public Type Type { get; set; }

        object _Value;
        public object Value
        {
            get => _Value; set { _Value = value; InitializePropertyWithValue = true; }
        }

        internal bool InitializePropertyWithValue { get; set; } = false;
    }

    public class ClassBuilderProperty<T> : ClassBuilderProperty
    {
        public ClassBuilderProperty() : base()
        {
            Type = typeof(T);
            Value = default(T);
            InitializePropertyWithValue = false;
        }

        public ClassBuilderProperty(string name) : this()
        {
            Name = name;
        }

        public ClassBuilderProperty(string name, T value) : this()
        {
            Name = name;
            Value = value;
        }
        public new T Value { get => (T)base.Value; set => base.Value = value; }
    }

    public class ClassBuilderDefinition
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string ModuleName { get; set; } = $"MainModule";

        string _ClassName = null;
        public string ClassName { get => _ClassName ?? (_ClassName = $"Class_{Id}"); set => _ClassName = value; }

        string _AssemblyName = null;
        public string AssemblyName { get => _AssemblyName ?? (_AssemblyName = $"Assembly_{Id}"); set => _AssemblyName = value; }

        public List<ClassBuilderProperty> Properties { get; set; } = new List<ClassBuilderProperty>();

        public ClassBuilderDefinition AddProperty<T>(string Name)
        {
            Properties.Add(new ClassBuilderProperty<T>(Name));
            return this;
        }
        public ClassBuilderDefinition AddProperty<T>(string Name, T value)
        {
            Properties.Add(new ClassBuilderProperty<T>(Name, value));
            return this;
        }

        public void SetPropertiesValues(object valueObject)
        {
            if (valueObject is null)
                return;
            foreach (var property in Properties.Where(p => p.InitializePropertyWithValue))
                if (valueObject.GetType().GetProperty(property.Name) is PropertyInfo propertyInfo)
                    propertyInfo.SetValue(valueObject, property.Value);
        }
    }

    public static class ClassBuilder
    {
        public static Type NewType(ClassBuilderDefinition defintion)
        {
            var newTypeInfo = CompileResultTypeInfo(defintion);
            return newTypeInfo.AsType();
        }

        public static object NewObject(this ClassBuilderDefinition defintion)
        {
            var value = Activator.CreateInstance(NewType(defintion));
            defintion.SetPropertiesValues(value);
            return value;
        }

        public static TypeInfo CompileResultTypeInfo(ClassBuilderDefinition defintion)
        {
            var typeBuilder = GetTypeBuilder(defintion);
            var constructor = typeBuilder.DefineDefaultConstructor(MethodAttributes.Public
                | MethodAttributes.SpecialName
                | MethodAttributes.RTSpecialName);

            foreach (var property in defintion.Properties)
                CreateProperty(typeBuilder, property.Name, property.Type);

            return typeBuilder.CreateTypeInfo();
        }

        private static TypeBuilder GetTypeBuilder(ClassBuilderDefinition defintion)
        {
            var assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName(defintion.AssemblyName), AssemblyBuilderAccess.Run);
            var moduleBuilder = assemblyBuilder.DefineDynamicModule(defintion.ModuleName);
            return moduleBuilder.DefineType(defintion.ClassName,
                    TypeAttributes.Public |
                    TypeAttributes.Class |
                    TypeAttributes.AutoClass |
                    TypeAttributes.AnsiClass |
                    TypeAttributes.BeforeFieldInit |
                    TypeAttributes.AutoLayout,
                    null);
        }

        private static void CreateProperty(TypeBuilder typeBuilder, string propertyName, Type propertyType)
        {
            var fieldBuilder = typeBuilder.DefineField("_" + propertyName, propertyType, FieldAttributes.Private);

            var propertyBuilder = typeBuilder.DefineProperty(propertyName, PropertyAttributes.HasDefault, propertyType, null);
            var getPropertyMedthodBuilder = typeBuilder.DefineMethod("get_" + propertyName, MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig, propertyType, Type.EmptyTypes);
            var getIl = getPropertyMedthodBuilder.GetILGenerator();

            getIl.Emit(OpCodes.Ldarg_0);
            getIl.Emit(OpCodes.Ldfld, fieldBuilder);
            getIl.Emit(OpCodes.Ret);

            var setPropertyMethodBuilder =
                typeBuilder.DefineMethod("set_" + propertyName,
                  MethodAttributes.Public |
                  MethodAttributes.SpecialName |
                  MethodAttributes.HideBySig,
                  null, new[] { propertyType });

            var setIl = setPropertyMethodBuilder.GetILGenerator();
            var modifyProperty = setIl.DefineLabel();
            var exitSet = setIl.DefineLabel();

            setIl.MarkLabel(modifyProperty);
            setIl.Emit(OpCodes.Ldarg_0);
            setIl.Emit(OpCodes.Ldarg_1);
            setIl.Emit(OpCodes.Stfld, fieldBuilder);

            setIl.Emit(OpCodes.Nop);
            setIl.MarkLabel(exitSet);
            setIl.Emit(OpCodes.Ret);

            propertyBuilder.SetGetMethod(getPropertyMedthodBuilder);
            propertyBuilder.SetSetMethod(setPropertyMethodBuilder);
        }
    }
}
