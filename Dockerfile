FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /source

COPY *.slnx Directory.Build.props* Directory.Packages.props* nuget.config* ./
COPY EduFlowHub.Api/*.csproj ./EduFlowHub.Api/
COPY EduFlowHub.Application/*.csproj ./EduFlowHub.Application/
COPY EduFlowHub.Infrastructure/*.csproj ./EduFlowHub.Infrastructure/
COPY EduFlowHub.Persistence/*.csproj ./EduFlowHub.Persistence/
COPY EduFlowHub.Domain/*.csproj ./EduFlowHub.Domain/

RUN dotnet restore

COPY . .
WORKDIR /source/EduFlowHub.Api
RUN dotnet publish -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0
RUN apt-get update \
    && apt-get install -y libfontconfig
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080

EXPOSE 8080

ENTRYPOINT ["dotnet", "EduFlowHub.Api.dll"]