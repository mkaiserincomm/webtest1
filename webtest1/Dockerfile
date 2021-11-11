#FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine3.14 AS base
FROM alpine:3.14.2 AS base
WORKDIR /app
EXPOSE 80
ENV ASPNETCORE_URLS=http://*:80

FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine3.14 AS build
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
#RUN dotnet publish "webtest1.csproj" -c Release -o /app/publish --version-suffix ${SUFFIX}
RUN dotnet publish "webtest1.csproj" -c Release -r linux-x64 -o /app/publish --version-suffix ${SUFFIX} --self-contained true /p:PublishSingleFile=true /p:DebugType=None /p:DebugSymbols=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["webtest1"]
