#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Order.Processor/Order.Processor.csproj", "Services/Order.Processor/"]
COPY ["Services/Order.Data/Order.Data.csproj", "Services/Order.Data/"]
RUN dotnet restore "Services/Order.Processor/Order.Processor.csproj"
COPY . .
WORKDIR "/src/Services/Order.Processor"
RUN dotnet build "Order.Processor.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Order.Processor.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Order.Processor.dll"]