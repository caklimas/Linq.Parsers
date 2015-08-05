using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linq.Parsers.Testing
{
    [TestClass]
    public class GrammarTest : Test
    {
        #region String

        [TestMethod]
        public void String()
        {
            var grammar = GetGrammar();
            var parser = grammar.String("");

            var result = parser.Parse("a");
            Assert.IsTrue(result.Value == "");
            Assert.IsTrue(result.Rest == "a");

            result = parser.Parse("abc");
            Assert.IsTrue(result.Value == "");
            Assert.IsTrue(result.Rest == "abc");

            parser = grammar.String("abc");

            result = parser.Parse("a");
            Assert.IsTrue(result == null);

            result = parser.Parse("abcdef");
            Assert.IsTrue(result.Value == "abc");
            Assert.IsTrue(result.Rest == "def");
        }

        #endregion //String

        #region Char

        [TestMethod]
        public void Char()
        {
            var grammar = GetGrammar();
            var parser = grammar.Char();

            var result = parser.Parse("");
            Assert.IsTrue(result == null);

            result = parser.Parse("ac");
            Assert.IsTrue(result.Value == "a");
            Assert.IsTrue(result.Rest == "c");

            result = parser.Parse("bc");
            Assert.IsTrue(result.Value == "b");
            Assert.IsTrue(result.Rest == "c");

            parser = grammar.Char('a');

            result = parser.Parse("ac");
            Assert.IsTrue(result.Value == "a");
            Assert.IsTrue(result.Rest == "c");

            result = parser.Parse("bc");
            Assert.IsTrue(result == null);
        }

        #endregion //Char

        #region Set

        [TestMethod]
        public void Set()
        {
            var grammar = GetGrammar();
            var parser = grammar.Set("");

            var result = parser.Parse("");
            Assert.IsTrue(result == null);

            result = parser.Parse("a");
            Assert.IsTrue(result == null);

            result = parser.Parse("abc");
            Assert.IsTrue(result == null);

            parser = grammar.Set("abc");

            result = parser.Parse("");
            Assert.IsTrue(result == null);

            result = parser.Parse("ab");
            Assert.IsTrue(result.Value == "a");
            Assert.IsTrue(result.Rest.ToString() == "b");

            result = parser.Parse("bde");
            Assert.IsTrue(result.Value == "b");
            Assert.IsTrue(result.Rest.ToString() == "de");

            result = parser.Parse("cfgh");
            Assert.IsTrue(result.Value == "c");
            Assert.IsTrue(result.Rest.ToString() == "fgh");

            result = parser.Parse("def");
            Assert.IsTrue(result == null);
        }

        #endregion //Set

        #region Range

        [TestMethod]
        public void Range()
        {
            var grammar = GetGrammar();
            var parser = grammar.Range('a', 'a');

            var result = parser.Parse(Text.Empty);
            Assert.IsTrue(result == null);

            result = parser.Parse("a");
            Assert.IsTrue(result.Value == "a");
            Assert.IsTrue(result.Rest.ToString() == "");

            result = parser.Parse("b");
            Assert.IsTrue(result == null);

            parser = grammar.Range('a', 'z');

            result = parser.Parse(Text.Empty);
            Assert.IsTrue(result == null);

            result = parser.Parse("a");
            Assert.IsTrue(result.Value == "a");
            Assert.IsTrue(result.Rest.ToString() == "");

            result = parser.Parse("m");
            Assert.IsTrue(result.Value == "m");
            Assert.IsTrue(result.Rest.ToString() == "");

            result = parser.Parse("z");
            Assert.IsTrue(result.Value == "z");
            Assert.IsTrue(result.Rest.ToString() == "");

            result = parser.Parse("1");
            Assert.IsTrue(result == null);

            parser = grammar.Range('A', 'Z');

            result = parser.Parse(Text.Empty);
            Assert.IsTrue(result == null);

            result = parser.Parse("A");
            Assert.IsTrue(result.Value == "A");
            Assert.IsTrue(result.Rest.ToString() == "");

            result = parser.Parse("M");
            Assert.IsTrue(result.Value == "M");
            Assert.IsTrue(result.Rest.ToString() == "");

            result = parser.Parse("Z");
            Assert.IsTrue(result.Value == "Z");
            Assert.IsTrue(result.Rest.ToString() == "");

            result = parser.Parse("1");
            Assert.IsTrue(result == null);

            parser = grammar.Range('0', '9');

            result = parser.Parse(Text.Empty);
            Assert.IsTrue(result == null);

            result = parser.Parse("0");
            Assert.IsTrue(result.Value == "0");
            Assert.IsTrue(result.Rest.ToString() == "");

            result = parser.Parse("5");
            Assert.IsTrue(result.Value == "5");
            Assert.IsTrue(result.Rest.ToString() == "");

            result = parser.Parse("9");
            Assert.IsTrue(result.Value == "9");
            Assert.IsTrue(result.Rest.ToString() == "");

            result = parser.Parse("a");
            Assert.IsTrue(result == null);
        }

        #endregion //Range

        #region Null

        [TestMethod]
        public void Null()
        {
            var grammar = GetGrammar();
            var parser = grammar.Null;

            var result = parser.Parse("");
            Assert.IsTrue(result == null);

            result = parser.Parse("a");
            Assert.IsTrue(result == null);

            result = parser.Parse("bc");
            Assert.IsTrue(result == null);
        }

        #endregion //Null

        #region SingleWhiteSpace

        [TestMethod]
        public void SingleWhiteSpace()
        {
            var grammar = GetGrammar();
            var parser = grammar.SingleWhiteSpace;

            var result = parser.Parse("");
            Assert.IsTrue(result == null);

            result = parser.Parse("  ");
            Assert.IsTrue(result.Value == " ");
            Assert.IsTrue(result.Rest == " ");

            result = parser.Parse("\f");
            Assert.IsTrue(result.Value == "\f");
            Assert.IsTrue(result.Rest == "");

            result = parser.Parse("\n");
            Assert.IsTrue(result.Value == "\n");
            Assert.IsTrue(result.Rest == "");

            result = parser.Parse("\r");
            Assert.IsTrue(result.Value == "\r");
            Assert.IsTrue(result.Rest == "");

            result = parser.Parse("\t");
            Assert.IsTrue(result.Value == "\t");
            Assert.IsTrue(result.Rest == "");

            result = parser.Parse("\v");
            Assert.IsTrue(result.Value == "\v");
            Assert.IsTrue(result.Rest == "");

            result = parser.Parse("a");
            Assert.IsTrue(result == null);
        }

        #endregion //SingleWhiteSpace

        #region WhiteSpace

        [TestMethod]
        public void WhiteSpace()
        {
            var grammar = GetGrammar();
            var parser = grammar.WhiteSpace;

            var result = parser.Parse("");
            Assert.IsTrue(result.Value == "");
            Assert.IsTrue(result.Value == "");

            result = parser.Parse("  ");
            Assert.IsTrue(result.Value == "  ");
            Assert.IsTrue(result.Rest == "");

            result = parser.Parse("\f");
            Assert.IsTrue(result.Value == "\f");
            Assert.IsTrue(result.Rest == "");

            result = parser.Parse("\n");
            Assert.IsTrue(result.Value == "\n");
            Assert.IsTrue(result.Rest == "");

            result = parser.Parse("\r");
            Assert.IsTrue(result.Value == "\r");
            Assert.IsTrue(result.Rest == "");

            result = parser.Parse("\t");
            Assert.IsTrue(result.Value == "\t");
            Assert.IsTrue(result.Rest == "");

            result = parser.Parse("\v");
            Assert.IsTrue(result.Value == "\v");
            Assert.IsTrue(result.Rest == "");

            result = parser.Parse("a");
            Assert.IsTrue(result.Value == "");
            Assert.IsTrue(result.Rest == "a");

            result = parser.Parse(" \v \r \nabc");
            Assert.IsTrue(result.Value == " \v \r \n");
            Assert.IsTrue(result.Rest == "abc");
        }

        #endregion //WhiteSpace

        #region Digit

        [TestMethod]
        public void Digit()
        {
            var grammar = GetGrammar();
            var parser = grammar.Digit;

            for (var i = 0; i < 10; i++)
            {
                var digit = i.ToString();
                var digitResult = parser.Parse(digit);
                Assert.IsTrue(digitResult.Value == digit);
                Assert.IsTrue(digitResult.Rest == "");
            }

            var result = parser.Parse("");
            Assert.IsTrue(result == null);

            result = parser.Parse("a");
            Assert.IsTrue(result == null);
        }

        #endregion //Digit

        #region Letter

        [TestMethod]
        public void Letter()
        {
            var grammar = GetGrammar();
            var parser = grammar.Letter;

            var letters = "abcdefghijklmnopqurtuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            foreach (var c in letters)
            {
                var letter = c.ToString();
                var result = parser.Parse(letter);
                Assert.IsTrue(result.Value == letter);
                Assert.IsTrue(result.Rest == "");
            }

            for (var i = 0; i < 10; i++)
            {
                var digit = i.ToString();
                var result = parser.Parse(digit);
                Assert.IsTrue(result == null);
            }
        }

        #endregion //Letter

        #region Utilities

        private Grammar<Text> GetGrammar()
        {
            return new TestGrammar();
        }

        #endregion //Utilities

        #region Test Classes

        private class TestGrammar : Grammar<Text>
        {
            public override Parser<Text, Text> Parser { get { return null; } }
        }

        #endregion //Test Classes
    }
}
