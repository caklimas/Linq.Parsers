using Linq.Parsers.Json.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq.Parsers.Json
{
    public class JsonGrammar : Grammar<JsonSyntaxNode>
    {
        #region Terminals

        #region CloseBrace

        private Parser<Text, CloseBrace> closeBrace;
        public virtual Parser<Text, CloseBrace> CloseBrace
        {
            get
            {
                if (this.closeBrace == null)
                {
                    this.closeBrace =
                        from result in Char('}').WithTrivia()
                        select new CloseBrace(result.LeftTrivia, result.Value, result.RightTrivia);
                }

                return this.closeBrace;
            }
        }

        #endregion //ColonBrace

        #region CloseBracket

        private Parser<Text, CloseBracket> closeBracket;
        public virtual Parser<Text, CloseBracket> CloseBracket
        {
            get
            {
                if (this.closeBracket == null)
                {
                    this.closeBracket =
                                from result in Char(']').WithTrivia()
                                select new CloseBracket(result.LeftTrivia, result.Value, result.RightTrivia);
                }

                return this.closeBracket;
            }
        }

        #endregion //CloseBracketParser

        #region Colon

        private Parser<Text, Colon> colon;
        public virtual Parser<Text, Colon> Colon
        {
            get
            {
                if (this.colon == null)
                {
                    this.colon =
                        from result in Char(':').WithTrivia()
                        select new Colon(result.LeftTrivia, result.Value, result.RightTrivia);
                }

                return this.colon;
            }
        }

        #endregion //Colon

        #region Comma

        private Parser<Text, Comma> comma;
        public virtual Parser<Text, Comma> Comma
        {
            get
            {
                if (this.comma == null)
                {
                    this.comma =
                        from result in Char(',').WithTrivia()
                        select new Comma(result.LeftTrivia, result.Value, result.RightTrivia);
                }

                return this.comma;
            }
        }

        #endregion //Comma

        #region DecimalPoint

        private Parser<Text, DecimalPoint> decimalPoint;
        public virtual Parser<Text, DecimalPoint> DecimalPoint
        {
            get
            {
                if (this.decimalPoint == null)
                {
                    this.decimalPoint =
                        from result in Char('.')
                        select new DecimalPoint(result);
                }

                return this.decimalPoint;
            }
        }

        #endregion //DecimalPoint

        #region ExponentiationLetter

        private Parser<Text, ExponentiationLetter> exponentiationLetter;
        public virtual Parser<Text, ExponentiationLetter> ExponentiationLetter
        {
            get
            {
                if (this.exponentiationLetter == null)
                {
                    this.exponentiationLetter =
                        from result in Set("eE")
                        select new ExponentiationLetter(result);
                }

                return this.exponentiationLetter;
            }
        }

        #endregion //ExponentiationLetter

        #region False

        private Parser<Text, False> falseParser;
        public virtual Parser<Text, False> False
        {
            get
            {
                if (this.falseParser == null)
                {
                    this.falseParser =
                                from result in this.String("false").WithTrivia()
                                select new False(result.LeftTrivia, result.Value, result.RightTrivia);
                }

                return this.falseParser;
            }
        }

        #endregion //False

        #region LeftQuote

        private Parser<Text, LeftQuote> leftQuote;
        public virtual Parser<Text, LeftQuote> LeftQuote
        {
            get
            {
                if (this.leftQuote == null)
                {
                    this.leftQuote =
                        from result in Char('\"').WithLeftTrivia()
                        select new LeftQuote(result.LeftTrivia, result.Value);
                }

                return this.leftQuote;
            }
        }

        #endregion //LeftQuoteParser

        #region Negative

        private Parser<Text, Negative> negative;
        public virtual Parser<Text, Negative> Negative
        {
            get
            {
                if (this.negative == null)
                {
                    this.negative =
                        from result in Char('-').WithLeftTrivia()
                        select new Negative(result.LeftTrivia, result.Value);
                }

                return this.negative;
            }
        }

        #endregion //Negative

        #region NullParser

        private Parser<Text, Null> nullParser;
        public virtual Parser<Text, Null> NullParser
        {
            get
            {
                if (this.nullParser == null)
                {
                    this.nullParser =
                                from result in this.String("null").WithTrivia()
                                select new Null(result.LeftTrivia, result.Value, result.RightTrivia);
                }

                return this.nullParser;
            }
        }

        #endregion //NullParser

        #region OpenBrace

        private Parser<Text, OpenBrace> openBrace;
        public virtual Parser<Text, OpenBrace> OpenBrace
        {
            get
            {
                if (this.openBrace == null)
                {
                    this.openBrace =
                        from result in Char('{').WithTrivia()
                        select new OpenBrace(result.LeftTrivia, result.Value, result.RightTrivia);
                }

                return this.openBrace;
            }
        }

        #endregion //OpenBrace

        #region OpenBracket

        private Parser<Text, OpenBracket> openBracket;
        public virtual Parser<Text, OpenBracket> OpenBracket
        {
            get
            {
                if (this.openBracket == null)
                {
                    this.openBracket =
                                from result in Char('[').WithTrivia()
                                select new OpenBracket(result.LeftTrivia, result.Value, result.RightTrivia);
                }

                return this.openBracket;
            }
        }

        #endregion //OpenBracket

        #region RightQuote

        private Parser<Text, RightQuote> rightQuote;
        public virtual Parser<Text, RightQuote> RightQuote
        {
            get
            {
                if (this.rightQuote == null)
                {
                    this.rightQuote =
                        from result in Char('\"').WithRightTrivia()
                        select new RightQuote(result.Value, result.RightTrivia);
                }

                return this.rightQuote;
            }
        }

        #endregion //LeftQuote

        #region True

        private Parser<Text, True> trueParser;
        public virtual Parser<Text, True> True
        {
            get
            {
                if (this.trueParser == null)
                {
                    this.trueParser =
                                from result in this.String("true").WithTrivia()
                                select new True(result.LeftTrivia, result.Value, result.RightTrivia);
                }

                return this.trueParser;
            }
        }

        #endregion //True

        #endregion //Terminals

        #region Non-Terminals

        #region Characters

        private Parser<Text, Characters> characters;
        public virtual Parser<Text, Characters> Characters
        {
            get
            {
                if (this.characters == null)
                {
                    this.characters =
                        from characters in
                            (Range('\u0020', '\u0021') |
                            Range('\u0023', '\u005B') |
                            Range('\u005D', '\u007E') |
                            Range('\u00A0', '\uFFFD') |
                            (Char('\\') + Set("\"\\/bfnrt"))).OneOrMore()
                        select new Characters(characters);
                }

                return this.characters;
            }
        }

        #endregion //Characters

        #region Exponentiation

        private Parser<Text, Exponentiation> exponentiation;
        public virtual Parser<Text, Exponentiation> Exponentiation
        {
            get
            {
                if (this.exponentiation == null)
                {
                    this.exponentiation =
                        from exponentiationLetter in this.ExponentiationLetter
                        from zeroPaddedInteger in this.ZeroPaddedInteger
                        select new Exponentiation(exponentiationLetter, zeroPaddedInteger);
                }

                return this.exponentiation;
            }
        }

        #endregion //Exponentiation

        #region Fraction

        private Parser<Text, Fraction> fraction;
        public virtual Parser<Text, Fraction> Fraction
        {
            get
            {
                if (this.fraction == null)
                {
                    this.fraction =
                        from decimalPoint in this.DecimalPoint
                        from fractionNumber in this.FractionNumber
                        select new Fraction(decimalPoint, fractionNumber);
                }

                return this.fraction;
            }
        }

        #endregion //Fraction

        #region FractionNumber

        private Parser<Text, FractionNumber> fractionNumber;
        public virtual Parser<Text, FractionNumber> FractionNumber
        {
            get
            {
                if (this.fractionNumber == null)
                {
                    this.fractionNumber =
                        from result in this.Digit.OneOrMore()
                        select new FractionNumber(result);
                }

                return this.fractionNumber;
            }
        }

        #endregion //FractionNumber

        #region Integer

        private Parser<Text, Integer> integer;
        public virtual Parser<Text, Integer> Integer
        {
            get
            {
                if (this.integer == null)
                {
                    this.integer =
                        from negative in this.Negative.Optional()
                        from number in Char('0') | (Range('1', '9') + this.Digit.ZeroOrMore())
                        select negative.Any() ? 
                            new Integer(negative.FirstOrDefault(), number) :
                            new Integer(number);
                }

                return this.integer;
            }
        }

        #endregion //Integer

        #region JsonArray

        private Parser<Text, JsonArray> jsonArray;
        public virtual Parser<Text, JsonArray> JsonArray
        {
            get
            {
                if (this.jsonArray == null)
                {
                    this.jsonArray =
                        from openBracket in this.OpenBracket
                        from arrayElements in this.JsonArrayElements.Optional()
                        from closeBracket in this.CloseBracket
                        select new JsonArray(openBracket, arrayElements.FirstOrDefault(), closeBracket);
                }

                return this.jsonArray;
            }
        }

        #endregion //JsonArray

        #region JsonArrayElement

        private Parser<Text, JsonArrayElement> jsonArrayElement;
        public virtual Parser<Text, JsonArrayElement> JsonArrayElement
        {
            get
            {
                if (this.jsonArrayElement == null)
                {
                    this.jsonArrayElement =
                        from member in this.MemberValue
                        select new JsonArrayElement(member);
                }

                return this.jsonArrayElement;
            }
        }

        #endregion //JsonArrayElement

        #region JsonArrayElements

        private Parser<Text, JsonArrayElements> jsonArrayElements;
        public virtual Parser<Text, JsonArrayElements> JsonArrayElements
        {
            get
            {
                if (this.jsonArrayElements == null)
                {
                    this.jsonArrayElements =
                        from arrayElement in this.JsonArrayElement
                        from arrayElements in this.TailJsonArrayElement.ZeroOrMore()
                        select new JsonArrayElements(arrayElement, arrayElements);
                }

                return this.jsonArrayElements;
            }
        }

        #endregion //JsonArrayElements

        #region Object

        private Parser<Text, JsonObject> jsonObject;
        public virtual Parser<Text, JsonObject> Object
        {
            get
            {
                if (this.jsonObject == null)
                {
                    this.jsonObject =
                        from openBrace in this.OpenBrace
                        from objectMembers in this.ObjectMembers.Optional()
                        from closeBrace in this.CloseBrace
                        select new JsonObject(openBrace, objectMembers.FirstOrDefault(), closeBrace);
                }

                return this.jsonObject;
            }
        }

        #endregion //Object

        #region MemberValue

        private Parser<Text, MemberValue> memberValue;
        public virtual Parser<Text, MemberValue> MemberValue
        {
            get
            {
                if (this.memberValue == null)
                {
                    var numberMemberValue =
                        from number in this.Number
                        select new MemberValue(number);

                    var stringMemberValue =
                        from str in this.String
                        select new MemberValue(str);

                    var arrayValue =
                        from array in this.JsonArray
                        select new MemberValue(array);

                    var objectValue =
                        from jsonObject in this.Object
                        select new MemberValue(jsonObject);

                    var nullMemberValue =
                        from n in this.NullParser
                        select new MemberValue(n);

                    var trueMemberValue =
                        from t in this.True
                        select new MemberValue(t);

                    var falseMemberValue =
                        from f in this.False
                        select new MemberValue(f);
                    
                    this.memberValue = numberMemberValue | stringMemberValue | arrayValue | objectValue | nullMemberValue | trueMemberValue | falseMemberValue;
                }

                return this.memberValue;
            }
        }

        #endregion //MemberValue

        #region Number

        private Parser<Text, Number> number;
        public virtual Parser<Text, Number> Number
        {
            get
            {
                if (this.number == null)
                {
                    var integer =
                        from i in this.Integer
                        select new Number(i);

                    var integerFraction =
                        from i in this.Integer
                        from fraction in this.Fraction
                        select new Number(i, fraction, null);

                    var integerExponentiationNumber =
                        from i in this.Integer
                        from exponentiation in this.Exponentiation
                        select new Number(i, exponentiation);

                    var integerFractionExponentiation =
                        from i in this.Integer
                        from fraction in this.Fraction
                        from exponentiation in this.Exponentiation
                        select new Number(i, fraction, exponentiation);

                    this.number =
                        integerFractionExponentiation |
                        integerFraction |
                        integerExponentiationNumber |
                        integer;
                }

                return this.number;
            }
        }

        #endregion //Number

        #region ObjectMember

        private Parser<Text, ObjectMember> objectMember;
        public virtual Parser<Text, ObjectMember> ObjectMember
        {
            get
            {
                if (this.objectMember == null)
                {
                    this.objectMember =
                        from objectName in this.String
                        from colon in this.Colon
                        from objectValue in this.MemberValue
                        select new ObjectMember(objectName, colon, objectValue);
                }

                return this.objectMember;
            }
        }

        #endregion //ObjectMember

        #region ObjectMembers

        private Parser<Text, ObjectMembers> objectMembers;
        public virtual Parser<Text, ObjectMembers> ObjectMembers
        {
            get
            {
                if (this.objectMembers == null)
                {
                    this.objectMembers =
                        from objectMember in this.ObjectMember
                        from objectMembers in this.TailObjectMember.ZeroOrMore()
                        select new ObjectMembers(objectMember, objectMembers);
                }

                return this.objectMembers;
            }
        }

        #endregion //ObjectMembers

        #region String

        private Parser<Text, StringToken> stringParser;
        public new virtual Parser<Text, StringToken> String
        {
            get
            {
                if (this.stringParser == null)
                {
                    this.stringParser =
                        from leftQuote in this.LeftQuote
                        from characters in this.Characters
                        from rightQuote in this.RightQuote
                        select
                        new StringToken(leftQuote, characters, rightQuote);
                }

                return this.stringParser;
            }
        }

        #endregion //String

        #region TailJsonArrayElement

        private Parser<Text, TailJsonArrayElement> tailJsonArrayElement;
        public virtual Parser<Text, TailJsonArrayElement> TailJsonArrayElement
        {
            get
            {
                if (this.tailJsonArrayElement == null)
                {
                    this.tailJsonArrayElement =
                        from comma in this.Comma
                        from arrayElement in this.JsonArrayElement
                        select new TailJsonArrayElement(comma, arrayElement);
                }

                return this.tailJsonArrayElement;
            }
        }

        #endregion //TailJsonArrayElement

        #region TailObjectMember
        
        private Parser<Text, TailObjectMember> tailObjectMember;
        public virtual Parser<Text, TailObjectMember> TailObjectMember
        {
            get
            {
                if (this.tailObjectMember == null)
                {
                    this.tailObjectMember =
                        from comma in this.Comma
                        from objectMember in this.ObjectMember
                        select new TailObjectMember(comma, objectMember);
                }

                return this.tailObjectMember;
            }
        }

        #endregion //TailObjectMember

        #region ZeroPaddedInteger

        private Parser<Text, Integer> zeroPaddedInteger;
        public virtual Parser<Text, Integer> ZeroPaddedInteger
        {
            get
            {
                if (this.zeroPaddedInteger == null)
                {
                    var zeroParser =
                        from negative in this.Negative.Optional()
                        from zero in Char('0')
                        select negative.Any() ? 
                            new Integer(negative.FirstOrDefault(), zero) :
                            new Integer(zero);

                    var zeroPaddedIntegerParser =
                        from negative in this.Negative.Optional()
                        from zeroPadding in Char('0').ZeroOrMore()
                        from number in Range('1', '9') + this.Digit.ZeroOrMore()
                        select negative.Any() ? 
                            new Integer(negative.FirstOrDefault(), zeroPadding, number) :
                            new Integer(zeroPadding, number);

                    this.zeroPaddedInteger = zeroParser | zeroPaddedIntegerParser;
                }

                return this.zeroPaddedInteger;
            }
        }

        #endregion //ZeroPaddedInteger

        #endregion //Non-Terminals

        #region Base Class Overrides

        private Parser<Text, JsonSyntaxNode> parser;
        public override Parser<Text, JsonSyntaxNode> Parser
        {
            get
            {
                if (this.parser == null)
                {
                    var objectParser =
                        from jsonObject in this.Object
                        select jsonObject as JsonSyntaxNode; ;

                    var arrayParser =
                        from jsonArray in this.JsonArray
                        select jsonArray as JsonSyntaxNode;

                    this.parser = objectParser | arrayParser;
                }

                return this.parser;
            }
        }
        
        #endregion //Base Class Overrides
    }
}
