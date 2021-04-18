using System;
using System.Collections.Generic;
using System.Text;
using MazeMapper.Domain;
using MazeMapper.Shared;

namespace MazeMapper.Core
{
    public class MazeMapManager : IMazeMapManager
    {
        public IMazeMap MazeMap { get; private set; }

        public MazeMapManager()
        {
            MazeMap = new MazeMap();
        }

        public void BuildMazeMapFromString(string mazeMapText)
        {
            string[] mazeColumns = mazeMapText.Split(Environment.NewLine);
        }
    }
}
