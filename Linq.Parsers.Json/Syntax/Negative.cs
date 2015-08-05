using Linq.Parsers.Json.Visitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq.Parsers.Json.Syntax
{
    public class Negative : JsonSyntaxNode
    {
        #region Fields

        private Text leftTrivia;
        private Text value;

        #endregion //Fields

        #region Properties

        #region LeftTrivia

        public string LeftTrivia
        {
            get
            {
                return this.leftTrivia.ToString();
            }
        }

        #endregion //LeftTrivia

        #region Value

        public string Value
        {
            get
            {
                return this.value.ToString();
            }
        }

        #endregion //Value
        
        #region NodeType

        public override JsonSyntaxNodeType NodeType
        {
            get
            {
                return JsonSyntaxNodeType.Negative;
            }
        }

        #endregion //NodeType
        
        #endregion //Properties

        #region Constructors

        public Negative(Text leftTrivia, Text value)
        {
            this.leftTrivia = leftTrivia;
            this.value = value;
        }

        #endregion //Constructors

        #region Methods

        #region Base Class Overrides

        #region ToString

        public override string ToString()
        {
            return this.leftTrivia
                .Append(this.Value)
                .ToString();
        }

        #endregion //ToString

        #endregion //Base Class Overrides

        #endregion //Methods
    }
}
