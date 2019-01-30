FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["EspressoShop.Web/EspressoShop.Web.csproj", "EspressoShop.Web/"]
RUN dotnet restore "EspressoShop.Web/EspressoShop.Web.csproj"
COPY . .
WORKDIR "/src/EspressoShop.Web"
RUN dotnet build "EspressoShop.Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "EspressoShop.Web.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "EspressoShop.Web.dll"]