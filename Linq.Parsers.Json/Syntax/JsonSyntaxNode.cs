using Linq.Parsers.Json.Visitor;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq.Parsers.Json.Syntax
{
    public abstract class JsonSyntaxNode
    {
        #region Fields

        private ReadOnlyCollection<JsonSyntaxNode> children;
        protected bool isEvaluated;
        internal string stringValue;

        #endregion //Fields

        #region Properties

        #region Indexer

        public JsonSyntaxNode this[int index]
        {
            get
            {
                return this.children[index];
            }
        }

        #endregion //Indexer

        #region JsonSyntaxNodeType

        public abstract JsonSyntaxNodeType NodeType { get; }

        #endregion //JsonSyntaxNodeType

        #endregion //Properties

        #region Constructors

        public JsonSyntaxNode(params JsonSyntaxNode[] children)
            : this(children.AsEnumerable())
        { }

        public JsonSyntaxNode(IEnumerable<JsonSyntaxNode> children)
        {
            children.AssertNotNull();
            this.children = new ReadOnlyCollection<JsonSyntaxNode>(children.ToList());
        }

        #endregion //Constructors

        #region Methods

        #region GetChildren

        public virtual JsonSyntaxNode[] GetChildren()
        {
            return this.children.ToArray();
        }

        #endregion //GetChildren

        #region ToString

        public override string ToString()
        {
            if (!this.isEvaluated)
                this.stringValue = Evaluate();

            return this.stringValue;
        }

        #endregion //ToString

        #endregion //Methods

        #region Utilities

        #region Evaluate

        private string Evaluate()
        {
            var stringBuilder = new StringBuilder();
            foreach (var child in this.children.Where(c => c != null))
                stringBuilder.Append(child.ToString());

            this.isEvaluated = true;
            return stringBuilder.ToString();
        }

        #endregion //Evaluate

        #endregion //Utilities
    }
}
