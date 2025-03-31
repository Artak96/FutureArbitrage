FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["FutureArbitrage.Api/FutureArbitrage.Api.csproj", "FutureArbitrage.Api/"]
COPY ["FutureArbitrage.Application/FutureArbitrage.Application.csproj", "FutureArbitrage.Application/"]
COPY ["FutureArbitrage.Domain/FutureArbitrage.Domain.csproj", "FutureArbitrage.Domain/"]
COPY ["FutureArbitrage.Infrastructure/FutureArbitrage.Infrastructure.csproj", "FutureArbitrage.Infrastructure/"]
RUN dotnet restore "./FutureArbitrage.Api/./FutureArbitrage.Api.csproj"
COPY . .  
WORKDIR "/src/FutureArbitrage.Api"
RUN dotnet build "./FutureArbitrage.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build  

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./FutureArbitrage.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FutureArbitrage.Api.dll"]
