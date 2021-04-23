# MazeMapper

This repository contains a web client and a API solution that is able to solve mazes. The web client is based on react while the API backend is based on ASP.NET core 3.1.

Given an input maze as a ser of '0's, '1's and '*'s this repository can find the minimum amount of steps from anywhere in the maze to the exit.

Example of input maze:
1111111111
1010100100
0010001101
1111111001
1001111101
1111110111
0101011101
*101110111

Where '1' represents a path across the maze, '*' the exist of the maze and '0' an empty point.

This is just an example of an application built with react and ASP.NET core.

In order to make the Github Workflow .yml work:
1) Edit .YML file (pointing to right DockerFile)
2) Edit DockerFile (pointing to right files)
3) Target .csproj needs to disable PublishRunWebpack target as it could run npm install in an environment without it installed
