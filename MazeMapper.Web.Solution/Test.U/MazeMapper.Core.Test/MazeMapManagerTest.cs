using System;
using System.Threading.Tasks;

using MazeMapper.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestU.MazeMapper.Core.Test
{
    [TestClass]
    public class MazeMapManagerTest
    {
        private MazeMapManager mazeMapManager;

        [TestInitialize]
        public void Initialize()
        {
            mazeMapManager = new MazeMapManager();
        }

        /// <summary>
        /// Input maze:
        /// 000000
        /// *1111
        /// 000000
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void BuildWrongMaze()
        {
            mazeMapManager.BuildMazeMapFromString($"000000{Environment.NewLine}*1111{Environment.NewLine}000000");
        }

        /// <summary>
        /// Input maze:
        /// 000000
        /// *11110
        /// 000000
        /// </summary>
        [TestMethod]
        public void BuildSimpleMaze()
        {
            string mazeMapString = $"000000{Environment.NewLine}*11110{Environment.NewLine}000000";


            mazeMapManager.BuildMazeMapFromString(mazeMapString);

            Assert.AreEqual(5, mazeMapManager.MazeMap.Nodes.Count);
            Assert.AreEqual(8, mazeMapManager.MazeMap.Arrows.Count);
            Assert.AreEqual(3, mazeMapManager.MazeMap.MazeMatrix.Length);
            Assert.AreEqual(6, mazeMapManager.MazeMap.MazeMatrix[0].Length);

            //Assert.IsTrue(mazeMapManager.MazeMap.ToString().Contains(mazeMapString));
        }

        /// <summary>
        /// Input maze:
        /// 000000
        /// *11110
        /// 000000
        /// </summary>
        [TestMethod]
        public async Task BuildAndSolveSimpleMaze()
        {
            string mazeMapString = $"000000{Environment.NewLine}*11110{Environment.NewLine}000000";


            mazeMapManager.BuildMazeMapFromString(mazeMapString);

            await mazeMapManager.SolveMazeAsync();
        }

        /// <summary>
        /// Input maze:
        /// 11111111
        /// 10101001
        /// 00100011
        /// 11111110
        /// 10010011
        /// 11110001
        /// 01010111
        /// *1011101
        /// </summary>
        [TestMethod]
        public void BuildComplexMaze()
        {
            string mazeMapString = $"11111111{Environment.NewLine}10101001{Environment.NewLine}00100011{Environment.NewLine}11111110{Environment.NewLine}10010011{Environment.NewLine}11110001{Environment.NewLine}01010111{Environment.NewLine}*1011101";
            
            mazeMapManager.BuildMazeMapFromString(mazeMapString);

            Assert.AreEqual(42, mazeMapManager.MazeMap.Nodes.Count);
            Assert.AreEqual(88, mazeMapManager.MazeMap.Arrows.Count);
            Assert.AreEqual(8, mazeMapManager.MazeMap.MazeMatrix.Length);
            Assert.AreEqual(8, mazeMapManager.MazeMap.MazeMatrix[0].Length);

            Console.WriteLine(mazeMapManager.MazeMap.ToString());
        }

        /// <summary>
        /// Input maze:
        /// 11111111
        /// 10101001
        /// 00100011
        /// 11111110
        /// 10010011
        /// 11110001
        /// 01010111
        /// *1011101
        /// </summary>
        [TestMethod]
        [Timeout(30000)]
        public async Task BuildAndSolveComplexMaze()
        {
            string mazeMapString = $"11111111{Environment.NewLine}10101001{Environment.NewLine}00100011{Environment.NewLine}11111110{Environment.NewLine}10010011{Environment.NewLine}11110001{Environment.NewLine}01010111{Environment.NewLine}*1011101";

            mazeMapManager.BuildMazeMapFromString(mazeMapString);

            await mazeMapManager.SolveMazeAsync();

            Console.WriteLine(mazeMapManager.MazeMap.ToString());
        }

        /// <summary>
        /// Input maze:
        /// 11110001
        /// 01010111
        /// *1011101
        /// </summary>
        [TestMethod]
        [Timeout(30000)]
        public async Task BuildAndSolveIntermediateMaze()
        {
            string mazeMapString = $"11110001{Environment.NewLine}01010111{Environment.NewLine}*1011101";

            mazeMapManager.BuildMazeMapFromString(mazeMapString);

            await mazeMapManager.SolveMazeAsync();

            Console.WriteLine(mazeMapManager.MazeMap.ToString());
        }
    }
}
