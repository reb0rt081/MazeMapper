using System;
using System.Collections.Generic;
using System.Text;

namespace MazeMapper.Shared
{
    public class MazeMap : IMazeMap
    {
        public MazeMap()
        {
            Arrows = new List<IArrow>();
            Nodes = new List<INode>();
        }

        public List<IArrow> Arrows { get; set; }
        public List<INode> Nodes { get; set; }
        public INode[][] MazeMatrix { get; set; }

        public override string ToString()
        {
            string result = "Result:" + Environment.NewLine;

            for (int i = 0; i < MazeMatrix.Length; i++)
            {
                for (int j = 0; j < MazeMatrix[i].Length; j++)
                {
                    result += MazeMatrix[i][j];
                }

                result += Environment.NewLine;
            }

            return result;
        }
    }
}
