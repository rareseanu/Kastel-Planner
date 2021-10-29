FROM mcr.microsoft.com/dotnet/sdk:5.0.103 AS build
WORKDIR /build

RUN curl -sL https://deb.nodesource.com/setup_14.x |  bash -
RUN apt-get install -y nodejs

COPY ./*.csproj .
RUN dotnet restore

COPY . .
WORKDIR /build
RUN dotnet publish -c release -o published --no-cache

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build /build/published ./
ENTRYPOINT ["dotnet", "Kastel-Planner-Backend.dll"]

