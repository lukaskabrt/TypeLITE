using System.Collections.Generic;
using a_b;
using a_b_c;
using Xunit;

namespace TypeLite.Tests.RegressionTests
{
    public class ModuleNameFormatterTests
    {
        [Fact]
        public void CanUseCustomFormatter()
        {
            var builder = new TsModelBuilder();
            builder.Add<Drink>();

            var generator = new TsGenerator();
            generator.SetModuleNameFormatter(_ => "custom");//everything should go into the 'custom' module
            var model = builder.Build();
            var result = generator.Generate(model);

            var expectedOutput = @"
declare namespace custom {
	interface Drink {
	}
}
";

            Assert.Equal(expectedOutput, result);
        }

        [Fact]
        public void CanDealWithNestedNamespaces()
        {
            var builder = new TsModelBuilder();
            builder.Add<Person>();
            builder.Add<Drink>();

            var generator = new TsGenerator();
            generator.SetModuleNameFormatter(_ => "custom");//everything should go into the 'custom' module
            var model = builder.Build();
            var result = generator.Generate(model);

            var expectedOutput = @"
declare namespace custom {
	interface Person {
		AllDrinks: custom.Drink[];
		WhiteRussian: custom.Drink;
	}
}
declare namespace custom {
	interface Drink {
	}
}
";

            Assert.Equal(expectedOutput, result);
        }
    }
}

namespace a_b
{
    public class Person
    {
        public Drink WhiteRussian { get; set; }
        public List<Drink> AllDrinks { get; set; }
    }
}

namespace a_b_c
{
    public class Drink
    {
    }
}
