using System;
using Bycicles.StringExtensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class T_StringFormats
    {
        [TestMethod]
        public void T_FormToLengthRight()
        {
            Assert.AreEqual("12 ", "12".FormToLengthRight(3, ".."));
            Assert.AreEqual("rest..", "restricted".FormToLengthRight(6, ".."));
        }
    }
}
