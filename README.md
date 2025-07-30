# Görev Takip API

Basit bir .NET 6 Web API uygulaması. PostgreSQL üzerinde `tasktracking` veritabanı ve `tasks` tablosunu otomatik oluşturur, CRUD uç noktaları sunar ve Postman üzerinden manuel test imkânı sağlar.

---

## 📌 Özellikler

- **Katmanlı Mimari**  
  - **Data**: `DatabaseInitializer` ile DB ve tablo yönetimi  
  - **Repository/Service**: İş mantığının soyutlanması (`ITaskRepository`, `TaskService`)  
  - **Controller**: HTTP uç noktaları (`TasksController`)  
- **Otomatik Veritabanı & Tablo**  
  Uygulama ilk çalıştırıldığında gerekli veritabanı ve tablo yoksa otomatik yaratılır.  
- **CRUD Endpoint’leri**  
  - **GET** `/api/tasks` — Tüm görevleri listeler  
  - **POST** `/api/tasks` — Yeni görev ekler  
  - **PUT** `/api/tasks/{id}` — Mevcut görevi günceller  
  - **DELETE** `/api/tasks/{id}` — Görevi siler  
- **Basit Ön Yüz (Opsiyonel)**  
  - `wwwroot/index.html` + fetch tabanlı JavaScript ile görev ekleme ve listeleme  
- **HTTPS Desteği**  
  - Geliştirme sertifikası (`dotnet dev-certs https --trust`) ile şifreli iletişim  
- **Manuel Test**  
  - Postman veya `curl` komutları ile uç noktıları adım adım doğrulama  

---

## 🛠️ Gereksinimler

- **.NET 6 SDK**  
- **PostgreSQL 14+**  

---

## 🚀 Kurulum & Çalıştırma

1. **Projeyi klonlayın**  
   ```bash
   git clone https://github.com/ydbilgin/GorevTakipAPI.git
   cd GorevTakipAPI
