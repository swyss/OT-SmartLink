FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["OPC-UA_WorkerService/OPC-UA_WorkerService.csproj", "OPC-UA_WorkerService/"]
RUN dotnet restore "OPC-UA_WorkerService/OPC-UA_WorkerService.csproj"
COPY . .
WORKDIR "/src/OPC-UA_WorkerService"
RUN dotnet build "OPC-UA_WorkerService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "OPC-UA_WorkerService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OPC-UA_WorkerService.dll"]
