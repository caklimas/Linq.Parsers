using Linq.Parsers.Json.Visitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq.Parsers.Json.Syntax
{
    public class Exponentiation : JsonSyntaxNode
    {
        #region Fields

        private ExponentiationLetter exponentiationLetter;
        private Integer integer;

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
                return JsonSyntaxNodeType.Exponentiation;
            }
        }

        #endregion //NodeType

        #endregion //Properties

        #region Constructors

        public Exponentiation(ExponentiationLetter exponentiationLetter, Integer integer)
            : base(exponentiationLetter, integer)
        {
            this.exponentiationLetter = exponentiationLetter;
            this.integer = integer;
        }

        #endregion //Constructors
    }
}
