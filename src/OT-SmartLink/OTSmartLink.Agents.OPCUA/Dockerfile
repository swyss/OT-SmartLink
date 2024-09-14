FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["OTSmartLink.Agents.OPCUA/OTSmartLink.Agents.OPCUA.csproj", "OTSmartLink.Agents.OPCUA/"]
RUN dotnet restore "OTSmartLink.Agents.OPCUA/OTSmartLink.Agents.OPCUA.csproj"
COPY . .
WORKDIR "/src/OTSmartLink.Agents.OPCUA"
RUN dotnet build "OTSmartLink.Agents.OPCUA.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "OTSmartLink.Agents.OPCUA.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OTSmartLink.Agents.OPCUA.dll"]
