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
            Assert.Equal(test.res, "with a value");
        }

        [Fact]
        public void FindsQuotedString()
        {
            var test2 = "this is a quoted \"string\"".Between("\"","\"");
            Assert.Equal(test2.res, "string");
        }

        [Fact]
        public void Finds_Entire_EscapedString()
        {
            var escaped = @"test with escape chars [this is the first \[ i should be included] and this too]".Between("[", "]");
            Assert.Equal(escaped.res, @"this is the first \[ i should be included");
        }
        // More tests:
        // escaped quotes in a string
        // separated by newlines
        // constants as value for key or value
    }
}
