using System;
using System.Collections.Generic;
using System.Text;
using TypeLite.Tests.Models;
using TypeLite.TsConfiguration.Conventions;
using Xunit;

namespace TypeLite.Tests.TsConfiguration.Conventions {
    public class ModuleFromReflectionConventionTests {
        ModuleFromReflectionConvention _convention = new ModuleFromReflectionConvention();

        [Fact]
        public void WhenApplyOnEnum_NamespaceOfEnumIsReturned() {
            var configuration = _convention.Apply(typeof(EnumWithAttribute));

            Assert.Equal("TypeLite.Tests.Models", configuration.Module);
        }
    }
}
