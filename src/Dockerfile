FROM mcr.microsoft.com/dotnet/core/aspnet:3.1.3-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1.201-buster AS build
WORKDIR /src
COPY ["Semerkand.Server/Semerkand.Server.csproj", "Semerkand.Server/"]
COPY ["Semerkand.Shared/Semerkand.Shared.csproj", "Semerkand.Shared/"]
COPY ["Semerkand.Client/Semerkand.Client.csproj", "Semerkand.Client/"]
COPY ["Semerkand.CommonUI/Semerkand.CommonUI.csproj", "Semerkand.CommonUI/"]
COPY ["Semerkand.Storage/Semerkand.Storage.csproj", "Semerkand.Storage/"]
RUN dotnet restore "Semerkand.Server/Semerkand.Server.csproj"
COPY . .
WORKDIR "/src/Semerkand.Server"
ARG BUILD_CONFIG="Release_CSB"
RUN dotnet build "Semerkand.Server.csproj" -c $BUILD_CONFIG -o /app/build --no-restore

FROM build AS publish
ARG BUILD_CONFIG="Release_CSB"
RUN dotnet publish "Semerkand.Server.csproj" -c $BUILD_CONFIG -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Semerkand.Server.dll"]