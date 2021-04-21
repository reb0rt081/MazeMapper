# MazeMapper

In order to make the Workflow yaml work:
1) Edit .YML file (pointing to right DockerFile)
2) Edit DockerFile (pointing to right files)
3) Target .csproj needs to disable PublishRunWebpack target as it could run npm install in an environment without it installed
