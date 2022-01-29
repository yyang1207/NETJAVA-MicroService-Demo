#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["src/Gateway.Ocelot/Gateway.Ocelot.csproj", "src/Gateway.Ocelot/"]
RUN dotnet restore "src/Gateway.Ocelot/Gateway.Ocelot.csproj" -s https://api.nuget.org/v3/index.json -s http://10.0.20.57:5555/v3/index.json
COPY . .
WORKDIR "/src/src/Gateway.Ocelot"
RUN dotnet build "Gateway.Ocelot.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Gateway.Ocelot.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

#¼àÌý¶Ë¿ÚºÅ
ENV ASPNETCORE_URLS http://+:9100

ENTRYPOINT ["dotnet", "Gateway.Ocelot.dll"]