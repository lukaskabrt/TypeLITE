using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using TypeLite.TsConfiguration.Conventions;
using Xunit;

namespace TypeLite.Tests.TsConfiguration.Conventions {
    public class MemberNameFromReflectionConventionTests {
        MemberNameFromReflectionConvention _convention = new MemberNameFromReflectionConvention();

        [Fact]
        public void WhenApplyOnProperty_NameOfPropertyIsReturned() {
            var type = typeof(ClassWithMembers);
            var typeInfo = type.GetTypeInfo();
            var propertyInfo = typeInfo.GetDeclaredProperty("IntProperty");

            var configuration = _convention.Apply(propertyInfo);

            Assert.Equal("IntProperty", configuration.Name);
        }

        class ClassWithMembers {
            public int IntProperty { get; set; }
        }
    }
}
