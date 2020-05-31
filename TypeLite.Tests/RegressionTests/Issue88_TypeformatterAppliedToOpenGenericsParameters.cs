using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TypeLite.Tests.RegressionTests {
    public class Issue88_TypeformatterAppliedToOpenGenericsParameters {

        [Fact(Skip="Not fixed")]
        public void WhenTypeFormaterIsUsed_ItIsntAppliedToOpenGenericsParameters() {
            var ts = TypeScript.Definitions()
                .For<UserPreference>()
                .WithTypeFormatter(((type, F) => "I" + ((TypeLite.TsModels.TsClass)type).Name))
                .Generate();

            Debug.Write(ts);

            Assert.Contains("Key: TKey", ts);
            Assert.Contains("Value: TValue", ts);
        }


        public class UserPreference {
            public Dictionary<string, string> Preferences { get; set; }
        }
    }
}
