#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["src/ComponentsSelectTest.ServiceA/ComponentsSelectTest.ServiceA.csproj", "src/ComponentsSelectTest.ServiceA/"]
COPY ["src/SkyWalkingAgentExtension/SkyWalkingAgentExtension.csproj", "src/SkyWalkingAgentExtension/"]
RUN dotnet restore "src/ComponentsSelectTest.ServiceA/ComponentsSelectTest.ServiceA.csproj" -s https://api.nuget.org/v3/index.json -s http://10.0.20.57:5555/v3/index.json
COPY . .
WORKDIR "/src/src/ComponentsSelectTest.ServiceA"
RUN dotnet build "ComponentsSelectTest.ServiceA.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ComponentsSelectTest.ServiceA.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

#监听端口号，多个时以分号分割
#ENV ASPNETCORE_URLS http://+:9001;https://+:9102
ENV ASPNETCORE_URLS http://+:9001

ENTRYPOINT ["dotnet", "ComponentsSelectTest.ServiceA.dll"]