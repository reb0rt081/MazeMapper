using System;
using System.Collections.Generic;
using System.Text;

namespace MazeMapper.Shared
{
    /// <summary>
    /// Represents an arrow that connects two nodes.
    /// </summary>
    public class Arrow : IArrow
    {
        public string Id { get; set; }
        public INode Vertex1 { get; set; }
        public INode Vertex2 { get; set; }
        public INode GetOppositeNode(INode vertex)
        {
            if(vertex == Vertex1)
            {
                return Vertex2;
            }

            if(vertex == Vertex2)
            {
                return Vertex1;
            }

            return null;
        }
    }
}
