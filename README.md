# Auvia — Backend API

.NET 8 Web API for the Auvia AI-Powered Clinical Consultation Workflow.  
Built for the 10Pearls Full-Stack Assessment — Candidate: Mubashir Hussain.

---

## Tech Stack

| Concern | Choice |
|---------|--------|
| Framework | .NET 8 Web API |
| Architecture | Clean Architecture (4 layers) |
| ORM | EF Core 8 |
| Database | SQL Server |
| API Docs | Swagger (Swashbuckle) |

---

## Project Structure

```
ClinicConsultationSystem/
├── ClinicConsultation.Api/            # Thin controllers + middleware
│   ├── Controllers/                   # 4 controllers (10 endpoints)
│   └── Middleware/                    # GlobalExceptionMiddleware
│
├── ClinicConsultation.Application/    # Business logic (no EF/framework deps)
│   ├── DTOs/                          # Input + output DTOs
│   ├── Exceptions/                    # NotFoundException, AppValidationException
│   ├── Interfaces/                    # Repository contracts + service interfaces
│   └── Services/                      # 4 application services
│
├── ClinicConsultation.Domain/         # Pure entities + enums (no dependencies)
│   ├── Entities/                      # Consultation, Message, Recommendation...
│   └── Enums/                         # ConsultationStatus, MessageRole
│
└── ClinicConsultation.Infrastructure/ # EF Core, SQL Server, repositories
    ├── Migrations/                    # 3 EF migrations
    ├── Persistence/                   # ApplicationDbContext
    └── Repositories/                  # GenericRepository + 2 specialised
```

---

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8)
- SQL Server (local instance or Docker)
- (Optional) [EF Core CLI tools](https://learn.microsoft.com/en-us/ef/core/cli/dotnet) — `dotnet tool install -g dotnet-ef`

---

## Setup & Run

### 1 — Configure the database connection

Edit `ClinicConsultation.Api/appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=ClinicConsultationDb;Trusted_Connection=True;TrustServerCertificate=True"
}
```

Replace `Server=.` with your SQL Server instance name if different (e.g. `Server=localhost\\SQLEXPRESS`).

### 2 — Apply EF Core migrations

```bash
cd ClinicConsultation.Api
dotnet ef database update
```

This creates the `ClinicConsultationDb` database with all 5 tables.

### 3 — Run the API

```bash
dotnet run --project ClinicConsultation.Api
```

The API starts at **http://localhost:5274** by default.  
Swagger UI is available at **http://localhost:5274/swagger**.

---

## API Endpoints

| Method | Route | Description |
|--------|-------|-------------|
| GET | /api/consultations | List all consultations |
| GET | /api/consultations/{id} | Get single consultation |
| POST | /api/consultations | Create consultation |
| PATCH | /api/consultations/{id}/status | Update status |
| GET | /api/consultations/{id}/messages | Get messages |
| POST | /api/consultations/{id}/messages | Add message |
| POST | /api/recommendation/{id}/generate | Generate recommendation |
| GET | /api/recommendation/{id}/recommendation | Get recommendation |
| GET | /api/appointments | List appointments |
| POST | /api/appointments | Create appointment |

---

## Error Handling

All errors flow through `GlobalExceptionMiddleware`:

| Exception | HTTP Status |
|-----------|-------------|
| `NotFoundException` | 404 |
| `AppValidationException` | 400 |
| Any other | 500 (stack trace logged, not exposed) |

---

## Assumptions

- No authentication — the API is open (JWT middleware is on the production roadmap)
- `RecommendationService.GenerateAsync()` runs a mock — the interface is defined for easy OpenAI swap-in
- Connection string uses Windows Authentication (Trusted_Connection) for local development
