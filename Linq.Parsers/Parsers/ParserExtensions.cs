using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq.Parsers
{
    public static class ParserExtensions
    {
        #region Where

        public static Parser<TInput, TValue> Where<TInput, TValue>(
            this Parser<TInput, TValue> parser, 
            Func<TValue, bool> predicate)
        {
            return new Parser<TInput, TValue>(input =>
            {
                var result = parser.Parse(input);
                if (result == null || !predicate(result.Value))
                    return null;
                return result;
            });
        }

        #endregion //Where

        #region Select

        public static Parser<TInput, TSecondValue> Select<TInput, TValue, TSecondValue>(
            this Parser<TInput, TValue> parser, 
            Func<TValue, TSecondValue> selector)
        {
            return new Parser<TInput, TSecondValue>(input =>
            {
                var result = parser.Parse(input);
                if (result == null)
                    return null;

                return new ParseResult<TInput, TSecondValue>(
                    selector(result.Value), result.Rest);
            });
        }

        #endregion //Select

        #region SelectMany

        public static Parser<TInput, TSecondValue> SelectMany<TInput, TValue, TIntermediate, TSecondValue>(
            this Parser<TInput, TValue> parser,
            Func<TValue, Parser<TInput, TIntermediate>> selector,
            Func<TValue, TIntermediate, TSecondValue> projector)
        {
            return new Parser<TInput, TSecondValue>(input =>
            {
                var result = parser.Parse(input);
                if (result == null)
                    return null;

                var value = result.Value;
                var secondResult = selector(value).Parse(result.Rest);
                if (secondResult == null)
                    return null;

                return new ParseResult<TInput, TSecondValue>(projector(value, secondResult.Value), secondResult.Rest);
            });
        }

        #endregion //SelectMany

        #region ZeroOrMore

        public static Parser<TInput, TValue[]> ZeroOrMore<TInput, TValue>(this Parser<TInput, TValue> parser)
        {
            return parser.OneOrMore() | Parser<TInput, TValue[]>.Insert(new TValue[0]);
        }

        #endregion //ZeroOrMore

        #region OneOrMore

        public static Parser<TInput, TValue[]> OneOrMore<TInput, TValue>(this Parser<TInput, TValue> parser)
        {
            return
                from result in parser
                from repeatedResult in parser.ZeroOrMore()
                select new[] { result }.Concat(repeatedResult).ToArray();
        }

        #endregion //OneOrMore

        #region Optional

        public static Parser<TInput, TValue[]> Optional<TInput, TValue>(this Parser<TInput, TValue> parser)
        {
            return new Parser<TInput, TValue[]>(input =>
            {
                var result = parser.Parse(input);
                if (result == null)
                    return new ParseResult<TInput, TValue[]>(new TValue[0], input);

                return new ParseResult<TInput, TValue[]>(new[] { result.Value }, result.Rest);
            });
        }

        #endregion //Optional
    }
}
