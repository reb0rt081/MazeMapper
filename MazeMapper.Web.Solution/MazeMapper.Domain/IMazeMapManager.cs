using System;
using System.Collections.Generic;
using System.Text;
using MazeMapper.Shared;

namespace MazeMapper.Domain
{
    public interface IMazeMapManager
    {
        IMazeMap MazeMap { get; }

        void BuildMazeMapFromString(string mazeMapText);
    }
}
