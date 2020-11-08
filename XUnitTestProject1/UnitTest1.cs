using System;
using Xunit;
using VSIXProject1;
using System.Text.RegularExpressions;

namespace XUnitTestProject1
{
    public class UnitTest1
    {

        [Fact]
        public void GeneralTestBetweenParens()
        {
            var test = "this is a string(with a value) and some more text".Between("(",")");
            Assert.Equal("with a value",test.res);
        }

        [Fact]
        public void FindsQuotedString()
        {
            var test2 = "this is a quoted \"string\"".Between("\"","\"");
            Assert.Equal("string",test2.res);
        }

        [Fact]
        public void Finds_Entire_EscapedString()
        {
            var escaped = @"test with escape chars [this is the first \] i should be included] and this too]".Between("[", "]", '\\');
            Assert.Equal(@"this is the first \] i should be included",escaped.res);
        }
        // More tests:
        // escaped quotes in a string
        // separated by newlines
        // constants as value for key or value
    }
}
