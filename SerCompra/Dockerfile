FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["SerCompra/SerCompra.csproj", "SerCompra/"]
RUN dotnet restore "SerCompra/SerCompra.csproj"
COPY . .
WORKDIR "/src/SerCompra"
RUN dotnet build "SerCompra.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SerCompra.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SerCompra.dll"]
