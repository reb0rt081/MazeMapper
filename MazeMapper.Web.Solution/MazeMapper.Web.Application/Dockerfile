#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /src
COPY ["MazeMapper.Web.Application/MazeMapper.Web.Application.csproj", "MazeMapper.Web.Application/"]
RUN dotnet restore "MazeMapper.Web.Application/MazeMapper.Web.Application.csproj"
COPY . .
WORKDIR "/src/MazeMapper.Web.Application"
RUN dotnet build "MazeMapper.Web.Application.csproj" -c Release -o /app/build
RUN dotnet publish "MazeMapper.Web.Application.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MazeMapper.Web.Application.dll"]

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/publish .
ENTRYPOINT ["dotnet", "MazeMapper.Web.Application.exe"]