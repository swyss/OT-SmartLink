FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["OTSmartLink.Agents.MQTT/OTSmartLink.Agents.MQTT.csproj", "OTSmartLink.Agents.MQTT/"]
RUN dotnet restore "OTSmartLink.Agents.MQTT/OTSmartLink.Agents.MQTT.csproj"
COPY . .
WORKDIR "/src/OTSmartLink.Agents.MQTT"
RUN dotnet build "OTSmartLink.Agents.MQTT.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "OTSmartLink.Agents.MQTT.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OTSmartLink.Agents.MQTT.dll"]
