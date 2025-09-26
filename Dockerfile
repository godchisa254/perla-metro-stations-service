# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy solution and project files
COPY *.sln ./
COPY perla-metro-stations-service/*.csproj perla-metro-stations-service/
RUN dotnet restore

# Copy everything else and publish
COPY . .
RUN dotnet publish -c Release -o /out --no-restore

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:8080

COPY --from=build /out .

ENTRYPOINT ["dotnet", "perla-metro-stations-service.dll"]
