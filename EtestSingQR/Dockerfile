#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["EtestSingQR/EtestSingQR.csproj", "EtestSingQR/"]
RUN dotnet restore "EtestSingQR/EtestSingQR.csproj"
COPY . .
WORKDIR "/src/EtestSingQR"
RUN dotnet build "EtestSingQR.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EtestSingQR.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EtestSingQR.dll"]