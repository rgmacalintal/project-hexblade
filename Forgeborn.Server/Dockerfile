FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Forgeborn.Server.csproj", "."]
RUN dotnet restore "Forgeborn.Server.csproj"
COPY . .
RUN dotnet build "Forgeborn.Server.csproj" -c $BUILD_CONFIGURATION -o /app/build
RUN dotnet publish "Forgeborn.Server.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Forgeborn.Server.dll"]
