using System;
using System.Collections.Generic;
using System.Text;

namespace MazeMapper.Shared
{
    public interface IMazeMap
    {
        /// <summary>
        /// List of arrows in this maze that connect nodes.
        /// </summary>
        List<IArrow> Arrows { get; set; }

        /// <summary>
        /// List of nodes in this maze that are connected via arrows.
        /// </summary>
        List<INode> Nodes { get; set; }

        /// <summary>
        /// Represents the raw maze map 
        /// </summary>
        INode[][] MazeMatrix { get; set; }
    }
}
