using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace MazeMapper.Shared
{
    public interface IArrow
    {
        /// <summary>
        /// The Id for the <see cref="IArrow"/>
        /// </summary>
        string Id { get; set; }


        /// <summary>
        /// First vertex of this <see cref="IArrow"/>
        /// </summary>
        INode Vertex1 { get; set; }

        /// <summary>
        /// Second vertex of this <see cref="IArrow"/>
        /// </summary>
        INode Vertex2 { get; set; }

        /// <summary>
        /// Returns the opposite vertex to this arrow.
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns></returns>
        INode GetOppositeNode(INode vertex);
    }
}
