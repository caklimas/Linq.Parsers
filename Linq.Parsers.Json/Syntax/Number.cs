using Linq.Parsers.Json.Visitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq.Parsers.Json.Syntax
{
    public class Number : JsonSyntaxNode
    {
        #region Fields
        
        private Integer integer;
        private Exponentiation exponentiation;
        private Fraction fraction;

        #endregion //Fields

        #region Properties

        #region Value

        public string Value
        {
            get
            {
                return this.ToString();
            }
        }

        #endregion //Value
        
        #region NodeType

        public override JsonSyntaxNodeType NodeType
        {
            get
            {
                return JsonSyntaxNodeType.Number;
            }
        }

        #endregion //NodeType

        #endregion //Properties

        #region Constructors

        public Number(Integer integer)
            : base(integer)
        {
            this.integer = integer;
        }

        public Number(Integer integer, Fraction fraction)
            : base(integer)
        {
            this.integer = integer;
            this.fraction = fraction;
        }

        public Number(Integer integer, Fraction fraction, Exponentiation exponentiation)
            : base(integer, fraction, exponentiation)
        {
            this.integer = integer;
            this.fraction = fraction;
            this.exponentiation = exponentiation;
        }

        public Number(Integer integer, Exponentiation exponentiation)
            : base(integer, exponentiation)
        {
            this.integer = integer;
            this.exponentiation = exponentiation;
        }

        #endregion //Constructors
    }
}
