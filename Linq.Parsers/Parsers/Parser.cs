using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("Linq.Parsers.Testing")]
namespace Linq.Parsers
{
    public class Parser<TInput, TValue>
    {
        #region Fields

        internal Func<TInput, ParseResult<TInput, TValue>> parse;

        #endregion //Fields

        #region Constructor

        public Parser(Func<TInput, ParseResult<TInput, TValue>> parse)
        {
            parse.AssertNotNull();
            this.parse = parse;
        }

        #endregion //Constructor

        #region Methods

        public ParseResult<TInput, TValue> Parse(TInput value)
        {
            return this.parse(value);
        }

        #endregion //Methods

        #region Static Members

        #region Operators

        public static Parser<TInput, TValue> operator |(Parser<TInput, TValue> first, Parser<TInput, TValue> second)
        {
            return new Parser<TInput, TValue>(input => 
                first.Parse(input) ?? 
                second.Parse(input));
        }

        #endregion //Operators

        #region Methods

        internal static Parser<TInput, TValue> Insert(TValue value)
        {
            return new Parser<TInput, TValue>(input =>
                new ParseResult<TInput, TValue>(value, input));
        }

        #endregion //Methods

        #endregion //Static Members
    }
}