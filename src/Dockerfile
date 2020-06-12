FROM mcr.microsoft.com/dotnet/core/aspnet:3.1.3-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1.201-buster AS build
WORKDIR /src
COPY ["UniLife.Server/UniLife.Server.csproj", "UniLife.Server/"]
COPY ["UniLife.Shared/UniLife.Shared.csproj", "UniLife.Shared/"]
COPY ["UniLife.Client/UniLife.Client.csproj", "UniLife.Client/"]
COPY ["UniLife.CommonUI/UniLife.CommonUI.csproj", "UniLife.CommonUI/"]
COPY ["UniLife.Storage/UniLife.Storage.csproj", "UniLife.Storage/"]
RUN dotnet restore "UniLife.Server/UniLife.Server.csproj"
COPY . .
WORKDIR "/src/UniLife.Server"
ARG BUILD_CONFIG="Release_CSB"
RUN dotnet build "UniLife.Server.csproj" -c $BUILD_CONFIG -o /app/build --no-restore

FROM build AS publish
ARG BUILD_CONFIG="Release_CSB"
RUN dotnet publish "UniLife.Server.csproj" -c $BUILD_CONFIG -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UniLife.Server.dll"]