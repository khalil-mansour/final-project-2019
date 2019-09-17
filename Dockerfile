FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /src

# Copy csproj and restore as distinct layers
COPY InsuranceBrokerApp.sln ./
COPY Web.Api.Core/*.csproj ./Web.Api.Core/
COPY Web.Api.Infrastructure/*.csproj ./Web.Api.Infrastructure/
COPY Web.Api/*.csproj ./Web.Api/

RUN dotnet restore
COPY . .

WORKDIR /src/Web.Api.Core
RUN dotnet build -c Release -o /app

WORKDIR /src/Web.Api.Infrastructure
RUN dotnet build -c Release -o /app

WORKDIR /src/Web.Api
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Web.Api.dll"]
