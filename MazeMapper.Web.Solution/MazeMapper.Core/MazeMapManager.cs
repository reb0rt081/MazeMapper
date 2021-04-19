using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MazeMapper.Domain;
using MazeMapper.Shared;

using Microsoft.VisualBasic;

namespace MazeMapper.Core
{
    public class MazeMapManager : IMazeMapManager
    {
        private static readonly object lockObject = new object();

        public IMazeMap MazeMap { get; private set; }

        private List<Task> roverTasks;

        public MazeMapManager()
        {
            MazeMap = new MazeMap();
        }

        public void BuildMazeMapFromString(string mazeMapText)
        {
            string[] mazeColumns = mazeMapText.Split(Environment.NewLine);

            if (mazeColumns.Select(m => m.Length).Distinct().Count() > 1)
            {
                throw new Exception("Maze map input string must have the same number of chars per line!");
            }

            MazeMap.MazeMatrix = new INode[mazeColumns.Count()][];


            for (int i = 0; i < mazeColumns.Count(); i++)
            {
                string mazeCell = mazeColumns[i];

                MazeMap.MazeMatrix[i] = new INode[mazeCell.Length];

                for (int j = 0; j < mazeCell.Length; j++)
                {
                    switch(mazeCell.ElementAt(j))
                    {
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
            roverTasks = new List<Task>();

            INode mazeStartLine = MazeMap.Nodes.Single(n => n is SourceNode);

            Rover rover = new Rover { Name = "OriginalRover" };
            rover.BookRoverToLocation(mazeStartLine);

            await MakeRoverExploreAsync(rover);

            await Task.WhenAll(roverTasks);
        }

        public List<INode> GetAdjacentNodes(INode node)
        {
            List<INode> adjacentNodes = new List<INode>();

            List<IArrow> connectedArrows = MazeMap.Arrows.Where(a => a.Vertex1 == node || a.Vertex2 == node).ToList();

            connectedArrows.ForEach(a => adjacentNodes.Add(a.GetOppositeNode(node)));

            return adjacentNodes.Distinct().ToList();
        }

        private async Task MakeRoverExploreAsync(IRover rover)
        {
            await Task.Run(() => 
            {
                bool shouldRoverExplore = true;

                if (rover.ReservedNode != null)
                {
                    lock (lockObject)
                    {
                        shouldRoverExplore = rover.TryGoToNextNode();
                    }
                }

                while (shouldRoverExplore)
                {
                    List<INode> nextNodes = GetAdjacentNodes(rover.CurrentNode).Where(n => n.Id != rover.PreviousNode?.Id).ToList();

                    if (nextNodes.Count == 1)
                    {
                        lock (lockObject)
                        {
                            rover.ReserveNextNode(nextNodes.First());
                            shouldRoverExplore = rover.TryGoToNextNode();
                        }
                    }
                    else if(nextNodes.Count > 1)
                    {
                        int numberOfruns = 0;

                        foreach (INode nextNode in nextNodes)
                        {
                            numberOfruns++;
                            INode currentNode = rover.CurrentNode;

                            if (numberOfruns < nextNodes.Count)
                            {
                                IRover newRover = new Rover { Name = rover.Name + "|" + numberOfruns };

                                lock (lockObject)
                                {
                                    newRover.BookRoverToLocation(currentNode);
                                    newRover.ReserveNextNode(nextNode);
                                }

                                roverTasks.Add(MakeRoverExploreAsync(newRover));
                            }
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
                    else
                    {
                        shouldRoverExplore = false;
                    }
                }
            });
        }
    }
}
