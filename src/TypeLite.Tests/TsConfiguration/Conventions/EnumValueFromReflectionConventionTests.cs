using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using TypeLite.Tests.Models;
using TypeLite.TsConfiguration.Conventions;
using Xunit;

namespace TypeLite.Tests.TsConfiguration.Conventions {
    public class EnumValueFromReflectionConventionTests {
        EnumValueFromReflectionConvention _convention = new EnumValueFromReflectionConvention();

        [Fact]
        public void WhenApplyOnByteEnum_ValueOfEnumValueIsReturned() {
            var enumValueInfo = typeof(ByteEnum).GetTypeInfo().GetField(nameof(ByteEnum.Value));

            var configuration = _convention.Apply(enumValueInfo);

            Assert.Equal(((byte)(ByteEnum.Value)).ToString(), configuration.Value);
        }

        [Fact]
        public void WhenApplyOnSByteEnum_ValueOfEnumValueIsReturned() {
            var enumValueInfo = typeof(SByteEnum).GetTypeInfo().GetField(nameof(SByteEnum.Value));

            var configuration = _convention.Apply(enumValueInfo);

            Assert.Equal(((sbyte)(SByteEnum.Value)).ToString(), configuration.Value);
        }

        [Fact]
        public void WhenApplyOnShortEnum_ValueOfEnumValueIsReturned() {
            var enumValueInfo = typeof(ShortEnum).GetTypeInfo().GetField(nameof(ShortEnum.Value));

            var configuration = _convention.Apply(enumValueInfo);

            Assert.Equal(((short)(ShortEnum.Value)).ToString(), configuration.Value);
        }

        [Fact]
        public void WhenApplyOnUShortEnum_ValueOfEnumValueIsReturned() {
            var enumValueInfo = typeof(UShortEnum).GetTypeInfo().GetField(nameof(UShortEnum.Value));

            var configuration = _convention.Apply(enumValueInfo);

            Assert.Equal(((ushort)(UShortEnum.Value)).ToString(), configuration.Value);
        }

        [Fact]
        public void WhenApplyOnIntEnum_ValueOfEnumValueIsReturned() {
            var enumValueInfo = typeof(IntEnum).GetTypeInfo().GetField(nameof(IntEnum.Value));

            var configuration = _convention.Apply(enumValueInfo);

            Assert.Equal(((int)(IntEnum.Value)).ToString(), configuration.Value);
        }

        [Fact]
        public void WhenApplyOnUIntEnum_ValueOfEnumValueIsReturned() {
            var enumValueInfo = typeof(UIntEnum).GetTypeInfo().GetField(nameof(UIntEnum.Value));

            var configuration = _convention.Apply(enumValueInfo);

            Assert.Equal(((uint)(UIntEnum.Value)).ToString(), configuration.Value);
        }

        [Fact]
        public void WhenApplyOnLongEnum_ValueOfEnumValueIsReturned() {
            var enumValueInfo = typeof(LongEnum).GetTypeInfo().GetField(nameof(LongEnum.Value));

            var configuration = _convention.Apply(enumValueInfo);

            Assert.Equal(((long)(LongEnum.Value)).ToString(), configuration.Value);
        }

        [Fact]
        public void WhenApplyOnULongEnum_ValueOfEnumValueIsReturned() {
            var enumValueInfo = typeof(ULongEnum).GetTypeInfo().GetField(nameof(ULongEnum.Value));

            var configuration = _convention.Apply(enumValueInfo);

            Assert.Equal(((ulong)(ULongEnum.Value)).ToString(), configuration.Value);
        }

        #region Enums

        private enum ByteEnum : byte {
            Value = byte.MinValue
        }

        private enum SByteEnum : sbyte {
            Value = sbyte.MaxValue
        }

        private enum ShortEnum : short {
            Value = short.MinValue
        }

        private enum UShortEnum : ushort {
            Value = ushort.MaxValue
        }

        private enum IntEnum : int {
            Value = int.MinValue
        }

        private enum UIntEnum : uint {
            Value = uint.MaxValue
        }

        private enum LongEnum : long {
            Value = long.MinValue
        }

        private enum ULongEnum : ulong {
            Value = long.MaxValue
        }

        #endregion
    }
}
