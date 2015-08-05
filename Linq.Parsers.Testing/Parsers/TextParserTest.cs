using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linq.Parsers.Testing
{
    [TestClass]
    public class TextParserTest : Test
    {
        #region Insert

        [TestMethod]
        public void Insert()
        {
            var parser = TextParser.Insert("Insert");
            var result = parser.Parse("");
            Assert.IsTrue(result.Value.ToString() == "Insert");
            Assert.IsTrue(result.Rest.ToString() == "");

            parser = TextParser.Insert("Insert");
            result = parser.Parse("Rest");
            Assert.IsTrue(result.Value.ToString() == "Insert");
            Assert.IsTrue(result.Rest.ToString() == "Rest");
        }

        #endregion //Insert

        #region Plus

        [TestMethod]
        public void Plus()
        {
            var first = GetStringParser("");
            var parser = first + first;

            var result = parser.Parse("");
            Assert.IsTrue(result.Value == "");
            Assert.IsTrue(result.Rest == "");

            result = parser.Parse("a");
            Assert.IsTrue(result.Value == "");
            Assert.IsTrue(result.Rest == "a");

            first = GetStringParser("a");
            var second = GetStringParser("");
            parser = first + second;

            result = parser.Parse("a");
            Assert.IsTrue(result.Value == "a");
            Assert.IsTrue(result.Rest == "");

            result = parser.Parse("b");
            Assert.IsTrue(result == null);

            first = GetStringParser("");
            second = GetStringParser("a");
            parser = first + second;

            result = parser.Parse("a");
            Assert.IsTrue(result.Value == "a");
            Assert.IsTrue(result.Rest == "");

            result = parser.Parse("b");
            Assert.IsTrue(result == null);

            first = GetStringParser("a");
            second = GetStringParser("b");
            parser = first + second;

            result = parser.Parse("abc");
            Assert.IsTrue(result.Value == "ab");
            Assert.IsTrue(result.Rest == "c");

            result = parser.Parse("a");
            Assert.IsTrue(result == null);

            result = parser.Parse("b");
            Assert.IsTrue(result == null);
        }

        #endregion //Plus

        #region Or

        [TestMethod]
        public void Or()
        {
            var first = GetStringParser("");
            var parser = first | first;

            var result = parser.Parse("");
            Assert.IsTrue(result.Value == "");
            Assert.IsTrue(result.Rest == "");

            result = parser.Parse("a");
            Assert.IsTrue(result.Value == "");
            Assert.IsTrue(result.Rest == "a");

            first = GetStringParser("a");
            var second = GetStringParser("");
            parser = first | second;

            result = parser.Parse("a");
            Assert.IsTrue(result.Value == "a");
            Assert.IsTrue(result.Rest == "");

            result = parser.Parse("b");
            Assert.IsTrue(result.Value == "");
            Assert.IsTrue(result.Rest == "b");

            first = GetStringParser("");
            second = GetStringParser("a");
            parser = first | second;

            result = parser.Parse("a");
            Assert.IsTrue(result.Value == "");
            Assert.IsTrue(result.Rest == "a");

            result = parser.Parse("b");
            Assert.IsTrue(result.Value == "");
            Assert.IsTrue(result.Rest == "b");

            first = GetStringParser("a");
            second = GetStringParser("b");
            parser = first | second;

            result = parser.Parse("ac");
            Assert.IsTrue(result.Value == "a");
            Assert.IsTrue(result.Rest == "c");

            result = parser.Parse("bc");
            Assert.IsTrue(result.Value == "b");
            Assert.IsTrue(result.Rest == "c");

            result = parser.Parse("c");
            Assert.IsTrue(result == null);
        }

        #endregion //Or

        #region ZeroOrMore

        [TestMethod]
        public void ZeroOrMore()
        {
            var parser = GetStringParser("").ZeroOrMore();

            var result = parser.Parse("");
            Assert.IsTrue(result.Value == "");
            Assert.IsTrue(result.Rest == "");

            result = parser.Parse("a");
            Assert.IsTrue(result.Value == "");
            Assert.IsTrue(result.Rest == "a");

            parser = GetStringParser(" ").ZeroOrMore();

            result = parser.Parse("");
            Assert.IsTrue(result.Value == "");
            Assert.IsTrue(result.Rest == "");

            result = parser.Parse("a");
            Assert.IsTrue(result.Value == "");
            Assert.IsTrue(result.Rest == "a");

            result = parser.Parse(" ");
            Assert.IsTrue(result.Value == " ");
            Assert.IsTrue(result.Rest == "");

            result = parser.Parse("   ");
            Assert.IsTrue(result.Value == "   ");
            Assert.IsTrue(result.Rest == "");

            parser = GetStringParser("ab").ZeroOrMore();

            result = parser.Parse("");
            Assert.IsTrue(result.Value == "");
            Assert.IsTrue(result.Rest == "");

            result = parser.Parse("a");
            Assert.IsTrue(result.Value == "");
            Assert.IsTrue(result.Rest == "a");

            result = parser.Parse("ab");
            Assert.IsTrue(result.Value == "ab");
            Assert.IsTrue(result.Rest == "");

            result = parser.Parse("ababab");
            Assert.IsTrue(result.Value == "ababab");
            Assert.IsTrue(result.Rest == "");
        }

        #endregion //ZeroOrMore

        #region OneOrMore

        [TestMethod]
        public void OneOrMore()
        {
            var parser = GetStringParser("").OneOrMore();

            var result = parser.Parse("");
            Assert.IsTrue(result.Value == "");
            Assert.IsTrue(result.Rest == "");

            result = parser.Parse("a");
            Assert.IsTrue(result.Value == "");
            Assert.IsTrue(result.Rest == "a");

            parser = GetStringParser(" ").OneOrMore();

            result = parser.Parse("");
            Assert.IsTrue(result == null);

            result = parser.Parse("a");
            Assert.IsTrue(result == null);

            result = parser.Parse(" ");
            Assert.IsTrue(result.Value == " ");
            Assert.IsTrue(result.Rest == "");

            result = parser.Parse("   ");
            Assert.IsTrue(result.Value == "   ");
            Assert.IsTrue(result.Rest == "");

            parser = GetStringParser("ab").OneOrMore();

            result = parser.Parse("");
            Assert.IsTrue(result == null);

            result = parser.Parse("a");
            Assert.IsTrue(result == null);

            result = parser.Parse("ab");
            Assert.IsTrue(result.Value == "ab");
            Assert.IsTrue(result.Rest == "");

            result = parser.Parse("ababab");
            Assert.IsTrue(result.Value == "ababab");
            Assert.IsTrue(result.Rest == "");
        }

        #endregion //OneOrMore

        #region Optional

        [TestMethod]
        public void Optional()
        {
            var parser = GetStringParser("").Optional();

            var result = parser.Parse("");
            Assert.IsTrue(result.Value == "");
            Assert.IsTrue(result.Rest == "");

            result = parser.Parse("a");
            Assert.IsTrue(result.Value == "");
            Assert.IsTrue(result.Rest == "a");

            parser = GetStringParser("a").Optional();

            result = parser.Parse("");
            Assert.IsTrue(result.Value == "");
            Assert.IsTrue(result.Rest == "");

            result = parser.Parse("a");
            Assert.IsTrue(result.Value == "a");
            Assert.IsTrue(result.Rest == "");

            result = parser.Parse("b");
            Assert.IsTrue(result.Value == "");
            Assert.IsTrue(result.Rest == "b");
        }

        #endregion //Optional

        #region WithTrivia

        [TestMethod]
        public void WithTrivia()
        {
            var parser = GetStringParser("").WithTrivia();

            var result = parser.Parse("");
            Assert.IsTrue(result.Value.LeftTrivia == "");
            Assert.IsTrue(result.Value.Value == "");
            Assert.IsTrue(result.Value.RightTrivia == "");
            Assert.IsTrue(result.Rest == "");

            result = parser.Parse(" ");
            Assert.IsTrue(result.Value.LeftTrivia == " ");
            Assert.IsTrue(result.Value.Value == "");
            Assert.IsTrue(result.Value.RightTrivia == "");
            Assert.IsTrue(result.Rest == "");

            parser = GetStringParser("a").WithTrivia();

            result = parser.Parse("a");
            Assert.IsTrue(result.Value.LeftTrivia == "");
            Assert.IsTrue(result.Value.Value == "a");
            Assert.IsTrue(result.Value.RightTrivia == "");
            Assert.IsTrue(result.Rest == "");

            result = parser.Parse(" a  b");
            Assert.IsTrue(result.Value.LeftTrivia == " ");
            Assert.IsTrue(result.Value.Value == "a");
            Assert.IsTrue(result.Value.RightTrivia == "  ");
            Assert.IsTrue(result.Rest == "b");
        }

        #endregion //WithTrivia

        #region Utilities

        private TextParser GetStringParser(string text)
        {
            return new TestGrammar().String(text);
        }

        #endregion //Utilities

        #region Test Classes

        private class TestGrammar : Grammar<Text>
        {
            public override Parser<Text, Text> Parser
            {
                get
                {
                    throw new NotImplementedException();
                }
            }
        }

        #endregion //Test Classes
    }
}
