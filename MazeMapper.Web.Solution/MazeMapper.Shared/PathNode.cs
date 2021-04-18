using System;
using System.Collections.Generic;
using System.Text;

namespace MazeMapper.Shared
{
    /// <summary>
    /// Represents a node that can be walked.
    /// </summary>
    public class PathNode : INode
    {
        public string Id { get; set; }
        public int Cost { get; set; }
    }
}
