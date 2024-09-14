FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["OTSmartLink.Services.Security/OTSmartLink.Services.Security.csproj", "OTSmartLink.Services.Security/"]
RUN dotnet restore "OTSmartLink.Services.Security/OTSmartLink.Services.Security.csproj"
COPY . .
WORKDIR "/src/OTSmartLink.Services.Security"
RUN dotnet build "OTSmartLink.Services.Security.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "OTSmartLink.Services.Security.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OTSmartLink.Services.Security.dll"]
