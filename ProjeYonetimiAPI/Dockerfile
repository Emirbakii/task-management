# 1. Aşama: Build (Derleme) ortamı
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["ProjeYonetimiAPI.csproj", "./"]
RUN dotnet restore "ProjeYonetimiAPI.csproj"
COPY . .
RUN dotnet publish "ProjeYonetimiAPI.csproj" -c Release -o /app/publish

# 2. Aşama: Çalışma (Runtime) ortamı
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Uygulamayı başlat
ENTRYPOINT ["dotnet", "ProjeYonetimiAPI.dll"]