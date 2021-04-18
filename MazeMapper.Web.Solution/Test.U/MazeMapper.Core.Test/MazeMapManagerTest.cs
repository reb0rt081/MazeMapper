using System;
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
            mazeMapManager.BuildMazeMapFromString($"000000{Environment.NewLine}*11110{Environment.NewLine}000000");
        }
    }
}
