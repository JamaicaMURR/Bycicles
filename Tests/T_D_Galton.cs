using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bycicles;

namespace Tests
{
    [TestClass]
    public class T_D_Galton
    {
        [TestMethod]
        public void T_GetVal()
        {
            Double_Galton dg = new Double_Galton(2, 100, 0, 1);

            dg.InsertValue(0.1);
            dg.InsertValue(0.2);

            Assert.AreEqual(0.15, dg.GetRandVal(), 0.001);
        }
    }
}
