using System;
using System.Collections.Generic;
using System.Text;
using TypeLite.TsConfiguration;
using Xunit;

namespace TypeLite.Tests.TsConfiguration {
    public class TsNodeConfigurationTests {
        [Fact]
        public void WhenMergeEmptyConfigurationCollection_NullIsReturned() {
            var merged = TsNodeConfiguration.Merge<TsModuleMemberConfiguration>(new TsModuleMemberConfiguration[] { });

            Assert.Null(merged);
        }

        [Fact]
        public void WhenMergeSingleConfiguration_PropertiesAreCopiedToResult() {
            var first = new TsModuleMemberConfiguration() { Name = "EnumName", Module = "EnumModule" };

            var merged = TsNodeConfiguration.Merge<TsModuleMemberConfiguration>(new[] { first });

            Assert.Equal(first.Name, merged.Name);
            Assert.Equal(first.Module, merged.Module);
        }

        [Fact]
        public void WhenMergeConfigurationsWithNullProperties_ResultIsNull() {
            var first = new TsModuleMemberConfiguration();
            var second = new TsModuleMemberConfiguration();

            var merged = TsNodeConfiguration.Merge<TsModuleMemberConfiguration>(new[] { first, second });

            Assert.Null(merged.Name);
        }

        [Fact]
        public void WhenMergeConfigurationsWithDifferentValues_LaterValueIsUsed() {
            var first = new TsModuleMemberConfiguration() { Name = "Name1"};
            var second = new TsModuleMemberConfiguration() { Name = "Name2" };

            var merged = TsNodeConfiguration.Merge<TsModuleMemberConfiguration>(new[] { first, second });

            Assert.Equal(second.Name, merged.Name);
        }

        [Fact]
        public void WhenMergeConfigurationsWithDifferentValues_LaterNullValueDoesntOverrideSpecifiedValue() {
            var first = new TsModuleMemberConfiguration() { Name = "Name1" };
            var second = new TsModuleMemberConfiguration() { Name = null };

            var merged = TsNodeConfiguration.Merge<TsModuleMemberConfiguration>(new[] { first, second });

            Assert.Equal(first.Name, merged.Name);
        }
    }
}
