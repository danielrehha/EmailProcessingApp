FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5000

ARG SQLCONNECTION
ARG BLOBCONNECTION
ARG HEADERAUTHKEY

ENV CONNECTIONSTRINGS__SQLCONNECTION=${SQLCONNECTION}
ENV CONNECTIONSTRINGS__BLOB=${BLOBCONNECTION}
ENV SECRETS__HEADERAUTHKEY=${HEADERAUTHKEY}

ENV ASPNETCORE_URLS=https://+:5000

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY *.sln ./
COPY src/API/EmailProcessingApp.API/EmailProcessingApp.API.csproj src/API/EmailProcessingApp.API/
COPY src/Core/EmailProcessingApp.Application/EmailProcessingApp.Application.csproj src/Core/EmailProcessingApp.Application/
COPY src/Core/EmailProcessingApp.Domain/EmailProcessingApp.Domain.csproj src/Core/EmailProcessingApp.Domain/
COPY src/Infrastructure/EmailProcessingApp.Infrastructure/EmailProcessingApp.Infrastructure.csproj src/Infrastructure/EmailProcessingApp.Infrastructure/
COPY src/Infrastructure/EmailProcessingApp.Persistence/EmailProcessingApp.Persistence.csproj src/Infrastructure/EmailProcessingApp.Persistence/
COPY tests/EmailProcessingApp.API.Tests/EmailProcessingApp.API.Tests.csproj tests/EmailProcessingApp.API.Tests/
COPY tests/EmailProcessingApp.Application.Tests/EmailProcessingApp.Application.Tests.csproj tests/EmailProcessingApp.Application.Tests/
COPY tests/EmailProcessingApp.Infrastructure.Tests/EmailProcessingApp.Infrastructure.Tests.csproj tests/EmailProcessingApp.Infrastructure.Tests/
COPY tests/EmailProcessingApp.Persistence.Tests/EmailProcessingApp.Persistence.Tests.csproj tests/EmailProcessingApp.Persistence.Tests/
COPY tests/EmailProcessingApp.Tests.Shared/EmailProcessingApp.Tests.Shared.csproj tests/EmailProcessingApp.Tests.Shared/

RUN dotnet restore
COPY . .
WORKDIR /src/src/API/EmailProcessingApp.API
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "EmailProcessingApp.API.dll"]
