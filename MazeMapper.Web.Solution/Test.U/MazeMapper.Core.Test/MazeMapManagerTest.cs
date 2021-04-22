using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MazeMapper.Core;
using MazeMapper.Shared;

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

            Console.WriteLine(mazeMapManager.MazeMap.ToString());

            Assert.IsTrue(mazeMapManager.MazeMap.Nodes.Select(n => n.Cost).Any(c => c == 4));
            Assert.IsTrue(mazeMapManager.MazeMap.Nodes.Select(n => n.Cost).Max() < 5);
        }

        /// <summary>
        /// Input maze:
        /// 000000
        /// *11110
        /// 000000
        /// </summary>
        [TestMethod]
        public void GetAdjacentNodesTest()
        {
            string mazeMapString = $"000000{Environment.NewLine}*11110{Environment.NewLine}000000";

            mazeMapManager.BuildMazeMapFromString(mazeMapString);

            INode node = mazeMapManager.MazeMap.MazeMatrix[1][2];

            List<INode> adjacentNodes = mazeMapManager.GetAdjacentNodes(node);

            Assert.AreEqual(2, adjacentNodes.Count);
            Assert.IsTrue(adjacentNodes.Contains(mazeMapManager.MazeMap.MazeMatrix[1][1]));
            Assert.IsTrue(adjacentNodes.Contains(mazeMapManager.MazeMap.MazeMatrix[1][3]));

            node = mazeMapManager.MazeMap.MazeMatrix[1][4];

            adjacentNodes = mazeMapManager.GetAdjacentNodes(node);

            Assert.AreEqual(1, adjacentNodes.Count);
            Assert.IsTrue(adjacentNodes.Contains(mazeMapManager.MazeMap.MazeMatrix[1][3]));
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

            Assert.IsTrue(mazeMapManager.MazeMap.Nodes.Select(n => n.Cost).Any(c => c == 15));
            Assert.IsTrue(mazeMapManager.MazeMap.Nodes.Select(n => n.Cost).Max() < 16);
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

            Assert.IsTrue(mazeMapManager.MazeMap.Nodes.Select(n => n.Cost).Any(c => c == 13));
            Assert.IsTrue(mazeMapManager.MazeMap.Nodes.Select(n => n.Cost).Max() < 14);
        }

        /// <summary>
        /// Input maze:
        /// 1111111111
        /// 1010100100
        /// 0010001101
        /// 1111111001
        /// 1001001101
        /// 1111000111
        /// 0101011101
        /// *101110111
        /// </summary>
        [TestMethod]
        [Timeout(30000)]
        public async Task BuildAndSolveMoreComplexMaze()
        {
            string mazeMapString = $"1111111111{Environment.NewLine}1010100100{Environment.NewLine}0010001101{Environment.NewLine}1111111001{Environment.NewLine}1001001101{Environment.NewLine}1111000111{Environment.NewLine}0101011101{Environment.NewLine}*101110111";

            mazeMapManager.BuildMazeMapFromString(mazeMapString);

            await mazeMapManager.SolveMazeAsync();

            Console.WriteLine(mazeMapManager.MazeMap.ToString());

            Assert.IsTrue(mazeMapManager.MazeMap.Nodes.Select(n => n.Cost).Any(c => c == 18));
            Assert.IsTrue(mazeMapManager.MazeMap.Nodes.Select(n => n.Cost).Max() < 19);
        }

        /// <summary>
        /// Input maze:
        /// 1111111111
        /// 1010100100
        /// 0010001101
        /// 1111111001
        /// 1001111101
        /// 1111110111
        /// 0101011101
        /// *101110111
        /// </summary>
        [TestMethod]
        [Timeout(30000)]
        public async Task BuildAndSolveHellMaze()
        {
            string mazeMapString = $"1111111111{Environment.NewLine}1010100100{Environment.NewLine}0010001101{Environment.NewLine}1111111001{Environment.NewLine}1001111101{Environment.NewLine}1111110111{Environment.NewLine}0101011101{Environment.NewLine}*101110111";

            mazeMapManager.BuildMazeMapFromString(mazeMapString);

            await mazeMapManager.SolveMazeAsync();

            Console.WriteLine(mazeMapManager.MazeMap.ToString());

            Assert.IsTrue(mazeMapManager.MazeMap.Nodes.Select(n => n.Cost).Any(c => c == 16));
            Assert.IsTrue(mazeMapManager.MazeMap.Nodes.Select(n => n.Cost).Max() < 17);
        }

        /// <summary>
        /// Input maze:
        /// 1111111111
        /// 1010100100
        /// 0010001101
        /// 1111111001
        /// 1001111101
        /// 1111110111
        /// 0111111101
        /// 0001*11101
        /// </summary>
        [TestMethod]
        [Timeout(30000)]
        public async Task BuildAndSolveHellMaze2()
        {
            string mazeMapString = $"1111111111{Environment.NewLine}1010100100{Environment.NewLine}0010001101{Environment.NewLine}1111111001{Environment.NewLine}1001111101{Environment.NewLine}1111110111{Environment.NewLine}0111111101{Environment.NewLine}0001*11101";

            mazeMapManager.BuildMazeMapFromString(mazeMapString);

            await mazeMapManager.SolveMazeAsync();

            Console.WriteLine(mazeMapManager.MazeMap.ToString());

            Assert.IsTrue(mazeMapManager.MazeMap.Nodes.Select(n => n.Cost).Any(c => c == 12));
            Assert.IsTrue(mazeMapManager.MazeMap.Nodes.Select(n => n.Cost).Max() < 13);
        }

        /// <summary>
        /// Input maze:
        /// 1111111111111111111
        /// 1111111111111111111
        /// 1111111111111111111
        /// 1111111111111111111
        /// 1111111111111111111
        /// 1111111111111111111
        /// 1111111111111111111
        /// 1111111111111111111
        /// 1111111111111111111
        /// 0000*00000000000000
        /// </summary>
        [TestMethod]
        [Timeout(30000)]
        public async Task BuildAndSolveHellMaze3()
        {
            string mazeMapString = $"1111111111111111111{Environment.NewLine}1111111111111111111{Environment.NewLine}1111111111111111111{Environment.NewLine}1111111111111111111{Environment.NewLine}1111111111111111111{Environment.NewLine}1111111111111111111{Environment.NewLine}1111111111111111111{Environment.NewLine}1111111111111111111{Environment.NewLine}1111111111111111111{Environment.NewLine}0000*00000000000000";

            mazeMapManager.BuildMazeMapFromString(mazeMapString);

            await mazeMapManager.SolveMazeAsync();

            Console.WriteLine(mazeMapManager.MazeMap.ToString());

            Assert.IsTrue(mazeMapManager.MazeMap.Nodes.Select(n => n.Cost).Any(c => c == 23));
            Assert.IsTrue(mazeMapManager.MazeMap.Nodes.Select(n => n.Cost).Max() < 24);
        }
    }
}
