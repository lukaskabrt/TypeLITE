using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeLite.Tests.TestModels;
using TypeLite.TsModels;
using Xunit;

namespace TypeLite.Tests.TsModels {
	public class TsEnumTests {

		[Fact]
		public void WhenInitializedWithNonEnumType_ArgumentExceptionIsThrown() {
			Assert.Throws<ArgumentException>(() => new TsEnum(typeof(Address)));
		}

		[Fact]
		public void WhenInitialized_NameIsSet() {
			var target = new TsEnum(typeof(ContactType));

			Assert.Equal("ContactType", target.Name);
		}
	}
}
