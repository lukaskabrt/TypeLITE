using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeLite.Tests.TestModels;
using TypeLite.TsModels;
using Xunit;

namespace TypeLite.Tests.TsModels {
	public class TsTypeTests {
		[Fact]
		public void WhenInitilized_ClrTypeIsSet() {
			var type = typeof(string);

			var target = new TsType(type);

			Assert.Equal(type, target.Type);
		}

		#region GetTypeFamily tests

		[Fact]
		public void WhenGetTypeFamilyForInt_SystemIsReturned() {
			var family = TsType.GetTypeFamily(typeof(int));

			Assert.Equal(TsTypeFamily.System, family);
		}

		[Fact]
		public void WhenGetTypeFamilyForString_SystemIsReturned() {
			var family = TsType.GetTypeFamily(typeof(string));

			Assert.Equal(TsTypeFamily.System, family);
		}

		[Fact]
		public void WhenGetTypeFamilyForDouble_SystemIsReturned() {
			var family = TsType.GetTypeFamily(typeof(double));

			Assert.Equal(TsTypeFamily.System, family);
		}

		[Fact]
		public void WhenGetTypeFamilyForBool_SystemIsReturned() {
			var family = TsType.GetTypeFamily(typeof(bool));

			Assert.Equal(TsTypeFamily.System, family);
		}

		[Fact]
		public void WhenGetTypeFamilyForDateTime_SystemIsReturned() {
			var family = TsType.GetTypeFamily(typeof(DateTime));

			Assert.Equal(TsTypeFamily.System, family);
		}

		[Fact]
		public void WhenGetTypeFamilyForDecimal_SystemIsReturned() {
			var family = TsType.GetTypeFamily(typeof(decimal));

			Assert.Equal(TsTypeFamily.System, family);
		}

		[Fact]
		public void WhenGetTypeFamilyForClass_ClassIsReturned() {
			var family = TsType.GetTypeFamily(typeof(Address));

			Assert.Equal(TsTypeFamily.Class, family);
		}

		[Fact]
		public void WhenGetTypeFamilyForEnum_EnumIsReturned() {
			var family = TsType.GetTypeFamily(typeof(CustomerKind));

			Assert.Equal(TsTypeFamily.Enum, family);
		}

		[Fact]
		public void WhenGetTypeFamilyForIEnumerable_ClassIsReturned() {
			var family = TsType.GetTypeFamily(typeof(List<int>));

			Assert.Equal(TsTypeFamily.Collection, family);
		}

		[Fact]
		public void WhenGetTypeFamilyForObject_TypeIsReturned() {
			var family = TsType.GetTypeFamily(typeof(object));

			Assert.Equal(TsTypeFamily.Type, family);
		}

		[Fact]
		public void WhenGetTypeFamilyForStruct_ClassIsReturned() {
			var family = TsType.GetTypeFamily(typeof(PointStruct));

			Assert.Equal(TsTypeFamily.Class, family);
		}

		[Fact]
		public void WhenGetTypeFamilyForNullableSystemType_SystemTypeIsReturned() {
			var family = TsType.GetTypeFamily(typeof(int?));

			Assert.Equal(TsTypeFamily.System, family);
		}

		[Fact]
		public void WhenGetTypeFamilyForNullableStruct_ClassIsReturned() {
			var family = TsType.GetTypeFamily(typeof(PointStruct?));

			Assert.Equal(TsTypeFamily.Class, family);
		}

		#endregion
	}
}
