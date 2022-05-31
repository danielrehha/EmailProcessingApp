#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["EmailProcessingApp.API/EmailProcessingApp.API.csproj", "EmailProcessingApp.API/"]
RUN dotnet restore "EmailProcessingApp.API/EmailProcessingApp.API.csproj"
COPY . .
WORKDIR "/src/EmailProcessingApp.API"
RUN dotnet build "EmailProcessingApp.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EmailProcessingApp.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EmailProcessingApp.API.dll"]