using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TypeLite.Tests.RegressionTests {
    public class Issue52_EnumsWithoutClass {

        [Fact]
        public void WhenAssemblyContainsEnumWithoutClass_EnumIsGeneratedInTheOutput() {
            var builder = new TsModelBuilder();
            builder.Add(typeof(TypeLite.Tests.AssemblyWithEnum.TestEnum).Assembly);

            var generator = new TsGenerator();
            var model = builder.Build();
            var result = generator.Generate(model, TsGeneratorOutput.Enums);

            Assert.Contains("TestEnum", result);
        }

        enum MyTestEnum {
            One,
            Two,
            Three
        }
    }
}
