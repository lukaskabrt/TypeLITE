using System;
using System.Collections.Generic;
using System.Text;
using TypeLite.Tests.Models;
using Xunit;

namespace TypeLite.Tests {
    public class TypeExtensionsTests {
        #region IsNullable tests

        [Fact]
        public void WhenIsNullableIsCalledWithNullableType_TrueIsReturned() {
            var type = typeof(int?);

            Assert.True(type.IsNullable());
        }

        [Fact]
        public void WhenIsNullableIsCalledWithValueType_FalseIsReturned() {
            var type = typeof(int);

            Assert.False(type.IsNullable());
        }

        [Fact]
        public void WhenIsNullableIsCalledWithReferenceType_FalseIsReturned() {
            var type = typeof(string);

            Assert.False(type.IsNullable());
        }

        #endregion

        #region GetNullableValueType tests

        [Fact]
        public void WhenGetNullableValueTypeWithNullableValueType_ValueTypeIsReturned() {
            var type = typeof(int?);

            Assert.Equal(typeof(int), type.GetNullableValueType());
        }

        [Fact]
        public void WhenGetNullableValueTypeWithNonNullableType_ExceptionIsThrown() {
            var type = typeof(string);

            Assert.Throws<InvalidOperationException>(() => type.GetNullableValueType());
        }

        #endregion

        #region IsCollection tests

        [Theory]
        [InlineData(typeof(System.Collections.IEnumerable))]
        [InlineData(typeof(Array))]
        public void WhenIsEnumerableForNonGenericEnumerable_TrueIsReturned(Type type) {
            var result = type.IsCollection();

            Assert.True(result);
        }

        [Theory]
        [InlineData(typeof(IEnumerable<int>))]
        [InlineData(typeof(int[]))]
        [InlineData(typeof(List<int>))]
        [InlineData(typeof(List<ClassWithoutAttribute>))]
        public void WhenIsEnumerableForGenericEnumerable_TrueIsReturned(Type type) {
            var result = type.IsCollection();

            Assert.True(result);
        }

        [Theory]
        [InlineData(typeof(ClassWithoutAttribute))]
        [InlineData(typeof(int))]
        [InlineData(typeof(IDisposable))]
        public void WhenIsEnumerableForNonEnumerable_FalseIsReturned(Type type) {
            var result = type.IsCollection();

            Assert.False(result);
        }

        #endregion

        #region GetCollectionItemType tests

        [Theory]
        [InlineData(typeof(int[]), typeof(int))]
        [InlineData(typeof(List<int>), typeof(int))]
        [InlineData(typeof(IList<int>), typeof(int))]
        [InlineData(typeof(IEnumerable<int>), typeof(int))]
        public void WhenGetCollectionItemTypeForGenericCollection_ItemTypeIsSetToTypeOfCollectionItem(Type collectionType, Type expectedItemType) {
            var itemType = collectionType.GetCollectionItemType();

            Assert.Equal(expectedItemType, itemType);
        }

        [Theory]
        [InlineData(typeof(System.Collections.IEnumerable))]
        [InlineData(typeof(Array))]
        public void WhenGetCollectionItemTypeForNonGenericCollection_NullIsReturned(Type collectionType) {
            var itemType = collectionType.GetCollectionItemType();

            Assert.Null(itemType);
        }

        #endregion
    }
}
