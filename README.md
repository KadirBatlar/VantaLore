# VantaLore

VantaLore is a RAG (Retrieval-Augmented Generation) based AI system. Its goal is to generate more accurate and context-aware responses by combining local data, embeddings, and LLMs.

---

## 🚀 Features

- Document-based Q&A using RAG architecture
- Local embedding support (via Ollama)
- Separate API and frontend architecture
- Extensible backend design
- Fully local AI pipeline support

---

## 🧠 Architecture

The system consists of 3 main parts:

- Frontend → User interface
- Backend API → Query processing & orchestration
- RAG Layer:
  - Embedding generation (nomic-embed-text-v2-moe)
  - Vector search
  - Context injection
- LLM (Ollama) → Response generation

---

## ⚙️ Requirements

- .NET (for backend)
- Node.js (for frontend)
- Ollama installed and running

### Models:
- nomic-embed-text-v2-moe
- (optional) llama3 / mistral

---

## 🔧 Installation

### 1. Install Ollama

Make sure Ollama is running:

http://localhost:11434

Pull required model:

```
ollama pull nomic-embed-text-v2-moe
```

---

### 2. Run Backend

From the solution root:

```
dotnet restore
dotnet build
dotnet run
```

Backend runs at:

http://localhost:5042

---

### 3. Run Frontend

Go to frontend folder:

```
npm install
npm run dev
```

Frontend usually runs at:

http://localhost:5173

---

## 🔗 Configuration

Example `appsettings.json`:

```
"Ollama": {
  "BaseUrl": "http://localhost:11434",
  "EmbeddingModel": "nomic-embed-text-v2-moe"
}
```

---

## 📦 API Endpoints

- POST /api/chat → send user message
- POST /api/ingest → add documents for RAG indexing
- GET /api/search → vector search test endpoint

---

## 🧪 RAG Flow

1. User sends a prompt
2. Prompt is converted into embeddings
3. Vector similarity search is performed
4. Relevant context is retrieved
5. Prompt + context is sent to LLM
6. Final response is generated

---

## 🧱 Technologies Used

- .NET Web API
- Ollama
- Vector Database
- Embedding Models
- React / Vite (frontend)

---

## 🧯 Troubleshooting

### Ollama not working
- Check if http://localhost:11434 is running
- Ensure model is installed

### CORS errors
- Check backend CORS configuration

### Embedding errors
- Verify model name
- Ensure nomic-embed-text-v2-moe is installed

---

## 🧭 Roadmap

- [ ] Hybrid search (BM25 + vector)
- [ ] Improved chunking strategy
- [ ] Memory system per user
- [ ] Chat history optimization
- [ ] Docker support

---

## 📌 Note

This project is under active development. Architecture and endpoints may change over time.
```
