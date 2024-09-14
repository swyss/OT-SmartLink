FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["OTSmartLink.Services.Monitoring/OTSmartLink.Services.Monitoring.csproj", "OTSmartLink.Services.Monitoring/"]
RUN dotnet restore "OTSmartLink.Services.Monitoring/OTSmartLink.Services.Monitoring.csproj"
COPY . .
WORKDIR "/src/OTSmartLink.Services.Monitoring"
RUN dotnet build "OTSmartLink.Services.Monitoring.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "OTSmartLink.Services.Monitoring.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OTSmartLink.Services.Monitoring.dll"]
