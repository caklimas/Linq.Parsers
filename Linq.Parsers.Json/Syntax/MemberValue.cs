using Linq.Parsers.Json.Visitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq.Parsers.Json.Syntax
{
    public class MemberValue : JsonSyntaxNode
    {
        #region Fields

        private JsonSyntaxNode valueNode;

        #endregion //Fields

        #region Properties

        #region Value 

        public string Value
        {
            get
            {
                return this.valueNode.ToString();
            }
        }

        #endregion //Value 
        
        #region NodeType

        public override JsonSyntaxNodeType NodeType
        {
            get
            {
                return JsonSyntaxNodeType.MemberValue;
            }
        }
        
        #endregion //Properties

        #endregion //Properties

        #region Constructors

        public MemberValue(JsonSyntaxNode valueNode)
            : base(valueNode)
        {
            this.valueNode = valueNode;
        }

        #endregion //Constructors
    }
}
