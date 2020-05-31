using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;

namespace TypeLite.Tests {
    public class GenericsTests {
        private string AddTypeAndGenerateTypeScript<TType>(Action<TsModel> modelAsserts = null) {
            var builder = new TsModelBuilder();
            builder.Add<TType>();
            var model = builder.Build();
            if (modelAsserts != null) {
                modelAsserts(model);
            }

            var typeScript = new TsGenerator().Generate(model);
            Console.WriteLine(typeScript);
            return typeScript;
        }

        [Fact]
        public void CanGenerateSpecificTypesForGenericProperties() {
            var typeScript = AddTypeAndGenerateTypeScript<GenerateSpecifyGenericTypesTestClass>(
                model => {
                    var kvpClass = model.Classes.Single(c => !c.IsIgnored && c.Name == "KeyValuePair");
                    Assert.NotNull(kvpClass);
                    Assert.Equal("TKey", kvpClass.Properties.Single(p => p.Name == "Key").PropertyType.Type.Name);
                    Assert.Equal("TValue", kvpClass.Properties.Single(p => p.Name == "Value").PropertyType.Type.Name);

                    var classDefinition = model.Classes.Single(c => c.Name == typeof(GenerateSpecifyGenericTypesTestClass).Name);
                    Assert.NotNull(classDefinition);
                    Assert.True(((PropertyInfo)classDefinition.Properties.Single(p => p.Name == "StringToInt").MemberInfo).PropertyType == typeof(KeyValuePair<string, int>));
                });
            Assert.Contains("StringToInt: System.Collections.Generic.KeyValuePair<string, number>;", typeScript);
        }

        [Fact]
        public void CanGenerateSpecificTypesForCollectionsOfGenericClasses() {
            var typeScript = AddTypeAndGenerateTypeScript<GenerateSpecificCollectionsOfGenericTypesTestClass>();
            Assert.Contains("ListOfIntToString: System.Collections.Generic.KeyValuePair<number, string>[];", typeScript);
        }


        [Fact]
        public void CanGenerateNestedGenericPropertiesForSystemTypes() {
            var typeScript = AddTypeAndGenerateTypeScript<HandleNestedGenericsSystemTypesTestClass>();
            Assert.Contains("NestedStringList: string[][]", typeScript);
        }


        [Fact]
        public void CanGenerateNestedGenericPropertiesForCustomTypes() {
            var typeScript = AddTypeAndGenerateTypeScript<HandleNestedGenericsCollectionCustomTypesTestClass>();
            Assert.Contains("NestedCustomClassList: TypeLite.Tests.GenericsTests.GenerateSpecifyGenericTypesTestClass[][][][];", typeScript);
        }

        [Fact]
        public void KeyValuePairWithSpecificArguments() {
            var typeScript = AddTypeAndGenerateTypeScript<ClassWithSpecificKeyValuePairArguments>();
            Assert.Contains("KeyValuePair: System.Collections.Generic.KeyValuePair<number, string[]>;", typeScript);
        }

        [Fact]
        public void CanHandleGenericArgsInBaseClass() {
            var typeScript = AddTypeAndGenerateTypeScript<DerivedGenericClass>();
            Assert.Contains("SomeGenericProperty: TType;", typeScript);
            Assert.Contains("SomeGenericArrayProperty: TType[];", typeScript);
            Assert.Contains("interface DerivedGenericClass extends TypeLite.Tests.GenericsTests.BaseGeneric<string> {", typeScript);
        }

        [Fact]
        public void GenericParameterTypeIsFullyQualified() {
            var typeScript = AddTypeAndGenerateTypeScript<DerivedGenericClassWithArgInDifferentNamespace>();
            Assert.Contains("interface DerivedGenericClassWithArgInDifferentNamespace extends TypeLite.Tests.GenericsTests.BaseGeneric<DummyNamespace.Test>", typeScript);
        }

        [Fact]
        public void GeneratesComplicatedNestedGenericProperties() {
            var typeScript = AddTypeAndGenerateTypeScript<ClassWithComplexNestedGenericProperty>();
            Assert.Contains("GenericsHell: System.Tuple<System.Collections.Generic.KeyValuePair<number, string>, TypeLite.Tests.GenericsTests.BaseGeneric<string>, number, System.Collections.Generic.KeyValuePair<number, DummyNamespace.Test>>;", typeScript);
        }

        [Fact]
        public void DeepGenericClassInheritenceTreeIsGeneratedCorrectly() {
            var typeScript = AddTypeAndGenerateTypeScript<DerivedGenericTwoLevelsDeep>();
            Assert.Contains("interface DerivedGenericTwoLevelsDeep extends TypeLite.Tests.GenericsTests.DerivedGenericWithNewTypeArgument<string, DummyNamespace.Test> {", typeScript);
            Assert.Contains("interface DerivedGenericWithNewTypeArgument<TNewType, TType> extends TypeLite.Tests.GenericsTests.BaseGeneric<TType> {", typeScript);
            Assert.Contains("NewGenericProperty: TNewType;", typeScript);
        }

        [Fact]
        public void DeepGenericInterfaceInheritenceTreeIsGeneratedCorrectly() {
            var typeScript = AddTypeAndGenerateTypeScript<IDerivedGenericTwoLevelsDeep>();
            Assert.Contains("interface IDerivedGenericTwoLevelsDeep extends TypeLite.Tests.GenericsTests.IDerivedGenericWithNewTypeArgument<string, DummyNamespace.Test> {", typeScript);
            Assert.Contains("interface IDerivedGenericWithNewTypeArgument<TNewType, TType> extends TypeLite.Tests.GenericsTests.IBaseGeneric<TType> {", typeScript);
            Assert.Contains("NewGenericProperty: TNewType;", typeScript);
        }

        [Fact]
        public void DerivedClassWithNewNameForGenericParameter() {
            var typescript = AddTypeAndGenerateTypeScript<DerivedClassWithNameGenericParameterName<int, string>>();

            Assert.Contains("DerivedClassWithNameGenericParameterName<TNewParam, TSomethingNew> extends TypeLite.Tests.GenericsTests.BaseGeneric<TSomethingNew>", typescript);
            Assert.Contains("interface BaseGeneric<TType>", typescript);
        }

        #region Test classes
        private class HandleNestedGenericsSystemTypesTestClass {
            public List<List<string>> NestedStringList { get; set; }
        }

        private class HandleNestedGenericsCollectionCustomTypesTestClass {
            public List<List<List<List<GenerateSpecifyGenericTypesTestClass>>>> NestedCustomClassList { get; set; }
        }

        private class GenerateSpecificCollectionsOfGenericTypesTestClass {
            public List<KeyValuePair<int, string>> ListOfIntToString { get; set; }
        }

        private class GenerateSpecifyGenericTypesTestClass {
            public KeyValuePair<string, int> StringToInt { get; set; }
        }

        private class ClassWithSpecificKeyValuePairArguments {
            public KeyValuePair<int, List<string>> KeyValuePair { get; set; }
        }

        internal class BaseGeneric<TType> {
            public TType SomeGenericProperty { get; set; }
            public TType[] SomeGenericArrayProperty { get; set; }
        }

        private class DerivedGenericClass : BaseGeneric<string> {
        }

        private class DerivedGenericWithNewTypeArgument<TNewType, TType> : BaseGeneric<TType> {
            public TNewType NewGenericProperty { get; set; }
        }

        private class DerivedGenericTwoLevelsDeep : DerivedGenericWithNewTypeArgument<string, DummyNamespace.Test> {
            public string NonGenericProperty { get; set; }
        }

        [TsInterface]
        internal interface IBaseGeneric<TType> {
            TType SomeGenericProperty { get; set; }
            TType[] SomeGenericArrayProperty { get; set; }
        }

        [TsInterface]
        private interface IDerivedGenericWithNewTypeArgument<TNewType, TType> : IBaseGeneric<TType> {
            TNewType NewGenericProperty { get; set; }
        }

        [TsInterface]
        private interface IDerivedGenericTwoLevelsDeep : IDerivedGenericWithNewTypeArgument<string, DummyNamespace.Test> {

        }

        private class DerivedGenericClassWithArgInDifferentNamespace : BaseGeneric<DummyNamespace.Test> {
        }

        private class ClassWithComplexNestedGenericProperty {
            public Tuple<KeyValuePair<int, string>, BaseGeneric<string>, decimal, KeyValuePair<int, DummyNamespace.Test>> GenericsHell { get; set; }
        }

        private class DerivedClassWithNameGenericParameterName<TNewParam, TSomethingNew> : BaseGeneric<TSomethingNew> {
            public TNewParam SomeProperty { get; set; }
        }

        #endregion
    }
}

namespace DummyNamespace {
    public class Test {
    }
}