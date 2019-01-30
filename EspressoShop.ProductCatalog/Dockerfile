FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["EspressoShop.ProductCatalog/EspressoShop.ProductCatalog.csproj", "EspressoShop.ProductCatalog/"]
RUN dotnet restore "EspressoShop.ProductCatalog/EspressoShop.ProductCatalog.csproj"
COPY . .
WORKDIR "/src/EspressoShop.ProductCatalog"
RUN dotnet build "EspressoShop.ProductCatalog.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "EspressoShop.ProductCatalog.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "EspressoShop.ProductCatalog.dll"]