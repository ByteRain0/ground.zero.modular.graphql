FROM mcr.microsoft.com/dotnet/runtime:9.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /app
COPY ["Directory.Build.props", "."]
COPY ["Directory.Packages.props", "."]
COPY ["src/Host/Japanese.Api.MigrationService/Japanese.Api.MigrationService.csproj", "src/Host/Japanese.Api.MigrationService/"]
COPY ["src/Modules/Anime/Anime.Service/Anime.Service.csproj", "src/Modules/Anime/Anime.Service/"]
COPY ["src/Utils/Core/Core.csproj", "src/Utils/Core/"]
COPY ["src/Modules/Anime/Anime.Contracts/Anime.Contracts.csproj", "src/Modules/Anime/Anime.Contracts/"]
COPY ["src/Utils/Common/Common.csproj", "src/Utils/Common/"]
COPY ["src/Modules/Manga/Manga.Service/Manga.Service.csproj", "src/Modules/Manga/Manga.Service/"]
COPY ["src/Modules/Manga/Manga.Contracts/Manga.Contracts.csproj", "src/Modules/Manga/Manga.Contracts/"]
RUN dotnet restore "src/Host/Japanese.Api.MigrationService/Japanese.Api.MigrationService.csproj"
COPY . .
WORKDIR "/app/src/Host/Japanese.Api.MigrationService"
RUN dotnet build "Japanese.Api.MigrationService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Japanese.Api.MigrationService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Japanese.Api.MigrationService.dll"]
