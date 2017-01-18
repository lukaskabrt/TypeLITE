using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TypeLite.Tests.Models;
using TypeLite.Ts;
using Xunit;

namespace TypeLite.Tests.Ts {
    public class TsPropertyTests : TsTests {
        [Fact]
        public void WhenCreateFromPropertyInfo_NameIsSetToValueProvidedByConfigurationProvider() {
            this.SetupTypeResolverFor<ClassWithProperty>();
            this.SetupTypeResolverFor<int>();
            var propertyConfiguration = this.SetupConfigurationForMember<ClassWithProperty>("Property");
            var propertyInfo = typeof(ClassWithProperty).GetTypeInfo().DeclaredProperties.Where(o => o.Name == "Property").Single();

            var property = TsProperty.CreateFrom(propertyInfo, _typeResolverMock.Object, _configurationProviderMock.Object);

            Assert.Equal(propertyConfiguration.Name, property.Name);
        }

        [Fact]
        public void WhenCreateFromPropertyInfo_TypeIsSetToValueProvidedByTypeResolver() {
            this.SetupTypeResolverFor<ClassWithProperty>();
            var type = this.SetupTypeResolverFor<int>();
            var propertyConfiguration = this.SetupConfigurationForMember<ClassWithProperty>("Property");
            var propertyInfo = typeof(ClassWithProperty).GetTypeInfo().DeclaredProperties.Where(o => o.Name == "Property").Single();

            var property = TsProperty.CreateFrom(propertyInfo, _typeResolverMock.Object, _configurationProviderMock.Object);

            Assert.Same(type, property.Type);
        }
    }
}
