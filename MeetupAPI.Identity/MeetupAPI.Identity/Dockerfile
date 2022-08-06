FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MeetupAPI.Identity/MeetupAPI.Identity.csproj", "MeetupAPI.Identity/"]
RUN dotnet restore "MeetupAPI.Identity/MeetupAPI.Identity.csproj"
COPY . .
WORKDIR "/src/MeetupAPI.Identity"
RUN dotnet build "MeetupAPI.Identity.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MeetupAPI.Identity.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MeetupAPI.Identity.dll"]
