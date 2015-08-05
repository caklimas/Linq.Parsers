using Linq.Parsers.Json.Visitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq.Parsers.Json.Syntax
{
    public class Fraction : JsonSyntaxNode
    {
        #region Fields

        private DecimalPoint decimalPoint;
        private FractionNumber fractionNumber;

        #endregion //Fields

        #region Properties

        #region DecimalPoint

        public string DecimalPoint
        {
            get
            {
                return this.decimalPoint.ToString();
            }
        }

        #endregion //DecimalPoint

        #region FractionNumber

        public string FractionNumber
        {
            get
            {
                return this.fractionNumber.ToString();
            }
        }

        #endregion //FractionNumber

        #region NodeType

        public override JsonSyntaxNodeType NodeType
        {
            get
            {
                return JsonSyntaxNodeType.Fraction;
            }
        }

        #endregion //NodeType

        #endregion //Properties

        #region Constructors

        public Fraction(DecimalPoint decimalPoint, FractionNumber fractionNumber)
            : base(decimalPoint, fractionNumber)
        {
            this.decimalPoint = decimalPoint;
            this.fractionNumber = fractionNumber;
        }

        #endregion //Constructors
    }
}
