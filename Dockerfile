# Dockerfile para BankBranches API
# Basado en .NET 8 SDK e Instancia de ejecución

# Etapa 1: Build y Publish
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar archivos de proyecto para restaurar dependencias
COPY ["BankBranches.Api/BankBranches.Api.csproj", "BankBranches.Api/"]
COPY ["BankBranches.Application/BankBranches.Application.csproj", "BankBranches.Application/"]
COPY ["BankBranches.Domain/BankBranches.Domain.csproj", "BankBranches.Domain/"]
COPY ["BankBranches.Infrastructure/BankBranches.Infrastructure.csproj", "BankBranches.Infrastructure/"]

# Restaurar paquetes
RUN dotnet restore "BankBranches.Api/BankBranches.Api.csproj"

# Copiar todo el código fuente
COPY . .

# Compilar y publicar
WORKDIR "/src/BankBranches.Api"
RUN dotnet build "BankBranches.Api.csproj" -c Release -o /app/build
RUN dotnet publish "BankBranches.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

COPY --from=build /app/publish .

# Render usa la variable de entorno PORT, ASP.NET Core usa ASPNETCORE_URLS
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "BankBranches.Api.dll"]
