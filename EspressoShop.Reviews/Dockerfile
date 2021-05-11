FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["EspressoShop.Reviews/EspressoShop.Reviews.csproj", "EspressoShop.Reviews/"]
RUN dotnet restore "EspressoShop.Reviews/EspressoShop.Reviews.csproj"
COPY . .
WORKDIR "/src/EspressoShop.Reviews"
RUN dotnet build "EspressoShop.Reviews.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "EspressoShop.Reviews.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "EspressoShop.Reviews.dll"]