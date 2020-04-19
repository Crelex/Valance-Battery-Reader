#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Battery Reader/Valance-Battery-Reader.csproj", "Battery Reader/"]
COPY ["Valence/Valence/Valence.csproj", "Valence/Valence/"]
RUN dotnet restore "Battery Reader/Valance-Battery-Reader.csproj"
COPY . .
WORKDIR "/src/Battery Reader"
RUN dotnet build "Valance-Battery-Reader.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Valance-Battery-Reader.csproj" -c Release -o /app/publish

FROM base AS final
#need to add dependencies that RJCP apparently uses under the covers 
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Valance-Battery-Reader.dll"]
