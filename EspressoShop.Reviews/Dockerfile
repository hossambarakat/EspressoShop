FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
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