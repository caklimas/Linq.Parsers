using Linq.Parsers.Json.Visitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq.Parsers.Json.Syntax
{
    public class JsonArrayElement : JsonSyntaxNode
    {
        #region Fields

        private MemberValue memberValue;

        #endregion //Fields

        #region Properties

        #region NodeType

        public override JsonSyntaxNodeType NodeType
        {
            get
            {
                return JsonSyntaxNodeType.JsonArrayElement;
            }
        }

        #endregion //NodeType

        #endregion //Properties

        #region Constructors

        public JsonArrayElement(MemberValue memberValue)
            : base(memberValue)
        {
            this.memberValue = memberValue;
        }

        #endregion //Constructors
    }
}
