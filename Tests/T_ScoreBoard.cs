using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bycicles;

namespace Tests
{
    [TestClass]
    public class T_ScoreBoard
    {
        [TestMethod]
        public void TryToInsert_test()
        {
            ScoreBoard<string, int> sb = new ScoreBoard<string, int>(3, ScoreBoardMode.HigherBest);

            sb.TryToInsert("worst", 34);
            sb.TryToInsert("best", 100);
            sb.TryToInsert("mid", 56);

            Assert.AreEqual(34, sb[2].Item2);
            Assert.AreEqual(56, sb[1].Item2);
            Assert.AreEqual(100, sb[0].Item2);

            sb.TryToInsert("new best", 120);

            Assert.AreEqual(56, sb[2].Item2);
            Assert.AreEqual(100, sb[1].Item2);
            Assert.AreEqual(120, sb[0].Item2);

            sb.TryToInsert("new worst", 3);

            Assert.AreEqual(56, sb[2].Item2);
            Assert.AreEqual(100, sb[1].Item2);
            Assert.AreEqual(120, sb[0].Item2);

            sb.TryToInsert("new mid", 102);

            Assert.AreEqual(100, sb[2].Item2);
            Assert.AreEqual(102, sb[1].Item2);
            Assert.AreEqual(120, sb[0].Item2);
        }

        [TestMethod]
        public void InsertWorst_test()
        {
            ScoreBoard<int, int> sb = new ScoreBoard<int, int>(10, ScoreBoardMode.LowerBest);

            sb.TryToInsert(1, 1);
            sb.TryToInsert(2, 2);
            sb.TryToInsert(3, 3);

            Assert.AreEqual(3, sb.Count);

            Assert.AreEqual(1, sb[0].Item1);
            Assert.AreEqual(2, sb[1].Item1);
            Assert.AreEqual(3, sb[2].Item1);

            sb = new ScoreBoard<int, int>(10, ScoreBoardMode.HigherBest);

            sb.TryToInsert(3, 3);
            sb.TryToInsert(2, 2);
            sb.TryToInsert(1, 1);

            Assert.AreEqual(3, sb.Count);

            Assert.AreEqual(3, sb[0].Item1);
            Assert.AreEqual(2, sb[1].Item1);
            Assert.AreEqual(1, sb[2].Item1);
        }
    }
}
