#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["OpenPlanningPoker.GameEngine.Api/OpenPlanningPoker.GameEngine.Api.csproj", "OpenPlanningPoker.GameEngine.Api/"]
RUN dotnet restore "OpenPlanningPoker.GameEngine.Api/OpenPlanningPoker.GameEngine.Api.csproj"
COPY . .
WORKDIR "/src/OpenPlanningPoker.GameEngine.Api"
RUN dotnet build "OpenPlanningPoker.GameEngine.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OpenPlanningPoker.GameEngine.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OpenPlanningPoker.GameEngine.Api.dll"]