using Linq.Parsers.Json.Visitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq.Parsers.Json.Syntax
{
    public class JsonObject : JsonSyntaxNode
    {
        #region Fields

        private OpenBrace openBrace;
        private ObjectMembers objectMembers;
        private CloseBrace closeBrace;

        #endregion //Fields

        #region Properties

        #region NodeType

        public override JsonSyntaxNodeType NodeType
        {
            get
            {
                return JsonSyntaxNodeType.JsonObject;
            }
        }

        #endregion //NodeType

        #endregion //Properties

        #region Constructors

        public JsonObject(OpenBrace openBrace, ObjectMembers objectMembers, CloseBrace closeBrace)
            : base(openBrace, objectMembers, closeBrace)
        {
            this.openBrace = openBrace;
            this.objectMembers = objectMembers;
            this.closeBrace = closeBrace;
        }
        
        #endregion //Constructors
    }
}
