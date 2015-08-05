using Linq.Parsers.Json.Visitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq.Parsers.Json.Syntax
{
    public class Integer : JsonSyntaxNode
    {
        #region Fields

        private Negative negative;
        private Text zeroPadding;
        private Text value;

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

        #region ZeroPadding

        public string ZeroPadding
        {
            get
            {
                return this.zeroPadding.ToString();
            }
        }

        #endregion //ZeroPadding

        #region NodeType

        public override JsonSyntaxNodeType NodeType
        {
            get
            {
                return JsonSyntaxNodeType.Integer;
            }
        }

        #endregion //NodeType

        #endregion //Properties

        #region Constructors

        public Integer(Text value)
            : this(Text.Empty, value) { }

        public Integer(Negative negative, Text value)
            : this(negative, Text.Empty, value)
        { }

        public Integer(Text zeroPadding, Text value)
        {
            this.zeroPadding = zeroPadding;
            this.value = value;
        }

        public Integer(Negative negative, Text zeroPadding, Text value)
            : base(negative)
        {
            this.negative = negative;
            this.zeroPadding = zeroPadding;
            this.value = value;
        }

        #endregion //Constructors

        #region Methods

        #region Base Class Overrides

        #region ToString

        public override string ToString()
        {
            if (!this.isEvaluated)
                this.stringValue = Evaluate();

            return this.stringValue;
        }

        #endregion //ToString

        #endregion //Base Class Overrides

        #endregion //Methods

        #region Utilities

        #region Evaluate

        private string Evaluate()
        {
            var stringBuilder = new StringBuilder();

            if (this.negative != null)
                stringBuilder.Append(this.negative.ToString());

            stringBuilder.Append(this.value);

            this.isEvaluated = true;
            return stringBuilder.ToString();
        }

        #endregion //Evaluate

        #endregion //Utilities
    }
}
