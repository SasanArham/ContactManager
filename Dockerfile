FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /source
COPY . .
RUN dotnet restore 
RUN dotnet publish -c release -o /publishDir --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /publishDir
COPY --from=build /publishDir ./

EXPOSE 80
ENTRYPOINT ["dotnet","WebAPI.dll"]