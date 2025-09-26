# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the .csproj file and restore dependencies
COPY *.csproj .
RUN dotnet restore

# Copy the rest of the application files and build the project
COPY . .
RUN dotnet publish -c Release -o /app --no-restore

# Stage 2: Serve the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080

# Copy the published output from the build stage
COPY --from=build /app .

# Set the entry point to run the application
ENTRYPOINT ["dotnet", "perla-metro-stations-service.dll"]
