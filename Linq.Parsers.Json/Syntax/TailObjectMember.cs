using Linq.Parsers.Json.Visitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq.Parsers.Json.Syntax
{
    public class TailObjectMember : JsonSyntaxNode
    {
        #region Fields

        private Comma comma;
        private ObjectMember objectMember;

        #endregion //Fields

        #region Properties

        #region NodeType

        public override JsonSyntaxNodeType NodeType
        {
            get
            {
                return JsonSyntaxNodeType.TailObjectMember;
            }
        }

        #endregion //NodeType

        #endregion //Properties

        #region Constructors

        public TailObjectMember(Comma comma, ObjectMember objectMember)
            : base(comma, objectMember)
        {
            this.comma = comma;
            this.objectMember = objectMember;
        }

        #endregion //Constructors
    }
}
