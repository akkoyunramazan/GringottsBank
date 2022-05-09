FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Transaction.Api.csproj", "Transaction.Api/"]
RUN dotnet restore "Transaction.Api.csproj"
COPY . .
WORKDIR "/src/Transaction.Api"
RUN dotnet build "Transaction.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Transaction.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Transaction.Api.dll"]
