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
            Assert.AreEqual(3, t[1]);
        }

        [TestMethod]
        public void T_Count()
        {
            Tail<int> t = new Tail<int>(10);

            t.Add(1);
            t.Add(2);
            t.Add(3);

            Assert.AreEqual(1, t[2]);
            Assert.AreEqual(1, t.Oldest);
            Assert.AreEqual(3, t.Newest);
            Assert.AreEqual(3, t.Count);
        }

        [TestMethod]
        public void T_IndexEx()
        {
            Tail<int> t = new Tail<int>(10);

            t.Add(1);
            t.Add(2);
            t.Add(3);

            bool catched = false;

            try
            {
                int i = t[4];
            }
            catch(Exception e)
            {
                Assert.AreEqual("Member not exists", e.Message);
                catched = true;
            }

            Assert.IsTrue(catched);
        }

        [TestMethod]
        public void T_Contains()
        {
            Tail<int> t = new Tail<int>(10);

            t.Add(1);
            t.Add(2);
            t.Add(3);

            Assert.IsTrue(t.Contains(1));
            Assert.IsTrue(t.Contains(2));

            Assert.IsFalse(t.Contains(0));
        }

        [TestMethod]
        public void T_Links()
        {
            Tail<Tail<int>> t = new Tail<Tail<int>>(10);

            Tail<int> ti = new Tail<int>(5);

            t.Add(ti);

            ti = null;

            Assert.IsNotNull(t.Newest);
        }
    }
}
