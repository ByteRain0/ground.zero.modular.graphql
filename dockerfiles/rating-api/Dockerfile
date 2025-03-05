FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /app
COPY ["Directory.Build.props", "."]
COPY ["Directory.Packages.props", "."]
COPY ["src/Host/Rating.Api/Rating.Api.csproj", "src/Host/Rating.Api/"]
COPY ["src/Utils/Core/Core.csproj", "src/Utils/Core/"]
COPY ["src/Utils/Common/Common.csproj", "src/Utils/Common/"]
RUN dotnet restore "src/Host/Rating.Api/Rating.Api.csproj"
COPY . .
WORKDIR "/app/src/Host/Rating.Api"
RUN dotnet build "Rating.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Rating.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Rating.Api.dll"]
