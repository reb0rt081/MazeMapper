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
    }
}
