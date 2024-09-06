FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/PetSave.API/PetSave.API.csproj", "src/PetSave.API/"]
COPY ["src/PetSave.Infra/PetSave.Infra.csproj", "src/PetSave.Infra/"]
COPY ["src/PetSave.Application/PetSave.Application.csproj", "src/PetSave.Application/"]
COPY ["src/PetSave.Domain/PetSave.Domain.csproj", "src/PetSave.Domain/"]
RUN dotnet restore "src/PetSave.API/PetSave.API.csproj"
COPY . .
WORKDIR "/src/src/PetSave.API"
RUN dotnet build "PetSave.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "PetSave.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PetSave.API.dll"]