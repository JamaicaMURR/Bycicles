using System;
using Bycicles;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class T_Tail
    {
        [TestMethod]
        public void T_Indexes()
        {
            Tail<int> t = new Tail<int>(3);

            Assert.AreEqual(3, t.Length);

            t.Add(1);
            t.Add(2);
            t.Add(3);

            Assert.AreEqual(1, t[2]);
            Assert.AreEqual(1, t.Oldest);
            Assert.AreEqual(3, t.Newest);

            t.Add(4);

            Assert.AreEqual(2, t.Oldest);
            Assert.AreEqual(4, t.Newest);
        }
    }
}
