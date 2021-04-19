using MazeMapper.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace MazeMapper.Core
{
    public class Rover : IRover
    {
        public string Name { get; set; }
        public INode PreviousNode { get; private set; }
        public INode CurrentNode { get; private set; }
        public INode ReservedNode { get; private set; }

        public void BookRoverToLocation(INode node)
        {
            if(node != null)
            {
                CurrentNode = node;
            }
        }

        public void ReserveNextNode(INode node)
        {
            if (node != null)
            {
                ReservedNode = node;
            }
        }

        public bool TryGoToNextNode()
        {
            Console.WriteLine($"Rover {Name} trying to go from {CurrentNode.Id} to {ReservedNode.Id}");

            if(CurrentNode != null && ReservedNode != null)
            {
                if(CurrentNode.Cost + 1 < ReservedNode.Cost || ReservedNode.Cost == 0)
                {
                    Console.WriteLine($"Rover {Name} finally going from {CurrentNode.Id} to {ReservedNode.Id}");

                    PreviousNode = CurrentNode;
                    ReservedNode.Cost = CurrentNode.Cost + 1;
                    CurrentNode = ReservedNode;
                    
                    return true;
                }
            }

            return false;
        }
    }
}
