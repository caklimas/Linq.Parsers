using Linq.Parsers.Json.Visitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq.Parsers.Json.Syntax
{
    public class True : JsonToken
    {
        #region Properties

        #region NodeType

        public override JsonSyntaxNodeType NodeType
        {
            get
            {
                return JsonSyntaxNodeType.True;
            }
        }

        #endregion //NodeType

        #endregion //Properties

        public True(Text leftTrivia, Text value, Text rightTrivia) 
            : base(leftTrivia, value, rightTrivia)
        {
        }
    }
}
