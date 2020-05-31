using Xunit;
using TypeLite.Net4;
using System.Diagnostics;

namespace TypeLite.Tests.RegressionTests
{
    public class JsDocTests
    {
        [Fact]
        public void GenericClassWithTypeparam()
        {
            // Exception raised documenting generic class with typeparam.
            var ts = TypeScript.Definitions().WithJSDoc()
                .For<UserPreference>();
            string result;
            Assert.DoesNotThrow(() => result = ts.Generate(TsGeneratorOutput.Properties));
            Debug.Write(ts);
        }

        /// <summary>
        /// User Preference
        /// </summary>
        public class UserPreference
        {
            /// <summary>
            /// Preferences's document.
            /// </summary>
            public GenericClass1<string> Preferences { get; set; }
        }

        /// <summary>
        /// GenericClass with T1
        /// </summary>
        /// <typeparam name="T1">typeparam T1</typeparam>
        public class GenericClass1<T1>
        {
            /// <summary>
            /// T1 Property
            /// </summary>
            public T1 Property1 { get; set; }
        }
    }
}
