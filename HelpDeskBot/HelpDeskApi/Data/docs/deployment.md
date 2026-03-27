# Deployment

This document describes how our services are built, published, and deployed to production.

## Overview

We deploy to a Linux VPS. Services run as systemd units. There is no Kubernetes or container orchestration — deployments are straightforward file transfers and service restarts.

## Build and Publish

To produce a self-contained release build:

```
dotnet publish -c Release -r linux-x64 --self-contained false -o ./publish
```

Copy the output to the server:

```
scp -r ./publish/* deploy@your-server:/opt/your-service/
```

## Service Management

Each service runs as a systemd unit. To restart a service after deploying new files:

```
sudo systemctl restart your-service.service
```

To check if it started correctly:

```
sudo systemctl status your-service.service
sudo journalctl -u your-service.service -n 50
```

## Environment Variables

Secrets and environment-specific configuration are stored in the systemd service file as `Environment=` entries. Do not store secrets in `appsettings.json` — those are committed to the repository.

To edit the service file:

```
sudo systemctl edit --full your-service.service
```

Reload systemd after editing:

```
sudo systemctl daemon-reload
```

## Rollback

We do not have automated rollback. Before deploying, keep a copy of the previous build on the server. If a deployment causes issues, replace the files with the backup and restart the service.

## Who to Contact

If a deployment fails or a service is down in production, contact the team lead or the on-call developer. Do not attempt to fix production issues without informing the team first.
