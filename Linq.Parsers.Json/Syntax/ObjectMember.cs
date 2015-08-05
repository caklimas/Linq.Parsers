using Linq.Parsers.Json.Visitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq.Parsers.Json.Syntax
{
    public class ObjectMember : JsonSyntaxNode
    {
        #region Fields
        
        private StringToken objectName;
        private Colon colon;
        private MemberValue objectValue;

        #endregion //Fields

        #region Properties

        #region NodeType

        public override JsonSyntaxNodeType NodeType
        {
            get
            {
                return JsonSyntaxNodeType.ObjectMember;
            }
        }

        #endregion //NodeType

        #endregion //Properties

        #region Constructors

        public ObjectMember(StringToken objectName, Colon colon, MemberValue objectValue) 
            : base(objectName, colon, objectValue)
        {
            this.objectName = objectName;
            this.colon = colon;
            this.objectValue = objectValue;
        }
        
        #endregion //Constructors
    }
}
