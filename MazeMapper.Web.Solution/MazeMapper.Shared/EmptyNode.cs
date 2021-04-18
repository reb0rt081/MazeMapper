using System;

namespace MazeMapper.Shared
{
    /// <summary>
    /// Represents an empty node which cannot be walked.
    /// </summary>
    public class EmptyNode : INode
    {
        public string Id { get; set; }
        public int Cost { get; set; }
    }
}
