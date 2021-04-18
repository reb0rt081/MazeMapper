using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MazeMapper.Domain;
using MazeMapper.Shared;

namespace MazeMapper.Core
{
    public class MazeMapManager : IMazeMapManager
    {
        public IMazeMap MazeMap { get; private set; }

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
    }
}
