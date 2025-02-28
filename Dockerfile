FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# # Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY ./wait-for-it.sh ./
RUN chmod +x wait-for-it.sh
COPY --from=build-env /app/out .
ENTRYPOINT  ["./wait-for-it.sh", "db:1433", "--", "dotnet", "Stations.dll"]
