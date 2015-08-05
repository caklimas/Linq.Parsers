using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linq.Parsers.Testing
{
    [TestClass]
    public class ParserExtensionsTest : Test
    {
        #region When

        [TestMethod]
        public void When()
        {
            var parser =
                (from c in Char()
                 where c == "a"
                 select c)
                .When(input => 
                    input.Length > 1 && 
                    input[1] == ' ');

            var result = parser.Parse("a ");
            Assert.IsTrue(result.Value == "a");
            Assert.IsTrue(result.Rest == " ");

            result = parser.Parse("a b");
            Assert.IsTrue(result.Value == "a");
            Assert.IsTrue(result.Rest == " b");

            result = parser.Parse("a");
            Assert.IsTrue(result == null);

            result = parser.Parse("ab");
            Assert.IsTrue(result == null);

            parser =
                (from c in Char()
                 where c == "a"
                 select c)
                .When(new InputPrecedesWhiteSpace());

            result = parser.Parse("a ");
            Assert.IsTrue(result.Value == "a");
            Assert.IsTrue(result.Rest == " ");

            result = parser.Parse("a b");
            Assert.IsTrue(result.Value == "a");
            Assert.IsTrue(result.Rest == " b");

            result = parser.Parse("a");
            Assert.IsTrue(result == null);

            result = parser.Parse("ab");
            Assert.IsTrue(result == null);
        }

        #endregion //When

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

            Assert.IsTrue(parser.Parse("") == null);
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

            var result = parser.Parse("b");
            Assert.IsTrue(result.Value.Count() == 0);
            Assert.IsTrue(result.Rest == "b");

            result = parser.Parse("a");
            Assert.IsTrue(result.Value.Count() == 1);
            Assert.IsTrue(result.Value.ElementAt(0) == "a");
            Assert.IsTrue(result.Rest == "");

            result = parser.Parse("aaab");
            Assert.IsTrue(result.Value.Count() == 3);
            for (var i = 0; i < 3; i++)
                Assert.IsTrue(result.Value.ElementAt(i) == "a");
            Assert.IsTrue(result.Rest == "b");
        }

        #endregion //ZeroOrMore

        #region OneOrMore

        [TestMethod]
        public void OneOrMore()
        {
            var parser = Char('a').OneOrMore();

            var result = parser.Parse("b");
            Assert.IsTrue(result == null);

            result = parser.Parse("a");
            Assert.IsTrue(result.Value.Count() == 1);
            Assert.IsTrue(result.Value.ElementAt(0) == "a");
            Assert.IsTrue(result.Rest == "");

            result = parser.Parse("aaab");
            Assert.IsTrue(result.Value.Count() == 3);
            for (var i = 0; i < 3; i++)
                Assert.IsTrue(result.Value.ElementAt(i) == "a");
            Assert.IsTrue(result.Rest == "b");
        }

        #endregion //OneOrMore

        #region Optional

        [TestMethod]
        public void Optional()
        {
            var parser = Char('a').Optional();
            Assert.IsTrue(parser.Parse("a").Value.ElementAt(0) == "a");
            Assert.IsTrue(parser.Parse("b").Value.Count() == 0);
            Assert.IsTrue(parser.Parse("ab").Value.ElementAt(0) == "a");
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
            return new Parser<string, string, CharParseCommand>(
                new CharParseCommand());
        }

        public Parser<string, string> Char(char character)
        {
            return
                from c in Char()
                where c[0] == character
                select c;
        }

        internal struct CharParseCommand : IParseCommand<string, string>
        {
            public ParseResult<string, string> Execute(string input)
            {
                return
                    string.IsNullOrEmpty(input) ?
                    null : new ParseResult<string, string>(input.Substring(0, 1), input.Substring(1));
            }
        }

        #endregion //Utilities

        #region Test Classes

        public class InputPrecedesWhiteSpace : IParsePredicate<string>
        {
            public bool Execute(string input)
            {
                return
                    input.Length > 1 &&
                    input[1] == ' ';
            }
        }

        #endregion //Test Classes
    }
}
