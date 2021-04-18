using System;
using System.Collections.Generic;
using System.Linq;
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

            if (mazeColumns.Select(m => m.Length).Distinct().Count() > 1)
            {
                throw new Exception("Maze map input string must have the same number of chars per line!");
            }
        }
    }
}
