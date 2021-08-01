using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bycicles;

namespace Tests
{
    [TestClass]
    public class T_Protocol_Executioner
    {
        [TestMethod]
        public void Invoke_Test()
        {
            int testVal = 0;

            ProtocolExecutioner pe = new ProtocolExecutioner();

            Action a = () => testVal++;
            Action b = () => testVal--;

            List<Action> p1 = new List<Action> { a, b, a, a };
            List<Action> p2 = new List<Action> { a, b, a };

            pe.Add(p1);
            pe.Add(p2);

            pe.Invoke(4);

            Assert.AreEqual(0, testVal);
            Assert.IsFalse(pe.IsOnStart);

            pe.Invoke(4);

            Assert.AreEqual(3, testVal);
            Assert.IsTrue(pe.IsOnStart);

            pe.Invoke(6);

            Assert.AreEqual(5, testVal);
            Assert.IsFalse(pe.IsOnStart);

            pe.Invoke(90);

            Assert.AreEqual(6, testVal);
            Assert.IsTrue(pe.IsOnStart);
        }
    }
}
