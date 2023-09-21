FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Brewery.API/Brewery.API.csproj", "Brewery.API/"]
RUN dotnet restore "Brewery.API/Brewery.API.csproj"
COPY . .
WORKDIR "/src/Brewery.API"
RUN dotnet build "Brewery.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Brewery.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Brewery.API.dll"]
