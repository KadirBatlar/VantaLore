# VantaLore

VantaLore, RAG (Retrieval-Augmented Generation) tabanlı bir AI sistemidir. Amaç; yerel veri + embedding + LLM kombinasyonu ile daha doğru, bağlama duyarlı cevaplar üretmektir.

---

## 🚀 Özellikler

- RAG mimarisi ile doküman bazlı cevap üretimi
- Yerel embedding modeli desteği (Ollama ile)
- API + frontend ayrık yapı
- Genişletilebilir backend mimarisi
- Lokal çalışabilen AI pipeline

---

## 🧠 Mimari

Sistem temel olarak 3 parçadan oluşur:

- Frontend → Kullanıcı arayüzü  
- Backend API → Sorgu işleme ve orchestration  
- RAG Layer:
  - Embedding üretimi (nomic-embed-text-v2-moe)
  - Vector search
  - Context injection  
- LLM (Ollama) → Yanıt üretimi

---

## ⚙️ Gereksinimler

- .NET (backend için)
- Node.js (frontend için)
- Ollama kurulu olmalı

### Model:
- nomic-embed-text-v2-moe
- (opsiyonel) llama3 / mistral

---

## 🔧 Kurulum

### 1. Ollama Kurulumu

Ollama servisinin çalıştığından emin ol:

http://localhost:11434

Model indir:
```
ollama pull nomic-embed-text-v2-moe
```

---

### 2. Backend Çalıştırma

Solution root klasöründe:

```
dotnet restore
dotnet build
dotnet run
```

Backend:
http://localhost:5042

---

### 3. Frontend Çalıştırma

Frontend klasörüne gir:

```
npm install
npm run dev
```

Frontend genelde:
http://localhost:5173

---

## 🔗 Konfigürasyon

appsettings.json:

```
"Ollama": {
  "BaseUrl": "http://localhost:11434",
  "EmbeddingModel": "nomic-embed-text-v2-moe"
}
```

---

## 📦 API Endpoints

- POST /api/chat → kullanıcı mesajı
- POST /api/ingest → doküman ekleme
- GET /api/search → vector search test

---

## 🧪 RAG Akışı

1. Kullanıcı prompt gönderir  
2. Embedding üretilir  
3. Vector DB’de similarity search yapılır  
4. Context çıkarılır  
5. Prompt + context LLM’e gönderilir  
6. Cevap döner  

---

## 🧱 Teknolojiler

- .NET Web API  
- Ollama  
- Vector Database  
- Embedding Models  
- React / Vite (frontend)  

---

## 🧯 Sorun Giderme

### Ollama çalışmıyor
- http://localhost:11434 açık mı kontrol et
- Model yüklü mü?

### CORS hatası
- Backend CORS ayarlarını kontrol et

### Embedding hatası
- Model adı doğru mu?
- nomic-embed-text-v2-moe yüklü mü?

---

## 🧭 Roadmap

- [ ] Hybrid search (BM25 + vector)
- [ ] Daha iyi chunking
- [ ] Memory system
- [ ] Chat history optimization
- [ ] Docker support

---

## 📌 Not

Bu proje aktif geliştirme aşamasındadır. Mimari ve endpoint’ler değişebilir.
