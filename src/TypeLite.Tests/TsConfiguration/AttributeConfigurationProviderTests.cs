using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using TypeLite.Tests.Models;
using TypeLite.TsConfiguration;
using Xunit;

namespace TypeLite.Tests.TsConfiguration {
    public class AttributeConfigurationProviderTests {
        AttributeConfigurationProvider _provider = new AttributeConfigurationProvider();

        #region Enum

        [Fact]
        public void WhenReadOnEnumWithAttribute_EnumConfigurationIsReturned() {
            var configuration = _provider.GetConfiguration(typeof(EnumWithAttribute));

            Assert.NotNull(configuration);
            Assert.IsType<TsEnumConfiguration>(configuration);

            var enumConfiguration = (TsEnumConfiguration)configuration;
            Assert.Equal("EnumWithAttributeName", enumConfiguration.Name);
            Assert.Equal("EnumWithAttributeModule", enumConfiguration.Module);
        }

        [Fact]
        public void WhenReadOnEnumWithoutAttribute_NullIsReturned() {
            var configuration = _provider.GetConfiguration(typeof(EnumWithoutAttribute));

            Assert.Null(configuration);
        }

        #endregion

        #region EnumValue

        [Fact]
        public void WhenReadOnEnumValueWithAttribute_EnumValueConfigurationIsReturned() {
            var enumTypeInfo = typeof(EnumWithAttribute).GetTypeInfo();
            var enumValueField = enumTypeInfo.GetField(nameof(EnumWithAttribute.EnumValueWithAttribute));

            var configuration = _provider.GetEnumValueConfiguration(enumValueField);

            Assert.NotNull(configuration);
            Assert.IsType<TsEnumValueConfiguration>(configuration);

            var enumConfiguration = (TsEnumValueConfiguration)configuration;
            Assert.Equal("EnumValueWithAttributeName", enumConfiguration.Name);
            Assert.Equal("100", enumConfiguration.Value);
        }

        [Fact]
        public void WhenReadOnEnumValueWithoutAttribute_NullIsReturned() {
            var enumTypeInfo = typeof(EnumWithAttribute).GetTypeInfo();
            var enumValueField = enumTypeInfo.GetField(nameof(EnumWithAttribute.EnumValue1));

            var configuration = _provider.GetEnumValueConfiguration(enumValueField);


            Assert.Null(configuration);
        }


        #endregion
    }
}
