#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/DebitoCredito.Api/DebitoCredito.Api.csproj", "src/DebitoCredito.Api/"]
COPY ["src/DebitoCredito.Infraestructure/DebitoCredito.Infraestructure.csproj", "src/DebitoCredito.Infraestructure/"]
COPY ["src/DebitoCredito.Domain/DebitoCredito.Domain.csproj", "src/DebitoCredito.Domain/"]
COPY ["src/DebitoCredito.Application/DebitoCredito.Application.csproj", "src/DebitoCredito.Application/"]
RUN dotnet restore "src/DebitoCredito.Api/DebitoCredito.Api.csproj"
COPY . .
WORKDIR "/src/src/DebitoCredito.Api"
RUN dotnet build "DebitoCredito.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DebitoCredito.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DebitoCredito.Api.dll"]