using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linq.Parsers.Json.Testing
{
    [TestClass]
    public class JsonGrammarTest
    {
        #region Fields

        private JsonGrammar grammar = new JsonGrammar();

        #endregion //Fields

        #region Terminals

        #region CloseBrace

        [TestMethod]
        public void CloseBrace()
        {
            var parser = grammar.CloseBrace;

            var result = parser.Parse("   }   a");
            Assert.IsTrue(result.Value.LeftTrivia == "   ");
            Assert.IsTrue(result.Value.Value == "}");
            Assert.IsTrue(result.Value.RightTrivia == "   ");
            Assert.IsTrue(result.Rest == "a");
        }

        #endregion //CloseBrace

        #region CloseBracket

        [TestMethod]
        public void CloseBracket()
        {
            var parser = grammar.CloseBracket;

            var result = parser.Parse("   ]   a");
            Assert.IsTrue(result.Value.LeftTrivia == "   ");
            Assert.IsTrue(result.Value.Value == "]");
            Assert.IsTrue(result.Value.RightTrivia == "   ");
            Assert.IsTrue(result.Rest == "a");
        }

        #endregion //CloseBracket

        #region Colon

        [TestMethod]
        public void Colon()
        {
            var parser = grammar.Colon;

            var result = parser.Parse("   :   a");
            Assert.IsTrue(result.Value.LeftTrivia == "   ");
            Assert.IsTrue(result.Value.Value == ":");
            Assert.IsTrue(result.Value.RightTrivia == "   ");
            Assert.IsTrue(result.Rest == "a");
        }

        #endregion //Colon

        #region Comma

        [TestMethod]
        public void Comma()
        {
            var parser = grammar.Comma;

            var result = parser.Parse("   ,   a");
            Assert.IsTrue(result.Value.LeftTrivia == "   ");
            Assert.IsTrue(result.Value.Value == ",");
            Assert.IsTrue(result.Value.RightTrivia == "   ");
            Assert.IsTrue(result.Rest == "a");
        }

        #endregion //Comma

        #region DecimalPoint

        [TestMethod]
        public void DecimalPoint()
        {
            var parser = grammar.DecimalPoint;

            var result = parser.Parse(".");
            Assert.IsTrue(result.Value.Value == ".");
        }

        #endregion //DecimalPoint

        #region ExponentiationLetter

        [TestMethod]
        public void ExponentiationLetter()
        {
            var parser = grammar.ExponentiationLetter;

            var result = parser.Parse("e");
            Assert.IsTrue(result.Value.Value == "e");

            result = parser.Parse("E");
            Assert.IsTrue(result.Value.Value == "E");
        }

        #endregion //ExponentiationLetter

        #region False

        [TestMethod]
        public void False()
        {
            var parser = grammar.False;

            var result = parser.Parse("false");
            Assert.IsTrue(result.Value.Value == "false");
        }

        #endregion //ExponentiationLetter

        #region LeftQuote

        [TestMethod]
        public void LeftQuote()
        {
            var leftQuoteParser = grammar.LeftQuote;

            var result = leftQuoteParser.Parse("        \"a");
            Assert.IsTrue(result.Value.LeftTrivia == "        ");
            Assert.IsTrue(result.Value.Value == "\"");
            Assert.IsTrue(result.Rest == "a");
        }

        #endregion //LeftQuote

        #region Negative

        [TestMethod]
        public void Negative()
        {
            var parser = grammar.Negative;

            var result = parser.Parse("        -a");
            Assert.IsTrue(result.Value.LeftTrivia == "        ");
            Assert.IsTrue(result.Value.Value == "-");
            Assert.IsTrue(result.Rest == "a");
        }

        #endregion //Negative

        #region NullParser

        [TestMethod]
        public void NullParser()
        {
            var parser = grammar.NullParser;

            var result = parser.Parse("null");
            Assert.IsTrue(result.Value.Value == "null");
        }

        #endregion //NullParser

        #region OpenBrace

        [TestMethod]
        public void OpenBrace()
        {
            var parser = grammar.OpenBrace;

            var result = parser.Parse("   {   a");
            Assert.IsTrue(result.Value.LeftTrivia == "   ");
            Assert.IsTrue(result.Value.Value == "{");
            Assert.IsTrue(result.Value.RightTrivia == "   ");
            Assert.IsTrue(result.Rest == "a");
        }

        #endregion //OpenBrace

        #region OpenBracket

        [TestMethod]
        public void OpenBracket()
        {
            var parser = grammar.OpenBracket;

            var result = parser.Parse("   [   a");
            Assert.IsTrue(result.Value.LeftTrivia == "   ");
            Assert.IsTrue(result.Value.Value == "[");
            Assert.IsTrue(result.Value.RightTrivia == "   ");
            Assert.IsTrue(result.Rest == "a");
        }

        #endregion //OpenBracket

        #region RightQuote

        [TestMethod]
        public void RightQuote()
        {
            var leftQuoteParser = grammar.RightQuote;

            var result = leftQuoteParser.Parse("\"        a");
            Assert.IsTrue(result.Value.Value == "\"");
            Assert.IsTrue(result.Value.RightTrivia == "        ");
            Assert.IsTrue(result.Rest == "a");
        }

        #endregion //RightQuote

        #region True

        [TestMethod]
        public void True()
        {
            var parser = grammar.True;

            var result = parser.Parse("true");
            Assert.IsTrue(result.Value.Value == "true");
        }

        #endregion //True

        #endregion //Terminals

        #region Non-Terminals

        #region Characters

        [TestMethod]
        public void Characters()
        {
            var charactersParser = grammar.Characters;
            
            var result = charactersParser.Parse("abcdef");
            Assert.IsTrue(result.Value.Value == "abcdef");

            result = charactersParser.Parse("123456");
            Assert.IsTrue(result.Value.Value == "123456");

            result = charactersParser.Parse("\\\"");
            Assert.IsTrue(result.Value.Value == "\\\"");

            result = charactersParser.Parse("\uFFDB\uFFDB\uFFDB");
            Assert.IsTrue(result.Value.Value == "\uFFDB\uFFDB\uFFDB");

            result = charactersParser.Parse("\"");
            Assert.IsTrue(result == null);
        }

        #endregion //Characters

        #region Exponentiation

        [TestMethod]
        public void Exponentiation()
        {
            var parser = grammar.Exponentiation;

            var result = parser.Parse("e59");
            Assert.IsTrue(result.Value.Value == "e59");

            result = parser.Parse("E59");
            Assert.IsTrue(result.Value.Value == "E59");

            result = parser.Parse("e-59");
            Assert.IsTrue(result.Value.Value == "e-59");

            result = parser.Parse("E-59");
            Assert.IsTrue(result.Value.Value == "E-59");
        }

        #endregion //Exponentiation

        #region Fraction

        [TestMethod]
        public void Fraction()
        {
            var parser = grammar.Fraction;

            var result = parser.Parse(".12345");
            Assert.IsTrue(result.Value.ToString() == ".12345");

            result = parser.Parse(".012345");
            Assert.IsTrue(result.Value.ToString() == ".012345");
        }

        #endregion //Fraction

        #region FractionNumber

        [TestMethod]
        public void FractionNumber()
        {
            var parser = grammar.FractionNumber;
            
            var result = parser.Parse("12345");
            Assert.IsTrue(result.Value.Value == "12345");
        }

        #endregion //FractionNumber

        #region Integer

        [TestMethod]
        public void Integer()
        {
            var parser = grammar.Integer;

            var result = parser.Parse("12345");
            Assert.IsTrue(result.Value.Value == "12345");

            result = parser.Parse("-12345");
            Assert.IsTrue(result.Value.Value == "-12345");
        }

        #endregion //Integer

        #region JsonArray

        [TestMethod]
        public void JsonArray()
        {
            var parser = grammar.JsonArray;

            var result = parser.Parse("[]");
            Assert.IsTrue(result.Value.ToString() == "[]");

            result = parser.Parse("[1, 2.45, 3e3   , \"Hi\", true, false, null]");
            Assert.IsTrue(result.Value.ToString() == "[1, 2.45, 3e3   , \"Hi\", true, false, null]");
        }

        #endregion //JsonArray

        #region JsonArrayElement

        [TestMethod]
        public void JsonArrayElement()
        {
            var parser = grammar.JsonArrayElement;

            var result = parser.Parse("true");
            Assert.IsTrue(result.Value.ToString() == "true");

            result = parser.Parse("false");
            Assert.IsTrue(result.Value.ToString() == "false");

            result = parser.Parse("null");
            Assert.IsTrue(result.Value.ToString() == "null");

            result = parser.Parse("12345");
            Assert.IsTrue(result.Value.ToString() == "12345");

            result = parser.Parse("\"Hello\"");
            Assert.IsTrue(result.Value.ToString() == "\"Hello\"");
        }

        #endregion //JsonArrayElement

        #region JsonArrayElements

        [TestMethod]
        public void JsonArrayElements()
        {
            var parser = grammar.JsonArrayElements;

            var result = parser.Parse("12345,\"Hello\",true,false,null");
            Assert.IsTrue(result.Value.ToString() == "12345,\"Hello\",true,false,null");
        }

        #endregion //JsonArrayElements

        #region MemberValue

        [TestMethod]
        public void MemberValue()
        {
            var parser = grammar.MemberValue;

            var result = parser.Parse("true");
            Assert.IsTrue(result.Value.ToString() == "true");

            result = parser.Parse("false");
            Assert.IsTrue(result.Value.ToString() == "false");

            result = parser.Parse("null");
            Assert.IsTrue(result.Value.ToString() == "null");

            result = parser.Parse("12345");
            Assert.IsTrue(result.Value.ToString() == "12345");

            result = parser.Parse("\"Hello\"");
            Assert.IsTrue(result.Value.ToString() == "\"Hello\"");

            result = parser.Parse("[1,2,3,4,5,\"Hi\"]");
            Assert.IsTrue(result.Value.ToString() == "[1,2,3,4,5,\"Hi\"]");

            result = parser.Parse("{ \"First\" : 5, \"Second\" : [1,2,3,4,5, false], \"Third\" : true }");
            Assert.IsTrue(result.Value.ToString() == "{ \"First\" : 5, \"Second\" : [1,2,3,4,5, false], \"Third\" : true }");
        }

        #endregion //MemberValue

        #region Number

        [TestMethod]
        public void Number()
        {
            var parser = grammar.Number;

            var result = parser.Parse("1234e1234");
            Assert.IsTrue(result.Value.Value == "1234e1234");

            result = parser.Parse("1234.1234");
            Assert.IsTrue(result.Value.Value == "1234.1234");

            result = parser.Parse("1234.1234e5");
            Assert.IsTrue(result.Value.Value == "1234.1234e5");

            result = parser.Parse("-1234.1234e5");
            Assert.IsTrue(result.Value.Value == "-1234.1234e5");

            result = parser.Parse("1234.1234e-5");
            Assert.IsTrue(result.Value.Value == "1234.1234e-5");

            result = parser.Parse("-1234.1234e-5");
            Assert.IsTrue(result.Value.Value == "-1234.1234e-5");
        }

        #endregion //NumberParser

        #region Object

        [TestMethod]
        public void Object()
        {
            var parser = grammar.Object;

            var result = parser.Parse("{}");
            Assert.IsTrue(result.Value.ToString() == "{}");

            result = parser.Parse("{ \"First\" : 5 }");
            Assert.IsTrue(result.Value.ToString() == "{ \"First\" : 5 }");

            result = parser.Parse("{ \"First\" : 5, \"Second\" : [1,2,3,4,5, false], \"Third\" : true, \"Fourth\": { \"First\" : 5, \"Second\" : [1,2,3,4,5, false], \"Third\" : true }}");
            Assert.IsTrue(result.Value.ToString() == "{ \"First\" : 5, \"Second\" : [1,2,3,4,5, false], \"Third\" : true, \"Fourth\": { \"First\" : 5, \"Second\" : [1,2,3,4,5, false], \"Third\" : true }}");
        }

        #endregion //Object

        #region ObjectMember

        [TestMethod]
        public void ObjectMember()
        {
            var parser = grammar.ObjectMember;

            var result = parser.Parse("\"Name\" : 5");
            Assert.IsTrue(result.Value.ToString() == "\"Name\" : 5");

            result = parser.Parse("\"\uffde\" : [1, 2, 3, 4]");
            Assert.IsTrue(result.Value.ToString() == "\"\uffde\" : [1, 2, 3, 4]");

            result = parser.Parse("\"5\" : \"Test\"");
            Assert.IsTrue(result.Value.ToString() == "\"5\" : \"Test\"");
        }

        #endregion //ObjectMember

        #region ObjectMembers

        [TestMethod]
        public void ObjectMembers()
        {
            var parser = grammar.ObjectMembers;

            var result = parser.Parse("\"First\" : 5, \"Second\":\"Hello\",\"Third\" :true, \"Fourth\": null, \"\uffde\" : [1, 2, 3, \"Hi\"]");
            Assert.IsTrue(result.Value.ToString() == "\"First\" : 5, \"Second\":\"Hello\",\"Third\" :true, \"Fourth\": null, \"\uffde\" : [1, 2, 3, \"Hi\"]");
        }

        #endregion //ObjectMembers

        #region String

        [TestMethod]
        public void String()
        {
            var stringParser = grammar.String;

            var result = stringParser.Parse("\"Hello world!\"Rest");
            Assert.IsTrue(result.Value.Value == "Hello world!");
            Assert.IsTrue(result.Rest == "Rest");

            result = stringParser.Parse("\"123 Hello world!\"Rest");
            Assert.IsTrue(result.Value.Value == "123 Hello world!");
            Assert.IsTrue(result.Rest == "Rest");

            result = stringParser.Parse("\"123 Hello world!\"Rest");
            Assert.IsTrue(result.Value.Value == "123 Hello world!");
            Assert.IsTrue(result.Rest == "Rest");

            result = stringParser.Parse("\"   123 Hello world!   \"Rest");
            Assert.IsTrue(result.Value.Value == "   123 Hello world!   ");
            Assert.IsTrue(result.Rest == "Rest");

            result = stringParser.Parse("   \"   123 Hello world!   \"   Rest");
            Assert.IsTrue(result.Value.Value == "   123 Hello world!   ");
            Assert.IsTrue(result.Rest == "Rest");
        }

        #endregion //String

        #region TailJsonArrayElement

        [TestMethod]
        public void TailJsonArrayElement()
        {
            var parser = grammar.TailJsonArrayElement;

            var result = parser.Parse(",59");
            Assert.IsTrue(result.Value.ToString() == ",59");

            result = parser.Parse(", \"Hello\"");
            Assert.IsTrue(result.Value.ToString() == ", \"Hello\"");
        }

        #endregion //TailJsonArrayElement

        #region TailObjectMember

        [TestMethod]
        public void TailObjectMember()
        {
            var parser = grammar.TailObjectMember;

            var result = parser.Parse(", \"Name\" : 5");
            Assert.IsTrue(result.Value.ToString() == ", \"Name\" : 5");

            result = parser.Parse(", \"\uffde\" : [1, 2, 3, 4]");
            Assert.IsTrue(result.Value.ToString() == ", \"\uffde\" : [1, 2, 3, 4]");

            result = parser.Parse(", \"5\" : \"Test\"");
            Assert.IsTrue(result.Value.ToString() == ", \"5\" : \"Test\"");
        }

        #endregion //TailObjectMember

        #endregion //Non-Terminals
    }
}
