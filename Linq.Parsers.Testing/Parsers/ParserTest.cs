using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linq.Parsers.Testing
{
    [TestClass]
    public class ParserTest : Test
    {
        #region Where

        [TestMethod]
        public void Where()
        {
            var parser =
                from c in Char()
                where c == "a" || c == "b"
                select c;

            Assert.IsTrue(parser.Parse("a").Value == "a");
            Assert.IsTrue(parser.Parse("b").Value == "b");
            Assert.IsTrue(parser.Parse("c") == null);
        }

        #endregion //Where

        #region Select

        [TestMethod]
        public void Select()
        {
            var parser =
                from c in Char()
                select "a";

            Assert.IsTrue(parser.Parse("a").Value == "a");
            Assert.IsTrue(parser.Parse("b").Value == "a");
            Assert.IsTrue(parser.Parse("c").Value == "a");
        }

        #endregion //Select

        #region SelectMany

        [TestMethod]
        public void SelectMany()
        {
            var parser =
                from c in Char()
                from c2 in Char()
                select "_" + c + c2 + "_";

            Assert.IsTrue(parser.Parse("a") == null);
            Assert.IsTrue(parser.Parse("ab").Value == "_ab_");
            Assert.IsTrue(parser.Parse("cde").Value == "_cd_");
        }

        #endregion //SelectMany

        #region ZeroOrMore

        [TestMethod]
        public void ZeroOrMore()
        {
            var parser = Char('a').ZeroOrMore();
            Assert.IsTrue(parser.Parse("b").Value.Length == 0);
            Assert.IsTrue(parser.Parse("a").Value.Length == 1);
            Assert.IsTrue(parser.Parse("aab").Value.Length == 2);
        }

        #endregion //ZeroOrMore

        #region OneOrMore

        [TestMethod]
        public void OneOrMore()
        {
            var parser = Char('a').OneOrMore();
            Assert.IsTrue(parser.Parse("b") == null);
            Assert.IsTrue(parser.Parse("a").Value.Length == 1);
            Assert.IsTrue(parser.Parse("aab").Value.Length == 2);
        }

        #endregion //OneOrMore

        #region Optional

        [TestMethod]
        public void Optional()
        {
            var parser = Char('a').Optional();
            Assert.IsTrue(parser.Parse("a").Value[0] == "a");
            Assert.IsTrue(parser.Parse("b").Value.Length == 0);
            Assert.IsTrue(parser.Parse("ab").Value[0] == "a");
        }

        #endregion //Optional

        #region Or

        [TestMethod]
        public void Or()
        {
            var parser = Char('a') | Char('b');
            Assert.IsTrue(parser.Parse("a").Value == "a");
            Assert.IsTrue(parser.Parse("b").Value == "b");
            Assert.IsTrue(parser.Parse("c") == null);
        }

        #endregion //Or

        #region Insert

        [TestMethod]
        public void Insert()
        {
            var parser = Parser<string, string>.Insert("a");
            var result = parser.Parse("bc");
            Assert.IsTrue(result.Value == "a");
            Assert.IsTrue(result.Rest == "bc");
        }

        #endregion //Insert

        #region Utilities

        public Parser<string, string> Char()
        {
            return new Parser<string, string>(input =>
                string.IsNullOrEmpty(input) ?
                null : new ParseResult<string, string>(input.Substring(0, 1), input.Substring(1)));
        }

        public Parser<string, string> Char(char character)
        {
            return
                from c in Char()
                where c[0] == character
                select c;
        }

        #endregion //Utilities
    }
}
