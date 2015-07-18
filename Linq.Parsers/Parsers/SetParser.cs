using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq.Parsers
{
    public class SetParser : TextParser<Text>
    {
        #region Constructor

        public SetParser(Func<Text, ParseResult<Text, Text>> parser) : base(parser) { }

        #endregion //Constructor

        #region Operators

        public static SetParser operator |(SetParser first, SetParser second)
        {
            return new SetParser(input =>
            {
                var firstResult = first.Parse(input);
                if (firstResult != null)
                    return firstResult;

                var secondResult = second.Parse(input);
                if (secondResult != null)
                    return secondResult;

                return null;
            });
        }

        public static SetParser operator !(SetParser parser)
        {
            return new SetParser(input =>
            {
                if (input.Length == 0)
                    return null;

                var result = parser.Parse(input);
                if (result != null)
                    return null;

                var split = input.Split(1);
                return new ParseResult<Text, Text>(split.Head, split.Tail);
            });
        }

        #endregion //Operators
    }
}
