FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["ApiFunkos.csproj", "./"]
RUN dotnet restore "ApiFunkos.csproj"

COPY . .

RUN dotnet build "ApiFunkos.csproj" \
    -c $BUILD_CONFIGURATION \
    -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release

RUN dotnet publish "ApiFunkos.csproj" \
    -c $BUILD_CONFIGURATION \
    -o /app/publish \
    /p:UseAppHost=false

FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .

RUN mkdir -p /app/data && chown -R app:app /app && chmod -R 777 /app/data

USER app

ENTRYPOINT ["dotnet", "ApiFunkos.dll"]