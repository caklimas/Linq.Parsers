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

            var result = parser.Parse(Text.Create("a"));
            Assert.IsTrue(result.Value.ToString() == "");
            Assert.IsTrue(result.Rest.ToString() == "a");

            result = parser.Parse(Text.Create("abc"));
            Assert.IsTrue(result.Value.ToString() == "");
            Assert.IsTrue(result.Rest.ToString() == "abc");

            parser = grammar.String("abc");

            result = parser.Parse(Text.Create("a"));
            Assert.IsTrue(result == null);

            result = parser.Parse(Text.Create("abcdef"));
            Assert.IsTrue(result.Value.ToString() == "abc");
            Assert.IsTrue(result.Rest.ToString() == "def");
        }

        #endregion //String

        #region Set

        [TestMethod]
        public void Set()
        {
            var grammar = GetGrammar();
            var parser = grammar.Set("");

            var result = parser.Parse(Text.Empty);
            Assert.IsTrue(result == null);

            result = parser.Parse(Text.Create("a"));
            Assert.IsTrue(result == null);

            result = parser.Parse(Text.Create("abc"));
            Assert.IsTrue(result == null);

            parser = grammar.Set("abc");

            result = parser.Parse(Text.Empty);
            Assert.IsTrue(result == null);

            result = parser.Parse(Text.Create("ab"));
            Assert.IsTrue(result.Value.ToString() == "a");
            Assert.IsTrue(result.Rest.ToString() == "b");

            result = parser.Parse(Text.Create("bde"));
            Assert.IsTrue(result.Value.ToString() == "b");
            Assert.IsTrue(result.Rest.ToString() == "de");

            result = parser.Parse(Text.Create("cfgh"));
            Assert.IsTrue(result.Value.ToString() == "c");
            Assert.IsTrue(result.Rest.ToString() == "fgh");

            result = parser.Parse(Text.Create("def"));
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

            result = parser.Parse(Text.Create("a"));
            Assert.IsTrue(result.Value.ToString() == "a");
            Assert.IsTrue(result.Rest.ToString() == "");

            result = parser.Parse(Text.Create("b"));
            Assert.IsTrue(result == null);

            parser = grammar.Range('a', 'z');

            result = parser.Parse(Text.Empty);
            Assert.IsTrue(result == null);

            result = parser.Parse(Text.Create("a"));
            Assert.IsTrue(result.Value.ToString() == "a");
            Assert.IsTrue(result.Rest.ToString() == "");

            result = parser.Parse(Text.Create("m"));
            Assert.IsTrue(result.Value.ToString() == "m");
            Assert.IsTrue(result.Rest.ToString() == "");

            result = parser.Parse(Text.Create("z"));
            Assert.IsTrue(result.Value.ToString() == "z");
            Assert.IsTrue(result.Rest.ToString() == "");

            result = parser.Parse(Text.Create("1"));
            Assert.IsTrue(result == null);

            parser = grammar.Range('A', 'Z');

            result = parser.Parse(Text.Empty);
            Assert.IsTrue(result == null);

            result = parser.Parse(Text.Create("A"));
            Assert.IsTrue(result.Value.ToString() == "A");
            Assert.IsTrue(result.Rest.ToString() == "");

            result = parser.Parse(Text.Create("M"));
            Assert.IsTrue(result.Value.ToString() == "M");
            Assert.IsTrue(result.Rest.ToString() == "");

            result = parser.Parse(Text.Create("Z"));
            Assert.IsTrue(result.Value.ToString() == "Z");
            Assert.IsTrue(result.Rest.ToString() == "");

            result = parser.Parse(Text.Create("1"));
            Assert.IsTrue(result == null);

            parser = grammar.Range('0', '9');

            result = parser.Parse(Text.Empty);
            Assert.IsTrue(result == null);

            result = parser.Parse(Text.Create("0"));
            Assert.IsTrue(result.Value.ToString() == "0");
            Assert.IsTrue(result.Rest.ToString() == "");

            result = parser.Parse(Text.Create("5"));
            Assert.IsTrue(result.Value.ToString() == "5");
            Assert.IsTrue(result.Rest.ToString() == "");

            result = parser.Parse(Text.Create("9"));
            Assert.IsTrue(result.Value.ToString() == "9");
            Assert.IsTrue(result.Rest.ToString() == "");

            result = parser.Parse(Text.Create("a"));
            Assert.IsTrue(result == null);
        }

        #endregion //Range

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
