# Shared Project Context

## Project Name
Customer Registration Application

## Business Goal
Allow internal users to register customers and view customer details

## Application Type
Full Stack Web Application

## Frontend
Angular

## Backend
.NET Web API

## Database
EF Core In-Memory

## Architecture
Frontend → REST API → Backend Service → Database

## Main Business Entity
Customer

## Customer Fields
- Id
- Name
- EmailId
- MobileNumber

## Core Features
- Register a new customer
- Validate required fields
- Validate email format
- View registered customers
- Store customers in in-memory database
- Retrieve customers through REST API

## Engineering Standards
- Use clean, readable code
- Prefer small focused files
- Keep business logic testable
- Use validation at UI and API layers
- Follow RESTful API standards
- Avoid unnecessary complexity
- Human review is required before accepting AI-generated code

## AI Usage Rules
- Do not generate the full application at once
- Generate only the requested file, method, or section
- Use concise outputs
- Do not invent requirements
- Ask for impact analysis before large changes
- Return code only when requested
- Keep token usage lean and intentional