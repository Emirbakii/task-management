ANKA Task - Full-Stack Proje Yönetim Sistemi

Bu proje, görev ve proje yönetimini kolaylaştırmak amacıyla geliştirilmiş, modern web teknolojileri (Full-Stack) kullanılarak inşa edilmiş bir yönetim sistemidir. Katmanlı mimari prensiplerine uygun olarak tasarlanmış olup, güvenli kimlik doğrulama ve gerçek zamanlı istatistik takibi sunar.

🚀 Teknolojiler (Tech Stack)

Backend

Framework: .NET 10 (C#)
Veritabanı: SQLite & Entity Framework Core
Mimari: N-Tier (Controller-Service Pattern)
Güvenlik: JWT (JSON Web Token) & BCrypt Password Hashing
Frontend

Framework: React & TypeScript
Build Aracı: Vite
HTTP İstemcisi: Axios
Yönlendirme: React Router DOM
DevOps & CI/CD

Konteynerizasyon: Docker (Backend)
Otomasyon: GitHub Actions ile CI/CD Pipeline
✨ Temel Özellikler

Güvenli Kimlik Doğrulama: JWT tabanlı, şifreli kullanıcı girişi ve kaydı.
Proje ve Görev Yönetimi: Projeler ve bu projelere bağlı görevler için tam CRUD işlemleri.
Dinamik Dashboard: Veritabanından çekilen gerçek zamanlı proje ve görev istatistikleri.
CORS & Entegrasyon: Backend ve Frontend arasında tam uyumlu çapraz origin iletişimi.
🛠️ Kurulum ve Çalıştırma

Projeyi yerel ortamınızda çalıştırmak için aşağıdaki adımları izleyebilirsiniz.

1. Ön Koşullar

.NET 10 SDK
Node.js (v16 veya üzeri)
Git
2. Backend'i Çalıştırma

# Backend klasörüne gidin
cd ProjeYonetimiAPI

# Gerekli paketleri yükleyin ve projeyi çalıştırın
dotnet restore
dotnet run
