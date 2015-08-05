using Linq.Parsers.Json.Visitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq.Parsers.Json.Syntax
{
    public class ObjectMembers : JsonSyntaxNode
    {
        #region Fields

        private ObjectMember objectMember;
        private IEnumerable<TailObjectMember> objectMembers;

        #endregion //Fields

        #region Properties

        #region NodeType

        public override JsonSyntaxNodeType NodeType
        {
            get
            {
                return JsonSyntaxNodeType.ObjectMembers;
            }
        }

        #endregion //NodeType

        #endregion //Properties

        #region Constructors

        public ObjectMembers(ObjectMember objectMember, IEnumerable<TailObjectMember> objectMembers)
            : base(new List<JsonSyntaxNode>() { objectMember }.Concat(objectMembers))
        {
            this.objectMember = objectMember;
            this.objectMembers = objectMembers;
        }

        #endregion //Constructors
    }
}
