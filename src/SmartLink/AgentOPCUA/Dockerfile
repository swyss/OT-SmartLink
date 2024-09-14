FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["AgentOPCUA/AgentOPCUA.csproj", "AgentOPCUA/"]
RUN dotnet restore "AgentOPCUA/AgentOPCUA.csproj"
COPY . .
WORKDIR "/src/AgentOPCUA"
RUN dotnet build "AgentOPCUA.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "AgentOPCUA.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AgentOPCUA.dll"]
