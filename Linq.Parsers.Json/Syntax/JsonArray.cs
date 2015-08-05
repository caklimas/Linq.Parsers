using Linq.Parsers.Json.Visitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq.Parsers.Json.Syntax
{
    public class JsonArray : JsonSyntaxNode
    {
        #region Fields

        private OpenBracket openBracket;
        private JsonArrayElements arrayElements;
        private CloseBracket closeBracket;

        #endregion //Fields

        #region Properties

        #region NodeType

        public override JsonSyntaxNodeType NodeType
        {
            get
            {
                return JsonSyntaxNodeType.JsonArray;
            }
        }

        #endregion //NodeType
        
        #endregion //Properties

        #region Constructors

        public JsonArray(OpenBracket openBracket, JsonArrayElements arrayElements, CloseBracket closeBracket)
            : base(openBracket, arrayElements, closeBracket)
        {
            this.openBracket = openBracket;
            this.arrayElements = arrayElements;
            this.closeBracket = closeBracket;
        }

        #endregion //Constructors
    }
}
