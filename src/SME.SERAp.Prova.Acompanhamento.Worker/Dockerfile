#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/SME.SERAp.Prova.Acompanhamento.Worker/SME.SERAp.Prova.Acompanhamento.Worker.csproj", "SME.SERAp.Prova.Acompanhamento.Worker/"]
COPY ["src/SME.SERAp.Prova.Acompanhamento.IoC/SME.SERAp.Prova.Acompanhamento.IoC.csproj", "SME.SERAp.Prova.Acompanhamento.IoC/"]
COPY ["src/SME.SERAp.Prova.Acompanhamento.Aplicacao/SME.SERAp.Prova.Acompanhamento.Aplicacao.csproj", "SME.SERAp.Prova.Acompanhamento.Aplicacao/"]
COPY ["src/SME.SERAp.Prova.Acompanhamento.Infra/SME.SERAp.Prova.Acompanhamento.Infra.csproj", "SME.SERAp.Prova.Acompanhamento.Infra/"]
COPY ["src/SME.SERAp.Prova.Acompanhamento.Dados/SME.SERAp.Prova.Acompanhamento.Dados.csproj", "SME.SERAp.Prova.Acompanhamento.Dados/"]
COPY ["src/SME.SERAp.Prova.Acompanhamento.Dominio/SME.SERAp.Prova.Acompanhamento.Dominio.csproj", "SME.SERAp.Prova.Acompanhamento.Dominio/"]
RUN dotnet restore "SME.SERAp.Prova.Acompanhamento.Worker/SME.SERAp.Prova.Acompanhamento.Worker.csproj"
COPY . .
WORKDIR "/src/src/SME.SERAp.Prova.Acompanhamento.Worker"
RUN dotnet build "SME.SERAp.Prova.Acompanhamento.Worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SME.SERAp.Prova.Acompanhamento.Worker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app

ENV TZ=America/Sao_Paulo

RUN apt-get update \
    && apt-get install -yq tzdata locales -y \
    && dpkg-reconfigure --frontend noninteractive tzdata \ 
	  && locale-gen en_US.UTF-8
COPY --from=publish /app/publish .

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SME.SERAp.Prova.Acompanhamento.Worker.dll"]