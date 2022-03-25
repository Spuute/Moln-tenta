FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /ee

COPY *.csproj ./
RUN dotnet restore

COPY . .
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0 
WORKDIR /ee
COPY --from=build-env /ee/out .
ENTRYPOINT ["dotnet", "frontend.dll"]