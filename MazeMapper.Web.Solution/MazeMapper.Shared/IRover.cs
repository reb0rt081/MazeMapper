using System;
using System.Collections.Generic;
using System.Text;

namespace MazeMapper.Shared
{
    public interface IRover
    {
        string Name { get; set; }

        INode PreviousNode { get; }

        INode CurrentNode { get; }

        INode ReservedNode { get; }

        void BookRoverToLocation(INode node);

        void ReserveNextNode(INode node);

        bool TryGoToNextNode();
    }
}
