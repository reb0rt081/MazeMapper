using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using MazeMapper.Shared;

namespace MazeMapper.Domain
{
    public interface IMazeMapManager
    {
        /// <summary>
        /// Gets the loaded maze to solve.
        /// </summary>
        IMazeMap MazeMap { get; }

        /// <summary>
        /// Builds a maze in string format that can be solved.
        /// </summary>
        /// <param name="mazeMapText"></param>
        void BuildMazeMapFromString(string mazeMapText);

        /// <summary>
        /// Solves the maze.
        /// </summary>
        /// <returns></returns>
        Task SolveMazeAsync();

        /// <summary>
        /// Returns the adjacents nodes to the input node.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        List<INode> GetAdjacentNodes(INode node);
    }
}
