using System;
using System.Collections.Generic;
using System.Text;
using TypeLite.Ts;
using Xunit;

namespace TypeLite.Tests {
    public class TsModelTests {
        [Fact]
        public void WhenInitialized_MemberCollectionsAreEmpty() {
            var model = new TsModel();

            Assert.Empty(model.Enums);
        }
    }
}
