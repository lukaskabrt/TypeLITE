        using System.Globalization;
        using Xunit;

        namespace TypeLite.Tests.RegressionTests
        {
            public class Issue103_GlobalizationTypes
            {
                [Fact]
                public void GlobalizationTypesCreatedWhenNotUsed()
                {
                    var ts = TypeScript.Definitions();
                    ts.WithConvertor<System.Globalization.CultureInfo>(type => { return "string"; }).For<TestClass>().WithConvertor<System.Globalization.CultureInfo>(type => { return "string"; });
                    var result = ts.Generate(TsGeneratorOutput.Properties);
            
            
                }

                class TestClass
                {
                   public CultureInfo CultureInfo { get; set; }
                }
            }
        }
