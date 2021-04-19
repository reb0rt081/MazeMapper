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

        public void BookRoverToLocation(INode node)
        {
            if(node != null)
            {
                CurrentNode = node;
            }
        }

        public bool TryGoToNextNode(INode nextNode)
        {
            Console.WriteLine($"Rover {Name} trying to go from {CurrentNode.Id} to {nextNode.Id}");

            if(CurrentNode != null && nextNode != null)
            {
                if(CurrentNode.Cost + 1 < nextNode.Cost || nextNode.Cost == 0)
                {
                    Console.WriteLine($"Rover {Name} finally going from {CurrentNode.Id} to {nextNode.Id}");

                    PreviousNode = CurrentNode;
                    nextNode.Cost = CurrentNode.Cost + 1;
                    CurrentNode = nextNode;
                    
                    return true;
                }
            }

            return false;
        }
    }
}
