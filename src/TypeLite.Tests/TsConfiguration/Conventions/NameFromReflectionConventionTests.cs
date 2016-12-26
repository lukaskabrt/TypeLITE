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
    }
}
