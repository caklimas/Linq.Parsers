using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linq.Parsers.Testing
{
    [TestClass]
    public class ComplexTextTest : Test
    {
        #region ToString

        [TestMethod]
        public new void ToString()
        {
            var text = new ComplexText(Text.Empty);
            Assert.IsTrue(text.ToString() == "");

            text = new ComplexText(Text.Create("a"));
            Assert.IsTrue(text.ToString() == "a");

            text = new ComplexText(Text.Create("ab"));
            Assert.IsTrue(text.ToString() == "ab");

            text = new ComplexText(
                Text.Empty,
                Text.Create("ab"),
                Text.Empty,
                Text.Create("c"),
                Text.Empty,
                Text.Create("def"),
                null,
                Text.Empty);

            Assert.IsTrue(text.ToString() == "abcdef");

            text = new ComplexText(
                new ComplexText(Text.Empty),
                new SimpleText("__ab__", 2, 2),
                new ComplexText(Text.Empty),
                new SimpleText("___c___", 3, 1),
                new ComplexText(Text.Empty),
                new SimpleText("_def_", 1, 3),
                null,
                new ComplexText(Text.Empty));

            Assert.IsTrue(text.ToString() == "abcdef");
        }

        #endregion //ToString
    }
}
