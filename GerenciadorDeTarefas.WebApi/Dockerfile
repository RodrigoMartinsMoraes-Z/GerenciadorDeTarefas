#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["GerenciadorDeTarefas.WebApi/GerenciadorDeTarefas.WebApi.csproj", "GerenciadorDeTarefas.WebApi/"]
COPY ["GerenciadorDeTarefas.Context/GerenciadorDeTarefas.Context.csproj", "GerenciadorDeTarefas.Context/"]
COPY ["GerenciadorDeTarefas.Domain/GerenciadorDeTarefas.Domain.csproj", "GerenciadorDeTarefas.Domain/"]
COPY ["GerenciadorDeTarefas.Common/GerenciadorDeTarefas.Common.csproj", "GerenciadorDeTarefas.Common/"]
RUN dotnet restore "GerenciadorDeTarefas.WebApi/GerenciadorDeTarefas.WebApi.csproj"
COPY . .
WORKDIR "/src/GerenciadorDeTarefas.WebApi"
RUN dotnet build "GerenciadorDeTarefas.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GerenciadorDeTarefas.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GerenciadorDeTarefas.WebApi.dll"]