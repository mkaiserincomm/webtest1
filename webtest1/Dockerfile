FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine3.16 AS base
WORKDIR /app
EXPOSE 80
ENV ASPNETCORE_URLS=http://*:80

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine3.16 AS build
ARG SUFFIX
RUN echo "versionsuffix=${SUFFIX}"
WORKDIR /src
COPY ["webtest1.csproj", "./"]
RUN dotnet restore "webtest1.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "webtest1.csproj" -c Release -o /app/build --version-suffix ${SUFFIX}

FROM build AS publish
ARG SUFFIX
RUN dotnet publish "webtest1.csproj" -c Release -o /app/publish --version-suffix ${SUFFIX}

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "webtest1.dll"]
