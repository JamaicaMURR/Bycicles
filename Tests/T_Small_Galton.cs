using System;
using Bycicles;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class T_Small_Galton
    {
        [TestMethod]
        public void T_InsertInLine()
        {
            Small_Galton tsg = new Small_Galton(5, 5);

            for(byte b1 = 0; b1 < 5; b1++)
                for(byte b2 = 0; b2 < b1; b2++)
                    tsg.InsertInLine(b1);

            Assert.AreEqual(0, tsg[0]);
            Assert.AreEqual(1, tsg[1]);
            Assert.AreEqual(2, tsg[2]);
            Assert.AreEqual(3, tsg[3]);
            Assert.AreEqual(4, tsg[4]);

            for(byte b = 0; b < 5; b++)
                tsg.InsertInLine(4);

            Assert.AreEqual(0, tsg[0]);
            Assert.AreEqual(0, tsg[1]);
            Assert.AreEqual(0, tsg[2]);
            Assert.AreEqual(0, tsg[3]);
            Assert.AreEqual(5, tsg[4]);
        }

        [TestMethod]
        public void T_GetRandom()
        {
            Small_Galton tsg = new Small_Galton(5, 5);

            tsg.InsertInLine(0);
            tsg.InsertInLine(4);

            for(byte b = 0; b < 100; b++)
            {
                byte result = tsg.GetRandom();

                Assert.IsTrue(result == 0 ^ result == 4);
            }
        }
    }
}
