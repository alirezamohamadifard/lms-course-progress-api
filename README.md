# LMS Course Progress API

A backend API for a Learning Management System (LMS), built with ASP.NET Core. The project will support course management, student enrollment, lesson-progress tracking, and role-based access control.

## Planned technology stack

- .NET 10 / ASP.NET Core Web API
- C#
- SQL Server and Entity Framework Core
- JWT authentication and role-based authorization
- Redis caching
- Docker

## Architecture

The solution follows a layered architecture:

- `Lms.Api`: HTTP endpoints and application configuration.
- `Lms.Application`: use cases, DTOs, interfaces, and business logic.
- `Lms.Domain`: core entities and business rules.
- `Lms.Infrastructure`: database persistence, caching, and external implementations.

## Initial scope

- Create and publish courses and lessons.
- Enroll students in courses.
- Track a student's lesson completion and overall course progress.
- Provide role-based access for administrators and students.

## Status

Project scaffolding is complete. The domain model and persistence layer are the next implementation steps.
