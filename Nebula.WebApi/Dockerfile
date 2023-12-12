# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /app
COPY ./Nebula.WebApi/Nebula.WebApi.csproj ./Nebula.WebApi/
RUN dotnet restore ./Nebula.WebApi/Nebula.WebApi.csproj
COPY . .
RUN dotnet publish -c Release -o out

# Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build-env /app/out .
EXPOSE 9050
ENV ASPNETCORE_URLS=http://+:9050
ENTRYPOINT ["dotnet", "Nebula.WebApi.dll"]
