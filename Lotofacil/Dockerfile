# Base para execução (Linux)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Imagem para build (Linux)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["Lotofacil/Lotofacil.Web.csproj", "Lotofacil/"]
COPY ["Lotofacil.Application/Lotofacil.Application.csproj", "Lotofacil.Application/"]
COPY ["Lotofacil.Domain/Lotofacil.Domain.csproj", "Lotofacil.Domain/"]
COPY ["Lotofacil.Infra.IoC/Lotofacil.Infra.IoC.csproj", "Lotofacil.Infra.IoC/"]
COPY ["Lotofacil.Infra.Data/Lotofacil.Infra.Data.csproj", "Lotofacil.Infra.Data/"]

RUN dotnet restore "./Lotofacil/Lotofacil.Web.csproj"
COPY . .
WORKDIR "/src/Lotofacil"
RUN dotnet build "./Lotofacil.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publicação
FROM build AS publish
RUN dotnet publish "./Lotofacil.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Finalização
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Lotofacil.Web.dll"]
