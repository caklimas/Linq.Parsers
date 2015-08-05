using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq.Parsers.Json.Visitor
{
    public enum JsonSyntaxNodeType
    {
        Characters,
        CloseBrace,
        CloseBracket,
        Colon,
        Comma,
        DecimalPoint,
        Exponentiation,
        ExponentiationLetter,
        False,
        Fraction,
        FractionNumber,
        Integer,
        JsonArray,
        JsonArrayElement,
        JsonArrayElements,
        JsonObject,
        LeftQuote,
        MemberValue,
        Negative,
        Number,
        Null,
        ObjectMember,
        ObjectMembers,
        OpenBrace,
        OpenBracket,
        RightQuote,
        StringToken,
        TailJsonArrayElement,
        TailObjectMember,
        True
    }
}
