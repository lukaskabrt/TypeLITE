using System;
using System.Collections.Generic;
using System.Text;
using TypeLite.Tests.Models;
using TypeLite.TsConfiguration.Conventions;
using Xunit;

namespace TypeLite.Tests.TsConfiguration.Conventions {
    public class NameFromReflectionConventionTests {
        NameFromReflectionConvention _convention = new NameFromReflectionConvention();

        [Fact]
        public void WhenApplyOnEnum_NameOfEnumIsReturned() {
            var configuration = _convention.Apply(typeof(EnumWithAttribute));

            Assert.Equal(nameof(EnumWithAttribute), configuration.Name);
        }

        [Fact]
        public void WhenApplyOnGenericClass_SimpleNameOfClassIsReturned() {
            var configuration = _convention.Apply(typeof(KeyValuePair<string, int>));

            Assert.Equal(nameof(KeyValuePair<string, int>), configuration.Name);
        }
    }
}
