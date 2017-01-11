using Moq;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using TypeLite.Tests.Models;
using TypeLite.Ts;
using TypeLite.TsConfiguration;
using Xunit;

namespace TypeLite.Tests.Ts {
    public class TsEnumValueTests {
        private Mock<ITsConfigurationProvider> _configurationProviderMock;

        public TsEnumValueTests() {
            _configurationProviderMock = new Mock<ITsConfigurationProvider>();
        }

        [Fact]
        public void WhenCreateFromFieldInfo_NameAndValueIsSetToValuesProvidedByConfigurationProvider() {
            var enumType = typeof(EnumWithoutAttribute);
            var enumTypeInfo = enumType.GetTypeInfo();
            var enumValueFieldInfo = enumTypeInfo.GetField("EnumValue1");

            _configurationProviderMock
                .Setup(o => o.GetEnumValueConfiguration(It.Is<FieldInfo>(f => f == enumValueFieldInfo)))
                .Returns(new TsEnumValueConfiguration() { Name = "EnumValue1", Value = "1" });

            var enumValue = TsEnumValue.CreateFrom(enumValueFieldInfo, _configurationProviderMock.Object);

            Assert.Equal("EnumValue1", enumValue.Name);
            Assert.Equal("1", enumValue.Value);
        }
    }
}
