using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linq.Parsers.Testing
{
    [TestClass]
    public class SetParserTest : Test
    {
        #region Where

        [TestMethod]
        public void Where()
        {
            var first = GetAnyCharParser();
            var parser =
                from c in GetAnyCharParser()
                where c == "a" || c == "b"
                select c;

            Assert.IsInstanceOfType(parser, typeof(SetParser));

            var result = parser.Parse("ac");
            Assert.IsTrue(result.Value == "a");
            Assert.IsTrue(result.Rest == "c");

            result = parser.Parse("bc");
            Assert.IsTrue(result.Value == "b");
            Assert.IsTrue(result.Rest == "c");

            result = parser.Parse("c");
            Assert.IsTrue(result == null);
        }

        #endregion //Where

        #region Or

        [TestMethod]
        public void Or()
        {
            var first = GetAnyCharParser();
            var parser = first | first;

            var result = parser.Parse("");
            Assert.IsTrue(result == null);

            result = parser.Parse("a");
            Assert.IsTrue(result.Value == "a");
            Assert.IsTrue(result.Rest == "");

            first = GetCharParser('a');
            var second = GetAnyCharParser();
            parser = first | second;

            result = parser.Parse("a");
            Assert.IsTrue(result.Value == "a");
            Assert.IsTrue(result.Rest == "");

            result = parser.Parse("b");
            Assert.IsTrue(result.Value == "b");

            first = GetCharParser('a');
            second = GetCharParser('b');
            parser = first | second;

            result = parser.Parse("");
            Assert.IsTrue(result == null);

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

        #region Not

        [TestMethod]
        public void Not()
        {
            var parser = !GetAnyCharParser();

            var result = parser.Parse("");
            Assert.IsTrue(result == null);

            result = parser.Parse("a");
            Assert.IsTrue(result == null);

            parser = !GetCharParser('a');

            result = parser.Parse("");
            Assert.IsTrue(result == null);

            result = parser.Parse("a");
            Assert.IsTrue(result == null);

            result = parser.Parse("b");
            Assert.IsTrue(result.Value == "b");
            Assert.IsTrue(result.Rest == "");

            parser = !(GetCharParser('a') | GetCharParser('b'));

            result = parser.Parse("");
            Assert.IsTrue(result == null);

            result = parser.Parse("a");
            Assert.IsTrue(result == null);

            result = parser.Parse("b");
            Assert.IsTrue(result == null);

            result = parser.Parse("c");
            Assert.IsTrue(result.Value == "c");
            Assert.IsTrue(result.Rest == "");
        }

        #endregion //Not

        #region Utilities

        private SetParser GetAnyCharParser()
        {
            return new TestGrammar().Char();
        }

        private SetParser GetCharParser(char character)
        {
            return new TestGrammar().Char(character);
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
