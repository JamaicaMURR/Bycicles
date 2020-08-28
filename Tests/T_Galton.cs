﻿using System;
using Bycicles;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class T_Galton
    {
        static Random rnd = new Random();

        [TestMethod]
        public void T_Change()
        {
            Galton g = new Galton(5, 100);

            for(int i = 0; i < 5; i++)
                g.Add(i, i);

            g.Change(10);

            Assert.AreEqual(60, g.Total);
        }

        [TestMethod]
        public void T_Equalize()
        {
            Galton g = new Galton(5, 100);

            for(int i = 0; i < 5; i++)
                g.Add(i, i);

            g.Equalize(2, 5);

            Assert.AreEqual(10, g.Total);
        }

        [TestMethod]
        public void T_Slice()
        {
            Galton g = new Galton(5, 100);

            for(int i = 0; i < 5; i++)
                g.Add(i, i);

            g.Slice(2);

            Assert.AreEqual(7, g.Total);
        }

        [TestMethod]
        public void T_Rise()
        {
            Galton g = new Galton(5, 100);

            for(int i = 0; i < 5; i++)
                g.Add(i, i);

            g.Rise(3, 1);

            Assert.AreEqual(13, g.Total);
        }

        [TestMethod]
        public void T_Fall()
        {
            Galton g = new Galton(5, 100);

            for(int i = 0; i < 5; i++)
                g.Add(i, i);

            g.Fall(2, 5);

            Assert.AreEqual(7, g.Total);
        }

        [TestMethod]
        public void T_GetRandom()
        {
            Galton g = new Galton(5, 100);

            for(int i = 0; i < 5; i++)
                g.Add(i, i);

            int line = g.GetRandom(out double chance);

            Assert.IsTrue((line == 1 && chance == 0.1) ^ (line == 2 && chance == 0.2) ^ (line == 3 && chance == 0.3) ^ (line == 4 && chance == 0.4));
        }

        [TestMethod]
        public void T_Fill()
        {
            Galton g = new Galton(5, 100);

            g.Fill();

            for(int i = 0; i < g.Lines; i++)
                Assert.AreEqual(g.Overload, g[i]);
        }
    }
}
