using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bycicles.Ranges;

namespace Tests
{
    [TestClass]
    public class T_Ranges
    {
        [TestMethod]
        public void T_Rescale()
        {
            Assert.AreEqual(0, 0.5.Rescale(0, 1, -5, 5), 0.001);
            Assert.AreEqual(75, 0.5.Rescale(0, 1, 50, 100), 0.001);
            Assert.AreEqual(25, 15d.Rescale(10, 30, 0, 100), 0.001);
        }

        [TestMethod]
        public void T_NotBelow()
        {
            Assert.AreEqual(0, (-0.5).NotBelow(0));
            Assert.AreEqual(0.5, 0.5f.NotBelow(0));

            Assert.AreEqual(-1, (-2).NotBelow(-1.5));
            Assert.AreEqual(2, 1.NotBelow(1.5));
        }

        [TestMethod]
        public void T_ExNotBelow()
        {
            Exception tex = null;

            try
            {
                0.5.ExNotBelow(1);
            }
            catch(Exception e)
            {
                tex = e;
            }

            Assert.AreEqual(tex.Message, "Value is below 1! ");

            tex = null;
            int res = 0;

            try
            {
                res = 1.ExNotBelow(1);
            }
            catch(Exception e)
            {
                tex = e;
            }

            Assert.AreEqual(1, res);
            Assert.IsNull(tex);
        }

        [TestMethod]
        public void T_NotAbove()
        {
            Assert.AreEqual(-0.5, (-0.5).NotAbove(0));
            Assert.AreEqual(0, 0.5f.NotAbove(0));

            Assert.AreEqual(-2, (-2).NotAbove(-1.5));
            Assert.AreEqual(1, 2.NotAbove(1.5));
        }

        [TestMethod]
        public void T_ExNotAbove()
        {
            Exception tex = null;

            try
            {
                1.5.ExNotAbove(1);
            }
            catch(Exception e)
            {
                tex = e;
            }

            Assert.AreEqual(tex.Message, "Value is above 1! ");

            tex = null;
            int res = 0;

            try
            {
                res = 1.ExNotAbove(1);
            }
            catch(Exception e)
            {
                tex = e;
            }

            Assert.AreEqual(1, res);
            Assert.IsNull(tex);
        }

        [TestMethod]
        public void T_ExAbove()
        {
            Exception tex = null;

            try
            {
                1f.ExAbove(1);
            }
            catch(Exception e)
            {
                tex = e;
            }

            Assert.AreEqual(tex.Message, "Value is not above 1! ");

            tex = null;
            double res = 0;

            try
            {
                res = 1.01.ExAbove(1);
            }
            catch(Exception e)
            {
                tex = e;
            }

            Assert.AreEqual(1.01, res);
            Assert.IsNull(tex);
        }

        [TestMethod]
        public void T_ExBelow()
        {
            Exception tex = null;

            try
            {
                1.5.ExBelow(1);
            }
            catch(Exception e)
            {
                tex = e;
            }

            Assert.AreEqual(tex.Message, "Value is not below 1! ");

            tex = null;
            double res = 0;

            try
            {
                res = 0.9.ExBelow(1);
            }
            catch(Exception e)
            {
                tex = e;
            }

            Assert.AreEqual(0.9, res);
            Assert.IsNull(tex);
        }

        [TestMethod]
        public void T_Gap()
        {
            Assert.AreEqual(2, 2.Gap(4));
            Assert.AreEqual(2, 2.Gap(0));
        }

        [TestMethod]
        public void T_EnSec()
        {
            Assert.AreEqual(1, 0.5.EnSec(0, 1, 3));
            Assert.AreEqual(1, 1d.EnSec(0, 1, 2));
            Assert.AreEqual(2, 0.5.EnSec(0, 1, 4));
        }

        [TestMethod]
        public void T_DeSec()
        {
            Assert.AreEqual(0.5, 1.DeSec(0, 1, 3), 0.001);
            Assert.AreEqual(0.25, 0.DeSec(0, 1, 2), 0.001);
        }

        [TestMethod]
        public void T_EnClose()
        {
            Assert.AreEqual(0, 1.5.EnClose(0, 2));
            Assert.AreEqual(1, 2.EnClose(0, 1));
            Assert.AreEqual(-1.5, -2.5.EnClose(0, 1));
        }
    }
}
