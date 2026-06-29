VantaLore
VantaLore, RAG (Retrieval-Augmented Generation) yaklaşımıyla geliştirilmiş bir yapay zeka bilgi asistanı projesidir. Amaç, büyük dil modellerini (LLM) dış veri kaynaklarıyla besleyerek daha doğru, güncel ve bağlamsal cevaplar üretmektir.
---
🚀 Proje Amacı
Geleneksel LLM’lerin en büyük problemi olan:
Güncel bilgi eksikliği
Halüsinasyon (uydurma cevaplar)
Kaynak göstermeme
gibi sorunları azaltmak için VantaLore geliştirilmiştir.
RAG mimarisi sayesinde sistem:
Harici dokümanlardan bilgi çeker
Bu bilgileri vektör arama ile bulur
LLM’e bağlam olarak verir
Daha doğru cevap üretir
---
🧠 Mimari
VantaLore temel olarak şu bileşenlerden oluşur:
Frontend: Basit UI (isteğe bağlı)
API Layer: ASP.NET Core / Node.js (projene göre)
Embedding Service: Metinleri vektöre çevirme
Vector Store: Benzerlik araması (FAISS / pgvector / Pinecone)
LLM Provider: Ollama / OpenAI / başka model
RAG Pipeline:
Kullanıcı sorgusu alınır
Embedding oluşturulur
Vektör veritabanında en yakın içerikler bulunur
LLM’e context olarak verilir
Cevap üretilir
---
🛠️ Kullanılan Teknolojiler
.NET / ASP.NET Core
Ollama (local LLM)
Nomic Embedding Model (nomic-embed-text)
Vector Database (FAISS / pgvector)
Swagger (API dokümantasyonu)
FluentValidation
REST API mimarisi
---
⚙️ Kurulum
1. Repoyu klonla
```bash
git clone https://github.com/kullaniciadi/vantalore.git
cd vantalore
```
2. Backend’i çalıştır
```bash
dotnet restore
dotnet run
```
3. Ollama kurulu olmalı
```bash
ollama run llama3
```
Embedding model:
```bash
ollama pull nomic-embed-text
```
---
📡 API Örnekleri
Soru sorma
POST /api/ask
{
"question": "RAG nedir?"
}
Cevap
{
"answer": "RAG, LLM'lerin dış veri kaynaklarıyla desteklenmesini sağlayan mimaridir..."
}
---
📌 Özellikler
RAG tabanlı bilgi sistemi
Yerel veya bulut LLM desteği
Vektör tabanlı arama
Genişletilebilir mimari
Swagger destekli API
Modüler yapı
---
🧪 Geliştirme Aşaması
Web UI geliştirme
Doküman upload sistemi
Çoklu embedding modeli desteği
Chat memory
Role-based context sistemi
---
📷 Akış
User → API → Embedding → Vector DB → Context → LLM → Response
