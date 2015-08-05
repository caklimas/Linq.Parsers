using Linq.Parsers.Json.Visitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq.Parsers.Json.Syntax
{
    public class StringToken : JsonSyntaxNode
    {
        #region Fields
        
        private LeftQuote leftQuote;
        private Characters value;
        private RightQuote rightQuote;

        #endregion //Fields

        #region Properties

        #region LeftQuote

        public string LeftQuote
        {
            get
            {
                return this.leftQuote.ToString();
            }
        }

        #endregion //LeftQuote

        #region Value

        public string Value
        {
            get
            {
                return this.value.ToString();
            }
        }

        #endregion //Value

        #region RightQuote

        public string RightQuote
        {
            get
            {
                return this.rightQuote.ToString();
            }
        }

        #endregion //RightQuote
        
        #region NodeType

        public override JsonSyntaxNodeType NodeType
        {
            get
            {
                return JsonSyntaxNodeType.StringToken;
            }
        }

        #endregion //NodeType
        
        #endregion //Properties

        #region Constructors

        public StringToken(LeftQuote leftQuote, Characters value, RightQuote rightQuote)
            : base(leftQuote, value, rightQuote)
        {
            this.leftQuote = leftQuote;
            this.value = value;
            this.rightQuote = rightQuote;
        }

        #endregion //Constructors
    }
}
