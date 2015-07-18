using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linq.Parsers.Testing
{
    [TestClass]
    public class TextTest : Test
    {
        #region Create

        [TestMethod]
        public void Create()
        {
            var text = Text.Create("");
            Assert.IsTrue(text.ToString() == "");

            text = Text.Create("a");
            Assert.IsTrue(text.ToString() == "a");

            text = Text.Create("ab");
            Assert.IsTrue(text.ToString() == "ab");

            text = Text.Create("abc");
            Assert.IsTrue(text.ToString() == "abc");

            text = Text.Create('a');
            Assert.IsTrue(text.ToString() == "a");

            text = Text.Create('b');
            Assert.IsTrue(text.ToString() == "b");

            text = Text.Create('c');
            Assert.IsTrue(text.ToString() == "c");
        }

        #endregion //Create
    }
}
