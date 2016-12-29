using System;
using System.Collections.Generic;
using System.Text;
using TypeLite.Ts;
using Xunit;

namespace TypeLite.Tests.Ts {
    public class TsBasicTypeTests {
        [Fact]
        public void WhenInitialized_GenericArgumentsIsEmpty() {
            var type = new TsBasicType();

            Assert.Empty(type.GenericArguments);
        }
    }
}
