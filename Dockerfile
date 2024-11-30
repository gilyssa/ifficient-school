# Etapa 1: Construção do projeto usando o SDK do .NET
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Definir o diretório de trabalho
WORKDIR /src

# Copiar o arquivo .csproj e restaurar as dependências
COPY ["ifficient-school.csproj", "./"]

# Restaurar dependências do projeto
RUN dotnet restore "ifficient-school.csproj"

# Copiar o restante dos arquivos do projeto
COPY . .

# Publicar a aplicação para o diretório /app/publish
RUN dotnet publish "ifficient-school.csproj" -c Release -o /app/publish

# Etapa 2: Configuração da imagem de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

# Definir o diretório de trabalho para a imagem de runtime
WORKDIR /app

# Copiar os arquivos da publicação para a imagem de runtime
COPY --from=build /app/publish .

# Copiar o arquivo CSV para o diretório correto no contêiner
COPY Data/students.csv /app/Data/students.csv

# Expor a porta 80 para acesso ao serviço
EXPOSE 80

# Comando para rodar a aplicação
ENTRYPOINT ["dotnet", "ifficient-school.dll"]
