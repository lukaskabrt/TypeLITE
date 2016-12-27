using System;
using System.Collections.Generic;
using System.Text;
using TypeLite.Tests.Models;
using TypeLite.TsConfiguration;
using TypeLite.TsConfiguration.Conventions;
using Xunit;

namespace TypeLite.Tests.TsConfiguration {
    public class ConventionConfigurationProviderTests {
        [Fact]
        public void WhenInitialized_ConventionsCollectionIsEmpty() {
            var provider = new ConventionConfigurationProvider();

            Assert.Empty(provider.Conventions);
        }

        [Fact]
        public void WhenGetConfigurationAndConventionsCollectionIsEmpty_NullIsReturned() {
            var provider = new ConventionConfigurationProvider();

            var configuration = provider.GetConfiguration(typeof(EnumWithAttribute));

            Assert.Null(configuration);
        }

        [Fact]
        public void WhenGetConfigurationAndConventionsContainsSingleApplicabaleConvention_ResultOfConventionIsReturned() {
            var provider = new ConventionConfigurationProvider();
            provider.Conventions.Add(new NameFromReflectionConvention());

            var configuration = (TsModuleMemberConfiguration)provider.GetConfiguration(typeof(EnumWithAttribute));

            Assert.Equal(nameof(EnumWithAttribute), configuration.Name);
            Assert.Null(configuration.Module);
        }

        [Fact]
        public void WhenGetConfigurationAndConventionsContainsNonConflictiongConventions_ResultsOfConventionsAreMerged() {
            var provider = new ConventionConfigurationProvider();
            provider.Conventions.Add(new NameFromReflectionConvention());
            provider.Conventions.Add(new ModuleFromReflectionConvention());

            var configuration = (TsModuleMemberConfiguration)provider.GetConfiguration(typeof(EnumWithAttribute));

            Assert.Equal(nameof(EnumWithAttribute), configuration.Name);
            Assert.Equal("TypeLite.Tests.Models", configuration.Module);
        }

        [Fact]
        public void WhenGetConfigurationAndConventionsContainsConflictiongConventions_ResultOfConventionWithHigherPriorityIsUsed() {
            var provider = new ConventionConfigurationProvider();
            provider.Conventions.Add(new NameFromReflectionConvention());
            provider.Conventions.Add(new TestNameConvention());

            var configuration = (TsModuleMemberConfiguration)provider.GetConfiguration(typeof(EnumWithAttribute));

            Assert.Equal("Test", configuration.Name);
        }

        private class TestNameConvention : IModuleMemberConvention {
            public TsModuleMemberConfiguration Apply(Type t) {
                return new TsModuleMemberConfiguration() { Name = "Test" };
            }
        }
    }
}
