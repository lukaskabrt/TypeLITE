using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;

using TypeLite.TsModels;

namespace TypeLite.Tests.TsModels {
    public class TsModuleTests {

        [Fact]
        public void WhenInitialized_ClassesCollectionIsEmpty() {
            var target = new TsModule("Tests");

            Assert.NotNull(target.Classes);
            Assert.Empty(target.Classes);
        }

        [Fact]
        public void WhenInitialized_NameIsSet() {
            var target = new TsModule("Tests");

            Assert.Equal("Tests", target.Name);
        }
    }
}
