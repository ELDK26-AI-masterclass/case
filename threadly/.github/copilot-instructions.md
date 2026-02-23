# Copilot instructions

These instructions define how GitHub Copilot (and other AI coding assistants) must behave when generating code for this solution. All generated code must strictly follow the architecture, conventions, and constraints defined below.

---

## Solution context

You are building **Apply Project Studio**, an agentic platform for developers and project managers to build the knowledge and primitives required for agent-driven development. The platform provides a web-based workspace for developers and project managers to collect the knowledge and descriptions, agent primitives, and context engineering. The platform should always keep markdown specifications as the single source of truth for development tasks.

---

## Tech stack

### Frontend
- React 19.x
- TypeScript 5.x
- Vite 7.x
- TanStack Query 5.x
- React Router 7.x
- Shadcn/UI + Tailwind CSS
- React Hook Form + Zod

### Backend
- .NET 10.0
- .slnx solution format
- ASP.NET Core Minimal APIs 10.0
- .NET Aspire 13.1 (orchestration)
- Model Context Protocol (MCP) via `ModelContextProtocol` NuGet package
- xUnit (testing)

### Infrastructure / Platform
- Azure Database for PostgreSQL flexible server (primary database)
- Azure Cache for Redis (cache)
- Azure Blob Storage (storage)
- Azure Key Vault (secrets)
- Microsoft Entra ID (authentication)
- Azure Container Apps (container hosting)
- Azure Monitor + Application Insights (monitoring/telemetry)

---

## Project conventions

### Code style
#### General
- Prefer readability over cleverness.
- Favor small, focused methods and explicit naming.
- All I/O MUST be async; avoid blocking calls (`.Result`, `.Wait()`).
- All outputs MUST be deterministic and reproducible.
- Always use file-scoped namespaces.
- Ensure always to have blank line between block and subsequent statement.
- Consecutive braces must not have blank line between them.

#### Backend (.NET)
- Nullable reference types enabled.
- Async/await throughout with `CancellationToken` support.
- Use `record` types for DTOs and message contracts (commands/events).
- Use `System.Text.Json` (do not use Newtonsoft.Json).
- Do not introduce MediatR.
- Do not introduce AutoMapper; mappings MUST be explicit.
- Prefer Microsoft packages; ask for approval before adding third-party NuGet packages.
- Always ensure that files, classes, and methods are formatted correctly before completing.
- Handle and support "Blank line required between block and subsequent statement".

#### Frontend (TypeScript)
- Components in PascalCase.
- Functions and hooks in camelCase.
- Use Zod schemas for runtime validation of form inputs.
- Use TanStack Query for server state; avoid ad-hoc global state for remote data.

#### Naming conventions
| Area | Convention | Example |
|------|------------|---------|
| C# Classes | PascalCase | `ProcessService` |
| C# Methods | PascalCase | `GetProcessAsync` |
| C# Private fields | _camelCase | `_processRepository` |
| TypeScript Components | PascalCase | `ProcessCanvas.tsx` |
| TypeScript Functions | camelCase | `useProcesses` |
| Database Tables | snake_case | `bpmn_elements` |
| Database Columns | snake_case | `tenant_id` |
| API Endpoints | kebab-case | `/api/v1/processes/{id}` |

---

## Architecture overview

### High-level rules
- Web frontend communicates with backend via REST API only.
- No direct database access from the frontend.
- All projects are placed in `src` folder.
- Solution is placed in `src` folder.

### Backend architecture (Clean Architecture)
The backend follows SOLID principles and Clean Architecture with strict separation of concerns.

Layering rules (mandatory):
- Domain is pure: no ASP.NET, EF, or serialization attributes.
- Dependencies always point inward.
- API and Infrastructure depend on Application.
- Application depends on Domain.
- No circular dependencies.
- Keep DB layer / repositories as light as possible - business logic should be in Domain layer.

Project layout:
- `AppHost/` - Aspire orchestrator
- `ServiceDefaults/` - shared service configuration
- `API/` - REST API endpoints (Minimal APIs)
- `Application/` - application services, DTOs, interfaces
- `Domain/` - entities, value objects, domain logic
- `Infrastructure/` - DB access, repositories, external integrations
- `WebApp/` - frontend React project

Backend API rules:
- Minimal API endpoints MUST be thin.
- Endpoints validate input, call application services, return DTOs.
- Endpoints MUST NOT access repositories directly.

Persistence conventions:
- Entity configurations live in separate `IEntityTypeConfiguration<T>` classes.
- PostgreSQL types MUST NOT leak outside Infrastructure.

### MCP server
- MCP server MUST be implemented using the `ModelContextProtocol` NuGet package.
- Document available tools and inputs/outputs.
- Handle errors gracefully and avoid leaking internal details.

---

## Dependency injection

- Register all services explicitly in `Program.cs`.
- Use constructor injection only.
- Prefer `Scoped` lifetime for application services.
- Use `Singleton` only for stateless services.
- Avoid service locator patterns.

---

## Error handling

- Use custom domain exceptions for business rule violations.
- Implement a global exception handling middleware.
- Map exceptions to appropriate HTTP status codes.
- Log errors using structured logging.
- Never expose internal exceptions or stack traces to clients.

---

## REST API rules

- Follow REST conventions.
- Use proper HTTP verbs.
- Return meaningful status codes.
- Use DTOs for all request and response payloads.
- Never expose domain entities directly.

### API testing
- When creating or modifying API endpoints, always create or update corresponding `.http` files in `src/API/tests/http`.
- Ensure `.http` files cover main scenarios (success, failure, validation errors).

---

## Security and governance

- Never commit secrets.
- Use environment variables and/or Key Vault for sensitive data.
- Implement authentication/authorization.
- Validate all inputs.
- Apply rate limiting where appropriate.

---

## Testing strategy

- Backend uses xUnit.
- Avoid flaky tests; tests MUST be deterministic.

---

## Docker and deployment conventions

Docker:
- Use official Microsoft .NET images.
- Multi-stage builds for smaller images.
- Run as non-root user.
- Include health checks.
- Optimize layer caching.

docker-compose:
- Include PostgreSQL emulator for local development.
- Define networks and volumes.
- Use environment variables for configuration.
- Include any required local dependencies.

---

## When in doubt

If any architectural, tooling, or dependency-related decision is unclear:
- Ask before implementing.
- Do not make assumptions.
- Do not introduce new patterns or frameworks without approval.
