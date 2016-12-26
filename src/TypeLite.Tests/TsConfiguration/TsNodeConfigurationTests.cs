using System;
using System.Collections.Generic;
using System.Text;
using TypeLite.TsConfiguration;
using Xunit;

namespace TypeLite.Tests.TsConfiguration {
    public class TsNodeConfigurationTests {
        [Fact]
        public void WhenMergeEmptyConfigurationCollection_NullIsReturned() {
            var merged = TsNodeConfiguration.Merge<TsEnumConfiguration>(new TsEnumConfiguration[] { });

            Assert.Null(merged);
        }

        [Fact]
        public void WhenMergeSingleConfiguration_PropertiesAreCopiedToResult() {
            var first = new TsEnumConfiguration() { Name = "EnumName", Module = "EnumModule" };

            var merged = TsNodeConfiguration.Merge<TsEnumConfiguration>(new[] { first });

            Assert.Equal(first.Name, merged.Name);
            Assert.Equal(first.Module, merged.Module);
        }

        [Fact]
        public void WhenMergeConfigurationsWithNullProperties_ResultIsNull() {
            var first = new TsEnumConfiguration();
            var second = new TsEnumConfiguration();

            var merged = TsNodeConfiguration.Merge<TsEnumConfiguration>(new[] { first, second });

            Assert.Null(merged.Name);
        }

        [Fact]
        public void WhenMergeConfigurationsWithDifferentValues_LaterValueIsUsed() {
            var first = new TsEnumConfiguration() { Name = "Name1"};
            var second = new TsEnumConfiguration() { Name = "Name2" };

            var merged = TsNodeConfiguration.Merge<TsEnumConfiguration>(new[] { first, second });

            Assert.Equal(second.Name, merged.Name);
        }

        [Fact]
        public void WhenMergeConfigurationsWithDifferentValues_LaterNullValueDoesntOverrideSpecifiedValue() {
            var first = new TsEnumConfiguration() { Name = "Name1" };
            var second = new TsEnumConfiguration() { Name = null };

            var merged = TsNodeConfiguration.Merge<TsEnumConfiguration>(new[] { first, second });

            Assert.Equal(first.Name, merged.Name);
        }
    }
}
