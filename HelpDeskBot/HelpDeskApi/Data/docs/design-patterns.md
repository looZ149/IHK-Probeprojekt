# Design Patterns

This document describes the architectural and design patterns we use consistently across the codebase. Understanding these will help you navigate and contribute to the project more quickly.

## Repository Pattern

We separate data access from business logic using the Repository pattern. Each entity has a corresponding repository interface and implementation.

- Business logic only talks to the repository interface — never to the database directly.
- This makes services easier to test (inject a mock repository) and easier to swap out data sources later.

## Dependency Injection

We rely on the built-in .NET DI container throughout. Services are registered in `Program.cs` and injected via constructors. Avoid using `new` to instantiate services — request them through the constructor instead.

- Use `AddSingleton` for stateless, shared services (e.g. HTTP clients, configuration wrappers).
- Use `AddScoped` for services with per-request state (e.g. database contexts).
- Use `AddTransient` for lightweight, stateless services that don't need to be shared.

## Service Layer

Business logic lives in service classes, not in controllers or API handlers. Controllers are thin — they validate input, call a service, and return a response. This keeps controllers readable and keeps logic testable.

## Result Pattern

Instead of throwing exceptions for expected failures (e.g. "user not found", "invalid input"), services return a result object that carries either a success value or an error. This makes error handling explicit at the call site.

## Options Pattern

Configuration is accessed via the `IOptions<T>` pattern — never via raw `IConfiguration`. Each logical group of settings has a corresponding strongly-typed options class registered in DI.
