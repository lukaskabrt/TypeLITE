using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        #region Generic tests

        [Fact]
        public void WhenResolveGenericClass_DataFromConfigurationProviderIsUsed() {
            this.ArrangeKeyValuePairConfiguration();

            var resolved = _resolver.ResolveType(typeof(KeyValuePair<string, int>)) as TsBasicType;

            Assert.Equal("KeyValuePair", resolved.TypeName);
            Assert.Equal("TypeLite.Tests", resolved.Module);
        }

        [Fact]
        public void WhenResolveGenericClass_GenericParametersAreResolved() {
            this.ArrangeKeyValuePairConfiguration();

            var resolved = _resolver.ResolveType(typeof(KeyValuePair<string, int>)) as TsBasicType;

            Assert.Equal(typeof(string), resolved.GenericArguments[0].Context);
            Assert.Equal(typeof(int), resolved.GenericArguments[1].Context);
        }

        private void ArrangeKeyValuePairConfiguration() {
            var typeConfiguration = new TsModuleMemberConfiguration() { Name = "KeyValuePair", Module = "TypeLite.Tests" };
            _configurationProviderMock
                .Setup(o => o.GetConfiguration(It.Is<Type>(t => t == typeof(KeyValuePair<string, int>))))
                .Returns(typeConfiguration);
        }

        #endregion
    }
}
