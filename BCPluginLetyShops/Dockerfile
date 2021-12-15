#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BCPluginLetyShops/BCPlugin.LetyShops.csproj", "BCPluginLetyShops/"]
COPY ["BCDataContext/BC.DataContext.csproj", "BCDataContext/"]
COPY ["BC.Interfaces/BC.Interfaces.csproj", "BC.Interfaces/"]
COPY ["BCModels/BC.Models.csproj", "BCModels/"]
COPY ["BCPlugin.Interfaces/BCPlugin.Interfaces.csproj", "BCPlugin.Interfaces/"]
RUN dotnet restore "BCPluginLetyShops/BCPlugin.LetyShops.csproj"
COPY . .
WORKDIR "/src/BCPluginLetyShops"
RUN dotnet build "BCPlugin.LetyShops.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BCPlugin.LetyShops.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BCPlugin.LetyShops.dll"]