using Microsoft.VisualStudio.TestTools.UnitTesting;
using SerkoService.Models;
using SerkoService.Models.Exceptions;
using System;

namespace SerkoService.Tests.Models
{
    [TestClass]
    public class XMLValueParserTest
    {
        [TestMethod]
        public void GoodData()
        {
            var XMLValueParser = new XMLValueParser();

            var value = XMLValueParser.ValueFinder("data", true, "xxxxxxxxx<data>abc</data>xxxxxxxx");

            Assert.AreEqual("abc", value);
        }

        [TestMethod]
        public void MissingOpeningTag()
        {
            var XMLValueParser = new XMLValueParser();

            try
            {
                var value = XMLValueParser.ValueFinder("data",true, "xxxxxxxxxdata>abc</data>xxxxxxxx");
            }
            catch (Exception ex)
            {
                Assert.AreEqual(typeof(NoOpeningTagException), ex.GetType());
            }
        }

        [TestMethod]
        public void MissingClosingTag()
        {
            var XMLValueParser = new XMLValueParser();

            try
            {
                var value = XMLValueParser.ValueFinder("data", false, "xxxxxxxxx<data>abc</dat>xxxxxxxx");

            }
            catch (Exception ex)
            {
                Assert.AreEqual(typeof(NoClosingTagException),ex.GetType());
            }
        }
        [TestMethod]
        public void EmptyValue()
        {
            var XMLValueParser = new XMLValueParser();
            var value = XMLValueParser.ValueFinder("data",true, "xxxxxxxxx<data></data>xxxxxxxx");
            Assert.AreEqual("",value);
        }

        [TestMethod]
        public void LeftEdgeValid()
        {
            var XMLValueParser = new XMLValueParser();

            var value = XMLValueParser.ValueFinder("data",true, "<data>abc</data>xxxxxxxx");

            Assert.AreEqual("abc",value);
        }

        [TestMethod]
        public void RightEdgeValid()
        {
            var XMLValueParser = new XMLValueParser();

            var value = XMLValueParser.ValueFinder("data", true, "xxxxxxxx<data>abc</data>");

            Assert.AreEqual("abc",value);
        }


        [TestMethod]
        public void OpeningTagOnRightEdge()
        {
            var XMLValueParser = new XMLValueParser();

            try
            {
                var value = XMLValueParser.ValueFinder("data",true, "xxxxxxxxx<data>");

            }
            catch (Exception ex)
            {
                Assert.AreEqual(typeof(NoClosingTagException), ex.GetType());
            }
        }

        [TestMethod]
        public void MandatoryField()
        {
            var XMLValueParser = new XMLValueParser();

            try
            {
                var value = XMLValueParser.ValueFinder("data", true, "xxxxxxxxxXX");

            }
            catch (Exception ex)
            {
                Assert.AreEqual(typeof(NoOpeningTagException), ex.GetType());
            }
        }

        [TestMethod]
        public void NonMandatoryField()
        {
            var XMLValueParser = new XMLValueParser();

            var value = XMLValueParser.ValueFinder("data", false, "xxxxxxxx");

            Assert.AreEqual(String.Empty, value);
        }
    }
}
