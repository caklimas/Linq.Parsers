using Linq.Parsers.Json.Visitor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linq.Parsers.Json.Syntax;

namespace Linq.Parsers.Json.Testing
{
    [TestClass]
    public class JsonSyntaxVisitorTest
    {
        #region Fields
        
        private string json = @"{ 
            ""Property"" : true,
            ""Property"" : false,
            ""Property"" : null,
            ""Property"" : [0, 0, 0],
            ""Property"" : -0.0e0,
            ""Property"" : ""Property""
        }";

        #endregion //Fields

        #region Methods

        [TestMethod]
        public void VisitorTest()
        {
            var jsonGrammar = new JsonGrammar();
            var result = jsonGrammar.Parser.Parse(json).Value;
            var testVisitor = new TestVisitor();

            testVisitor.Visit(result);
            foreach (var property in testVisitor.GetType().GetFields().Where(f => f.FieldType == typeof(bool)))
                Assert.IsTrue((bool)property.GetValue(testVisitor));
        }

        #endregion //Methods

        #region Test Classes

        private class TestVisitor : JsonSyntaxVisitor
        {
            public bool visitedCharacters;
            protected override Characters VisitCharacters(Characters node)
            {
                visitedCharacters = true;
                Assert.IsTrue(node.Value == "Property");
                return base.VisitCharacters(node);
            }

            public bool visitedCloseBrace;
            protected override CloseBrace VisitCloseBrace(CloseBrace node)
            {
                visitedCloseBrace = true;
                Assert.IsTrue(node.Value == "}");
                return base.VisitCloseBrace(node);
            }

            public bool visitedCloseBracket;
            protected override CloseBracket VisitCloseBracket(CloseBracket node)
            {
                visitedCloseBracket = true;
                Assert.IsTrue(node.Value == "]");
                return base.VisitCloseBracket(node);
            }

            public bool visitedColon;
            protected override Colon VisitColon(Colon node)
            {
                visitedColon = true;
                Assert.IsTrue(node.Value == ":");
                return base.VisitColon(node);
            }

            public bool visitedComma;
            protected override Comma VisitComma(Comma node)
            {
                visitedComma = true;
                Assert.IsTrue(node.Value == ",");
                return base.VisitComma(node);
            }

            public bool visitedDecimalPoint;
            protected override DecimalPoint VisitDecimalPoint(DecimalPoint node)
            {
                visitedDecimalPoint = true;
                Assert.IsTrue(node.Value == ".");
                return base.VisitDecimalPoint(node);
            }

            public bool visitedExponentiation;
            protected override Exponentiation VisitExponentiation(Exponentiation node)
            {
                visitedExponentiation = true;
                Assert.IsTrue(node.Value == "e0");
                return base.VisitExponentiation(node);
            }

            public bool visitedExponentiationLetter;
            protected override ExponentiationLetter VisitExponentiationLetter(ExponentiationLetter node)
            {
                visitedExponentiationLetter = true;
                Assert.IsTrue(node.Value == "e");
                return base.VisitExponentiationLetter(node);
            }

            public bool visitedFraction;
            protected override Fraction VisitFraction(Fraction node)
            {
                visitedFraction = true;
                Assert.IsTrue(node.ToString() == ".0");
                return base.VisitFraction(node);
            }

            public bool visitedFractionNumber;
            protected override FractionNumber VisitFractionNumber(FractionNumber node)
            {
                visitedFractionNumber = true;
                Assert.IsTrue(node.ToString() == "0");
                return base.VisitFractionNumber(node);
            }

            public bool visitedFalse;
            protected override False VisitFalse(False node)
            {
                visitedFalse = true;
                Assert.IsTrue(node.ToString() == "false");
                return base.VisitFalse(node);
            }

            public bool visitedInteger;
            protected override Integer VisitInteger(Integer node)
            {
                visitedInteger = true;
                Assert.IsTrue(node.ToString() == "0" || node.ToString() == "-0");
                return base.VisitInteger(node);
            }

            public bool visitedJsonArray;
            protected override JsonArray VisitJsonArray(JsonArray node)
            {
                visitedJsonArray = true;
                Assert.IsTrue(node.ToString() == "[0, 0, 0]");
                return base.VisitJsonArray(node);
            }

            public bool visitedJsonArrayElement;
            protected override JsonArrayElement VisitJsonArrayElement(JsonArrayElement node)
            {
                visitedJsonArrayElement = true;
                Assert.IsTrue(node.ToString() == "0");
                return base.VisitJsonArrayElement(node);
            }

            public bool visitedJsonArrayElements;
            protected override JsonArrayElements VisitJsonArrayElements(JsonArrayElements node)
            {
                visitedJsonArrayElements = true;
                Assert.IsTrue(node.ToString() == "0, 0, 0");
                return base.VisitJsonArrayElements(node);
            }

            public bool visitedJsonObject;
            protected override JsonObject VisitJsonObject(JsonObject node)
            {
                visitedJsonObject = true;
                Assert.IsTrue(node[1].GetChildren().Count() == 6);
                return base.VisitJsonObject(node);
            }

            public bool visitedLeftQuote;
            protected override LeftQuote VisitLeftQuote(LeftQuote node)
            {
                visitedLeftQuote = true;
                Assert.IsTrue(node.Value == "\"");
                return base.VisitLeftQuote(node);
            }

            public bool visitedMemberValue;
            protected override MemberValue VisitMemberValue(MemberValue node)
            {
                visitedMemberValue = true;
                return base.VisitMemberValue(node);
            }

            public bool visitedNegative;
            protected override Negative VisitNegative(Negative node)
            {
                visitedNegative = true;
                Assert.IsTrue(node.Value == "-");
                return base.VisitNegative(node);
            }

            public bool visitedNumber;
            protected override Number VisitNumber(Number node)
            {
                visitedNumber = true;
                Assert.IsTrue(node.ToString() == "0" || node.ToString() == "-0.0e0");
                return base.VisitNumber(node);
            }

            public bool visitedNull;
            protected override Null VisitNull(Null node)
            {
                visitedNull = true;
                Assert.IsTrue(node.Value == "null");
                return base.VisitNull(node);
            }

            public bool visitedObjectMember;
            protected override ObjectMember VisitObjectMember(ObjectMember node)
            {
                visitedObjectMember = true;
                Assert.IsTrue(node.ToString().StartsWith("\"Property\" :"));
                return base.VisitObjectMember(node);
            }

            public bool visitedObjectMembers;
            protected override ObjectMembers VisitObjectMembers(ObjectMembers node)
            {
                visitedObjectMembers = true;
                Assert.IsTrue(node.GetChildren().Count() == 6);
                return base.VisitObjectMembers(node);
            }

            public bool visitedOpenBrace;
            protected override OpenBrace VisitOpenBrace(OpenBrace node)
            {
                visitedOpenBrace = true;
                Assert.IsTrue(node.Value == "{");
                return base.VisitOpenBrace(node);
            }

            public bool visitedOpenBracket;
            protected override OpenBracket VisitOpenBracket(OpenBracket node)
            {
                visitedOpenBracket = true;
                Assert.IsTrue(node.Value == "[");
                return base.VisitOpenBracket(node);
            }

            public bool visitedTailJsonArrayElement;
            protected override TailJsonArrayElement VisitTailJsonArrayElement(TailJsonArrayElement node)
            {
                visitedTailJsonArrayElement = true;
                Assert.IsTrue(node.ToString() == ", 0");
                return base.VisitTailJsonArrayElement(node);
            }

            public bool visitedRightQuote;
            protected override RightQuote VisitRightQuote(RightQuote node)
            {
                visitedRightQuote = true;
                Assert.IsTrue(node.Value == "\"");
                return base.VisitRightQuote(node);
            }

            public bool visitedString;
            protected override StringToken VisitString(StringToken node)
            {
                visitedString = true;
                Assert.IsTrue(node.Value == "Property");
                return base.VisitString(node);
            }

            public bool visitedTailObjectMember;
            protected override TailObjectMember VisitTailObjectMember(TailObjectMember node)
            {
                visitedTailObjectMember = true;
                Assert.IsTrue(node.ToString().StartsWith(","));
                return base.VisitTailObjectMember(node);
            }

            public bool visitedTrue;
            protected override True VisitTrue(True node)
            {
                visitedTrue = true;
                Assert.IsTrue(node.Value == "true");
                return base.VisitTrue(node);
            }
        }

        #endregion //Test Classes
    }
}
