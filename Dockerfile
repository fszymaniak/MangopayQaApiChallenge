# Build stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copy solution and project files
COPY MangopayQaApiChallenge.sln ./
COPY tests/MangopayQaApiChallenge.Tests.Api/MangopayQaApiChallenge.Tests.Api.csproj tests/MangopayQaApiChallenge.Tests.Api/

# Restore dependencies
RUN dotnet restore

# Copy all source files
COPY . .

# Build the project
WORKDIR /src/tests/MangopayQaApiChallenge.Tests.Api
RUN dotnet build -c Release --no-restore

# Test stage
FROM build AS test
WORKDIR /src

# Install Allure command-line tool (requires Java)
RUN apt-get update && \
    apt-get install -y openjdk-17-jre-headless wget && \
    wget -q https://github.com/allure-framework/allure2/releases/download/2.24.1/allure-2.24.1.tgz && \
    tar -zxf allure-2.24.1.tgz -C /opt/ && \
    ln -s /opt/allure-2.24.1/bin/allure /usr/bin/allure && \
    rm allure-2.24.1.tgz && \
    apt-get clean && \
    rm -rf /var/lib/apt/lists/*

# Set environment variable for Java
ENV JAVA_HOME=/usr/lib/jvm/java-17-openjdk-amd64

# Set working directory to test project
WORKDIR /src/tests/MangopayQaApiChallenge.Tests.Api

# Volume for allure results
VOLUME ["/src/tests/MangopayQaApiChallenge.Tests.Api/bin/Release/net6.0/allure-results"]

# Default command to run tests
CMD ["dotnet", "test", "-c", "Release", "--no-build", "--verbosity", "normal"]
