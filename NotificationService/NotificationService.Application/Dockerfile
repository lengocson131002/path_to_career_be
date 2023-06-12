FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["NotificationService/NotificationService.App/NotificationService.App.csproj", "NotificationService/NotificationService.App/"]
RUN dotnet restore "NotificationService/NotificationService.App/NotificationService.App.csproj"
COPY . .
WORKDIR "/src/NotificationService/NotificationService.App"
RUN dotnet build "NotificationService.App.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NotificationService.App.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NotificationService.App.dll"]
