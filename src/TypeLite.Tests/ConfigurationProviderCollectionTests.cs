using System;
using System.Collections.Generic;
using System.Text;
using TypeLite.Tests.Models;
using TypeLite.TsConfiguration;
using TypeLite.TsConfiguration.Conventions;
using Xunit;

namespace TypeLite.Tests {
    public class ConfigurationProviderCollectionTests {
        ConfigurationProviderCollection _collection;

        public ConfigurationProviderCollectionTests() {
            var conventionProvider = new ConventionConfigurationProvider();
            conventionProvider.Conventions.Add(new NameFromReflectionConvention());
            conventionProvider.Conventions.Add(new ModuleFromReflectionConvention());

            _collection = new ConfigurationProviderCollection(new ITsConfigurationProvider[] {
                conventionProvider,
                new AttributeConfigurationProvider()
            });
        }

        [Fact]
        public void WhenInitialized_ProvidersCollectionIsEmpty() {
            var collection = new ConfigurationProviderCollection();

            Assert.Empty(collection.Providers);
        }

        [Fact]
        public void WhenInitializedWithProviderCollection_ProvidersCollectionContainsProviders() {
            Assert.IsType<ConventionConfigurationProvider>(_collection.Providers[0]);
            Assert.IsType<AttributeConfigurationProvider>(_collection.Providers[1]);
        }

        [Fact]
        public void WhenGetConfigurationAndProvidersCollectionIsEmpty_NullIsReturned() {
            var collection = new ConfigurationProviderCollection();

            var configuration = collection.GetConfiguration(typeof(EnumWithAttribute));

            Assert.Null(configuration);
        }

        [Fact]
        public void WhenGetConfiguration_ResultsOfProvidersAreCombined() {
            var configuration = _collection.GetConfiguration(typeof(EnumWithNameAttribute)) as TsModuleMemberConfiguration;

            Assert.Equal("EnumWithCustomName", configuration.Name);
            Assert.Equal("TypeLite.Tests.Models", configuration.Module);
        }
    }
}
