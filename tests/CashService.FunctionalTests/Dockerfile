#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["tests/CashService.FunctionalTests/CashService.FunctionalTests.csproj", "tests/CashService.FunctionalTests/"]
COPY ["src/CashService.GRPC/CashService.GRPC.csproj", "src/CashService.GRPC/"]
COPY ["src/CashService.BusinessLogic/CashService.BusinessLogic.csproj", "src/CashService.BusinessLogic/"]
COPY ["src/CashService.DataAccess/CashService.DataAccess.csproj", "src/CashService.DataAccess/"]
RUN dotnet restore "tests/CashService.FunctionalTests/CashService.FunctionalTests.csproj"
COPY . .
WORKDIR "/src/tests/CashService.FunctionalTests"
RUN dotnet build "CashService.FunctionalTests.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CashService.FunctionalTests.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM publish AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CashService.FunctionalTests.dll"]