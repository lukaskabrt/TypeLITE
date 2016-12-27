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

        [Fact]
        public void WhenApplyOnEnumDefinedInsideClass_NamespaceAndClassNameIsReturned() {
            var configuration = _convention.Apply(typeof(EnclosingClass.EnumInsideClass));
            Assert.Equal("TypeLite.Tests.Models.EnclosingClass", configuration.Module);
        }

        [Fact]
        public void WhenApplyOnEnumDefinedInsideRecrusivellyNestedClass_NamespaceAndClassesNameIsReturned() {
            var configuration = _convention.Apply(typeof(EnclosingClass.EnclosingClassInEnclosingClass.EnumInsideClassInsideClass));
            Assert.Equal("TypeLite.Tests.Models.EnclosingClass.EnclosingClassInEnclosingClass", configuration.Module);
        }
    }
}
