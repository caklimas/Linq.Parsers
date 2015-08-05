using Linq.Parsers.Json.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Linq.Parsers.Json.Visitor
{
    public abstract class JsonSyntaxVisitor
    {
        #region Methods

        #region Visit

        public virtual JsonSyntaxNode Visit(JsonSyntaxNode node)
        {
            node.AssertNotNull();

            switch (node.NodeType)
            {
                case JsonSyntaxNodeType.Characters:
                {
                    VisitCharacters(node as Characters);
                    break;
                }
                case JsonSyntaxNodeType.CloseBrace:
                {
                    VisitCloseBrace(node as CloseBrace);
                    break;
                }
                case JsonSyntaxNodeType.CloseBracket:
                {
                    VisitCloseBracket(node as CloseBracket);
                    break;
                }
                case JsonSyntaxNodeType.Colon:
                {
                    VisitColon(node as Colon);
                    break;
                }
                case JsonSyntaxNodeType.Comma:
                {
                    VisitComma(node as Comma);
                    break;
                }
                case JsonSyntaxNodeType.DecimalPoint:
                {
                    VisitDecimalPoint(node as DecimalPoint);
                    break;
                }
                case JsonSyntaxNodeType.Exponentiation:
                {
                    VisitExponentiation(node as Exponentiation);
                    break;
                }
                case JsonSyntaxNodeType.ExponentiationLetter:
                {
                    VisitExponentiationLetter(node as ExponentiationLetter);
                    break;
                }
                case JsonSyntaxNodeType.False:
                {
                    VisitFalse(node as False);
                    break;
                }
                case JsonSyntaxNodeType.Fraction:
                {
                    VisitFraction(node as Fraction);
                    break;
                }
                case JsonSyntaxNodeType.FractionNumber:
                {
                    VisitFractionNumber(node as FractionNumber);
                    break;
                }
                case JsonSyntaxNodeType.Integer:
                {
                    VisitInteger(node as Integer);
                    break;
                }
                case JsonSyntaxNodeType.JsonArray:
                {
                    VisitJsonArray(node as JsonArray);
                    break;
                }
                case JsonSyntaxNodeType.JsonArrayElement:
                {
                    VisitJsonArrayElement(node as JsonArrayElement);
                    break;
                }
                case JsonSyntaxNodeType.JsonArrayElements:
                {
                    VisitJsonArrayElements(node as JsonArrayElements);
                    break;
                }
                case JsonSyntaxNodeType.JsonObject:
                {
                    VisitJsonObject(node as JsonObject);
                    break;
                }
                case JsonSyntaxNodeType.LeftQuote:
                {
                    VisitLeftQuote(node as LeftQuote);
                    break;
                }
                case JsonSyntaxNodeType.MemberValue:
                {
                    VisitMemberValue(node as MemberValue);
                    break;
                }
                case JsonSyntaxNodeType.Negative:
                {
                    VisitNegative(node as Negative);
                    break;
                }
                case JsonSyntaxNodeType.Number:
                {
                    VisitNumber(node as Number);
                    break;
                }
                case JsonSyntaxNodeType.Null:
                {
                    VisitNull(node as Null);
                    break;
                }
                case JsonSyntaxNodeType.ObjectMember:
                {
                    VisitObjectMember(node as ObjectMember);
                    break;
                }
                case JsonSyntaxNodeType.ObjectMembers:
                {
                    VisitObjectMembers(node as ObjectMembers);
                    break;
                }
                case JsonSyntaxNodeType.OpenBrace:
                {
                    VisitOpenBrace(node as OpenBrace);
                    break;
                }
                case JsonSyntaxNodeType.OpenBracket:
                {
                    VisitOpenBracket(node as OpenBracket);
                    break;
                }
                case JsonSyntaxNodeType.RightQuote:
                {
                    VisitRightQuote(node as RightQuote);
                    break;
                }
                case JsonSyntaxNodeType.StringToken:
                {
                    VisitString(node as StringToken);
                    break;
                }
                case JsonSyntaxNodeType.TailJsonArrayElement:
                {
                    VisitTailJsonArrayElement(node as TailJsonArrayElement);
                    break;
                }
                case JsonSyntaxNodeType.TailObjectMember:
                {
                    VisitTailObjectMember(node as TailObjectMember);
                    break;
                }
                case JsonSyntaxNodeType.True:
                {
                    VisitTrue(node as True);
                    break;
                }
                default:
                    throw new NotSupportedException(string.Format("Unrecognized Node Type {0}", node.NodeType.ToString()));
            }

            return node;
        }

        #endregion //Visit

        #region VisitSyntaxNode

        protected virtual JsonSyntaxNode VisitSyntaxNode(JsonSyntaxNode node)
        {
            foreach (var child in node.GetChildren())
                Visit(child);

            return node;
        }

        #endregion //VisitSyntaxNode

        #region VisitCharacters

        protected virtual Characters VisitCharacters(Characters node)
        {
            return VisitSyntaxNode(node) as Characters;
        }

        #endregion //VisitCharacters

        #region VisitCloseBrace

        protected virtual CloseBrace VisitCloseBrace(CloseBrace node)
        {
            return VisitSyntaxNode(node) as CloseBrace;
        }

        #endregion //VisitCloseBrace

        #region VisitCloseBracket

        protected virtual CloseBracket VisitCloseBracket(CloseBracket node)
        {
            return VisitSyntaxNode(node) as CloseBracket;
        }

        #endregion //VisitCloseBracket

        #region VisitColon

        protected virtual Colon VisitColon(Colon node)
        {
            return VisitSyntaxNode(node) as Colon;
        }

        #endregion //VisitColon

        #region VisitComma

        protected virtual Comma VisitComma(Comma node)
        {
            return VisitSyntaxNode(node) as Comma;
        }

        #endregion //VisitComma

        #region VisitDecimal

        protected virtual DecimalPoint VisitDecimalPoint(DecimalPoint node)
        {
            return VisitSyntaxNode(node) as DecimalPoint;
        }

        #endregion //VisitDecimal

        #region VisitExponentiation

        protected virtual Exponentiation VisitExponentiation(Exponentiation node)
        {
            return VisitSyntaxNode(node) as Exponentiation;
        }

        #endregion //VisitExponentiation

        #region VisitExponentiationLetter

        protected virtual ExponentiationLetter VisitExponentiationLetter(ExponentiationLetter node)
        {
            return VisitSyntaxNode(node) as ExponentiationLetter;
        }

        #endregion //VisitExponentiationLetter

        #region VisitFalse

        protected virtual False VisitFalse(False node)
        {
            return VisitSyntaxNode(node) as False;
        }

        #endregion //VisitFalse

        #region VisitFraction

        protected virtual Fraction VisitFraction(Fraction node)
        {
            return VisitSyntaxNode(node) as Fraction;
        }

        #endregion //VisitFraction

        #region VisitFractionNumber

        protected virtual FractionNumber VisitFractionNumber(FractionNumber node)
        {
            return VisitSyntaxNode(node) as FractionNumber;
        }

        #endregion //VisitFractionNumber

        #region VisitInteger

        protected virtual Integer VisitInteger(Integer node)
        {
            return VisitSyntaxNode(node) as Integer;
        }

        #endregion //VisitInteger

        #region VisitJsonArray

        protected virtual JsonArray VisitJsonArray(JsonArray node)
        {
            return VisitSyntaxNode(node) as JsonArray;
        }

        #endregion //VisitJsonArray

        #region VisitJsonArrayElement

        protected virtual JsonArrayElement VisitJsonArrayElement(JsonArrayElement node)
        {
            return VisitSyntaxNode(node) as JsonArrayElement;
        }

        #endregion //VisitJsonArrayElement

        #region VisitJsonArrayElements

        protected virtual JsonArrayElements VisitJsonArrayElements(JsonArrayElements node)
        {
            return VisitSyntaxNode(node) as JsonArrayElements;
        }

        #endregion //VisitJsonArrayElements

        #region VisitJsonObject

        protected virtual JsonObject VisitJsonObject(JsonObject node)
        {
            return VisitSyntaxNode(node) as JsonObject;
        }

        #endregion //VisitJsonObject
        
        #region VisitLeftQuote

        protected virtual LeftQuote VisitLeftQuote(LeftQuote node)
        {
            return VisitSyntaxNode(node) as LeftQuote;
        }

        #endregion //VisitLeftQuote

        #region VisitMemberValue

        protected virtual MemberValue VisitMemberValue(MemberValue node)
        {
            return VisitSyntaxNode(node) as MemberValue;
        }

        #endregion //VisitMemberValue

        #region VisitNegative

        protected virtual Negative VisitNegative(Negative node)
        {
            return VisitSyntaxNode(node) as Negative;
        }

        #endregion //VisitNegative

        #region VisitNumber

        protected virtual Number VisitNumber(Number node)
        {
            return VisitSyntaxNode(node) as Number;
        }

        #endregion //VisitNumber

        #region VisitNull

        protected virtual Null VisitNull(Null node)
        {
            return VisitSyntaxNode(node) as Null;
        }

        #endregion //VisitNull

        #region VisitObjectMember

        protected virtual ObjectMember VisitObjectMember(ObjectMember node)
        {
            return VisitSyntaxNode(node) as ObjectMember;
        }

        #endregion //VisitObjectMember

        #region VisitObjectMembers

        protected virtual ObjectMembers VisitObjectMembers(ObjectMembers node)
        {
            return VisitSyntaxNode(node) as ObjectMembers;
        }

        #endregion //VisitObjectMembers

        #region VisitOpenBrace

        protected virtual OpenBrace VisitOpenBrace(OpenBrace node)
        {
            return VisitSyntaxNode(node) as OpenBrace;
        }

        #endregion //VisitOpenBrace

        #region VisitOpenBracket

        protected virtual OpenBracket VisitOpenBracket(OpenBracket node)
        {
            return VisitSyntaxNode(node) as OpenBracket;
        }

        #endregion //VisitOpenBracket

        #region VisitRightQuote

        protected virtual RightQuote VisitRightQuote(RightQuote node)
        {
            return VisitSyntaxNode(node) as RightQuote;
        }

        #endregion //VisitRightQuote

        #region VisitString

        protected virtual StringToken VisitString(StringToken node)
        {
            return VisitSyntaxNode(node) as StringToken;
        }

        #endregion //VisitString

        #region VisitTailJsonArrayElement

        protected virtual TailJsonArrayElement VisitTailJsonArrayElement(TailJsonArrayElement node)
        {
            return VisitSyntaxNode(node) as TailJsonArrayElement;
        }

        #endregion //VisitTailJsonArrayElement

        #region VisitTailObjectMember

        protected virtual TailObjectMember VisitTailObjectMember(TailObjectMember node)
        {
            return VisitSyntaxNode(node) as TailObjectMember;
        }

        #endregion //VisitTailObjectMember

        #region VisitTrue

        protected virtual True VisitTrue(True node)
        {
            return VisitSyntaxNode(node) as True;
        }

        #endregion //VisitTrue

        #endregion //Methods
    }
}
