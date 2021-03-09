using System;
using Bycicles.StringFormats;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class T_StringFormats
    {
        [TestMethod]
        public void T_FormToLengthRight()
        {
            Assert.AreEqual("12 ", "12".FormToLengthRight(3), "13");
            Assert.AreEqual("rest..", "restricted".FormToLengthRight(6), "14");
        }

        [TestMethod]
        public void T_FormToLengthLeft()
        {
            Assert.AreEqual(" 12", "12".FormToLengthLeft(3), "20");
            Assert.AreEqual("..cted", "restricted".FormToLengthLeft(6), "21");
        }

        [TestMethod]
        public void T_CutToLength()
        {
            Assert.AreEqual("some..", "something".CutToLength(6), "27");
            Assert.AreEqual("some", "some".CutToLength(6), "28");
        }
    }
}
