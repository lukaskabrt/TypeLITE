using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using TypeLite.Tests.Models;
using TypeLite.TsConfiguration.Conventions;
using Xunit;

namespace TypeLite.Tests.TsConfiguration.Conventions {
    public class EnumValueNameFromReflectionConventionTests {
        EnumValueNameFromReflectionConvention _convention = new EnumValueNameFromReflectionConvention();

        [Fact]
        public void WhenApplyOnEnum_NameOfEnumValueIsReturned() {
            var enumValueInfo = typeof(EnumWithAttribute).GetTypeInfo().GetField(nameof(EnumWithAttribute.EnumValue1));

            var configuration = _convention.Apply(enumValueInfo);

            Assert.Equal(nameof(EnumWithAttribute.EnumValue1), configuration.Name);
        }
    }
}
