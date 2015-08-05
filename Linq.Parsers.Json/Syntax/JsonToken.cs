using Linq.Parsers.Json.Visitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq.Parsers.Json.Syntax
{
    public abstract class JsonToken : JsonSyntaxNode
    {
        #region Fields

        private Text leftTrivia;
        private Text value;
        private Text rightTrivia;

        #endregion //Fields

        #region Properties

        #region LeftTrivia

        public string LeftTrivia
        {
            get
            {
                return this.leftTrivia.ToString();
            }
        }

        #endregion //LeftTrivia

        #region Value

        public string Value
        {
            get
            {
                return this.value.ToString();
            }
        }

        #endregion //Value

        #region RightTrivia

        public string RightTrivia
        {
            get
            {
                return this.rightTrivia.ToString();
            }
        }

        #endregion //RightTrivia

        #endregion //Properties

        #region Constructors

        public JsonToken(Text leftTrivia, Text value, Text rightTrivia)
        {
            this.leftTrivia = leftTrivia;
            this.value = value;
            this.rightTrivia = rightTrivia;
        }

        #endregion //Constructors

        #region Methods

        #region Base Class Overrides

        #region GetChildren

        public override JsonSyntaxNode[] GetChildren()
        {
            return emptyChildren;
        }

        #endregion //GetChildren

        #region ToString

        public override string ToString()
        {
            return 
                this.leftTrivia
                .Append(this.value)
                .Append(this.rightTrivia)
                .ToString();
        }

        #endregion //ToString

        #endregion //Base Class Overrides

        #endregion //Methods

        #region Static Members

        #region Fields

        private static JsonSyntaxNode[] emptyChildren = new JsonSyntaxNode[0];

        #endregion //Fields

        #endregion //Static Members
    }
}
