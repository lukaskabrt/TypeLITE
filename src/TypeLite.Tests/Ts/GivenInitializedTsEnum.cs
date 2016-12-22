using System;
using System.Collections.Generic;
using System.Text;
using TypeLite.Ts;
using Xunit;

namespace TypeLite.Tests.Ts {
    public class GivenInitializedTsEnum {
        private TsEnum _sut;

        public GivenInitializedTsEnum() {
            _sut = new TsEnum();
        }

        [Fact]
        public void WhenInitialized_ValuesIsEmptyCollection() {
            Assert.Empty(_sut.Values);
        }
    }
}
