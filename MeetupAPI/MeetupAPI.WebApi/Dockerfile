FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MeetupAPI.WebApi/MeetupAPI.WebApi.csproj", "MeetupAPI.WebApi/"]
RUN dotnet restore "MeetupAPI.WebApi/MeetupAPI.WebApi.csproj"
COPY . .
WORKDIR "/src/MeetupAPI.WebApi"
RUN dotnet build "MeetupAPI.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MeetupAPI.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MeetupAPI.WebApi.dll"]
