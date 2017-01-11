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

namespace TypeLite.Tests.Ts {
    public class TsEnumTests {
        private Mock<ITsConfigurationProvider> _configurationProviderMock;
        private Mock<TypeResolver> _typeResolverMock;

        public TsEnumTests() {
            _configurationProviderMock = new Mock<ITsConfigurationProvider>();
            _typeResolverMock = new Mock<TypeResolver>();
        }

        [Fact]
        public void WhenInitialized_ValuesIsEmptyCollection() {
            var typeName = new TsBasicType() { Context = typeof(EnumWithoutAttribute), TypeName = "EnumWithoutAttribute" };
            var sut = new TsEnum(typeName);

            Assert.Empty(sut.Values);
        }

        [Fact]
        public void WhenInitialized_NameIsSet() {
            var typeName = new TsBasicType() { Context = typeof(EnumWithoutAttribute), TypeName = "EnumWithoutAttribute" };
            var sut = new TsEnum(typeName);

            Assert.Same(typeName, sut.Name);
        }

        [Fact]
        public void WhenCreateFromEnum_NameIsSetToTypeNameReturnedByTypeResolver() {
            var enumType = typeof(EnumWithoutAttribute);
            var enumTypeInfo = enumType.GetTypeInfo();
            var enumValueFieldInfo = enumTypeInfo.GetField("EnumValue1");

            _configurationProviderMock
                .Setup(o => o.GetEnumValueConfiguration(It.Is<FieldInfo>(f => f == enumValueFieldInfo)))
                .Returns(new TsEnumValueConfiguration() { Name = "EnumValue1", Value = "1" });

            var enumResolvedType = new TsBasicType() { Context = typeof(EnumWithoutAttribute), TypeName = "EnumWithoutAttribute" };
            _typeResolverMock
                .Setup(o => o.ResolveType(It.Is<Type>(t => t == typeof(EnumWithoutAttribute))))
                .Returns(enumResolvedType);

            var @enum = TsEnum.CreateFrom<EnumWithoutAttribute>(_typeResolverMock.Object, _configurationProviderMock.Object);

            Assert.Same(enumResolvedType, @enum.Name);
        }

        [Fact]
        public void WhenCreateFromEnum_EnumValueIsAddedWithInformationFromConfigurationProvider() {
            var enumType = typeof(EnumWithoutAttribute);
            var enumTypeInfo = enumType.GetTypeInfo();
            var enumValueFieldInfo = enumTypeInfo.GetField("EnumValue1");

            _configurationProviderMock
                .Setup(o => o.GetEnumValueConfiguration(It.Is<FieldInfo>(f => f == enumValueFieldInfo)))
                .Returns(new TsEnumValueConfiguration() { Name = "EnumValue1", Value = "1" });

            var enumResolvedType = new TsBasicType() { Context = typeof(EnumWithoutAttribute), TypeName = "EnumWithoutAttribute" };
            _typeResolverMock
                .Setup(o => o.ResolveType(It.Is<Type>(t => t == typeof(EnumWithoutAttribute))))
                .Returns(enumResolvedType);

            var @enum = TsEnum.CreateFrom<EnumWithoutAttribute>(_typeResolverMock.Object, _configurationProviderMock.Object);
            var enumValue = @enum.Values.Single();

            Assert.Equal("EnumValue1", enumValue.Name);
            Assert.Equal("1", enumValue.Value);
        }
    }
}
