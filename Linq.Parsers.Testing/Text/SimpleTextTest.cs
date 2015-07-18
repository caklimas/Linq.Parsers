using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linq.Parsers.Testing
{
    [TestClass]
    public class SimpleTextTest : Test
    {
        #region Constructor

        [TestMethod]
        public void Constructor()
        {
            new SimpleText("", 0, 0);
            new SimpleText("a", 0, 0);
            new SimpleText("a", 0, 1);
            new SimpleText("ab", 0, 0);
            new SimpleText("ab", 1, 0);
            new SimpleText("ab", 0, 1);
            new SimpleText("ab", 1, 1);

            AssertThrows(() => new SimpleText("", 1, 0));
            AssertThrows(() => new SimpleText("", 0, 1));
            AssertThrows(() => new SimpleText("", -1, 0));
            AssertThrows(() => new SimpleText("", 0, -1));
            AssertThrows(() => new SimpleText("a", 1, 0));
            AssertThrows(() => new SimpleText("a", 1, 1));
            AssertThrows(() => new SimpleText("ab", 1, 1));
        }

        #endregion //Constructor

        #region ToString

        [TestMethod]
        public new void ToString()
        {
            var text = new SimpleText("", 0, 0);
            Assert.IsTrue(text.ToString() == "");

            text = new SimpleText("a", 0, 0);
            Assert.IsTrue(text.ToString() == "");

            text = new SimpleText("a", 0, 1);
            Assert.IsTrue(text.ToString() == "a");

            text = new SimpleText("ab", 0, 0);
            Assert.IsTrue(text.ToString() == "");

            text = new SimpleText("ab", 1, 0);
            Assert.IsTrue(text.ToString() == "");

            text = new SimpleText("ab", 0, 1);
            Assert.IsTrue(text.ToString() == "a");

            text = new SimpleText("ab", 1, 1);
            Assert.IsTrue(text.ToString() == "b");
        }

        #endregion //ToString

        #region Split

        [TestMethod]
        public void Split()
        {
            var text = new SimpleText("", 0, 0);

            var split = text.Split(0);
            Assert.IsTrue(split.Head.ToString() == "");
            Assert.IsTrue(split.Tail.ToString() == "");

            AssertThrows(() => text.Split(1));
            AssertThrows(() => text.Split(-1));

            text = new SimpleText("a", 0, 1);

            split = text.Split(0);
            Assert.IsTrue(split.Head.ToString() == "");
            Assert.IsTrue(split.Tail.ToString() == "a");

            split = text.Split(1);
            Assert.IsTrue(split.Head.ToString() == "a");
            Assert.IsTrue(split.Tail.ToString() == "");

            AssertThrows(() => text.Split(2));
            AssertThrows(() => text.Split(-1));

            text = new SimpleText("__a__", 2, 1);
            Assert.IsTrue(text.ToString() == "a");

            split = text.Split(0);
            Assert.IsTrue(split.Head.ToString() == "");
            Assert.IsTrue(split.Tail.ToString() == "a");

            split = text.Split(1);
            Assert.IsTrue(split.Head.ToString() == "a");
            Assert.IsTrue(split.Tail.ToString() == "");

            AssertThrows(() => text.Split(2));
            AssertThrows(() => text.Split(-1));

            text = new SimpleText("__abc__", 2, 3);
            Assert.IsTrue(text.ToString() == "abc");

            split = text.Split(0);
            Assert.IsTrue(split.Head.ToString() == "");
            Assert.IsTrue(split.Tail.ToString() == "abc");

            split = text.Split(1);
            Assert.IsTrue(split.Head.ToString() == "a");
            Assert.IsTrue(split.Tail.ToString() == "bc");

            split = text.Split(2);
            Assert.IsTrue(split.Head.ToString() == "ab");
            Assert.IsTrue(split.Tail.ToString() == "c");

            split = text.Split(3);
            Assert.IsTrue(split.Head.ToString() == "abc");
            Assert.IsTrue(split.Tail.ToString() == "");

            AssertThrows(() => text.Split(4));
            AssertThrows(() => text.Split(-1));
        }

        #endregion //Split
    }
}
