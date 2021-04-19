using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using MazeMapper.Shared;

namespace MazeMapper.Domain
{
    public interface IMazeMapManager
    {
        IMazeMap MazeMap { get; }

        void BuildMazeMapFromString(string mazeMapText);

        Task SolveMazeAsync();

        List<INode> GetAdjacentNodes(INode node);
    }
}
