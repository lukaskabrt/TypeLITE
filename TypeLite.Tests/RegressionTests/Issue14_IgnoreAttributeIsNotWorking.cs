using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeLite.Tests.TestModels;
using Xunit;

namespace TypeLite.Tests.RegressionTests {
	public class Issue14_IgnoreAttributeIsNotWorking {
		[Fact]
		public void WhenPropertyHastsIgnoreAttribute_TypeOfIgnoredPropertyIsExcludedFromModel() {
			var builder = new TsModelBuilder();
			builder.Add<Issue14Example>();

			var generator = new TsGenerator();
			var model = builder.Build();
			var script = generator.Generate(model);

			Assert.DoesNotContain("Person", script);
		}
	}

	public class Issue14Example {
		public string Name { get; set; }

		[TsIgnore]
		public Person Contact { get; set; }
	}
}
