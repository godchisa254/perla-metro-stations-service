# 🚇 Perla Metro Stations Service

The **Perla Metro Stations Service** is a backend service built with **ASP.NET Core Web API**.
It manages station data (create, update, delete, list) and exposes secure RESTful endpoints for use by the [**Main API**](https://github.com/bxnjadev/perla-metro-main-api) and other related services in the system.

This service follows a **clean architecture approach**, using:

* **Entity Framework Core (EF Core)** for data access.
* **DTOs** and **mappers** for API ↔️ database abstraction.
* **Soft delete** strategy for data integrity.
* **Role-based authorization**.

---

## Tech Stack

* **Language:** C# (.NET 9)
* **Framework:** ASP.NET Core Web API
* **ORM:** Entity Framework Core
* **Database:** MySQL (Railway)
* **Authentication:** JWT-based (via Main API)
* **Deployment:** Docker + Render (backend) / Railway (only DB)

---

## Project Structure

```
src/
 ├── Controller/         # API endpoints
 ├── Dto/                # Data Transfer Objects
 ├── Helpers/            # Query helpers, validation
 ├── Interface/          # Repository interfaces
 ├── Mapper/             # Extension methods for mapping Model ↔ DTO
 ├── Model/              # Entity models (EF Core)
 ├── Repository/         # Repository implementations
 └── Program.cs          # App entrypoint and configuration
```

---

## Setup & Installation

### 1. Clone the repository

```bash
git clone https://github.com/your-username/perla-metro-stations-service.git
cd perla-metro-stations-service
```

### 2. Configure environment variables

This service **does not commit `.env` or `appsettings.json`**.
Instead, set environment variables (locally or in your hosting provider):

Example for MySQL (Railway):

```
ConnectionStrings__DefaultConnection=Server=containers-us-west-123.railway.app;Port=6543;Database=dbname;User Id=username;Password=password;SslMode=Required;
```

### 3. Run locally

```bash
dotnet restore
dotnet build
dotnet run
```

API will be available at:

```
http://localhost:9090/api/stations
```

---

## Docker

### Build image

```bash
docker build -t perla-metro-stations-service .
```

### Run container

```bash
docker run -d -p 8080:8080 \
  -e ConnectionStrings__DefaultConnection="your-connection-string" \
  perla-metro-stations-service
```

---

## Deployment

### Render (API)

* Deploy using the provided `Dockerfile`
* Set environment variable in **Render Dashboard**:

  ```
  ConnectionStrings__DefaultConnection
  ```
* Ensure port binding: `ASPNETCORE_URLS=http://+:8080`

### Railway (Database)

* If API is on Render → use `MYSQL_PUBLIC_URL` with `SslMode=Required`
* If API is on Railway → use `mysql.railway.internal` (secure, private)

---

## Endpoints

* **GET** `/api/stations` → List all stations (Admin only, with pagination/sorting)
* **GET** `/api/stations/{id}` → Get station by ID
* **POST** `/api/stations` → Create new station
* **PUT** `/api/stations/{id}` → Update station
* **DELETE** `/api/stations/{id}` → Soft-delete station (Admin only)

---

## Notes

* Authorization is handled via **Main API** (JWT tokens).
* Soft delete is implemented via the `IsActive` flag.
* Designed to integrate with other metro services (Users, Main API, etc.).

---

## License

This project is proprietary software. All rights reserved.

---