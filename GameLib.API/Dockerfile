FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app
COPY ./GameLib.Model        /app/GameLib.Model
COPY ./GameLib.Repository   /app/GameLib.Repository
COPY ./GameLib.Service      /app/GameLib.Service
COPY ./GameLib.API          /app/GameLib.API

WORKDIR /app/GameLib.API
RUN dotnet restore
RUN dotnet publish --no-restore  -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/GameLib.API/out ./

ENTRYPOINT ["dotnet", "GameLib.API.dll"]
