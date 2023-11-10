#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Proyecto_Laboratotio_Back2/Proyecto_Laboratotio_Back2.csproj", "Proyecto_Laboratotio_Back2/"]
RUN dotnet restore "Proyecto_Laboratotio_Back2/Proyecto_Laboratotio_Back2.csproj"
COPY . .
WORKDIR "/src/Proyecto_Laboratotio_Back2"
RUN dotnet build "Proyecto_Laboratotio_Back2.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Proyecto_Laboratotio_Back2.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Proyecto_Laboratotio_Back2.dll"]