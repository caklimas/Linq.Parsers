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

        public TextParser String(string text)
        {
            return new TextParser<StringParseCommand>(
                new StringParseCommand(text));
        }

        #endregion //String

        #region Char

        public SetParser Char()
        {
            return new SetParser<AnyCharParseCommand>(
                new AnyCharParseCommand());
        }

        public SetParser Char(char character)
        {
            return new SetParser<CharParseCommand>(
                new CharParseCommand(character));
        }

        #endregion //Char

        #region Set

        public SetParser Set(string set)
        {
            return new SetParser<SetParseCommand>(
                new SetParseCommand(set));
        }

        #endregion //Set

        #region Range

        public SetParser Range(char from, char to)
        {
            return new SetParser<RangeParseCommand>(
                new RangeParseCommand(from, to));
        }

        #endregion //Range

        #region Null

        private TextParser nullParser;
        public TextParser Null
        {
            get
            {
                if (this.nullParser == null)
                    this.nullParser = new TextParser<NullTextParseCommand>(
                        new NullTextParseCommand());
                return this.nullParser;
            }
        }

        #endregion //Null

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

        private TextParser whiteSpace;
        public TextParser WhiteSpace
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

        private TextParser digit;
        public TextParser Digit
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

        private TextParser letter;
        public TextParser Letter
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

    #region NullTextParseCommand

    internal struct NullTextParseCommand : IParseCommand<Text, Text>
    {
        public ParseResult<Text, Text> Execute(Text input)
        {
            return null;
        }
    }

    #endregion //NullTextParseCommand

    #region StringParseCommand

    internal struct StringParseCommand : IParseCommand<Text, Text>
    {
        private string text;

        public StringParseCommand(string text)
        {
            this.text = text;
        }

        public ParseResult<Text, Text> Execute(Text input)
        {
            if (input.Length < this.text.Length)
                return null;

            for (var i = 0; i < this.text.Length; i++)
                if (text[i] != input[i])
                    return null;

            var split = input.Split(this.text.Length);
            return new ParseResult<Text, Text>(split.Head, split.Tail);
        }
    }

    #endregion //StringParseCommand

    #region AnyCharParseCommand

    internal struct AnyCharParseCommand : IParseCommand<Text, Text>
    {
        public ParseResult<Text, Text> Execute(Text input)
        {
            if (input.Length == 0)
                return null;

            var split = input.Split(1);
            return new ParseResult<Text, Text>(split.Head, split.Tail);
        }
    }

    #endregion //CharParseCommand

    #region CharParseCommand

    internal struct CharParseCommand : IParseCommand<Text, Text>
    {
        private char character;

        public CharParseCommand(char character)
        {
            this.character = character;
        }

        public ParseResult<Text, Text> Execute(Text input)
        {
            if (input.Length == 0 || input[0] != this.character)
                return null;

            var split = input.Split(1);
            return new ParseResult<Text, Text>(split.Head, split.Tail);
        }
    }

    #endregion //CharParseCommand

    #region SetParseCommand

    internal struct SetParseCommand : IParseCommand<Text, Text>
    {
        private string set;

        public SetParseCommand(string set)
        {
            this.set = set;
        }

        public ParseResult<Text, Text> Execute(Text input)
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
        }
    }

    #endregion //SetParseCommand

    #region RangeParseCommand

    internal struct RangeParseCommand : IParseCommand<Text, Text>
    {
        private char from;
        private char to;

        public RangeParseCommand(char from, char to)
        {
            this.from = from;
            this.to = to;
        }

        public ParseResult<Text, Text> Execute(Text input)
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
        }
    }

    #endregion //RangeParseCommand
}