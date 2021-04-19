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

        void BookRoverToLocation(INode node);

        bool TryGoToNextNode(INode nextNode);
    }
}
