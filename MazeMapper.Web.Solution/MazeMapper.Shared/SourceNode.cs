using System;
using System.Collections.Generic;
using System.Text;

namespace MazeMapper.Shared
{
    public class SourceNode : PathNode
    {
        public string Id { get; set; }
        public int Cost { get; set; }

        public SourceNode()
        {
            Cost = 0;
        }

        public override string ToString()
        {
            return "*";
        }
    }
}
