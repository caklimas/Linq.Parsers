using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq.Parsers
{
    public class TextParser<TInput> : Parser<TInput, Text>
    {
        #region Constructor

        public TextParser(Func<TInput, ParseResult<TInput, Text>> parse)
            : base(parse) { }

        #endregion //Constructor

        #region Methods

        #region ZeroOrMore

        public TextParser<TInput> ZeroOrMore()
        {
            return this.OneOrMore() | TextParser<TInput>.Insert(Text.Empty);
        }

        #endregion //ZeroOrMore

        #region OneOrMore

        public TextParser<TInput> OneOrMore()
        {
            return new TextParser<TInput>(
                (from result in this
                 from repeatedResult in this.ZeroOrMore()
                 select result.Append(repeatedResult))
                .parse);
                
        }

        #endregion //OneOrMore

        #region Optional

        public TextParser<TInput> Optional()
        {
            return new TextParser<TInput>(input =>
            {
                var result = this.Parse(input);
                if (result == null)
                    return new ParseResult<TInput, Text>(Text.Empty, input);

                return result;
            });
        }

        #endregion //Optional

        #endregion //Methods

        #region Static Members

        #region Operators

        public static TextParser<TInput> operator |(TextParser<TInput> first, TextParser<TInput> second)
        {
            return new TextParser<TInput>(input =>
                first.Parse(input) ??
                second.Parse(input));
        }

        public static TextParser<TInput> operator +(TextParser<TInput> first, TextParser<TInput> second)
        {
            return new TextParser<TInput>(input =>
            {
                var firstResult = first.Parse(input);
                if (firstResult == null)
                    return null;

                var secondResult = second.Parse(firstResult.Rest);
                if (secondResult == null)
                    return null;

                return new ParseResult<TInput, Text>(
                    firstResult.Value.Append(secondResult.Value), 
                    secondResult.Rest);
            });
        }

        #endregion //Operators

        #region Methods

        internal new static TextParser<TInput> Insert(Text value)
        {
            return new TextParser<TInput>(input =>
                new ParseResult<TInput, Text>(value, input));
        }

        #endregion //Methods

        #endregion //Static Members
    }
}
