using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TypeLite.Tests.RegressionTests {
	public class Issue15_ConvertSystemType {
		[Fact]
		public void WhenClassContainsPropertyOfSystemType_InvalidCastExceptionIsntThrown() {
			var builder = new TsModelBuilder();
			builder.Add<Issue15Example>();

			var generator = new TsGenerator();
			var model = builder.Build();

			Assert.DoesNotThrow(() => generator.Generate(model));
		}
	}

	public class Issue15Example {
		public System.Type Type { get; set; }
	}
}
