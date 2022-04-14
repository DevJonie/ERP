#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/ERP.Web/ERP.Web.csproj", "src/ERP.Web/"]
COPY ["src/ERP.ProductCatalog/ERP.ProductCatalog/ERP.ProductCatalog.csproj", "src/ERP.ProductCatalog/ERP.ProductCatalog/"]
COPY ["src/ERP.Common/ERP.Common/ERP.Common.csproj", "src/ERP.Common/ERP.Common/"]
COPY ["src/ERP.ProductCatalog/ERP.ProductCatalog.Contracts/ERP.ProductCatalog.Contracts.csproj", "src/ERP.ProductCatalog/ERP.ProductCatalog.Contracts/"]
COPY ["src/ERP.SalesOrder/ERP.SalesOrder.Contracts/ERP.SalesOrder.Contracts.csproj", "src/ERP.SalesOrder/ERP.SalesOrder.Contracts/"]
COPY ["src/ERP.SalesOrder/ERP.SalesOrder/ERP.SalesOrder.csproj", "src/ERP.SalesOrder/ERP.SalesOrder/"]
RUN dotnet restore "src/ERP.Web/ERP.Web.csproj"
COPY . .
WORKDIR "/src/src/ERP.Web"
RUN dotnet build "ERP.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ERP.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ERP.Web.dll"]
