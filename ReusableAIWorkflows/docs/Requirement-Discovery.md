# Customer Registration Application Requirements

## User Stories

**US-01 — Register a New Customer**
As an internal user, I want to register a new customer by entering their details, so that the customer is stored in the system and can be retrieved later.

**US-02 — View Registered Customers**
As an internal user, I want to view a list of all registered customers, so that I can verify and manage customer records.

**US-03 — Validate Customer Input**
As an internal user, I want the system to validate the data I enter before submission, so that only accurate and complete customer records are saved.

---

## Acceptance Criteria

**US-01 — Register a New Customer**
- The registration form must include: Name, Email ID, Mobile Number
- All required fields must be filled before the form can be submitted
- On successful registration, the customer is stored and a success message is shown
- The user is able to register another customer after successful submission
- The system assigns a unique ID to each registered customer

**US-02 — View Registered Customers**
- The customer list displays: Id, Name, Email ID, Mobile Number
- The list updates to reflect newly registered customers
- If no customers are registered, an appropriate empty-state message is shown

**US-03 — Validate Customer Input**
- Inline validation messages appear when a required field is left empty
- Validation triggers both at UI layer (Angular) and API layer (.NET Web API)
- Invalid email format shows a specific error message
- Mobile Number must meet defined format rules before submission is allowed

---

## Validation Rules

| Field | Rule |
|---|---|
| Name | Required, non-empty string |
| Email ID | Required, must follow valid email format (e.g. `user@domain.com`) |
| Mobile Number | Required, must be numeric, must meet length constraints |
| Id | System-generated, not user-supplied |

---

## Business Rules

- Only internal users can register or view customers
- Each customer record must have a unique Email ID
- Customer data is persisted in an in-memory database (EF Core)
- Customer retrieval is only available via the REST API
- No bulk import or bulk delete is in scope for this version
- Customer records cannot be edited or deleted through the UI in this version

---

## Error Scenarios

| Scenario | Expected Behavior |
|---|---|
| Required field left blank | Show inline validation error; prevent submission |
| Invalid email format entered | Show "Please enter a valid email address" message |
| Invalid mobile number format | Show format-specific error message; prevent submission |
| Duplicate email submitted | API returns a conflict error; UI displays an appropriate message |
| API is unreachable | UI displays a generic "Service unavailable, please try again" error |
| Unexpected server error (500) | UI displays a user-friendly error; does not expose stack trace |
| Empty customer list on load | Display "No customers registered yet" message |

---

## Security Considerations

| Area | Consideration |
|---|---|
| Input Validation | Validate and sanitize all inputs at both UI and API layers to prevent injection attacks |
| Email Format Enforcement | Strict format validation prevents malformed data and reduces injection surface |
| No Sensitive Data Exposure | API error responses must not expose stack traces, internal paths, or database details |
| Internal Access Only | Application is intended for internal users; external access should be restricted by network/auth controls |
| API Endpoint Protection | REST API endpoints should be protected from unauthenticated external calls (auth mechanism to be confirmed) |
| Data in Transit | HTTPS should be enforced between Angular frontend and .NET API |
| Unique ID Generation | Customer IDs must be system-generated to prevent ID manipulation by users |

---

## Open Questions

1. Is authentication required for internal users, or is it assumed they are on a trusted internal network?
2. What is the exact format and length constraint for Mobile Number (e.g. 10 digits, specific country format)?
3. Should duplicate email registration be silently rejected or explicitly reported to the user?
4. Is there a pagination requirement for the customer list view?
5. Will the in-memory database persist across application restarts, or is data reset expected?