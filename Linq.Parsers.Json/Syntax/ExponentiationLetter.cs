using Linq.Parsers.Json.Visitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq.Parsers.Json.Syntax
{
    public class ExponentiationLetter : JsonSyntaxNode
    {
        #region Fields
        
        private Text value;

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

        #region NodeType

        public override JsonSyntaxNodeType NodeType
        {
            get
            {
                return JsonSyntaxNodeType.ExponentiationLetter;
            }
        }

        #endregion //NodeType

        #endregion //Properties

        #region Constructors

        public ExponentiationLetter(Text value)
        {
            this.value = value;
        }

        #endregion //Constructors

        #region Methods

        #region Base Class Overrides

        #region ToString

        public override string ToString()
        {
            return this.value;
        }

        #endregion //ToString

        #endregion //Base Class Overrides

        #endregion //Methods
    }
}
