using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TypeLite.Tests {
    public class ScriptBuilderTests {
        ScriptBuilder _sb;
        public ScriptBuilderTests() {
            _sb = new ScriptBuilder("\t");
        }

        [Fact]
        public void WhenIncreaseIndentation_IndentationLevelIsIncreased() {
            _sb.IncreaseIndentation();

            Assert.Equal(1, _sb.IndentationLevels);
        }

        [Fact]
        public void WhenIndentationLevelIsDisposed_IndentationLevelIsDecreased() {
            using (_sb.IncreaseIndentation()) {
                using (_sb.IncreaseIndentation()) {
                    ;
                }

                Assert.Equal(1, _sb.IndentationLevels);
            }
        }

        [Fact]
        public void WhenAppendIndentation_IndentationCharactersAccordingToIndentationLevelsAreAppended() {
            _sb.IncreaseIndentation();
            _sb.IncreaseIndentation();

            _sb.AppendIndentation();

            var script = _sb.ToString();

            Assert.Equal("\t\t", script);
        }

        [Fact]
        public void WhenAppendString_StrignIsAppended() {
            _sb.Append("test");

            var script = _sb.ToString();

            Assert.Equal("test", script);
        }

        [Fact]
        public void WhenAppendFormatString_StrignIsAppended() {
            _sb.AppendFormat("test {0}", 1);

            var script = _sb.ToString();

            Assert.Equal("test 1", script);
        }

        [Fact]
        public void WhenAppendIndentedString_IndentedStringIsAppended() {
            _sb.IncreaseIndentation();
            _sb.AppendIndented("test");

            var script = _sb.ToString();

            Assert.Equal("\ttest", script);
        }

        [Fact]
        public void WhenAppendFormatIndented_IndentedStringIsAppended() {
            _sb.IncreaseIndentation();
            _sb.AppendFormatIndented("test {0}", 1);

            var script = _sb.ToString();

            Assert.Equal("\ttest 1", script);
        }

    }
}
