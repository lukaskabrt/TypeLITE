using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TypeLite.Tests {
	public class TypeConvertorCollectionTests {
		[Fact]
		public void WhenConvertTypeAndNoConverterRegistered_NullIsReturned() {
			var target = new TypeConvertorCollection();

			var result = target.ConvertType(typeof(string));

			Assert.Null(result);
		}

		[Fact]
		public void WhenConvertType_ConvertedValueIsReturned() {
			var target = new TypeConvertorCollection();
			target.RegisterTypeConverter<string>(type => "KnockoutObservable<string>");

			var result = target.ConvertType(typeof(string));

			Assert.Equal("KnockoutObservable<string>", result);
		}
	}
}
