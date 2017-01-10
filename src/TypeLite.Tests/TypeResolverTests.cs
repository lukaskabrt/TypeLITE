using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TypeLite.Tests.Models;
using TypeLite.Ts;
using TypeLite.TsConfiguration;
using Xunit;

namespace TypeLite.Tests {
    public class TypeResolverTests {
        TypeResolver _resolver;
        Mock<ITsConfigurationProvider> _configurationProviderMock;

        public TypeResolverTests() {
            _configurationProviderMock = new Mock<ITsConfigurationProvider>();
            _resolver = new TypeResolver(_configurationProviderMock.Object);
        }

        [Fact]
        public void WhenResolveTypeObject_AnyIsReturned() {
            var resolved = _resolver.ResolveType(typeof(object));

            Assert.Same(TsBasicType.Any, resolved);
        }

        #region System types

        [Theory]
        [InlineData(typeof(byte), "number")]
        [InlineData(typeof(sbyte), "number")]
        [InlineData(typeof(short), "number")]
        [InlineData(typeof(ushort), "number")]
        [InlineData(typeof(int), "number")]
        [InlineData(typeof(uint), "number")]
        [InlineData(typeof(long), "number")]
        [InlineData(typeof(ulong), "number")]
        [InlineData(typeof(float), "number")]
        [InlineData(typeof(double), "number")]
        [InlineData(typeof(decimal), "number")]
        [InlineData(typeof(string), "string")]
        [InlineData(typeof(char), "string")]
        [InlineData(typeof(bool), "boolean")]
        public void WhenResolveTypeSystemType_CorrectTypeIsReturned(Type type, string typeName) {
            var resolved = _resolver.ResolveType(type);

            var basicType = resolved as TsBasicType;
            Assert.Equal(typeName, basicType.TypeName);
            Assert.Equal(type, basicType.Context);
        }

        #endregion

        #region Nullable types

        [Fact]
        public void WhenResolveNullableType_UnderlayingTypeIsReturned() {
            var resolved = _resolver.ResolveType(typeof(int?));

            var basicType = resolved as TsBasicType;
            Assert.Equal("number", basicType.TypeName);
            Assert.Equal(typeof(int), basicType.Context);
        }

        #endregion

        #region Collections types

        [Theory]
        [InlineData(typeof(int[]), typeof(int))]
        [InlineData(typeof(List<int>), typeof(int))]
        [InlineData(typeof(IList<int>), typeof(int))]
        [InlineData(typeof(IEnumerable<int>), typeof(int))]
        public void WhenResolveTypedCollection_ItemTypeIsSetToTypeOfCollectionItem(Type collectionType, Type itemType) {
            var resolved = _resolver.ResolveType(collectionType) as TsCollectionType;

            Assert.NotNull(resolved);
            Assert.Equal(itemType, resolved.ItemType.Context);
            Assert.Equal(collectionType, resolved.Context);
        }

        [Fact]
        public void WhenResolveNestedTypedCollection_ItemTypeIsSetToTypeOfCollectionItem() {
            var collectionType = typeof(IEnumerable<IEnumerable<int>>);
            var itemType = typeof(IEnumerable<int>);

            var resolved = _resolver.ResolveType(collectionType) as TsCollectionType;
            var resolvedItemType = resolved.ItemType as TsCollectionType;

            Assert.NotNull(resolvedItemType);
            Assert.Equal(resolvedItemType.Context, itemType);
            Assert.Equal(resolvedItemType.ItemType.Context, typeof(int));
        }

        [Theory]
        [InlineData(typeof(System.Collections.IEnumerable))]
        [InlineData(typeof(Array))]
        public void WhenResolveNonGenericCollection_ItemTypeIsSetToAny(Type collectionType) {
            var resolved = _resolver.ResolveType(collectionType) as TsCollectionType;

            Assert.NotNull(resolved);
            Assert.Same(TsBasicType.Any, resolved.ItemType);
            Assert.Equal(collectionType, resolved.Context);
        }

        #endregion

        #region Class tests

        [Fact]
        public void WhenResolveClass_DataFromConfigurationProviderIsUsed() {
            this.ArrangeConfigurationForType(typeof(ClassWithoutAttribute));

            var resolved = _resolver.ResolveType(typeof(ClassWithoutAttribute)) as TsBasicType;

            Assert.Equal("ClassWithoutAttribute", resolved.TypeName);
            Assert.Equal("TypeLite.Tests.Models", resolved.Module);
        }

        #endregion

        #region Struct tests

        [Fact]
        public void WhenResolveStruct_DataFromConfigurationProviderIsUsed() {
            this.ArrangeConfigurationForType(typeof(StructWithoutAttribute));

            var resolved = _resolver.ResolveType(typeof(StructWithoutAttribute)) as TsBasicType;

            Assert.Equal("StructWithoutAttribute", resolved.TypeName);
            Assert.Equal("TypeLite.Tests.Models", resolved.Module);
        }

        #endregion

        #region Interface tests

        [Fact]
        public void WhenResolveInterface_DataFromConfigurationProviderIsUsed() {
            this.ArrangeConfigurationForType(typeof(IInterfaceWithoutAttribute));

            var resolved = _resolver.ResolveType(typeof(IInterfaceWithoutAttribute)) as TsBasicType;

            Assert.Equal("IInterfaceWithoutAttribute", resolved.TypeName);
            Assert.Equal("TypeLite.Tests.Models", resolved.Module);
        }

        #endregion

        #region Generic tests

        [Fact]
        public void WhenResolveGenericType_GenericArgumentsAreResolved() {
            this.ArrangeConfigurationForType(typeof(KeyValuePair<string, int>));

            var resolved = _resolver.ResolveType(typeof(KeyValuePair<string, int>)) as TsBasicType;

            Assert.Equal(typeof(string), resolved.GenericArguments[0].Context);
            Assert.Equal(typeof(int), resolved.GenericArguments[1].Context);
        }

        [Fact]
        public void WhenResolveOpenGenericType_GenericParametersAreResolved() {
            this.ArrangeConfigurationForType(typeof(GenericClass<>));

            var genericClassInfo = typeof(GenericClass<>).GetTypeInfo();
            this.ArrangeConfigurationForType(genericClassInfo.GenericTypeParameters[0]);

            var resolved = _resolver.ResolveType(typeof(GenericClass<>)) as TsBasicType;

            Assert.Equal(genericClassInfo.GenericTypeParameters[0], resolved.GenericArguments[0].Context);
        }

        [Fact]
        public void WhenResolveOpenGenericTypesWithSameParametrNames_GenericParametersAreResolvedToDifferentTypes() {
            this.ArrangeConfigurationForType(typeof(GenericClass<>));
            this.ArrangeConfigurationForType(typeof(GenericClassWithAttribute<>));

            var genericClassInfo = typeof(GenericClass<>).GetTypeInfo();
            this.ArrangeConfigurationForType(genericClassInfo.GenericTypeParameters[0]);

            var genericClassInfo2 = typeof(GenericClassWithAttribute<>).GetTypeInfo();
            this.ArrangeConfigurationForType(genericClassInfo2.GenericTypeParameters[0]);

            var resolved = _resolver.ResolveType(typeof(GenericClass<>)) as TsBasicType;
            var resolved2 = _resolver.ResolveType(typeof(GenericClassWithAttribute<>)) as TsBasicType;

            Assert.NotSame(resolved.GenericArguments[0], resolved2.GenericArguments[0]);
        }

        #endregion

        [Fact]
        public void WhenResolveClassMultipleTimes_SameTypeIsReturned() {
            this.ArrangeConfigurationForType(typeof(ClassWithoutAttribute));

            var resolved = _resolver.ResolveType(typeof(ClassWithoutAttribute)) as TsBasicType;
            var resolved2 = _resolver.ResolveType(typeof(ClassWithoutAttribute)) as TsBasicType;

            Assert.Same(resolved, resolved2);
        }

        private void ArrangeConfigurationForType(Type type) {
            var typeConfiguration = new TsModuleMemberConfiguration() { Name = type.Name, Module = "TypeLite.Tests.Models" };
            _configurationProviderMock
                .Setup(o => o.GetConfiguration(It.Is<Type>(t => t == type)))
                .Returns(typeConfiguration);
        }
    }
}
