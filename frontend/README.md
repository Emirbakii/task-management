
# ANKA Task - Full-Stack Proje Yönetim Sistemi

Bu proje, görev ve proje yönetimini kolaylaştırmak amacıyla geliştirilmiş, modern web teknolojileri (Full-Stack) kullanılarak inşa edilmiş bir yönetim sistemidir. Katmanlı mimari prensiplerine uygun olarak tasarlanmış olup, güvenli kimlik doğrulama ve gerçek zamanlı istatistik takibi sunar.

## 🚀 Teknolojiler (Tech Stack)

### Backend
* **Framework:** .NET 10 (C#)
* **Veritabanı:** SQLite & Entity Framework Core
* **Mimari:** N-Tier (Controller-Service Pattern)
* **Güvenlik:** JWT (JSON Web Token) & BCrypt Password Hashing

### Frontend
* **Framework:** React & TypeScript
* **Build Aracı:** Vite
* **HTTP İstemcisi:** Axios
* **Yönlendirme:** React Router DOM

### DevOps & CI/CD
* **Konteynerizasyon:** Docker (Backend)
* **Otomasyon:** GitHub Actions ile CI/CD Pipeline

## ✨ Temel Özellikler
* **Güvenli Kimlik Doğrulama:** JWT tabanlı, şifreli kullanıcı girişi ve kaydı.
* **Proje ve Görev Yönetimi:** Projeler ve bu projelere bağlı görevler için tam CRUD işlemleri.
* **Dinamik Dashboard:** Veritabanından çekilen gerçek zamanlı proje ve görev istatistikleri.
* **CORS & Entegrasyon:** Backend ve Frontend arasında tam uyumlu çapraz origin iletişimi.

## 🛠️ Kurulum ve Çalıştırma

Projeyi yerel ortamınızda çalıştırmak için aşağıdaki adımları izleyebilirsiniz.

### 1. Ön Koşullar
* [.NET 10 SDK](https://dotnet.microsoft.com/download)
* [Node.js](https://nodejs.org/) (v16 veya üzeri)
* Git

### 2. Backend'i Çalıştırma
```bash
# Backend klasörüne gidin
cd ProjeYonetimiAPI

# Gerekli paketleri yükleyin ve projeyi çalıştırın
dotnet restore
dotnet run



# React + TypeScript + Vite

This template provides a minimal setup to get React working in Vite with HMR and some Oxlint rules.

Currently, two official plugins are available:

- [@vitejs/plugin-react](https://github.com/vitejs/vite-plugin-react/blob/main/packages/plugin-react) uses [Oxc](https://oxc.rs)
- [@vitejs/plugin-react-swc](https://github.com/vitejs/vite-plugin-react/blob/main/packages/plugin-react-swc) uses [SWC](https://swc.rs/)

## React Compiler

The React Compiler is not enabled on this template because of its impact on dev & build performances. To add it, see [this documentation](https://react.dev/learn/react-compiler/installation).

## Expanding the Oxlint configuration

If you are developing a production application, we recommend enabling type-aware lint rules by installing `oxlint-tsgolint` and editing `.oxlintrc.json`:

```json
{
  "$schema": "./node_modules/oxlint/configuration_schema.json",
  "plugins": ["react", "typescript", "oxc"],
  "options": {
    "typeAware": true
  },
  "rules": {
    "react/rules-of-hooks": "error",
    "react/only-export-components": ["warn", { "allowConstantExport": true }]
  }
}
```

See the [Oxlint rules documentation](https://oxc.rs/docs/guide/usage/linter/rules) for the full list of rules and categories.
