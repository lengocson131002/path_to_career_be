FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ClientService/ClientService.API/ClientService.API.csproj", "ClientService/ClientService.API/"]
RUN dotnet restore "ClientService/ClientService.API/ClientService.API.csproj"
COPY . .
WORKDIR "/src/ClientService/ClientService.API"
RUN dotnet build "ClientService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ClientService.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ClientService.API.dll"]
