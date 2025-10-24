# Gorev Takip API

Basit bir .NET 6 Web API uygulamasidir. PostgreSQL uzerinde `tasktracking` veritabanini ve `tasks` tablosunu ilk calistirmada olusturur; katmanli mimariyle gorevleri yonetmek icin REST uc noktalari saglar.

---

## Ozellikler

- **Katmanli mimari**
  - `Domain`: temel entity tanimlari
  - `Persistence`: PostgreSQL baglantisi, tablo olusturma ve repository katmani
  - `Application`: DTO, servis ve arayuzler
  - `Web`: API katmani (`TasksController`) ve statik web arayuzu
- **Otomatik veritabani & tablo**
  - `DatabaseInitializer` gerekli veritabani ve tablo yoksa acilista olusturur
- **Gorev islemleri**
  - **GET** `/api/tasks` - tum gorevleri getirir
  - **POST** `/api/tasks` - yeni gorev olusturur
  - **PUT** `/api/tasks/{id}` - gorev basligini ve aciklamasini gunceller
  - **PUT** `/api/tasks/{id}/complete` - gorevi tamamlandi olarak isaretler
  - **DELETE** `/api/tasks/{id}` - gorevi siler
- **Statik arayuz (opsiyonel)**
  - `wwwroot/index.html` dosyasi fetch tabanli basit bir on yuz sunar

> Not: Guncelleme islemi yalnizca baslik ve aciklamayi degistirir; tamamlanma durumu icin `PUT /api/tasks/{id}/complete` uc noktasini kullanin.

---

## Gereksinimler

- .NET 6 SDK
- PostgreSQL 14 veya uzeri

---

## Kurulum ve Calistirma

1. Depoyu klonlayin
   ```bash
   git clone https://github.com/ydbilgin/GorevTakipAPI.git
   cd GorevTakipAPI
   ```
2. PostgreSQL baglanti ayarlarini `appsettings.json` dosyasinda guncelleyin
3. Uygulamayi baslatin
   ```bash
   dotnet run
   ```
4. API'yi test edin
   - `http://localhost:5000/api/tasks`
   - Statik arayuz icin: `http://localhost:5000`

---

## Postman / curl Ornekleri

- **Gorev listesi**
  ```bash
  curl http://localhost:5000/api/tasks
  ```
- **Yeni gorev olusturma**
  ```bash
  curl -X POST http://localhost:5000/api/tasks \
       -H "Content-Type: application/json" \
       -d "{\"title\":\"Rapor yaz\",\"description\":\"Haftalik rapor\"}"
  ```
- **Gorev guncelleme**
  ```bash
  curl -X PUT http://localhost:5000/api/tasks/1 \
       -H "Content-Type: application/json" \
       -d "{\"title\":\"Yeni baslik\",\"description\":\"Guncel aciklama\"}"
  ```
- **Gorev tamamlama**
  ```bash
  curl -X PUT http://localhost:5000/api/tasks/1/complete
  ```
- **Gorev silme**
  ```bash
  curl -X DELETE http://localhost:5000/api/tasks/1
  ```

---

## Katki

Pull request gondermeden once lutfen derlemenin temiz oldugundan emin olun ve gerekiyorsa yeni uc noktalar icin README'yi guncelleyin.
