FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build
WORKDIR /MyCookin.API

# copy csproj and restore as distinct layers
COPY /. ./
RUN dotnet restore

# copy everything else and build app
#COPY MyCookin.API/. ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine AS runtime
WORKDIR /MyCookin.API
COPY --from=build /MyCookin.API/out ./
ENTRYPOINT ["dotnet", "MyCookin.API.dll"]

EXPOSE 80
EXPOSE 443
