using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("Linq.Parsers.Testing")]
namespace Linq.Parsers
{
    #region Parser<TInput, TValue>

    public abstract class Parser<TInput, TValue>
    {
        #region Methods

        public abstract ParseResult<TInput, TValue> Parse(TInput value);

        #endregion //Methods

        #region Static Members

        #region Operators

        public static Parser<TInput, TValue> operator |(Parser<TInput, TValue> first, Parser<TInput, TValue> second)
        {
            return new Parser<TInput, TValue, ParserOrCommand<TInput, TValue>>(
                new ParserOrCommand<TInput, TValue>(first, second));
        }

        #endregion //Operators

        #region Methods

        internal static Parser<TInput, TValue> Insert(TValue value)
        {
            return new Parser<TInput, TValue, ParserInsertCommand<TInput, TValue>>(
                new ParserInsertCommand<TInput, TValue>(value));
        }

        #endregion //Methods

        #endregion //Static Members
    }

    #endregion //Parser<TInput, TValue>

    #region Parser<TInput, TValue, TCommand>

    public class Parser<TInput, TValue, TCommand> : Parser<TInput, TValue>
        where TCommand : IParseCommand<TInput, TValue>
    {
        #region Fields

        internal TCommand command;

        #endregion //Fields

        #region Constructor

        public Parser(TCommand command)
        {
            this.command = command;
        }

        #endregion //Constructor

        #region Methods

        public override ParseResult<TInput, TValue> Parse(TInput input)
        {
            return this.command.Execute(input);
        }

        #endregion //Methods
    }

    #endregion Parser<TInput, TValue, TCommand>

    #region ParserOrCommand

    internal struct ParserOrCommand<TInput, TValue> : IParseCommand<TInput, TValue>
    {
        private Parser<TInput, TValue> first;
        private Parser<TInput, TValue> second;

        public ParserOrCommand(Parser<TInput, TValue> first, Parser<TInput, TValue> second)
        {
            this.first = first;
            this.second = second;
        }

        public ParseResult<TInput, TValue> Execute(TInput input)
        {
            return
                this.first.Parse(input) ??
                this.second.Parse(input);
        }
    }

    #endregion //ParserOrCommand

    #region ParserInsertCommand

    internal struct ParserInsertCommand<TInput, TValue> : IParseCommand<TInput, TValue>
    {
        TValue value;

        public ParserInsertCommand(TValue value)
        {
            this.value = value;
        }

        public ParseResult<TInput, TValue> Execute(TInput input)
        {
            return new ParseResult<TInput, TValue>(this.value, input);
        }
    }

    #endregion //ParserInsertCommand
}