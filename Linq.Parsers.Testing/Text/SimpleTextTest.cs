using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
        }

        #endregion //Constructor

        #region GetEnumerator

        [TestMethod]
        public void GetEnumerator()
        {
            var text = new SimpleText("", 0, 0);
            var characters = text.ToArray();
            Assert.IsTrue(characters.Length == 0);

            text = new SimpleText("abc", 0, 3);
            characters = text.ToArray();
            Assert.IsTrue(characters.Length == 3);
            Assert.IsTrue(characters[0] == 'a');
            Assert.IsTrue(characters[1] == 'b');
            Assert.IsTrue(characters[2] == 'c');

            text = new SimpleText("__abc__", 2, 3);
            characters = text.ToArray();
            Assert.IsTrue(characters.Length == 3);
            Assert.IsTrue(characters[0] == 'a');
            Assert.IsTrue(characters[1] == 'b');
            Assert.IsTrue(characters[2] == 'c');
        }

        #endregion //GetEnumerator

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
        }

        #endregion //Split

        #region IsSimpleTextAppendableTo

        [TestMethod]
        public void IsSimpleTextAppendableTo()
        {
            var text = "abc";
            var head = new SimpleText(text, 0, 1);
            var tail = new SimpleText(text, 2, 1);
            Assert.IsFalse(head.IsSimpleTextAppendableTo(tail));

            text = "abc";
            head = new SimpleText(text, 0, 2);
            tail = new SimpleText(text, 1, 2);
            Assert.IsFalse(head.IsSimpleTextAppendableTo(tail));

            text = "abc";
            head = new SimpleText(text, 0, 1);
            tail = new SimpleText(text, 1, 2);
            Assert.IsTrue(head.IsSimpleTextAppendableTo(tail));

            text = "abc";
            head = new SimpleText(text, 0, 2);
            tail = new SimpleText(text, 2, 1);
            Assert.IsTrue(head.IsSimpleTextAppendableTo(tail));
        }

        #endregion //IsSimpleTextAppendableTo

        #region IsComplexTextAppendableTo

        [TestMethod]
        public void IsComplexTextAppendableTo()
        {
            var text = "abc";
            var head = new SimpleText(text, 0, 1);
            var tail = new ComplexText(new[] { new SimpleText(text, 2, 1) });
            Assert.IsFalse(head.IsComplexTextAppendableTo(tail));

            text = "abc";
            head = new SimpleText(text, 0, 2);
            tail = new ComplexText(new[] { new SimpleText(text, 1, 2) });
            Assert.IsFalse(head.IsComplexTextAppendableTo(tail));

            text = "abc";
            head = new SimpleText(text, 0, 1);
            tail = new ComplexText(new[] { new SimpleText(text, 1, 2) });
            Assert.IsTrue(head.IsComplexTextAppendableTo(tail));

            text = "abc";
            head = new SimpleText(text, 0, 2);
            tail = new ComplexText(new[] { new SimpleText(text, 2, 1) });
            Assert.IsTrue(head.IsComplexTextAppendableTo(tail));
        }

        #endregion //IsComplexTextAppendableTo

        #region AppendSimpleText

        [TestMethod]
        public void AppendSimpleText()
        {
            var text = "abc";
            var head = new SimpleText(text, 0, 1);
            var tail = new SimpleText(text, 2, 1);
            var result = head.AppendSimpleText(tail);
            Assert.IsTrue(result == "ac");
            Assert.IsFalse(result is SimpleText);

            text = "abc";
            head = new SimpleText(text, 0, 2);
            tail = new SimpleText(text, 1, 2);
            result = head.AppendSimpleText(tail);
            Assert.IsTrue(result == "abbc");
            Assert.IsFalse(result is SimpleText);

            text = "abc";
            head = new SimpleText(text, 0, 1);
            tail = new SimpleText(text, 1, 2);
            result = head.AppendSimpleText(tail);
            Assert.IsTrue(result == "abc");
            Assert.IsTrue(result is SimpleText);

            text = "abc";
            head = new SimpleText(text, 0, 2);
            tail = new SimpleText(text, 2, 1);
            result = head.AppendSimpleText(tail);
            Assert.IsTrue(result == "abc");
            Assert.IsTrue(result is SimpleText);
        }

        #endregion //AppendSimpleText

        #region AppendComplexText

        [TestMethod]
        public void AppendComplexText()
        {
            var text = "abc";
            var head = new SimpleText(text, 0, 1);
            var tail = new ComplexText(new[] { new SimpleText(text, 2, 1) });
            var result = head.AppendComplexText(tail);
            Assert.IsTrue(result == "ac");
            Assert.IsFalse(result is SimpleText);

            text = "abc";
            head = new SimpleText(text, 0, 2);
            tail = new ComplexText(new[] { new SimpleText(text, 1, 2) });
            result = head.AppendComplexText(tail);
            Assert.IsTrue(result == "abbc");
            Assert.IsFalse(result is SimpleText);

            text = "abc";
            head = new SimpleText(text, 0, 1);
            tail = new ComplexText(new[] { new SimpleText(text, 1, 2) });
            result = head.AppendComplexText(tail);
            Assert.IsTrue(result == "abc");
            Assert.IsTrue(result is SimpleText);

            text = "abc";
            head = new SimpleText(text, 0, 2);
            tail = new ComplexText(new[] { new SimpleText(text, 2, 1) });
            result = head.AppendComplexText(tail);
            Assert.IsTrue(result == "abc");
            Assert.IsTrue(result is SimpleText);
        }

        #endregion //AppendComplexText
    }
}
