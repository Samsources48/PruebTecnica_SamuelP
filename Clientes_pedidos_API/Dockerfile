FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy solution and project files maintaining the correct directory structure
COPY ["Plant_HexArquitecture_API/Plant_HexArquitecture_API.sln", "Plant_HexArquitecture_API/"]
COPY ["Plant_HexArquitecture_API/Plant_HexArquitecture_API.csproj", "Plant_HexArquitecture_API/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]

# Run restore from the solution directory
WORKDIR /src/Plant_HexArquitecture_API
RUN dotnet restore

# Copy all source code
WORKDIR /src
COPY . .

# Build and publish
WORKDIR /src/Plant_HexArquitecture_API
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

RUN adduser --disabled-password --gecos "" appuser

COPY --from=build /app/publish .

# Set environment variables
ENV ASPNETCORE_URLS=http://+:8081
ENV ASPNETCORE_ENVIRONMENT=Docker

RUN chown -R appuser:appuser /app
USER appuser

EXPOSE 8081

ENTRYPOINT ["dotnet", "Plant_HexArquitecture_API.dll"]
