FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 5020

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Customer.Api/Customer.Api.csproj", "Customer.Api/"]
RUN dotnet restore "Customer.Api/Customer.Api.csproj"
COPY . .
WORKDIR "/src/Customer.Api"
RUN dotnet build "Customer.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Customer.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS="http://*:5020"
ENTRYPOINT ["dotnet", "Customer.Api.dll"]
