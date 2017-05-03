using CSharpVerbalExpressions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VerbalExpressionsUnitTests
{
    [TestFixture]
    class Commit_me_bro_tests_2
    {
        [Test]
        public void StartOfLine_CreatesCorrectRegex_False()
        {
            var verbEx = VerbalExpressions.DefaultExpression;
            verbEx.StartOfLine(false);
            Assert.AreEqual("", verbEx.ToString(), "missing start of line regex");
        }

        [Test]
        public void SomethingBut_EmptyStringAsParameter_DoesNotMatch_False()
        {
            // Arange
            VerbalExpressions verbEx = VerbalExpressions.DefaultExpression.SomethingBut("Test", false);
            string testString = string.Empty;

            // Act
            bool isMatch = verbEx.IsMatch(testString);

            // Assert
            Assert.IsFalse(isMatch, "Test string should be empty.");
        }

        [Test]
        public void Range_EmptyString()
        {
            //Arrange
            var verbEx = VerbalExpressions.DefaultExpression;
            object[] range = new object[3] { "", "", ""};

            //Act
            verbEx.Range(range);
            //Assert
            Assert.AreEqual("", verbEx.ToString());
        }

        [Test]
        public void TestingIfWeHaveAValidURL_WithFind()
        {
            var verbEx = VerbalExpressions.DefaultExpression
                        .StartOfLine()
                        .Find("http")
                        .Maybe("s")
                        .Then("://")
                        .Maybe("www.")
                        .AnythingBut(" ")
                        .EndOfLine();

            var testMe = "https://www.google.com";

            Assert.IsTrue(verbEx.Test(testMe), "The URL is incorrect");
        }

    }
}
