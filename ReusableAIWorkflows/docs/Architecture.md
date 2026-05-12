# Architecture Document: Customer Registration Application

## 1. Solution Overview

### Business Objective
Enable internal users to register new customers and view existing customer records through a web-based interface with proper data validation and error handling.

### Primary Architecture Style
- **Layered Architecture** with Clean Architecture principles
- **Client-Server** model with REST API communication
- **Separation of Concerns** across presentation, business logic, and data access layers

### Core System Responsibilities
| Layer | Responsibility |
|-------|----------------|
| Presentation (Angular) | User interface, form handling, client-side validation, API communication |
| API (Web API) | Request routing, input validation, response formatting, CORS handling |
| Business Logic (Services) | Business rules, domain validation, orchestration |
| Data Access (EF Core) | Entity persistence, query execution, data mapping |

---

## 2. High-Level Architecture

```text
┌─────────────────────────────────────┐
│         CLIENT / BROWSER            │
└──────────────────┬──────────────────┘
                   │
                   ▼
┌─────────────────────────────────────┐
│        ANGULAR FRONTEND             │
│  ┌─────────────┐  ┌──────────────┐  │
│  │ Registration│  │ Customer List│  │
│  │    Form     │  │   Component  │  │
│  └─────────────┘  └──────────────┘  │
│  ┌─────────────────────────────────┐│
│  │       Customer Service          ││
│  └─────────────────────────────────┘│
└──────────────────┬──────────────────┘
                   │ HTTP / REST
                   ▼
┌─────────────────────────────────────┐
│         .NET WEB API                │
│  ┌─────────────────────────────────┐│
│  │      CustomerController         ││
│  │   POST /api/customers           ││
│  │   GET  /api/customers           ││
│  └─────────────────────────────────┘│
└──────────────────┬──────────────────┘
                   │
                   ▼
┌─────────────────────────────────────┐
│        BUSINESS LOGIC               │
│  ┌─────────────────────────────────┐│
│  │       CustomerService           ││
│  │  - RegisterCustomer()           ││
│  │  - GetAllCustomers()            ││
│  │  - EmailExists()                ││
│  └─────────────────────────────────┘│
└──────────────────┬──────────────────┘
                   │
                   ▼
┌─────────────────────────────────────┐
│        DATA ACCESS LAYER            │
│  ┌─────────────────────────────────┐│
│  │      CustomerDbContext          ││
│  │        (EF Core)                ││
│  └─────────────────────────────────┘│
└──────────────────┬──────────────────┘
                   │
                   ▼
┌─────────────────────────────────────┐
│      IN-MEMORY DATABASE             │
│         (EF Core Provider)          │
└─────────────────────────────────────┘
```

---

## 3. Component Breakdown

### Frontend Components (Angular)

| Component | Responsibility |
|-----------|----------------|
| `RegistrationFormComponent` | Customer registration form with reactive forms and inline validation |
| `CustomerListComponent` | Display list of registered customers with empty state handling |
| `CustomerService` | HTTP client service for API communication |
| `Customer` (interface) | TypeScript model for customer data |
| `AppRoutingModule` | Route configuration between views |

### Backend Components (.NET)

| Component | Responsibility |
|-----------|----------------|
| `CustomerController` | REST API endpoints, request/response handling |
| `CustomerService` | Business logic, validation rules, data orchestration |
| `Customer` (entity) | Domain entity with data annotations |
| `CustomerDbContext` | EF Core DbContext with in-memory configuration |
| `Program.cs` | DI registration, middleware configuration |

### Data Layer

| Component | Responsibility |
|-----------|----------------|
| `Customer` (table) | Id (PK), Name, EmailId (unique), MobileNumber |
| In-Memory Provider | Transient storage for development/demo |

---

## 4. Layer Responsibilities

### Presentation Layer (Angular)
- Render user interface components
- Handle user input and form state
- Perform client-side validation
- Display validation errors inline
- Communicate with backend via HTTP
- Handle loading states and error messages

### API Layer (.NET Web API)
- Expose RESTful endpoints
- Route incoming HTTP requests
- Perform request validation
- Transform DTOs to/from domain entities
- Return appropriate HTTP status codes
- Handle CORS for cross-origin requests

### Business Logic Layer (Services)
- Implement business rules
- Validate domain constraints (unique email)
- Orchestrate data operations
- Encapsulate domain-specific logic

### Data Access Layer (EF Core)
- Abstract database operations
- Manage entity persistence
- Execute queries
- Handle transactions

---

## 5. Communication Flow

### Register Customer Flow

```text
1. User fills registration form (Name, Email, Mobile)
2. Angular validates input (required fields, email format)
3. On submit, CustomerService.register() called
4. HTTP POST to /api/customers with customer data
5. CustomerController receives request
6. Server-side validation executes
7. CustomerService.RegisterCustomer() called
8. Email uniqueness check performed
9. If valid, customer saved to DbContext
10. 201 Created returned with customer data
11. Angular displays success message
12. Form resets for next entry
```

### View Customers Flow

```text
1. User navigates to customer list view
2. CustomerListComponent initializes
3. CustomerService.getAll() called
4. HTTP GET to /api/customers
5. CustomerController.GetAll() invoked
6. CustomerService.GetAllCustomers() retrieves data
7. DbContext queries in-memory store
8. Customer list returned as JSON
9. Angular renders customer table
10. Empty state shown if list is empty
```

---

## 6. API Contract Summary

### Endpoints

| Method | Endpoint | Description | Request | Response |
|--------|----------|-------------|---------|----------|
| POST | `/api/customers` | Register customer | CustomerDto | 201 + Customer / 400 / 409 |
| GET | `/api/customers` | List all customers | — | 200 + Customer[] |

### Request/Response Models

**CustomerDto (Request)**
```json
{
  "name": "string (required)",
  "emailId": "string (required, valid email)",
  "mobileNumber": "string (required, numeric, 10 digits)"
}
```

**Customer (Response)**
```json
{
  "id": "int (system-generated)",
  "name": "string",
  "emailId": "string",
  "mobileNumber": "string"
}
```

### Error Responses

| Status | Scenario |
|--------|----------|
| 400 Bad Request | Validation failure (missing/invalid fields) |
| 409 Conflict | Duplicate email address |
| 500 Internal Server Error | Unexpected server error (no details exposed) |

---

## 7. Security Considerations

| Area | Implementation |
|------|----------------|
| Input Validation | Validate at both Angular (reactive forms) and .NET (Data Annotations/FluentValidation) |
| Email Format | Strict regex validation to prevent malformed data |
| Error Responses | Generic messages; no stack traces, paths, or DB details exposed |
| HTTPS | Enforce TLS between frontend and API |
| ID Generation | System-generated IDs prevent client manipulation |
| Sanitization | Escape/sanitize inputs to prevent injection attacks |
| Access Control | Internal network restriction (auth deferred to v2) |

---

## 8. Scalability Considerations

| Aspect | Current State | Future Path |
|--------|---------------|-------------|
| Database | In-Memory (demo) | SQL Server / PostgreSQL |
| Data Persistence | Reset on restart | Persistent storage |
| Authentication | None (internal network) | JWT / OAuth 2.0 |
| Pagination | Not required (low volume) | Add for large datasets |
| Caching | Not implemented | Response caching for list |
| Load Balancing | Single instance | Stateless API enables horizontal scaling |

---

## 9. Assumptions

1. Application runs on a trusted internal network (no external access)
2. User volume is low; pagination not required for v1
3. Data loss on restart is acceptable for demo/development
4. Mobile number format: 10-digit numeric (no country code)
5. Single deployment instance sufficient
6. Modern browser support only (Angular requirements)
7. CORS configured to allow Angular dev server origin

---

## 10. Architecture Decision Records (ADRs)

### ADR-001: In-Memory Database

**Decision**: Use EF Core In-Memory provider instead of SQL Server

**Rationale**:
- Simplifies development setup
- No database installation required
- Sufficient for demo/POC scope
- Easy migration path to persistent DB

**Consequences**: Data resets on application restart

---

### ADR-002: Layered Architecture with Clean Architecture Principles

**Decision**: Separate concerns into distinct layers (Presentation, API, Business, Data)

**Rationale**:
- Maintainability through clear boundaries
- Testability via dependency injection
- Enterprise alignment
- Enables independent layer evolution

**Consequences**: Additional abstraction overhead; acceptable for maintainability gains

---

### ADR-003: REST API Communication

**Decision**: Use REST over alternatives (GraphQL, gRPC)

**Rationale**:
- Simple CRUD operations fit REST model
- Wide tooling support
- Team familiarity
- Angular HttpClient optimized for REST

**Consequences**: Multiple endpoints for complex queries (acceptable for this scope)

---

### ADR-004: Defer Authentication to v2

**Decision**: No authentication in v1

**Rationale**:
- Internal network assumption
- Reduces scope complexity
- Focus on core functionality first

**Consequences**: Must implement before external exposure

---

### ADR-005: Client-Side and Server-Side Validation

**Decision**: Implement validation at both layers

**Rationale**:
- UX: Immediate feedback via Angular
- Security: Server-side enforcement
- Defense in depth

**Consequences**: Validation logic maintained in two places (mitigated by clear rules)

---

## Appendix: File Structure

### Backend (.NET)
```
CustomerApi/
├── Controllers/
│   └── CustomerController.cs
├── Models/
│   └── Customer.cs
├── Data/
│   └── CustomerDbContext.cs
├── Services/
│   └── CustomerService.cs
└── Program.cs
```

### Frontend (Angular)
```
customer-app/
├── src/
│   ├── app/
│   │   ├── models/
│   │   │   └── customer.model.ts
│   │   ├── services/
│   │   │   └── customer.service.ts
│   │   ├── components/
│   │   │   ├── registration-form/
│   │   │   └── customer-list/
│   │   └── app-routing.module.ts
│   └── environments/
└── angular.json
```
