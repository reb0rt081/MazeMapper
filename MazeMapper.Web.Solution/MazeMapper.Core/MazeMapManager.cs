﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MazeMapper.Domain;
using MazeMapper.Shared;

namespace MazeMapper.Core
{
    public class MazeMapManager : IMazeMapManager
    {
        private static readonly object lockObject = new object();

        public IMazeMap MazeMap { get; private set; }

        public MazeMapManager()
        {
            MazeMap = new MazeMap();
        }

        public void BuildMazeMapFromString(string mazeMapText)
        {
            string[] mazeRows = mazeMapText.Split(Environment.NewLine);

            mazeRows = mazeRows.Where(m => !string.IsNullOrEmpty(m)).ToArray();

            if (mazeRows.Select(m => m.Length).Distinct().Count() > 1)
            {
                throw new Exception("Maze map input string must have the same number of chars per line!");
            }

            MazeMap = new MazeMap();

            // Setting maze matrix number of rows
            MazeMap.MazeMatrix = new INode[mazeRows.Count()][];


            for (int i = 0; i < mazeRows.Count(); i++)
            {
                string mazeCell = mazeRows[i];

                // Setting maze matrix number of columns
                MazeMap.MazeMatrix[i] = new INode[mazeCell.Length];

                for (int j = 0; j < mazeCell.Length; j++)
                {
                    //  Transforming input char into appropiate node and adding it to the List of connected nodes if meaningful
                    switch(mazeCell.ElementAt(j))
                    {
                        //  Empty nodes are not used to calculate paths
                        case '0': 
                            MazeMap.MazeMatrix[i][j] = new EmptyNode { Id = $"EmptyNode[{i}][{j}]" };                            
                            break;
                        case '1':
                            var pathNode = new PathNode { Id = $"PathNode[{i}][{j}]" };
                            MazeMap.MazeMatrix[i][j] = pathNode;
                            MazeMap.Nodes.Add(pathNode);
                            break;
                        case '*':
                            var sourceNode = new SourceNode { Id = $"SourceNode[{i}][{j}]" };
                            MazeMap.MazeMatrix[i][j] = sourceNode;
                            MazeMap.Nodes.Add(sourceNode);
                            break;
                    }
                }                    
            }

            //  Finding out all possible arrows across valid path cells in four possible directions: up, left, down, right
            for (int i = 0; i < MazeMap.MazeMatrix.Count(); i++)
            {                
                for (int j = 0; j < MazeMap.MazeMatrix[i].Length; j++)
                {
                    if(MazeMap.MazeMatrix[i][j] is PathNode)
                    { 
                        if(i - 1 >= 0 && MazeMap.MazeMatrix[i - 1][j] is PathNode)
                        {
                            MazeMap.Arrows.Add(new Arrow { Id = Guid.NewGuid().ToString(), Vertex1 = MazeMap.MazeMatrix[i][j], Vertex2 = MazeMap.MazeMatrix[i - 1][j] });
                        }
                        
                        if (j - 1 >= 0 && MazeMap.MazeMatrix[i][j - 1] is PathNode)
                        {
                            MazeMap.Arrows.Add(new Arrow { Id = Guid.NewGuid().ToString(), Vertex1 = MazeMap.MazeMatrix[i][j], Vertex2 = MazeMap.MazeMatrix[i][j - 1] });
                        }

                        if (i + 1 < MazeMap.MazeMatrix.Length && MazeMap.MazeMatrix[i + 1][j] is PathNode)
                        {
                            MazeMap.Arrows.Add(new Arrow { Id = Guid.NewGuid().ToString(), Vertex1 = MazeMap.MazeMatrix[i][j], Vertex2 = MazeMap.MazeMatrix[i + 1][j] });
                        }

                        if (j +  1 < MazeMap.MazeMatrix[i].Length && MazeMap.MazeMatrix[i][j + 1] is PathNode)
                        {
                            MazeMap.Arrows.Add(new Arrow { Id = Guid.NewGuid().ToString(), Vertex1 = MazeMap.MazeMatrix[i][j], Vertex2 = MazeMap.MazeMatrix[i][j + 1] });
                        }
                    }
                }
            }
        }

        public async Task SolveMazeAsync()
        {
            INode mazeStartLine = MazeMap.Nodes.Single(n => n is SourceNode);

            //  Initialize original rover and start exploring the maze
            Rover rover = new Rover { Name = "OriginalRover" };

            //  Book the rover to the source node (the start line)
            rover.BookRoverToLocation(mazeStartLine);

            //  Start exploring

            //  Task.Run
            //  Advantages:
            //  1. Simple to use
            //  2. Suitable for offloading CPU-bound work from the main thread
            // Disadvantages:
            //  1. Not suitable for I/O-bound operations
            //  2. If overused, it can lead to excessive thread creation and context switching, which can degrade performance
            await MakeRoverExploreAsync(rover);
        }

        public async Task SolveMazeFactoryAsync()
        {
            INode mazeStartLine = MazeMap.Nodes.Single(n => n is SourceNode);

            //  Initialize original rover and start exploring the maze
            Rover rover = new Rover { Name = "OriginalRover" };

            //  Book the rover to the source node (the start line)
            rover.BookRoverToLocation(mazeStartLine);

            //  Task.Factory.StartNew
            //  Advantages:
            //  1. More flexible than Task.Run as it allows setting task creation options
            // Disadvantages:
            //  1. More complex and often unnecessary for common use cases where Task.Run suffices
            await Task.Factory.StartNew(async () => await MakeRoverExploreAsync(rover), TaskCreationOptions.AttachedToParent);
        }

        public void SolveMaze()
        {
            INode mazeStartLine = MazeMap.Nodes.Single(n => n is SourceNode);

            //  Initialize original rover and start exploring the maze
            Rover rover = new Rover { Name = "OriginalRover" };

            //  Book the rover to the source node (the start line)
            rover.BookRoverToLocation(mazeStartLine);

            //  ThreadPool
            //  Advantages:
            //  1. Directly interacts with the thread pool.
            //  2. Can be more efficient.
            // Disadvantages:
            //  1. Less control over the task lifecycle
            //  2. More manual handling of state and context
            ManualResetEvent doneEvent = new ManualResetEvent(false);
            ThreadPool.QueueUserWorkItem(state => MakeRoverExploreAsync(rover).ContinueWith(task => 
                { 
                    if(task.IsCompleted && !task.IsFaulted)
                    {
                        doneEvent.Set();
                    }
                }).Wait());

            doneEvent.WaitOne();
        }

        public List<INode> GetAdjacentNodes(INode node)
        {
            List<INode> adjacentNodes = new List<INode>();

            List<IArrow> connectedArrows = MazeMap.Arrows.Where(a => a.Vertex1 == node).ToList();

            connectedArrows.ForEach(a => adjacentNodes.Add(a.GetOppositeNode(node)));

            return adjacentNodes.Distinct().ToList();
        }

        private async Task MakeRoverExploreAsync(IRover rover)
        {
            ConcurrentDictionary<string, Task> roverTasks = new ConcurrentDictionary<string, Task>();

            //  Start async task to explore the maze
            await Task.Run(() => 
            {
                bool shouldRoverExplore = true;

                //  If an address is already reserved go there
                if (rover.ReservedNode != null)
                {
                    lock (lockObject)
                    {
                        shouldRoverExplore = rover.TryGoToNextNode();
                    }
                }

                //  If we have more nodes to explore the rover needs to continue moving
                while (shouldRoverExplore)
                {
                    //  Get adjacent nodes to the current node where the rover is
                    List<INode> nextNodes = GetAdjacentNodes(rover.CurrentNode).Where(n => n.Id != rover.PreviousNode?.Id).ToList();

                    //  If only one node to visit send the rover there and reserve the node
                    if (nextNodes.Count == 1)
                    {
                        lock (lockObject)
                        {
                            rover.ReserveNextNode(nextNodes.First());
                            shouldRoverExplore = rover.TryGoToNextNode();
                        }
                    }
                    //  When more than one potential node to visit we need to send more rovers to explore
                    else if(nextNodes.Count > 1)
                    {
                        int numberOfruns = 0;

                        foreach (INode nextNode in nextNodes)
                        {
                            numberOfruns++;
                            INode currentNode = rover.CurrentNode;

                            //  The first potential next nodes will be visited by new rovers
                            if (numberOfruns < nextNodes.Count)
                            {
                                IRover newRover = new Rover { Name = rover.Name + "|" + numberOfruns };

                                lock (lockObject)
                                {
                                    newRover.BookRoverToLocation(currentNode);
                                    newRover.ReserveNextNode(nextNode);
                                }

                                //  Make the new rover start exploring from the current node towards the next node
                                roverTasks.TryAdd(newRover.Name, MakeRoverExploreAsync(newRover));
                            }
                            //  The original rover continues travelling for the last node to visit
                            else
                            {
                                lock (lockObject)
                                {
                                    rover.ReserveNextNode(nextNode);
                                    shouldRoverExplore = rover.TryGoToNextNode();
                                }
                            }
                        }
                    }
                    //  No more nodes to visit, end of the maze
                    else
                    {
                        shouldRoverExplore = false;
                    }
                }
            });

            //  Wait for all child rovers to explore the maze
            await Task.WhenAll(roverTasks.Values.ToList());
        }
    }
}
