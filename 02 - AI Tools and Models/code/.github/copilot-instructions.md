# Copilot instructions

These instructions define how GitHub Copilot (and other AI coding assistants) must behave when generating code for this solution. All generated code must strictly follow the architecture, conventions, and constraints defined below.

---

## Solution context

You are building an **assessment tool** consisting of:

- **Frontend**: React + TypeScript (web app)
- **Backend**: .NET 10 (C#)
- **Communication**: REST API only (no direct DB access from frontend)

The backend follows **SOLID principles** and **Clean Architecture** with strict separation of concerns.

---

## Architecture overview

The backend is organized into the following layers:

### API layer
Responsibilities:
- HTTP controllers
- MCP server implementation
- Request/response handling
- Authentication & authorization
- Input validation

Rules:
- Controllers must be **thin**
- No business logic
- Delegate all work to application services
- Never access repositories directly

### Application layer
Responsibilities:
- Application services
- DTOs
- Interfaces for repositories and external dependencies
- Orchestration of domain logic

Rules:
- Contains no infrastructure code
- Uses dependency injection
- Explicit mapping between domain entities and DTOs
- Coordinates use cases

### Domain layer
Responsibilities:
- Entities
- Value objects
- Domain services
- Domain logic and invariants

Rules:
- **Pure domain**
- No framework, database, or serialization concerns
- No attributes from ASP.NET, EF, or Cosmos
- No dependency on any other layer

### Infrastructure layer
Responsibilities:
- Cosmos DB access
- Repository implementations
- External integrations
- Persistence models if needed

Rules:
- Implements interfaces defined in Application layer
- Contains all infrastructure-specific code
- No business logic beyond persistence concerns

---

## Clean Architecture rules (mandatory)

- Domain entities must remain pure
- Dependencies must always point inward
- Application layer depends on Domain
- API and Infrastructure depend on Application
- Never bypass layers
- Never introduce circular dependencies

---

## General rules

- **NO MediatR** – use direct service injection
- **NO AutoMapper** – write explicit mapping methods
- **NO Newtonsoft.Json** – use `System.Text.Json`
- **Prefer Microsoft packages**
- **Always ask before adding third-party NuGet packages**
- **Use built-in .NET features whenever possible**
- **Use ModelContextProtocol**
  - MCP server must be implemented using the `ModelContextProtocol` NuGet package
- **Always follow the editor.config rules and code style**

---

## Naming conventions

- Classes and methods: meaningful and descriptive
- Interfaces: `IServiceName`
- DTOs: `SomethingDto`
- Async methods: `MethodNameAsync`
- Private fields: `_camelCase`
- Constants: `PascalCase`
- Avoid abbreviations unless well-established

---

## Async and concurrency

- Always use async for I/O operations
- Return `Task` or `Task<T>`, never `void`
- Suffix async methods with `Async`
- Use `ConfigureAwait(false)` in library code
- Do not block on async code (`.Result`, `.Wait()`)

---

## Dependency injection

- Register **all services** explicitly in `Program.cs`
- Use constructor injection only
- Prefer `Scoped` lifetime for application services
- Use `Singleton` only for stateless services
- Avoid service locator patterns

---

## Error handling

- Use custom domain exceptions for business rule violations
- Implement a **global exception handling middleware**
- Map exceptions to appropriate HTTP status codes
- Log errors using structured logging
- Never expose internal exceptions or stack traces to clients

---

## Controllers

Controllers must:
- Validate input
- Call application services
- Return HTTP responses

Controllers must NOT:
- Contain business logic
- Access repositories
- Perform data mapping beyond request/response DTOs

---

## Repositories

- Use the repository pattern
- Define repository interfaces in the Application layer
- Implement repositories in Infrastructure
- Repositories must be async
- Do not leak Cosmos DB SDK types outside Infrastructure

---

## Mapping rules

- All mappings must be explicit
- No reflection-based or convention-based mapping
- Mapping logic belongs in:
  - Application services, or
  - Dedicated mapping helpers in Application layer

---

## REST API rules

- Follow REST conventions
- Use proper HTTP verbs
- Return meaningful status codes
- Use DTOs for all request and response payloads
- Never expose domain entities directly

## API Testing

- When creating or modifying API endpoints, **always** create or update corresponding `.http` files in `src/backend/ADS.API/tests/http`
- Ensure `.http` files cover main scenarios (success, failure, validation errors)

---

## Frontend interaction

- Backend is a pure REST API
- Frontend never accesses database or infrastructure directly
- All contracts are defined via DTOs
- Backend must remain frontend-agnostic

---

## Code quality expectations

- Favor readability over cleverness
- Keep methods small and focused
- Write testable code
- Follow SOLID principles at all times
- If uncertain, ask before generating code

---

## When in doubt

If any architectural, tooling, or dependency-related decision is unclear:
- **Ask before implementing**
- Do not make assumptions
- Do not introduce new patterns or frameworks without approval
