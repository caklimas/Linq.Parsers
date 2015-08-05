using Linq.Parsers.Json.Visitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq.Parsers.Json.Syntax
{
    public class False : JsonToken
    {
        #region Properties

        #region NodeType

        public override JsonSyntaxNodeType NodeType
        {
            get
            {
                return JsonSyntaxNodeType.False;
            }
        }

        #endregion //NodeType

        #endregion //Properties

        public False(Text leftTrivia, Text value, Text rightTrivia) 
            : base(leftTrivia, value, rightTrivia)
        {
        }
    }
}
