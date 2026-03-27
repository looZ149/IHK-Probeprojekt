# Internal APIs

This document describes the internal services exposed as HTTP APIs within our infrastructure. These are not public — they are only accessible from within the VPN or from other backend services.

## Authentication

All internal APIs require an `X-Api-Key` header. API keys are stored in the team password manager (ask your team lead for access). Never hard-code keys in source code — use environment variables.

## Services

### User Service
- **Base URL:** `http://user-service/api`
- **Purpose:** Manages employee accounts, roles, and authentication tokens.
- **Key endpoints:**
  - `GET /users/{id}` — fetch a user by ID
  - `POST /users` — create a new user account
  - `DELETE /users/{id}` — deactivate an account

### Notification Service
- **Base URL:** `http://notification-service/api`
- **Purpose:** Sends emails and internal alerts. Do not send emails directly — always go through this service.
- **Key endpoints:**
  - `POST /notify/email` — send an email to one or more recipients
  - `POST /notify/slack` — post a message to a Slack channel

### File Storage Service
- **Base URL:** `http://storage-service/api`
- **Purpose:** Handles file uploads and downloads (documents, exports, attachments).
- **Key endpoints:**
  - `POST /files` — upload a file, returns a file ID
  - `GET /files/{id}` — download a file by ID
  - `DELETE /files/{id}` — remove a file

## Error Handling

All services return standard HTTP status codes. Errors include a JSON body with a `message` field. Always handle `401` (invalid or missing API key), `404` (resource not found), and `500` (server error) explicitly in your client code.
