# 1. SDK aþamasý: Kodun derlenmesi
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Proje dosyalarýný kopyala
COPY ["BookStore.Api/BookStore.Api.csproj", "BookStore.Api/"]
COPY ["BookStore.Web/BookStore.Web.csproj", "BookStore.Web/"]

# Baðýmlýlýklarý yükle
RUN dotnet restore "BookStore.Api/BookStore.Api.csproj"

# Tüm kodlarý kopyala ve yayýnla
COPY . .
WORKDIR "/src/BookStore.Api"
RUN dotnet publish "BookStore.Api.csproj" -c Release -o /app/publish

# 2. Çalýþtýrma aþamasý: Sadece çalýþma zamaný
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

# Render'ýn portunu ayarla (Render genelde 80 veya 10000 kullanýr)
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80

ENTRYPOINT ["dotnet", "BookStore.Api.dll"]