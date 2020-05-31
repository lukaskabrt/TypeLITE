using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeLite.Tests.BackboneExtension {
    class Program {
        static void Main(string[] args) {
            var builder = new TsModelBuilder();
            builder.Add<TestObject>();
            var model = builder.Build();
            var target = new TypeLite.AlternateGenerators.TsBackboneModelGenerator();

            target.SetTypeVisibilityFormatter((c, v) => { return true; });
            var script = target.Generate(model, TsGeneratorOutput.Properties | TsGeneratorOutput.Enums);
        }
    }
}
