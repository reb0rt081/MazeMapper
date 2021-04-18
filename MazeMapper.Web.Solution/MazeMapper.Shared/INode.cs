using System;
using System.Collections.Generic;
using System.Text;

namespace MazeMapper.Shared
{
    public interface INode
    {
        /// <summary>
        /// The Id for the <see cref="INode"/>
        /// </summary>
        string Id { get; set; }

        /// <summary>
        /// The cost it takes to arrive to this <see cref="INode"/>
        /// </summary>
        int Cost { get; set; }
    }
}
