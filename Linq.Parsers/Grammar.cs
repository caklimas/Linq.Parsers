using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq.Parsers
{
    public abstract class Grammar<TOutput>
    {
        #region Parser

        public abstract Parser<Text, TOutput> Parser { get; }

        #endregion //Parser

        #region String

        public TextParser<Text> String(string text)
        {
            return new TextParser<Text>(input =>
            {
                if (input.Length < text.Length)
                    return null;

                for (var i = 0; i < text.Length; i++)
                    if (text[i] != input[i])
                        return null;

                var split = input.Split(text.Length);
                return new ParseResult<Text, Text>(split.Head, split.Tail);
            });
        }

        #endregion //String

        #region Character

        public SetParser Character(char character)
        {
            return new SetParser(input =>
            {
                if (input.Length == 0)
                    return null;

                var split = input.Split(1);
                return new ParseResult<Text, Text>(split.Head, split.Tail);
            });
        }

        #endregion //Character

        #region Set

        public SetParser Set(string set)
        {
            return new SetParser(input =>
            {
                if (input.Length == 0)
                    return null;

                var character = input[0];
                var inSet = false;

                for (var i = 0; i < set.Length; i++)
                {
                    if (set[i] == character)
                    {
                        inSet = true;
                        break;
                    }
                }

                if (!inSet)
                    return null;

                var split = input.Split(1);
                return new ParseResult<Text, Text>(split.Head, split.Tail);
            });
        }

        #endregion //Set

        #region Range

        public SetParser Range(char from, char to)
        {
            return new SetParser(input =>
            {
                if (input.Length == 0)
                    return null;

                var characterNumber = (int)input[0];
                var inSet = false;

                var fromNumber = (int)from;
                var toNumber = (int)to;
                if (fromNumber > toNumber)
                {
                    var temp = fromNumber;
                    fromNumber = toNumber;
                    toNumber = temp;
                }

                for (var currentCharacterNumber = fromNumber; currentCharacterNumber <= toNumber; currentCharacterNumber++)
                {
                    if (currentCharacterNumber == characterNumber)
                    {
                        inSet = true;
                        break;
                    }
                }

                if (!inSet)
                    return null;

                var split = input.Split(1);
                return new ParseResult<Text, Text>(split.Head, split.Tail);
            });
        }

        #endregion //Range

        #region SingleWhiteSpace

        private SetParser singleWhiteSpace;
        public SetParser SingleWhiteSpace
        {
            get
            {
                if (this.singleWhiteSpace == null)
                    this.singleWhiteSpace = Set(" \f\n\r\t\v");
                return this.singleWhiteSpace;
            }
        }

        #endregion //SingleWhiteSpace

        #region WhiteSpace

        private TextParser<Text> whiteSpace;
        public TextParser<Text> WhiteSpace
        {
            get
            {
                if (this.whiteSpace == null)
                    this.whiteSpace = this.SingleWhiteSpace.ZeroOrMore();
                return this.whiteSpace;
            }
        }

        #endregion //WhiteSpace

        #region Digit

        private TextParser<Text> digit;
        public TextParser<Text> Digit
        {
            get
            {
                if (this.digit == null)
                    this.digit = Range('0', '9');
                return this.digit;
            }
        }

        #endregion //Digit

        #region Letter

        private TextParser<Text> letter;
        public TextParser<Text> Letter
        {
            get
            {
                if (this.letter == null)
                    this.letter = Range('a', 'z') | Range('A', 'Z');
                return this.letter;
            }
        }

        #endregion //Letter
    }
}