FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG TARGETOS
ARG TARGETARCH
WORKDIR /src

COPY ["WebApi.csproj", "./"]
RUN dotnet restore "./WebApi.csproj" -r $TARGETOS-$TARGETARCH

COPY . .
RUN dotnet publish "./WebApi.csproj" -c Release -o /app/publish -r $TARGETOS-$TARGETARCH /p:UseAppHost=false --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:8009
ENV ASPNETCORE_ENVIRONMENT=Production

COPY --from=build /app/publish .

EXPOSE 8009

ENTRYPOINT ["dotnet", "WebApi.dll"]
