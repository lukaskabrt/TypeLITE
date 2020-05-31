using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TypeLite.Tests.RegressionTests
{
    public class Issue32_DuplicateInterfacesForClosedGenericPropertyTypes
    {
        [Fact]
        public void WhenClosedGenericTypeAppearsAsPropertyTypeMultipleTimes_OnlyOneInterfaceGenerated()
        {
            var builder = new TsModelBuilder();
            builder.Add<GenericPropertiesBug>();

            var generator = new TsGenerator();
            var model = builder.Build();

            var result = generator.Generate(model);

            Assert.True(result.IndexOf("interface KeyValuePair") > -1, "KeyValuePair interface missing");
            Assert.True(result.IndexOf("interface KeyValuePair") == result.LastIndexOf("interface KeyValuePair"), "KeyValuePair interface generated too many times");

            Assert.True(result.Contains("Test1: System.Collections.Generic.KeyValuePair"));
            Assert.True(result.Contains("Test2: System.Collections.Generic.KeyValuePair"));
        }

        [TsClass]
        public class GenericPropertiesBug 
        { 
            public KeyValuePair<string, string> Test1 { get; set; } 
            public KeyValuePair<string, int> Test2 { get; set; } 
        }
    }
}
