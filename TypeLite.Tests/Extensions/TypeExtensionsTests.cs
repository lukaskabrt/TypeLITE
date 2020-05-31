using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using TypeLite.Extensions;

namespace TypeLite.Tests.Extensions {
	public class TypeExtensionsTests {

		#region IsNullable tests

		[Fact]
		public void WhenIsNullableIsCalledWithNullableType_TrueIsReturned() {
			var type = typeof(int?);
			
			Assert.True(type.IsNullable());
		}

		[Fact]
		public void WhenIsNullableIsCalledWithValueType_FalseIsReturned() {
			var type = typeof(int);
			
			Assert.False(type.IsNullable());
		}

		[Fact]
		public void WhenIsNullableIsCalledWithReferenceType_FalseIsReturned() {
			var type = typeof(string);
			
			Assert.False(type.IsNullable());
		}

		#endregion

		#region GetNullableValueType tests

		[Fact]
		public void WhenGetNullableValueTypeWithNullableValueType_ValueTypeIsReturned() {
			var type = typeof(int?);

			Assert.Equal(typeof(int), type.GetNullableValueType());
		}

		[Fact]
		public void WhenGetNullableValueTypeWithNonNullableType_ExceptionIsThrown() {
			var type = typeof(string);

			Assert.Throws<InvalidOperationException>(() => type.GetNullableValueType());
		}

		#endregion
	}
}
