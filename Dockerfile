# Use the official .NET 6 SDK image as the base image
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Set the working directory inside the conta7iner
WORKDIR /app

# Copy the necessary files for the build process
COPY . .

# Build the application
RUN dotnet publish -c Release -o out

# Create a runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime

# Set the working directory inside the container
WORKDIR /app

# Copy the published output from the build image to the runtime image
COPY --from=build /app/out ./

# Expose the port that the application will run on
EXPOSE 80

# Define the entry point for the application
ENTRYPOINT ["dotnet", "MiniTrello.Web.dll"]
