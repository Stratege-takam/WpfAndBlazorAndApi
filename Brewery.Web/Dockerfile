FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Brewery.Web/Brewery.Web.csproj", "Brewery.Web/"]
RUN dotnet restore "Brewery.Web/Brewery.Web.csproj"
COPY . .
WORKDIR "/src/Brewery.Web"
RUN dotnet build "Brewery.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Brewery.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Brewery.Web.dll"]
