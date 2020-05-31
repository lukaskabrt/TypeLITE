using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeLite.Tests.TestModels;
using Xunit;

namespace TypeLite.Tests.RegressionTests {
	public class Generics0_8Tests {
		[Fact]
		public void WhenClassHasGenericArguments_ValidTypescriptNameIsGenerated() {
			var builder = new TsModelBuilder();
			builder.Add<ClassWithGenericArguments<Address>>();

			var generator = new TsGenerator();
			var model = builder.Build();
			var result = generator.Generate(model);
			Console.WriteLine(result);

			Assert.Contains("interface ClassWithGenericArguments<T> {", result);
		}

		[Fact]
		public void WhenClassHasGenericProperty_PropertyTypeIsResolvedToTypeOfGenericArgument() {
			var builder = new TsModelBuilder();
			builder.Add<ClassWithGenericArguments<Address>>();

			var generator = new TsGenerator();
			var model = builder.Build();
			var result = generator.Generate(model);

			Assert.Contains("Property: T", result);
		}
	}

	public class ClassWithGenericArguments<T> {
		public T Property { get; set; }
	}
}
