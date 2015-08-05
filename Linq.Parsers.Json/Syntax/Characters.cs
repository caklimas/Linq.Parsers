using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linq.Parsers.Json.Visitor;

namespace Linq.Parsers.Json.Syntax
{
    public class Characters : JsonSyntaxNode
    {
        #region Fields

        private Text value;

        #endregion //Fields

        #region Properties

        #region Value

        public string Value
        {
            get
            {
                return this.value.ToString();
            }
        }

        #endregion //Value

        #region NodeType

        public override JsonSyntaxNodeType NodeType
        {
            get
            {
                return JsonSyntaxNodeType.Characters;
            }
        }

        #endregion //NodeType

        #endregion //Properties

        #region Constructors

        public Characters(Text value)
        {
            this.value = value;
        }

        #endregion //Constructors

        public override string ToString()
        {
            return this.value.ToString();
        }
    }
}
