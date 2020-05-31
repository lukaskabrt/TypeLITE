using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeLite.Tests.TestModels;
using Xunit;

namespace TypeLite.Tests.RegressionTests {
    public class Issue79_ModuleNameFormatterIsnNotUsed {
        [Fact]
        public void WhenScriptGeneratorGenerateIsCalledModuleNameFormatterIsUsed() {

            var ts = TypeScript.Definitions();
            ts.WithModuleNameFormatter(m => "XXX");
            ts.ModelBuilder.Add<Product>();

            var model = ts.ModelBuilder.Build();
            var myType = model.Classes.First();
            var name = ts.ScriptGenerator.GetFullyQualifiedTypeName(myType);

            Assert.Equal("XXX.Product", name);
        }
    }
}
