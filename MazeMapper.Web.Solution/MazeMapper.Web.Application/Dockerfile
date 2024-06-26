#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["MazeMapper.Web.Solution/MazeMapper.Web.Application/MazeMapper.Web.Application.csproj", "MazeMapper.Web.Solution/MazeMapper.Web.Application/"]
RUN dotnet restore "MazeMapper.Web.Solution/MazeMapper.Web.Application/MazeMapper.Web.Application.csproj"
COPY . .
WORKDIR "/src/MazeMapper.Web.Solution/MazeMapper.Web.Application"
RUN dotnet build "MazeMapper.Web.Application.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MazeMapper.Web.Application.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MazeMapper.Web.Application.exe"]