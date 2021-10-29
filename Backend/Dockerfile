FROM mcr.microsoft.com/dotnet/sdk:5.0.103 AS build
WORKDIR /build

# copy csproj and restore as distinct layers
COPY *.sln .
COPY ./Application/*.csproj ./Application/
COPY ./Domain/*.csproj ./Domain/
COPY ./Infrastructure/*.csproj ./Infrastructure/
COPY ./Kastel-Planner-Backend/*.csproj ./Kastel-Planner-Backend/
RUN dotnet restore

# copy everything else and build app
COPY . .
WORKDIR /build
RUN dotnet publish -c release -o published --no-cache
# Development ssl certificates
RUN dotnet dev-certs https --clean
RUN dotnet dev-certs https --trust
# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build /root/.dotnet/corefx/cryptography/x509stores/my/* /root/.dotnet/corefx/cryptography/x509stores/my/
COPY --from=build /build/published ./
# ENTRYPOINT ["dotnet", "Kastel-Planner-Backend.dll"]
