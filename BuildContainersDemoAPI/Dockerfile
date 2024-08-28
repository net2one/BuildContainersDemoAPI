ARG ASPNET_VERISON=8.0

# Use the official ASP.NET Core runtime as a base image
#FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
FROM mcr.microsoft.com/dotnet/aspnet:$ASPNET_VERISON AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Use the .NET SDK to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy the csproj and restore as distinct layers
COPY ["BuildContainersDemoAPI.csproj", "."]
RUN dotnet restore "BuildContainersDemoAPI.csproj"

# Copy everything else and build the application
COPY . .
RUN dotnet build "./BuildContainersDemoAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the application
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BuildContainersDemoAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Use the runtime base image to run the application
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Set environment variables
ENV ASPNETCORE_ENVIRONMENT=Development

# Set the entry point
ENTRYPOINT ["dotnet", "BuildContainersDemoAPI.dll"]