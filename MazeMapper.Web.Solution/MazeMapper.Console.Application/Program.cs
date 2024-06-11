using System.IO;

using CommandLine;
using System.Text;

using MazeMapper.Core;

namespace MazeMapper.Console.Application
{
    internal class Program
    {
        /// <summary>
        /// Solves a maze given an input text. Optional argument: -f, -file
        /// Example: MazeMapper.Console.Application.exe -f C:\Users\input.txt
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Arguments>(args).WithParsed(options => 
                {
                    string maze;
                    if (string.IsNullOrEmpty(options.FilePath))
                    {
                        System.Console.WriteLine(@"<---- Welcome to the MazeMapper console application ---->
Please enter maze (type '*' for source node, type '0' for void, type '1' for path, type 'END' on a new line to finish):");
                        StringBuilder inputMaze = new StringBuilder();
                        while ((maze = System.Console.ReadLine()) != null && maze != "END")
                        {
                            if (!string.IsNullOrEmpty(maze))
                            {
                                inputMaze.AppendLine(maze);
                            }
                        }
                        maze = inputMaze.ToString();
                    }
                    else
                    {
                       maze = File.ReadAllText(options.FilePath);
                    }

                    System.Console.WriteLine("You entered:");
                    System.Console.WriteLine(maze);
                    System.Console.WriteLine(@"Do you want to confirm this maze?
(Y) for yes.
(N) for no.");
                    string result = System.Console.ReadLine();

                    if (result == Options.Y.ToString())
                    {
                        System.Console.WriteLine(@"What option do you want to run? 
(A) Analyze maze.");
                        result = System.Console.ReadLine();

                        if (result == Options.A.ToString())
                        {
                            MazeMapManager mazeMapManager = new MazeMapManager();
                            mazeMapManager.BuildMazeMapFromString(maze);

                            mazeMapManager.SolveMaze();

                            System.Console.WriteLine("Solution for maze:");
                            System.Console.WriteLine(mazeMapManager.MazeMap.ToString());
                            System.Console.ReadLine();
                        }
                    }
                });
        }
    }

    enum Options
    {
        Y,
        N,
        A
    }

    class Arguments
    {
        [Option('f', "file", Required = false, HelpText = "Input file path.")]
        public string FilePath { get; set; }
    }


}
