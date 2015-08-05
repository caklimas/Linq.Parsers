using Linq.Parsers.Json.Visitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq.Parsers.Json.Syntax
{
    public class JsonArrayElements : JsonSyntaxNode
    {
        #region Fields

        private JsonArrayElement arrayElement;
        private IEnumerable<TailJsonArrayElement> elements;

        #endregion //Fields

        #region Properties

        #region NodeType

        public override JsonSyntaxNodeType NodeType
        {
            get
            {
                return JsonSyntaxNodeType.JsonArrayElements;
            }
        }

        #endregion //NodeType

        #endregion //Properties

        #region Constructors

        public JsonArrayElements(JsonArrayElement arrayElement, IEnumerable<TailJsonArrayElement> elements)
            : base(new List<JsonSyntaxNode>() { arrayElement }.Concat(elements))
        {
            this.arrayElement = arrayElement;
            this.elements = elements;
        }

        #endregion //Constructors
    }
}
