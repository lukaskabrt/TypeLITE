using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TypeLite.Tests.RegressionTests {
    public class Issue63_SelfReferencingEnumerable {
        /// <summary>
        /// When a self-referencing enumerable is present but ignored, it shouldn't break the build.
        /// </summary>
        [Fact]
        public void WhenBuild_NoSelfReferencingEnumerableInfiniteLoop() {
            var target = new TsModelBuilder();
            target.Add(typeof(IgnoredSelfReferencingEnumerableWrapper));

            // May cause infinite loop or stack overflow, if not handled correctly.
            target.Build();
        }

        /// <summary>
        /// A self-referencing enumerable should emit type "any".
        /// </summary>
        [Fact]
        public void SelfReferencingEnumerableGenerateAny() {
            string output = TypeScript.Definitions()
                .For<SelfReferencingEnumerableWrapper>()
                .Generate(TsGeneratorOutput.Properties);

            Assert.Contains("MyProperty: any;", output);
        }
        
        [Fact]
        public void SelfReferencingEnumerableInheritorGenerateAnyArray() {
            var ts = TypeScript.Definitions();
            ts
                .WithMemberTypeFormatter((tsProperty, memberTypeName) => {
                    // The following is a workaround proof-of-concept for the JSON.NET JObject class (among others).
                    // Without this, it would be similar to a List<JToken> (in the way that they both
                    // implement IEnumerable<JToken>) and emit type "any[]" instead of the desired type "any".
                    // Example: tsProperty.PropertyType.Type.FullName == "Newtonsoft.Json.Linq.JObject"
                    if (tsProperty.PropertyType.Type.Name == "SelfReferencingEnumerableInheritor") {
                        // No "[]" emitted.
                        return memberTypeName;
                    }
                    return ts.ScriptGenerator.DefaultMemberTypeFormatter(tsProperty, memberTypeName);
                })
                .For<SelfReferencingEnumerableInheritorWrapper>();
            string output = ts
                .Generate(TsGeneratorOutput.Properties);
            
            Assert.Contains("MyInheritorProperty: any;", output);
            Assert.Contains("MyArrayProperty: any[];", output);
            Assert.Contains("MyListProperty: any[];", output);
        }
    }

    public class IgnoredSelfReferencingEnumerableWrapper {
        [TsIgnore]
        public SelfReferencingEnumerable MyIgnoredProperty { get; set; }
    }

    public class SelfReferencingEnumerableWrapper {
        public SelfReferencingEnumerable MyProperty { get; set; }
    }

    public class SelfReferencingEnumerableInheritorWrapper {
        public SelfReferencingEnumerableInheritor MyInheritorProperty { get; set; }

        public SelfReferencingEnumerable[] MyArrayProperty { get; set; }

        public List<SelfReferencingEnumerable> MyListProperty { get; set; }
    }

    public class SelfReferencingEnumerable : IEnumerable<SelfReferencingEnumerable> {
        public IEnumerator<SelfReferencingEnumerable> GetEnumerator() {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            throw new NotImplementedException();
        }
    }

    public class SelfReferencingEnumerableInheritor : SelfReferencingEnumerable {
    }
}
