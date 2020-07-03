using Microsoft.VisualStudio.TestTools.UnitTesting;
using skat_kodetest;
using System.Collections.Generic;

namespace skat_kodetest_tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Scoring_WithPopulatedList_ScoresCorrectly()
        {
            //Arrange
            var data = new List<List<int>>();
            data.Add(new List<int>() { 7, 3 });
            data.Add(new List<int>() { 3, 3 });
            data.Add(new List<int>() { 2, 3 });
            data.Add(new List<int>() { 10, 0 });
            data.Add(new List<int>() { 7, 2 });

            //Act
            var result = Program.ScoreGame(data);

            //Assert
            CollectionAssert.AreEqual(new List<int>() { 13, 19, 24, 43, 52 }, result);
        }

        [TestMethod]
        public void Scoring_WithNullList_ReturnsNull()
        {
            //Arrange
            List<List<int>> data = null;

            //Act
            var result = Program.ScoreGame(data);

            //Assert
            Assert.AreEqual(null, result);
        }

        [TestMethod]
        public void Scoring_WithEmptyFrames_ReturnsNull()
        {
            //Arrange
            List<List<int>> data = new List<List<int>>();
            data.Add(new List<int>());
            data.Add(new List<int>());
            data.Add(new List<int>());
            data.Add(new List<int>());

            //Act
            var result = Program.ScoreGame(data);

            //Assert
            Assert.AreEqual(null, result);
        }

        [TestMethod]
        public void Scoring_WithLargerFrame_ScoresCorrectly()
        {
            //Arrange            
            var data = new List<List<int>>();
            data.Add(new List<int>() { 7, 3, 22 });
            data.Add(new List<int>() { 3, 3, 23 });
            data.Add(new List<int>() { 2, 3, 24 });
            data.Add(new List<int>() { 10, 0, 25 });
            data.Add(new List<int>() { 7, 2, 26 });

            //Act
            var result = Program.ScoreGame(data);

            //Assert
            CollectionAssert.AreEqual(new List<int>() { 13, 19, 24, 43, 52 }, result);
        }

        [TestMethod]
        public void Scoring_LastThrowIsStrike_ScoresCorrectly()
        {
            //Arrange            
            var data = new List<List<int>>();
            data.Add(new List<int>() { 10, 0 });
            data.Add(new List<int>() { 10, 0 });
            data.Add(new List<int>() { 10, 0 });
            data.Add(new List<int>() { 10, 0 });
            data.Add(new List<int>() { 10, 0 });
            data.Add(new List<int>() { 10, 0 });
            data.Add(new List<int>() { 10, 0 });
            data.Add(new List<int>() { 10, 0 });
            data.Add(new List<int>() { 10, 0 });
            data.Add(new List<int>() { 10, 10 });

            //Act
            var result = Program.ScoreGame(data);

            //Assert
            CollectionAssert.AreEqual(new List<int>() { 30, 60, 90, 120, 150, 180, 210, 240, 270, 300 }, result);
        }

        [TestMethod]
        public void Scoring_LastThrowIsSpare_ScoresCorrectly()
        {
            //Arrange            
            var data = new List<List<int>>();
            data.Add(new List<int>() { 10, 0 });
            data.Add(new List<int>() { 10, 0 });
            data.Add(new List<int>() { 10, 0 });
            data.Add(new List<int>() { 10, 0 });
            data.Add(new List<int>() { 10, 0 });
            data.Add(new List<int>() { 10, 0 });
            data.Add(new List<int>() { 10, 0 });
            data.Add(new List<int>() { 10, 0 });
            data.Add(new List<int>() { 5, 0 });
            data.Add(new List<int>() { 5, 5 });

            //Act
            var result = Program.ScoreGame(data);

            //Assert
            CollectionAssert.AreEqual(new List<int>() { 30, 60, 90, 120, 150, 180, 205, 220, 225, 235 }, result);
        }
    }
}
