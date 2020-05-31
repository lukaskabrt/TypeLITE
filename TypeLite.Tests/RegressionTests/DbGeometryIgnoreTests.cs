using System;
using System.Collections.Generic;
using System.Data.Spatial;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TypeLite.Tests.RegressionTests {
	public class DbGeometryIgnoreTests {
		[Fact]
		public void WhenPropertyWithDbGeometryTypeIsAnnotedWithTsIgnoreAttribute_GeneratorDoesntCrash() {
			var builder = new TsModelBuilder();
			builder.Add<DbGeometryTestClass>();

			var generator = new TsGenerator();
			var model = builder.Build();

			Assert.DoesNotThrow(() => generator.Generate(model));
		}
	}

	public class DbGeometryTestClass {
		public int ID { get; set; }

		[TsIgnore]
		public DbGeometry Position { get; set; }
	}
}
