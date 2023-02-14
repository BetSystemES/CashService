#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/CashService.DatabaseMigrator/CashService.DatabaseMigrator.csproj", "src/CashService.DatabaseMigrator/"]
COPY ["src/CashService.DataAccess/CashService.DataAccess.csproj", "src/CashService.DataAccess/"]
COPY ["src/CashService.BusinessLogic/CashService.BusinessLogic.csproj", "src/CashService.BusinessLogic/"]
RUN dotnet restore "src/CashService.DatabaseMigrator/CashService.DatabaseMigrator.csproj"
COPY . .
WORKDIR "/src/src/CashService.DatabaseMigrator"
RUN dotnet build "CashService.DatabaseMigrator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CashService.DatabaseMigrator.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .