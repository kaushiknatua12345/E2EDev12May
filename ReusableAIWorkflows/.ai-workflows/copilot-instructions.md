# GitHub Copilot Instructions

## Project AI Usage Standards

Follow the reusable AI workflow files located in:

`.ai-workflows/`

## General Rules

- Use lean context
- Generate small focused outputs
- Follow the shared project context
- Do not generate unrelated files
- Do not invent business requirements
- Prefer secure, maintainable, and testable code
- Follow existing project structure and naming conventions
- Keep responses concise
- Return code only when requested

## Development Rules

- Follow clean code principles
- Keep business logic testable
- Validate input at appropriate layers
- Use dependency injection where applicable
- Avoid unnecessary abstractions
- Prefer readable code over clever code

## Review Rules

When reviewing code, focus on:
- Maintainability
- Security
- Validation
- Error handling
- Testability
- Performance only when relevant

## Testing Rules

When generating tests:
- Cover positive scenarios
- Cover negative scenarios
- Cover validation rules
- Avoid duplicate tests
- Focus on business behavior

## Token Discipline

Avoid:
- Large context loading
- Full project generation
- Repeated role recreation
- Unbounded outputs

Prefer:
- Focused prompts
- Constrained outputs
- Single-file generation
- Reusable workflow templates