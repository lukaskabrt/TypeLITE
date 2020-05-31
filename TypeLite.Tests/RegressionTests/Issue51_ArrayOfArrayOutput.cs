using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TypeLite.Tests.RegressionTests
{
    public class Issue51_ArrayOfArrayOutput
    {
        [Fact]
        public void WhenArrayOfArrayEncountered_ArrayOfArrayTypeScriptTypeGenerated()
        {
            var builder = new TsModelBuilder();
            builder.Add<TestClass>();

            var generator = new TsGenerator();
            var model = builder.Build();
            var result = generator.Generate(model);

            Assert.Contains("MyStringProperty: string;", result);
            Assert.Contains("MyArray: string[];", result);
            Assert.Contains("MyJaggedArray: string[][];", result);
            Assert.Contains("MyVeryJaggedArray: string[][][];", result);
            Assert.Contains("MyIEnumerableOfString: string[];", result);
            Assert.Contains("MyListOfString: string[];", result);

            Assert.Contains("MyListOfStringArrays: string[][];", result);
            Assert.Contains("MyListOfIEnumerableOfString: string[][];", result);
            Assert.Contains("MyListOfListOfStringArray: string[][][];", result);
        }

        class TestClass
        {
            public string MyStringProperty { get; set; }
            public string[] MyArray { get; set; }
            public string[][] MyJaggedArray { get; set; }
            public string[][][] MyVeryJaggedArray { get; set; }
            public IEnumerable<string> MyIEnumerableOfString { get; set; }
            public List<string> MyListOfString { get; set; }
            public List<string[]> MyListOfStringArrays { get; set; }
            public List<IEnumerable<string>> MyListOfIEnumerableOfString { get; set; }
            public List<List<string[]>> MyListOfListOfStringArray { get; set; }
        }
    }
}
