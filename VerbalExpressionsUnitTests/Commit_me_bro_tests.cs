using CSharpVerbalExpressions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace VerbalExpressionsUnitTests
{
    [TestFixture]
    class Commit_me_bro_tests
    {


        [Test]
        public void add()
        {
            //Arrange
            var verbEx = VerbalExpressions.DefaultExpression;
            string value = null;

            //Act
            //Assert
            Assert.Throws<ArgumentNullException>(() => verbEx.Add(value, true));
        }


        [Test]
        public void Capture()
        {
            // Arrange
            VerbalExpressions verbEx = VerbalExpressions.DefaultExpression;

            // Act
            verbEx.Add("COD")
                .BeginCapture("GroupNumber")
                .Any("0-9")
                .RepeatPrevious(3)
                .EndCapture()
                .Add("END");

            // Assert
            Assert.AreEqual(@"COD(?<GroupNumber>[0-9]{3})END", verbEx.ToString());
            Assert.IsNull(verbEx.Capture("COD123EN", "GroupNumber"));
        }

        [Test]
        public void EndOfLine()
        {
            var verbEx = VerbalExpressions.DefaultExpression;
            verbEx.Add(".com").EndOfLine(false);

            var isMatch = verbEx.IsMatch("www.google.com/");
            Assert.IsTrue(isMatch, "Should not end the line with .com");
        }

        [Test]
        public void Multiple()
        {
            //Arrange
            var verbEx = VerbalExpressions.DefaultExpression;
            string text = "testesting 123 yahoahoahou another test";
            string expectedExpression = "y(aho)+u";
            //Act
            verbEx.Add("y")
                .Multiple("aho", false)
                .Add("u");

            //Assert
            Assert.IsTrue(verbEx.Test(text));
            Assert.AreEqual(expectedExpression, verbEx.ToString());
        }
        [Test]
        public void AnyOf()
        {
            //Arrange
            var verbEx = VerbalExpressions.DefaultExpression;
            verbEx.Add("A")
                .AnyOf("0-9", false);

            //Act
            //Assert  
            var testMe = "A0";

            Assert.IsTrue(verbEx.Test(testMe), verbEx.ToString());
           // Assert.Throws<ArgumentNullException>(() => verbEx.AnyOf(value, false));
        }

        [Test]
        public void AnythingBut()
        {
            var verbEx = VerbalExpressions.DefaultExpression
                        .StartOfLine()
                        .AnythingBut("a", false)
                        .EndOfLine();

            var testMe = "a";

            Assert.IsFalse(verbEx.Test(testMe));
        }

        [Test]
        public void RemoveModifier()
        {
            var verbEx = VerbalExpressions.DefaultExpression;
            verbEx.AddModifier('s');

            verbEx.RemoveModifier('s');
            var regex = verbEx.ToRegex();
            Assert.IsFalse(regex.Options.HasFlag(RegexOptions.Singleline), "RegexOptions should now have been removed");
        }


        [Test]
        public void Replace()
        {
            var verbEx = VerbalExpressions.DefaultExpression;
            verbEx.Add("asd");

            verbEx.Replace("valami");
            var test = "valami";

            Assert.IsTrue(verbEx.IsMatch(test));
        }

        [Test]
        public void WithOptions()
        {
            var verbEx = VerbalExpressions.DefaultExpression;
            verbEx.WithOptions(RegexOptions.Singleline);

            var regex = verbEx.ToRegex();
            Assert.IsTrue(regex.Options.HasFlag(RegexOptions.Singleline));
        }

        [Test]
        public void Get_RegexCache()
        {
            RegexCache cache = new RegexCache();

            Assert.Throws<ArgumentNullException>(() => cache.Get(null, RegexOptions.Multiline));
        }
    }
}
