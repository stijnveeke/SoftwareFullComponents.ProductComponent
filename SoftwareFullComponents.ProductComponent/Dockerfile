#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["SoftwareFullComponents.ProductComponent/SoftwareFullComponents.ProductComponent.csproj", "SoftwareFullComponents.ProductComponent/"]
RUN dotnet restore "SoftwareFullComponents.ProductComponent/SoftwareFullComponents.ProductComponent.csproj"
COPY . .
WORKDIR "/src/SoftwareFullComponents.ProductComponent"
RUN dotnet build "SoftwareFullComponents.ProductComponent.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SoftwareFullComponents.ProductComponent.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SoftwareFullComponents.ProductComponent.dll"]