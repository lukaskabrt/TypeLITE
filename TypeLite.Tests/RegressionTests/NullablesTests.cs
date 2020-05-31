using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using TypeLite;

namespace TypeLite.Tests.RegressionTests {
	public class NullablesTests {

		[Fact]
		public void WhenClassHasNullableStructure_StructureIsAddedToModel() {
			var builder = new TsModelBuilder();
			builder.Add<NullableStructureContainer>();

			var generator = new TsGenerator();
			var model = builder.Build();
			var result = generator.Generate(model);

			Assert.Contains("NullableStructure: TypeLite.Tests.RegressionTests.Structure1;", result);
			Assert.Contains("NullableStructureCollection: TypeLite.Tests.RegressionTests.Structure2[];", result);
			Assert.Contains("NullableInt: number;", result);
		}

		[Fact]
		public void WhenClassContainsNullableAndNonNullableStruct_StructureIsIncludedInModelOnlyOnce() {
			var builder = new TsModelBuilder();
			builder.Add<NullableAndNonNullableContainer>();

			var generator = new TsGenerator();
			var model = builder.Build();

			Assert.Single(model.Classes.Where(o => o.Type == typeof(Structure1)));
		}
	}

	public class NullableAndNonNullableContainer {
		public Structure1? NullableStructure { get; set; }
		public Structure1 Structure { get; set; }
	}

	public class NullableStructureContainer {
		public Structure1? NullableStructure { get; set; }
		public IEnumerable<Structure2?> NullableStructureCollection { get; set; }
		public int? NullableInt { get; set; }
	}

	public struct Structure1 {
		public int X { get; set; }
	}

	public struct Structure2 {
		public int Y { get; set; }
	}
}
