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
            D_Galton dg = new D_Galton(2, 100, 0, 1);

            dg.Add(0.1, 3);
            dg.Add(0.2, 3);

            Assert.AreEqual(0.15, dg.GetVal(), 0.001);
        }
    }
}
