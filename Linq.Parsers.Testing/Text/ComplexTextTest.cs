using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linq.Parsers.Testing
{
    [TestClass]
    public class ComplexTextTest : Test
    {
        #region Constructor

        [TestMethod]
        public void Constructor()
        {
            var text = new ComplexText(new[]
            {
                new SimpleText("", 0, 0)
            });
            Assert.IsTrue(text == "");

            text = new ComplexText(new[]
            {
                new SimpleText("", 0, 0),
                new SimpleText("", 0, 0),
                new SimpleText("", 0, 0)
            });
            Assert.IsTrue(text == "");

            text = new ComplexText(new[]
            {
                new SimpleText("", 0, 0)
            }, 
            enableOptimizations: false);
            Assert.IsTrue(text == "");

            text = new ComplexText(new[]
            {
                new SimpleText("", 0, 0),
                new SimpleText("", 0, 0),
                new SimpleText("", 0, 0)
            }, 
            enableOptimizations: false);
            Assert.IsTrue(text == "");

            text = new ComplexText(new[]
            {
                new SimpleText("a", 0, 1),
                new SimpleText("b", 0, 1),
                new SimpleText("c", 0, 1)
            });
            Assert.IsTrue(text == "abc");

            text = new ComplexText(new[]
            {
                new SimpleText("a", 0, 1),
                new SimpleText("b", 0, 1),
                new SimpleText("c", 0, 1)
            },
            enableOptimizations: false);
            Assert.IsTrue(text == "abc");

            var str = "abc";
            text = new ComplexText(new[]
            {
                new SimpleText(str, 0, 1),
                new SimpleText(str, 1, 1),
                new SimpleText(str, 2, 1)
            });
            Assert.IsTrue(text == "abc");

            text = new ComplexText(new[]
            {
                new SimpleText(str, 0, 1),
                new SimpleText(str, 1, 1),
                new SimpleText(str, 2, 1)
            },
            enableOptimizations: false);
            Assert.IsTrue(text == "abc");
        }

        #endregion //Constructor

        #region FlattenText

        [TestMethod]
        public void FlattenText()
        {
            var texts = new Text[] { };
            Assert.IsTrue(ComplexText.FlattenText(texts).Count() == 0);

            texts = new Text[] 
            {
                new SimpleText("", 0, 0),
                new SimpleText("", 0, 0),
                new SimpleText("", 0, 0)
            };
            Assert.IsTrue(ComplexText.FlattenText(texts).Count() == 3);

            texts = new Text[]
            {
                new ComplexText(new[]
                {
                    new SimpleText("", 0, 0),
                    new SimpleText("", 0, 0),
                    new SimpleText("", 0, 0)
                },
                enableOptimizations: false),
                new ComplexText(new[]
                {
                    new SimpleText("", 0, 0),
                    new SimpleText("", 0, 0),
                    new SimpleText("", 0, 0)
                },
                enableOptimizations: false),
                new ComplexText(new[]
                {
                    new SimpleText("", 0, 0),
                    new SimpleText("", 0, 0),
                    new SimpleText("", 0, 0)
                },
                enableOptimizations: false),
            };
            Assert.IsTrue(ComplexText.FlattenText(texts).Count() == 9);

            var str = "abc";
            texts = new Text[]
            {
                new SimpleText(str, 0, 1),
                new SimpleText(str, 1, 1),
                new SimpleText(str, 2, 1)
            };
            Assert.IsTrue(ComplexText.FlattenText(texts).Count() == 3);
        }

        #endregion //FlattenText

        #region FlattenTextOptimally

        [TestMethod]
        public void FlattenTextOptimally()
        {
            var texts = new Text[] { };
            Assert.IsTrue(ComplexText.FlattenTextOptimally(texts).Count() == 0);

            texts = new Text[]
            {
                new SimpleText("", 0, 0),
                new SimpleText("", 0, 0),
                new SimpleText("", 0, 0)
            };
            Assert.IsTrue(ComplexText.FlattenTextOptimally(texts).Count() == 0);

            texts = new Text[]
            {
                new ComplexText(new[]
                {
                    new SimpleText("", 0, 0),
                    new SimpleText("", 0, 0),
                    new SimpleText("", 0, 0)
                },
                enableOptimizations: false),
                new ComplexText(new[]
                {
                    new SimpleText("", 0, 0),
                    new SimpleText("", 0, 0),
                    new SimpleText("", 0, 0)
                },
                enableOptimizations: false),
                new ComplexText(new[]
                {
                    new SimpleText("", 0, 0),
                    new SimpleText("", 0, 0),
                    new SimpleText("", 0, 0)
                },
                enableOptimizations: false),
            };
            Assert.IsTrue(ComplexText.FlattenTextOptimally(texts).Count() == 0);

            var str = "abc";
            texts = new Text[]
            {
                new SimpleText(str, 0, 1),
                new SimpleText(str, 1, 1),
                new SimpleText(str, 2, 1)
            };
            Assert.IsTrue(ComplexText.FlattenTextOptimally(texts).Count() == 1);

            texts = new Text[]
            {
                new ComplexText(new[] 
                {
                    new SimpleText("a", 0, 1),
                    new SimpleText("a", 0, 1),
                    new SimpleText("a", 0, 1)
                }),
                new ComplexText(new[]
                {
                    new SimpleText("b", 0, 1),
                    new SimpleText("b", 0, 1),
                    new SimpleText("b", 0, 1)
                }),
                new ComplexText(new[]
                {
                    new SimpleText("c", 0, 1),
                    new SimpleText("c", 0, 1),
                    new SimpleText("c", 0, 1)
                }),
            };
            Assert.IsTrue(ComplexText.FlattenTextOptimally(texts).Count() == 9);

            texts = new Text[]
            {
                new ComplexText(new[]
                {
                    new SimpleText(str, 0, 1)
                }),
                new SimpleText(str, 1, 1),
                new ComplexText(new[]
                {
                    new SimpleText(str, 2, 1)
                }),
            };
            Assert.IsTrue(ComplexText.FlattenTextOptimally(texts).Count() == 1);
        }

        #endregion //FlattenTextOptimally

        #region GetEnumerator

        [TestMethod]
        public void GetEnumerator()
        {
            var text = new ComplexText(new SimpleText("", 0, 0));
            var characters = text.ToArray();
            Assert.IsTrue(characters.Length == 0);

            text = new ComplexText(new SimpleText("abc", 0, 3));
            characters = text.ToArray();
            Assert.IsTrue(characters.Length == 3);
            Assert.IsTrue(characters[0] == 'a');
            Assert.IsTrue(characters[1] == 'b');
            Assert.IsTrue(characters[2] == 'c');

            text = new ComplexText(
                new SimpleText("a", 0, 1),
                new SimpleText("b", 0, 1),
                new SimpleText("c", 0, 1));
            characters = text.ToArray();
            Assert.IsTrue(characters.Length == 3);
            Assert.IsTrue(characters[0] == 'a');
            Assert.IsTrue(characters[1] == 'b');
            Assert.IsTrue(characters[2] == 'c');

            text = new ComplexText(
                new ComplexText(new[]
                {
                    new SimpleText("a", 0, 1),
                    new SimpleText("a", 0, 1),
                    new SimpleText("a", 0, 1)
                }),
                new ComplexText(new[]
                {
                    new SimpleText("b", 0, 1),
                    new SimpleText("b", 0, 1),
                    new SimpleText("b", 0, 1)
                }),
                new ComplexText(new[]
                {
                    new SimpleText("c", 0, 1),
                    new SimpleText("c", 0, 1),
                    new SimpleText("c", 0, 1)
                }));
            characters = text.ToArray();
            Assert.IsTrue(characters.Length == 9);
            Assert.IsTrue(characters[0] == 'a');
            Assert.IsTrue(characters[1] == 'a');
            Assert.IsTrue(characters[2] == 'a');
            Assert.IsTrue(characters[3] == 'b');
            Assert.IsTrue(characters[4] == 'b');
            Assert.IsTrue(characters[5] == 'b');
            Assert.IsTrue(characters[6] == 'c');
            Assert.IsTrue(characters[7] == 'c');
            Assert.IsTrue(characters[8] == 'c');
        }

        #endregion //GetEnumerator

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
                Text.Empty);

            Assert.IsTrue(text.ToString() == "abcdef");

            text = new ComplexText(
                new ComplexText(Text.Empty),
                new SimpleText("__ab__", 2, 2),
                new ComplexText(Text.Empty),
                new SimpleText("___c___", 3, 1),
                new ComplexText(Text.Empty),
                new SimpleText("_def_", 1, 3),
                new ComplexText(Text.Empty));

            Assert.IsTrue(text.ToString() == "abcdef");
        }

        #endregion //ToString

        #region IsSimpleTextAppendableTo

        [TestMethod]
        public void IsSimpleTextAppendableTo()
        {
            var text = "abc";
            var head = new ComplexText(new SimpleText(text, 0, 1));
            var tail = new SimpleText(text, 2, 1);
            Assert.IsFalse(head.IsSimpleTextAppendableTo(tail));

            text = "abc";
            head = new ComplexText(new SimpleText(text, 0, 2));
            tail = new SimpleText(text, 1, 2);
            Assert.IsFalse(head.IsSimpleTextAppendableTo(tail));

            text = "abc";
            head = new ComplexText(new SimpleText(text, 0, 1));
            tail = new SimpleText(text, 1, 2);
            Assert.IsTrue(head.IsSimpleTextAppendableTo(tail));

            text = "abc";
            head = new ComplexText(new SimpleText(text, 0, 2));
            tail = new SimpleText(text, 2, 1);
            Assert.IsTrue(head.IsSimpleTextAppendableTo(tail));
        }

        #endregion //IsSimpleTextAppendableTo

        #region IsComplexTextAppendableTo

        [TestMethod]
        public void IsComplexTextAppendableTo()
        {
            var text = "abc";
            var head = new ComplexText(new SimpleText(text, 0, 1));
            var tail = new ComplexText(new SimpleText(text, 2, 1));
            Assert.IsFalse(head.IsComplexTextAppendableTo(tail));

            text = "abc";
            head = new ComplexText(new SimpleText(text, 0, 2));
            tail = new ComplexText(new[] { new SimpleText(text, 1, 2) });
            Assert.IsFalse(head.IsComplexTextAppendableTo(tail));

            text = "abc";
            head = new ComplexText(new SimpleText(text, 0, 1));
            tail = new ComplexText(new[] { new SimpleText(text, 1, 2) });
            Assert.IsTrue(head.IsComplexTextAppendableTo(tail));

            text = "abc";
            head = new ComplexText(new SimpleText(text, 0, 2));
            tail = new ComplexText(new[] { new SimpleText(text, 2, 1) });
            Assert.IsTrue(head.IsComplexTextAppendableTo(tail));
        }

        #endregion //IsComplexTextAppendableTo

        #region AppendSimpleText

        [TestMethod]
        public void AppendSimpleText()
        {
            var text = "abc";
            var head = new ComplexText(new SimpleText(text, 0, 1));
            var tail = new SimpleText(text, 2, 1);
            var result = head.AppendSimpleText(tail);
            Assert.IsTrue(result == "ac");
            Assert.IsTrue((result as ComplexText).texts.Count == 2);

            text = "abc";
            head = new ComplexText(new SimpleText(text, 0, 2));
            tail = new SimpleText(text, 1, 2);
            result = head.AppendSimpleText(tail);
            Assert.IsTrue(result == "abbc");
            Assert.IsTrue((result as ComplexText).texts.Count == 2);

            text = "abc";
            head = new ComplexText(new SimpleText(text, 0, 1));
            tail = new SimpleText(text, 1, 2);
            result = head.AppendSimpleText(tail);
            Assert.IsTrue(result == "abc");
            Assert.IsTrue(result is SimpleText);

            text = "abc";
            head = new ComplexText(new SimpleText(text, 0, 2));
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
            var head = new ComplexText(new SimpleText(text, 0, 1));
            var tail = new ComplexText(new SimpleText(text, 2, 1));
            var result = head.AppendComplexText(tail);
            Assert.IsTrue(result == "ac");
            Assert.IsTrue((result as ComplexText).texts.Count == 2);

            text = "abc";
            head = new ComplexText(new SimpleText(text, 0, 2));
            tail = new ComplexText(new SimpleText(text, 1, 2));
            result = head.AppendComplexText(tail);
            Assert.IsTrue(result == "abbc");
            Assert.IsTrue((result as ComplexText).texts.Count == 2);

            text = "abc";
            head = new ComplexText(new SimpleText(text, 0, 1));
            tail = new ComplexText(new SimpleText(text, 1, 2));
            result = head.AppendComplexText(tail);
            Assert.IsTrue(result == "abc");
            Assert.IsTrue(result is SimpleText);

            text = "abc";
            head = new ComplexText(new SimpleText(text, 0, 2));
            tail = new ComplexText(new SimpleText(text, 2, 1));
            result = head.AppendComplexText(tail);
            Assert.IsTrue(result == "abc");
            Assert.IsTrue(result is SimpleText);
        }

        #endregion //AppendComplexText
    }
}
