# Reusable AI Workflows

Reusable AI workflows help teams use GitHub Copilot in a consistent, scalable, and token-efficient way for enterprise development.

## What this repository is for

This folder contains reusable AI workflow templates that support different stages of software development, including:

- gathering project context
- turning requirements into user stories
- designing APIs
- building backend and frontend code
- generating tests
- reviewing code
- following a complete SDLC workflow

## Available workflow files

| File | What it does |
|---|---|
| `shared-context.md` | Stores project-specific context |
| `requirement-assistant.md` | Converts requirements into user stories |
| `api-design-assistant.md` | Helps design endpoints and DTOs |
| `backend-build-assistant.md` | Generates backend code |
| `frontend-build-assistant.md` | Generates frontend code |
| `test-assistant.md` | Creates test scenarios and test code |
| `review-assistant.md` | Reviews selected files or code |
| `workflow-checklist.md` | Guides the full software development workflow |

## How to use these templates

1. Update `shared-context.md` with your project details.
2. Choose the assistant file that matches the development stage you are working on.
3. Give the assistant a small, focused task.
4. Ask for a limited and specific output.
5. Review all AI-generated content carefully before using it.

## Simple formula

**Reusable AI Role**  
+ **Project Context**  
+ **Specific Task**  
= **Controlled AI Output**

## Example

Using `shared-context.md` and `backend-build-assistant.md`:

- Generate only `CustomerCreateDto.cs`
- Fields:
  - FullName
  - Email
  - PhoneNumber
  - City
- Include validation attributes
- Return only the code
