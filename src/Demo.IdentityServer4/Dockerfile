#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["src/Demo.IdentityServer4/Demo.IdentityServer4.csproj", "src/Demo.IdentityServer4/"]
RUN dotnet restore "src/Demo.IdentityServer4/Demo.IdentityServer4.csproj"
COPY . .
WORKDIR "/src/src/Demo.IdentityServer4"
RUN dotnet build "Demo.IdentityServer4.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Demo.IdentityServer4.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# sqlserver TLSÐ­Òé°æ±¾½µµÍ
RUN sed -i 's/DEFAULT@SECLEVEL=2/DEFAULT@SECLEVEL=1/g' /etc/ssl/openssl.cnf
RUN sed -i 's/MinProtocol = TLSv1.2/MinProtocol = TLSv1/g' /etc/ssl/openssl.cnf
RUN sed -i 's/DEFAULT@SECLEVEL=2/DEFAULT@SECLEVEL=1/g' /usr/lib/ssl/openssl.cnf
RUN sed -i 's/MinProtocol = TLSv1.2/MinProtocol = TLSv1/g' /usr/lib/ssl/openssl.cnf

#¼àÌý¶Ë¿ÚºÅ
ENV ASPNETCORE_URLS http://+:5000

ENTRYPOINT ["dotnet", "Demo.IdentityServer4.dll"]