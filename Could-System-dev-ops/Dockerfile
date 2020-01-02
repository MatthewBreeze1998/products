FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Could-System-dev-ops/ThAmCo-Products.csproj", "Could-System-dev-ops/"]
RUN dotnet restore "Could-System-dev-ops/ThAmCo-Products.csproj"
COPY . .
WORKDIR "/src/Could-System-dev-ops"
RUN dotnet build "ThAmCo-Products.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ThAmCo-Products.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ThAmCo-Products.dll"]