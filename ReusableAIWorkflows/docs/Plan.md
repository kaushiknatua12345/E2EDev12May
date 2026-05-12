# Project Plan: Customer Registration Application

## TL;DR
Build a full-stack web application allowing internal users to register and view customers. Angular frontend communicates with .NET Web API backend using EF Core in-memory database.

---

## Phase 1: Backend Foundation

1. Create .NET Web API project with EF Core in-memory setup
2. Define `Customer` entity (Id, Name, EmailId, MobileNumber)
3. Create `CustomerDbContext` with in-memory configuration
4. Implement `CustomerService` (RegisterCustomer, GetAllCustomers, EmailExists)
5. Build `CustomerController` with `POST /api/customers` and `GET /api/customers`
6. Add API-level validation using Data Annotations or FluentValidation

## Phase 2: Backend Testing

7. Unit tests for `CustomerService` (valid data, duplicate email, list retrieval)
8. Integration tests for API endpoints (success, validation errors, conflicts)

## Phase 3: Frontend Foundation

9. Create Angular project with CLI, configure API URL
10. Define TypeScript `Customer` interface
11. Implement `CustomerService` with HttpClient
12. Build registration form with reactive forms + inline validation
13. Create customer list component with empty state handling
14. Configure routing between views

## Phase 4: Frontend Testing

15. Unit tests for form validation and submission flow
16. Unit tests for service with mocked HttpClient

## Phase 5: Integration & Polish

17. Connect frontend to backend, configure CORS
18. Align error handling (API codes → UI messages)
19. Security hardening (HTTPS, input sanitization, no stack traces)

---

## Relevant Files

### Backend (.NET)
| File | Purpose |
|------|---------|
| `Models/Customer.cs` | Customer entity definition |
| `Data/CustomerDbContext.cs` | EF Core DbContext with in-memory config |
| `Services/CustomerService.cs` | Business logic implementation |
| `Controllers/CustomerController.cs` | REST API endpoints |
| `Program.cs` | DI registration and app configuration |

### Frontend (Angular)
| File | Purpose |
|------|---------|
| `models/customer.model.ts` | TypeScript interface |
| `services/customer.service.ts` | HTTP client service |
| `components/registration-form/` | Customer registration form |
| `components/customer-list/` | Customer list display |
| `app-routing.module.ts` | Route definitions |

---

## Verification

### Automated
- Run backend unit tests: `dotnet test`
- Run frontend unit tests: `ng test`
- Verify API endpoints via Postman/curl

### Manual
- Register customer with valid data → success message
- Submit empty fields → inline validation errors
- Submit invalid email → specific error message
- Submit duplicate email → conflict error displayed
- View empty list → "No customers registered yet"

---

## Scope

### In Scope
- Customer registration (create)
- Customer list (read)
- Field validation (UI + API)
- In-memory data storage
- Error handling

### Out of Scope
- Edit/delete customers
- Bulk import/export
- Pagination
- Authentication (deferred to v2)
- Data persistence across restarts

---

## Open Questions

| # | Question | Recommendation |
|---|----------|----------------|
| 1 | Is authentication required? | Defer to v2; assume trusted network |
| 2 | Mobile number format? | Default to 10-digit numeric |
| 3 | Duplicate email handling? | Report explicitly with 409 Conflict |
| 4 | Pagination needed? | Not for v1 if low volume |
| 5 | Data persist across restarts? | No; expected for demo/dev |