# Docker Setup Guide

This document provides detailed information about running the Mangopay QA API Challenge tests using Docker.

## Overview

The Docker setup includes:
- **Dockerfile**: Multi-stage build for test execution with Allure support
- **docker-compose.yml**: Orchestration file for easy test execution and report generation
- **.env.example**: Template for environment configuration
- **.dockerignore**: Optimized build context

## Architecture

### Dockerfile Stages

1. **Build Stage**
   - Based on `mcr.microsoft.com/dotnet/sdk:6.0`
   - Restores NuGet packages
   - Builds the test project in Release mode

2. **Test Stage**
   - Extends the build stage
   - Installs Java 17 JRE for Allure
   - Installs Allure command-line tool (v2.24.1)
   - Configured to run tests and generate reports

### Docker Compose Services

1. **mangopay-tests**
   - Main service for running tests
   - Accepts environment variables for API credentials
   - Mounts allure-results volume for report persistence

2. **allure-server** (Optional)
   - Web UI for Allure reports
   - Activated using `--profile report`
   - Accessible at http://localhost:5050
   - Auto-refreshes reports every 5 seconds

## Usage

### Basic Test Execution

```bash
# Build and run tests
docker-compose up --build

# Run in detached mode
docker-compose up -d

# View logs
docker-compose logs -f mangopay-tests

# Stop containers
docker-compose down
```

### With Allure Reports

```bash
# Run tests and start report server
docker-compose --profile report up --build

# Access reports at http://localhost:5050

# Stop all services
docker-compose down
```

### Environment Variables

Create a `.env` file from the template:
```bash
cp .env.example .env
```

Edit `.env` with your credentials:
```env
CLIENT_ID=your_client_id
API_KEY=your_api_key
```

Alternatively, set environment variables:
```bash
export CLIENT_ID=your_client_id
export API_KEY=your_api_key
docker-compose up
```

### Advanced Usage

#### Run Specific Tests
```bash
docker-compose run --rm mangopay-tests dotnet test --filter "FullyQualifiedName~UserTests"
```

#### Override Test Configuration
```bash
docker-compose run --rm \
  -e MangopayApi__CLIENT_ID=custom_id \
  -e MangopayApi__API_KEY=custom_key \
  mangopay-tests
```

#### Build Only
```bash
docker-compose build
```

#### Clean Up Everything
```bash
# Remove containers, networks, and volumes
docker-compose down -v

# Remove local allure results
rm -rf allure-results allure-reports
```

## Volume Mounts

- **allure-results**: Test execution results in Allure JSON format
- **allure-reports**: Generated HTML reports (when using allure-server)

Results are persisted on the host machine in `./allure-results` and `./allure-reports` directories.

## CI/CD Integration

### GitHub Actions Example

```yaml
name: Run Tests

on: [push, pull_request]

jobs:
  test:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v3

      - name: Create .env file
        run: |
          echo "CLIENT_ID=${{ secrets.MANGOPAY_CLIENT_ID }}" >> .env
          echo "API_KEY=${{ secrets.MANGOPAY_API_KEY }}" >> .env

      - name: Run tests
        run: docker-compose up --abort-on-container-exit

      - name: Upload Allure Results
        if: always()
        uses: actions/upload-artifact@v3
        with:
          name: allure-results
          path: allure-results/
```

## Troubleshooting

### Tests Fail with Authentication Error
- Verify your `.env` file has correct CLIENT_ID and API_KEY
- Ensure environment variables are being passed correctly

### Allure Server Not Starting
- Check if port 5050 is available
- Verify Docker has enough resources allocated

### Build Fails
- Ensure Docker has internet access to download dependencies
- Check Docker has sufficient disk space
- Try rebuilding without cache: `docker-compose build --no-cache`

### Permission Issues with Volumes
```bash
# Linux/Mac: Fix ownership of mounted directories
sudo chown -R $USER:$USER allure-results allure-reports
```

## Benefits of Docker Setup

1. **Consistency**: Same environment across all machines
2. **Isolation**: No need to install .NET SDK or Java locally
3. **Portability**: Easy to run on any platform with Docker
4. **CI/CD Ready**: Simple integration with pipelines
5. **Allure Integration**: Report generation included out of the box
6. **No Local Dependencies**: Everything runs in containers

## File Structure

```
.
├── Dockerfile                    # Multi-stage build definition
├── docker-compose.yml           # Service orchestration
├── .dockerignore               # Build context optimization
├── .env.example                # Environment template
└── DOCKER.md                   # This file
```

## Requirements

- Docker 20.10+
- Docker Compose 2.0+

## Security Notes

- Never commit `.env` file with real credentials
- Use Docker secrets for production environments
- API credentials are passed via environment variables only
- No credentials are baked into Docker images
