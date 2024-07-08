FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /src
COPY ["src/Factora.Web.Host/Factora.Web.Host.csproj", "src/Factora.Web.Host/"]
COPY ["src/Factora.Web.Core/Factora.Web.Core.csproj", "src/Factora.Web.Core/"]
COPY ["src/Factora.Application/Factora.Application.csproj", "src/Factora.Application/"]
COPY ["src/Factora.Core/Factora.Core.csproj", "src/Factora.Core/"]
COPY ["src/Factora.EntityFrameworkCore/Factora.EntityFrameworkCore.csproj", "src/Factora.EntityFrameworkCore/"]
WORKDIR "/src/src/Factora.Web.Host"
RUN dotnet restore 

WORKDIR /src
COPY ["src/Factora.Web.Host", "src/Factora.Web.Host"]
COPY ["src/Factora.Web.Core", "src/Factora.Web.Core"]
COPY ["src/Factora.Application", "src/Factora.Application"]
COPY ["src/Factora.Core", "src/Factora.Core"]
COPY ["src/Factora.EntityFrameworkCore", "src/Factora.EntityFrameworkCore"]
WORKDIR "/src/src/Factora.Web.Host"
RUN dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:7.0
EXPOSE 80
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "Factora.Web.Host.dll"]
