# RvcApp
The web application that simulates a robot moving.

### How to run:
1. Create `.env` file in the same directory as `docker-compose.yml`. The example of the list with environment variables:
   - DB_HOST=postgres
   - DB_PORT=5432
   - DB_NAME=rvc
   - DB_USER=postgres
   - DB_PASSWORD=postgres
2. Run docker-compose.yml: 
```bash
docker-compose up --build
```
Two docker containers should be started (rvc_webapp and postgres_db).
3. Send a http request to run a robot:
```bash
   curl -X POST http://localhost:5000/tibber-developer-test/enter-path -H "Content-Type: application/json" -d '{"start": {"x": 10, "y": 22}, "commands": [{"direction": "east", "steps": 2}]}' 
```

### How to shut down:
```bash
docker-compose down -v
```