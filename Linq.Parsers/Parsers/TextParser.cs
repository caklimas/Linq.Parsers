using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq.Parsers
{
    #region TextParser

    public abstract class TextParser : Parser<Text, Text>
    {
        #region Methods

        #region ZeroOrMore

        public TextParser ZeroOrMore()
        {
            return new TextParser<TextParserZeroOrMoreCommand>(new TextParserZeroOrMoreCommand(this));
        }

        #endregion //ZeroOrMore

        #region OneOrMore

        public TextParser OneOrMore()
        {
            return new TextParser<TextParserOneOrMoreCommand>(
                new TextParserOneOrMoreCommand(this));
        }

        #endregion //OneOrMore

        #region Optional

        public TextParser Optional()
        {
            return new TextParser<TextParserOptionalCommand>(
                new TextParserOptionalCommand(this));
        }

        #endregion //Optional

        #region WithTrivia

        public Parser<Text, ValueWithTrivia> WithTrivia()
        {
            return
                from leftTrivia in WhiteSpace
                from value in this
                from rightTrivia in WhiteSpace
                select new ValueWithTrivia(leftTrivia, value, rightTrivia);
        }

        #endregion //WithTrivia

        #region WithLeftTrivia

        public Parser<Text, ValueWithLeftTrivia> WithLeftTrivia()
        {
            return
                from leftTrivia in WhiteSpace
                from value in this
                select new ValueWithLeftTrivia(leftTrivia, value);
        }

        #endregion //WithLeftTrivia

        #region WithRightTrivia

        public Parser<Text, ValueWithRightTrivia> WithRightTrivia()
        {
            return
                from value in this
                from rightTrivia in WhiteSpace
                select new ValueWithRightTrivia(value, rightTrivia);
        }

        #endregion //WithLeftTrivia

        #endregion //Methods

        #region Static Members

        #region Properties

        private static TextParser whiteSpace;
        internal static TextParser WhiteSpace
        {
            get
            {
                if (whiteSpace == null)
                    whiteSpace = new TextGrammar().WhiteSpace;
                return whiteSpace;
            }
        }

        #endregion //Properties

        #region Methods

        internal new static TextParser Insert(Text value)
        {
            return new TextParser<TextParserInsertCommand>(
                new TextParserInsertCommand(value));
        }

        #endregion //Methods

        #region Operators

        public static TextParser operator |(TextParser first, TextParser second)
        {
            return new TextParser<TextParserOrCommand>(
                new TextParserOrCommand(first, second));
        }

        public static TextParser operator +(TextParser first, TextParser second)
        {
            return new TextParser<TextParserPlusCommand>(
                new TextParserPlusCommand(first, second));
        }

        #endregion //Operators

        #endregion //Static Members

        #region Nested Classes

        internal class TextGrammar : Grammar<Text>
        {
            public override Parser<Text, Text> Parser
            {
                get
                {
                    throw new NotImplementedException();
                }
            }
        }

        #endregion //Nested Classes
    }

    #endregion //TextParser

    #region TextParser<TCommand>

    public class TextParser<TCommand> : TextParser
        where TCommand : IParseCommand<Text, Text>
    {
        #region Fields

        internal TCommand command;

        #endregion //Fields

        #region Constructor

        public TextParser(TCommand command)
        {
            this.command = command;
        }

        #endregion //Constructor

        #region Methods

        public override ParseResult<Text, Text> Parse(Text input)
        {
            return this.command.Execute(input);
        }

        #endregion //Methods
    }

    #endregion //TextParser<TCommand>

    #region ValueWithTrivia

    public struct ValueWithTrivia
    {
        private Text leftTrivia;
        private Text value;
        private Text rightTrivia;

        public Text LeftTrivia { get { return this.leftTrivia; } }
        public Text Value { get { return this.value; } }
        public Text RightTrivia { get { return this.rightTrivia; } }

        public ValueWithTrivia(Text leftTrivia, Text value, Text rightTrivia)
        {
            this.leftTrivia = leftTrivia;
            this.value = value;
            this.rightTrivia = rightTrivia;
        }
    }

    #endregion //ValueWithTrivia

    #region ValueWithLeftTrivia

    public struct ValueWithLeftTrivia
    {
        private Text leftTrivia;
        private Text value;

        public Text LeftTrivia { get { return this.leftTrivia; } }
        public Text Value { get { return this.value; } }

        public ValueWithLeftTrivia(Text leftTrivia, Text value)
        {
            this.leftTrivia = leftTrivia;
            this.value = value;
        }
    }

    #endregion //ValueWithLeftTrivia

    #region ValueWithRightTrivia

    public struct ValueWithRightTrivia
    {
        private Text value;
        private Text rightTrivia;
        
        public Text Value { get { return this.value; } }
        public Text RightTrivia { get { return this.rightTrivia; } }

        public ValueWithRightTrivia(Text value, Text rightTrivia)
        {
            this.value = value;
            this.rightTrivia = rightTrivia;
        }
    }

    #endregion //ValueWithRightTrivia

    #region TextParserOrCommand

    internal struct TextParserOrCommand : IParseCommand<Text, Text>
    {
        private TextParser first;
        private TextParser second;

        public TextParserOrCommand(TextParser first, TextParser second)
        {
            this.first = first;
            this.second = second;
        }

        public ParseResult<Text, Text> Execute(Text input)
        {
            return
                this.first.Parse(input) ??
                this.second.Parse(input);
        }
    }

    #endregion //TextParserOrCommand

    #region TextParserPlusCommand

    internal struct TextParserPlusCommand : IParseCommand<Text, Text>
    {
        private TextParser first;
        private TextParser second;

        public TextParserPlusCommand(TextParser first, TextParser second)
        {
            this.first = first;
            this.second = second;
        }

        public ParseResult<Text, Text> Execute(Text input)
        {
            var firstResult = first.Parse(input);
            if (firstResult == null)
                return null;

            var secondResult = second.Parse(firstResult.Rest);
            if (secondResult == null)
                return null;

            return new ParseResult<Text, Text>(
                firstResult.Value.Append(secondResult.Value),
                secondResult.Rest);
        }
    }

    #endregion //TextParserPlusCommand

    #region TextParserInsertCommand

    internal struct TextParserInsertCommand : IParseCommand<Text, Text>
    {
        private Text value;

        public TextParserInsertCommand(Text value)
        {
            this.value = value;
        }

        public ParseResult<Text, Text> Execute(Text input)
        {
            return new ParseResult<Text, Text>(this.value, input);
        }
    }

    #endregion //TextParserInsertCommand

    #region TextParserZeroOrMoreCommand

    internal struct TextParserZeroOrMoreCommand : IParseCommand<Text, Text>
    {
        private TextParser parser;

        public TextParserZeroOrMoreCommand(TextParser parser)
        {
            this.parser = parser;
        }

        public ParseResult<Text, Text> Execute(Text input)
        {
            ParseResult<Text, Text> result;
            Text value = Text.Empty;
            var rest = input;
            do
            {
                result = this.parser.Parse(rest);
                if (result != null)
                {
                    value = value.Append(result.Value);
                    rest = result.Rest;
                }
            }
            while (result != null && input.Length > result.Rest.Length);

            return new ParseResult<Text, Text>(value, rest);
        }
    }

    #endregion //TextParserZeroOrMoreCommand

    #region TextParserOneOrMoreCommand

    internal struct TextParserOneOrMoreCommand : IParseCommand<Text, Text>
    {
        private TextParser parser;

        public TextParserOneOrMoreCommand(TextParser parser)
        {
            this.parser = parser;
        }

        public ParseResult<Text, Text> Execute(Text input)
        {
            var result = this.parser.Parse(input);
            if (result == null)
                return null;
            if (result.Rest.Length == input.Length)
                return result;

            var zeroOrMoreTextParser = new TextParser<TextParserZeroOrMoreCommand>(
                new TextParserZeroOrMoreCommand(this.parser));

            var secondResult = zeroOrMoreTextParser.Parse(result.Rest);
            if (secondResult == null)
                return result;

            return new ParseResult<Text, Text>(
                result.Value.Append(secondResult.Value), 
                secondResult.Rest);
        }
    }

    #endregion //TextParserOneOrMoreCommand

    #region TextParserOptionalCommand

    internal struct TextParserOptionalCommand : IParseCommand<Text, Text>
    {
        private TextParser parser;

        public TextParserOptionalCommand(TextParser parser)
        {
            this.parser = parser;
        }

        public ParseResult<Text, Text> Execute(Text input)
        {
            var result = parser.Parse(input);
            if (result == null)
                return new ParseResult<Text, Text>(Text.Empty, input);

            return result;
        }
    }

    #endregion //TextParserOptionalCommand
}
