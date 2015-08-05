using Linq.Parsers.Json.Visitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq.Parsers.Json.Syntax
{
    public class RightQuote : JsonSyntaxNode
    {
        #region Fields

        private Text value;
        private Text rightTrivia;

        #endregion //Fields

        #region Properties

        #region Value

        public string Value
        {
            get
            {
                return this.value.ToString();
            }
        }

        #endregion //Value

        #region RightTrivia

        public string RightTrivia
        {
            get
            {
                return this.rightTrivia.ToString();
            }
        }

        #endregion //RightTrivia
        
        #region NodeType

        public override JsonSyntaxNodeType NodeType
        {
            get
            {
                return JsonSyntaxNodeType.RightQuote;
            }
        }

        #endregion //NodeType
        
        #endregion // Properties

        #region Constructors

        public RightQuote(Text value, Text rightTrivia)
        {
            this.value = value;
            this.rightTrivia = rightTrivia;
        }

        #endregion //Constructors

        #region Methods

        #region Base Class Overrides

        #region ToString

        public override string ToString()
        {
            return
                this.value
                .Append(this.rightTrivia)
                .ToString();
        }

        #endregion //ToString

        #endregion //Base Class Overrides

        #endregion //Methods
    }
}
