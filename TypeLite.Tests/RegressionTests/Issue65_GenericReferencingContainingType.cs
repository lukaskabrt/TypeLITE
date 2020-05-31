using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TypeLite.Tests.RegressionTests {
    public class Issue65_GenericReferencingContainingType {
        [Fact]
        public void WhenClassReferencesItself_ClassIsGenerated() {
            var builder = new TsModelBuilder();
            builder.Add<GenericWithSelfReference>();

            var generator = new TsGenerator();
            var model = builder.Build();
            var result = generator.Generate(model);

            Assert.Contains("Children: TypeLite.Tests.RegressionTests.GenericWithSelfReference[]", result);
        }

    }

    public class GenericWithSelfReference {
        public List<GenericWithSelfReference> Children { get; set; }
    }
}
