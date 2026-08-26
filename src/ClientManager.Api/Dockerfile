FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/ClientManager.Api/ClientManager.Api.csproj", "src/ClientManager.Api/"]
COPY ["src/ClientManager.Core/ClientManager.Core.csproj", "src/ClientManager.Core/"]
COPY ["src/ClientManager.Infrastructure/ClientManager.Infrastructure.csproj", "src/ClientManager.Infrastructure/"]
RUN dotnet restore "src/ClientManager.Api/ClientManager.Api.csproj"
COPY . .
WORKDIR "/src/src/ClientManager.Api"
RUN dotnet build "ClientManager.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ClientManager.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ClientManager.Api.dll"]
