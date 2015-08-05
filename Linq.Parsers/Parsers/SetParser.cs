using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq.Parsers
{
    #region SetParser

    public abstract class SetParser : TextParser
    {
        #region Methods

        public SetParser Where(Func<Text, bool> predicate)
        {
            return new SetParser<SetParserWhereCommand>(
                new SetParserWhereCommand(this, predicate));
        }

        #endregion //Methods

        #region Operators

        public static SetParser operator |(SetParser first, SetParser second)
        {
            return new SetParser<SetParserOrCommand>(
                new SetParserOrCommand(first, second));
        }

        public static SetParser operator !(SetParser parser)
        {
            return new SetParser<SetParserNotCommand>(
                new SetParserNotCommand(parser));
        }

        #endregion //Operators
    }

    #endregion //SetParser

    #region SetParser<TCommand>

    public class SetParser<TCommand> : SetParser
        where TCommand : IParseCommand<Text, Text>
    {
        internal TCommand command;

        public SetParser(TCommand command)
        {
            this.command = command;
        }

        public override ParseResult<Text, Text> Parse(Text value)
        {
            return this.command.Execute(value);
        }
    }

    #endregion //SetParser<TCommand>

    #region SetParserWhereCommand

    internal struct SetParserWhereCommand : IParseCommand<Text, Text>
    {
        private SetParser parser;
        private Func<Text, bool> predicate;

        public SetParserWhereCommand(SetParser parser, Func<Text, bool> predicate)
        {
            this.parser = parser;
            this.predicate = predicate;
        }

        public ParseResult<Text, Text> Execute(Text input)
        {
            var result = this.parser.Parse(input);
            if (result == null || !this.predicate(result.Value))
                return null;

            return result;
        }
    }

    #endregion //SetParserWhereCommand

    #region SetParserOrCommand

    internal struct SetParserOrCommand : IParseCommand<Text, Text>
    {
        private SetParser first;
        private SetParser second;

        public SetParserOrCommand(SetParser first, SetParser second)
        {
            this.first = first;
            this.second = second;
        }

        public ParseResult<Text, Text> Execute(Text input)
        {
            var firstResult = first.Parse(input);
            if (firstResult != null)
                return firstResult;

            var secondResult = second.Parse(input);
            if (secondResult != null)
                return secondResult;

            return null;
        }
    }

    #endregion //SetParserOrCommand

    #region SetParserNotCommand

    internal struct SetParserNotCommand : IParseCommand<Text, Text>
    {
        private SetParser parser; 

        public SetParserNotCommand(SetParser parser)
        {
            this.parser = parser;
        }

        public ParseResult<Text, Text> Execute(Text input)
        {
            if (input.Length == 0)
                return null;

            var result = this.parser.Parse(input);
            if (result != null)
                return null;

            var split = input.Split(1);
            return new ParseResult<Text, Text>(split.Head, split.Tail);
        }
    }

    #endregion //SetParserNotCommand
}
