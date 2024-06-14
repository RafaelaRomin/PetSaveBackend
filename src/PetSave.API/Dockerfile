# Estágio 1: Construir a aplicação
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app
EXPOSE 80

# Copiar todos os arquivos csproj e restaurar dependências
COPY src/PetSave.API/*.csproj ./PetSave.API/
COPY src/PetSave.Application/*.csproj ./PetSave.Application/
COPY src/PetSave.Domain/*.csproj ./PetSave.Domain/
COPY src/PetSave.Infra/*.csproj ./PetSave.Infra/

WORKDIR /app/PetSave.API
RUN dotnet restore

# Copiar o código e construir a aplicação
WORKDIR /app/
COPY . .

WORKDIR /app/PetSave.API
RUN dotnet publish -c Release -o out

# Estágio 2: Publicar a aplicação
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
WORKDIR /app
COPY --from=build /app/PetSave.API/out ./
ENTRYPOINT ["dotnet", "PetSave.API.dll"]


