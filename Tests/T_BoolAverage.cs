using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bycicles;

namespace Tests
{
    [TestClass]
    public class T_BoolAverage
    {
        [TestMethod]
        public void T_Add()
        {
            Bool_Average ba = new Bool_Average();

            ba.Add(true);
            ba.Add(true);
            ba.Add(false);

            Assert.IsTrue(ba.Val);
        }

        [TestMethod]
        public void T_Add2()
        {
            Bool_Average ba = new Bool_Average();

            ba.Add(true, 254);
            ba.Add(false, 255);

            Assert.IsFalse(ba.Val);
        }
    }
}
