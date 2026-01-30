FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Sadece proje dosyalarýný kopyalayarak 'restore' yapýyoruz (Hýzlý build için)
# Eðer klasör adýn 'Bookstore' ise burayý ona göre düzelt!
COPY ["BookStore.Api/BookStore.Api.csproj", "BookStore.Api/"]
COPY ["BookStore.Web/BookStore.Web.csproj", "BookStore.Web/"]

RUN dotnet restore "BookStore.Api/BookStore.Api.csproj"

# Kalan tüm dosyalarý kopyala
COPY . .

# API projesini yayýnla
WORKDIR "/src/BookStore.Api"
RUN dotnet publish "BookStore.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

# Render için port ayarý
ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000

ENTRYPOINT ["dotnet", "BookStore.Api.dll"]