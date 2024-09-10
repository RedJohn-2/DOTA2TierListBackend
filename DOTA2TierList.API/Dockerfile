#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["DOTA2TierList.API/DOTA2TierList.API.csproj", "DOTA2TierList.API/"]
COPY ["DOTA2TierList.Application/DOTA2TierList.Application.csproj", "DOTA2TierList.Application/"]
COPY ["DOTA2TierList.Logic/DOTA2TierList.Logic.csproj", "DOTA2TierList.Logic/"]
COPY ["DOTA2TierList.Infrastructure/DOTA2TierList.Infrastructure.csproj", "DOTA2TierList.Infrastructure/"]
COPY ["DOTA2TierList.Persistence/DOTA2TierList.Persistence.csproj", "DOTA2TierList.Persistence/"]
RUN dotnet restore "./DOTA2TierList.API/DOTA2TierList.API.csproj"
COPY . .
WORKDIR "/src/DOTA2TierList.API"
RUN dotnet build "./DOTA2TierList.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./DOTA2TierList.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DOTA2TierList.API.dll"]