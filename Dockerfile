FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
ENV ASPNETCORE_URLS=http://*:80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["webtest1.csproj", "./"]
RUN dotnet restore "webtest1.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build /p:Version=$Version "webtest1.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "webtest1.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "webtest1.dll"]
