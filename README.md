This is simple webApi project with .net. This is mini library just to explain some topics for students.

## Docker deploy

### 1. Build image locally

```bash
docker build -t your-dockerhub-username/webapi:latest .
```

### 2. Push image to registry

```bash
docker login
docker push your-dockerhub-username/webapi:latest
```

### 2.1 Multi-arch build for ARM server

For ARM servers, publish a multi-arch image from Windows PowerShell:

```powershell
.\scripts\docker-buildx-push.ps1 -ImageName your-dockerhub-username/webapi
```

This script publishes both:

- `linux/amd64`
- `linux/arm64`

You can verify the manifest with:

```powershell
docker buildx imagetools inspect your-dockerhub-username/webapi:latest
```

### 3. Run on server with pull only

Create `.env` on the server from `.env.example`, then run:

```bash
docker pull your-dockerhub-username/webapi:latest
docker compose -f docker-compose.server.yml up -d
```

### Notes

- App listens on port `8009` inside the container.
- PostgreSQL runs in the same compose stack and is reachable from the API as host `db`.
- Production settings should be passed through environment variables.
- Database migrations are applied automatically when the container starts.
- If needed, disable startup migrations with `APPLY_MIGRATIONS_ON_STARTUP=false`.
- Swagger can stay enabled in production with `ENABLE_SWAGGER=true`.
- Health check endpoint: `GET /health`
