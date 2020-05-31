using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TypeLite.Tests.RegressionTests {
    public class Issue41_EnumsInGenericClasses {
        [Fact]
        public void DoesNotThrowNullReferenceException_WhenEnumPropertyInGenericClass() {
            Assert.DoesNotThrow(() => {
                var builder = new TsModelBuilder();
                builder.Add<Bob<object>>();

                var generator = new TsGenerator();
                var model = builder.Build();
                var result = generator.Generate(model);
            });
        }

        [TsClass]
        public class Bob<T> {
            public MyTestEnum MyEnum { get; set; }
            public string TestString { get; set; }
            public string MyProperty { get; set; }
        }

        [TsEnum]
        public enum MyTestEnum {
            One,
            Two,
            Three
        }
    }
}
