using System;
using System.Collections.Generic;
using System.Text;

using MazeMapper.Shared;

namespace MazeMapper.Domain
{
    public interface IRover
    {
        /// <summary>
        /// Gets or sets the Id for this <see cref="IRover"/>.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets the previous node where this <see cref="IRover"/> was.
        /// </summary>
        INode PreviousNode { get; }

        /// <summary>
        /// Gets the current node where this <see cref="IRover"/> is at the moment.
        /// </summary>
        INode CurrentNode { get; }

        /// <summary>
        /// Gets the next node this <see cref="IRover"/> will visit.
        /// </summary>
        INode ReservedNode { get; }

        /// <summary>
        /// Places this <see cref="IRover"/> in the input node.
        /// </summary>
        /// <param name="node"></param>
        void BookRoverToLocation(INode node);

        /// <summary>
        /// Reserves the next node this <see cref="IRover"/> will visit in the next move.
        /// </summary>
        /// <param name="node"></param>
        void ReserveNextNode(INode node);

        /// <summary>
        /// Tries to move this <see cref="IRover"/> to the reserved node.
        /// </summary>
        /// <returns></returns>
        bool TryGoToNextNode();
    }
}
