using System;
using System.Collections.Generic;
using System.Linq;
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
            int maxLength = Nodes.Select(n => n.Cost).Max() > 9 ? 2 : 1;

            string result = "Result:" + Environment.NewLine;

            for (int i = 0; i < MazeMatrix.Length; i++)
            {
                for (int j = 0; j < MazeMatrix[i].Length; j++)
                {
                    if(maxLength == 2 && MazeMatrix[i][j].Cost <= 9)
                    {
                        result += "[0" + MazeMatrix[i][j] + "]";
                    }
                    else
                    {
                        result += "[" + MazeMatrix[i][j] + "]";
                    }
                    
                }

                result += Environment.NewLine;
            }

            return result;
        }
    }
}
