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

        #region Append

        [TestMethod]
        public void Append()
        {
            var head = Text.Empty;
            var tail = Text.Empty;
            Assert.IsTrue(head.Append(tail) == "");

            head = Text.Create('a') ;
            tail = Text.Empty;
            Assert.IsTrue(head.Append(tail) == "a");

            head = Text.Empty;
            tail = Text.Create('a');
            Assert.IsTrue(head.Append(tail) == "a");

            head = Text.Create("ab");
            tail = Text.Create("cd");
            Assert.IsTrue(head.Append(tail) == "abcd");
        }

        #endregion //Append

        #region StartsWith

        [TestMethod]
        public void StartsWith()
        {
            var text = Text.Create("");
            Assert.IsTrue(text.StartsWith(""));
            Assert.IsFalse(text.StartsWith("a"));
            
            text = Text.Create("abc");
            Assert.IsTrue(text.StartsWith(""));
            Assert.IsTrue(text.StartsWith("a"));
            Assert.IsTrue(text.StartsWith("ab"));
            Assert.IsTrue(text.StartsWith("abc"));
            Assert.IsFalse (text.StartsWith("b"));
            Assert.IsFalse(text.StartsWith("c"));
        }

        #endregion //StartsWith

        #region Skip

        [TestMethod]
        public void Skip()
        {
            var text = Text.Create("");
            Assert.IsTrue(text.Skip(0) == "");
            Assert.IsTrue(text.Skip(1) == "");
            Assert.IsTrue(text.Skip(2) == "");
            AssertThrows(() => text.Skip(-1));

            text = Text.Create("abc");
            Assert.IsTrue(text.Skip(0) == "abc");
            Assert.IsTrue(text.Skip(1) == "bc");
            Assert.IsTrue(text.Skip(2) == "c");
            Assert.IsTrue(text.Skip(3) == "");
            Assert.IsTrue(text.Skip(4) == "");
            AssertThrows(() => text.Skip(-1));
        }

        #endregion //Skip

        #region SkipWhile

        [TestMethod]
        public void SkipWhile()
        {
            var text = Text.Create("");
            Assert.IsTrue(text.SkipWhile(c => c != 'a') == "");

            text = Text.Create("abc");
            Assert.IsTrue(text.SkipWhile(c => c == 'a' || c == 'b' || c == 'c') == "");
            Assert.IsTrue(text.SkipWhile(c => c == 'a' || c == 'b') == "c");
            Assert.IsTrue(text.SkipWhile(c => c == 'a') == "bc");
            Assert.IsTrue(text.SkipWhile(c => c == 'd') == "abc");
        }

        #endregion //SkipWhile

        #region Take

        [TestMethod]
        public void Take()
        {
            var text = Text.Create("");
            Assert.IsTrue(text.Take(0) == "");
            Assert.IsTrue(text.Take(1) == "");
            Assert.IsTrue(text.Take(2) == "");
            AssertThrows(() => text.Take(-1));

            text = Text.Create("abc");
            Assert.IsTrue(text.Take(0) == "");
            Assert.IsTrue(text.Take(1) == "a");
            Assert.IsTrue(text.Take(2) == "ab");
            Assert.IsTrue(text.Take(3) == "abc");
            Assert.IsTrue(text.Take(4) == "abc");
            AssertThrows(() => text.Skip(-1));
        }

        #endregion //Take

        #region TakeWhile

        [TestMethod]
        public void TakeWhile()
        {
            var text = Text.Create("");
            Assert.IsTrue(text.TakeWhile(c => c == 'a') == "");

            text = Text.Create("abc");
            Assert.IsTrue(text.TakeWhile(c => c == 'a' || c == 'b' || c == 'c') == "abc");
            Assert.IsTrue(text.TakeWhile(c => c == 'a' || c == 'b') == "ab");
            Assert.IsTrue(text.TakeWhile(c => c == 'a') == "a");
            Assert.IsTrue(text.TakeWhile(c => c == 'd') == "");
        }

        #endregion //TakeWhile
    }
}
