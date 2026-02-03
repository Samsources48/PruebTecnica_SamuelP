FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["Plant_HexArquitecture_API/Plant_HexArquitecture_API.sln", "./"]
COPY ["Plant_HexArquitecture_API/Plant_HexArquitecture_API.csproj", "Plant_HexArquitecture_API/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]

RUN dotnet restore

COPY . .

WORKDIR /src/Plant_HexArquitecture_API
RUN dotnet publish -c Release -o /app/publish --no-restore

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
