FROM mcr.microsoft.com/dotnet/aspnet:7.0

WORKDIR /app

COPY /publish .

EXPOSE 80

EXPOSE 443

EXPOSE 9090 

CMD ["dotnet", "NotificationService.Application.dll"]