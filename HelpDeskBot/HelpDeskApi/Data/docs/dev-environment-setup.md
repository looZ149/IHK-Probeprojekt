# Development Environment Setup

This guide walks you through setting up your local development environment from scratch.

## Prerequisites

Install the following tools before starting:

- **Git** — version control
- **.NET SDK 8** — backend services run on .NET 8
- **Node.js 20 LTS** — required for frontend tooling
- **Docker Desktop** — used to run local dependencies (database, message broker)
- **Visual Studio 2022** or **Rider** — recommended IDEs

## Initial Setup

1. Clone the main repository:
   ```
   git clone https://github.com/lug-trug/platform.git
   ```

2. Copy the example environment file and fill in the missing values (ask your team lead for secrets):
   ```
   cp .env.example .env
   ```

3. Start local infrastructure via Docker:
   ```
   docker compose up -d
   ```

4. Restore dependencies and run the backend:
   ```
   dotnet restore
   dotnet run --project src/Api
   ```

5. Install frontend dependencies and start the dev server:
   ```
   npm install
   npm run dev
   ```

## Database

We use PostgreSQL. The Docker Compose setup provisions a local instance automatically on port 5432. Migrations run on startup — you do not need to run them manually in development.

## Common Issues

- **Port conflicts:** If port 5432 or 5000 is in use, check for other running Docker containers.
- **Missing secrets:** The `.env.example` file has placeholders for all required variables. Missing ones will cause startup errors with a clear message.
- **Outdated SDK:** Always match the version in `global.json` at the repo root.
