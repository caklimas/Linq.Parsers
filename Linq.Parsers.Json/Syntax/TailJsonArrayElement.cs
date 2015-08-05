using Linq.Parsers.Json.Visitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq.Parsers.Json.Syntax
{
    public class TailJsonArrayElement : JsonSyntaxNode
    {
        #region Fields

        private Comma comma;
        private JsonArrayElement arrayElement;

        #endregion //Fields

        #region Properties

        #region NodeType

        public override JsonSyntaxNodeType NodeType
        {
            get
            {
                return JsonSyntaxNodeType.TailJsonArrayElement;
            }
        }

        #endregion //NodeType

        #endregion //Properties

        #region Constructors

        public TailJsonArrayElement(Comma comma, JsonArrayElement arrayElement)
            : base(comma, arrayElement)
        {
            this.comma = comma;
            this.arrayElement = arrayElement;
        }

        #endregion //Constructors
    }
}
