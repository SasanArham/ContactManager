## Overview

Welcome to this learning repository! This project is a .NET 8 Web API designed using Clean Architecture principles. It aims to demonstrate best practices and tools in various areas of modern web development. Please note that this project is intended for learning purposes only and should not be considered a production-ready solution.

## Table of Contents

- [Technologies, Tools and Key Components](#technologies-used)
- [Project Structure](#project-structure)
- [Getting Started](#getting-started)

<a id="technologies-used"></a>
## Technologies, Tools and Key Components

### 💎 RabbitMQ, MassTransit and Integration Events

**RabbitMQ** is used as the message broker to facilitate communication between different parts of the system through message queues.

**MassTransit** is a .NET library that provides a framework for working with RabbitMQ and simplifies message handling.

**Integration Events** Integration events are used to communicate between different services or components within the system. They are published to RabbitMQ using MassTransit, allowing for asynchronous communication and decoupling of services.


### 💎 Mediatr

**Mediatr** is used to handle domain events and commands, promoting a decoupled architecture by separating the request/response logic from the actual business logic.

### 💎 Redis and Distributed Caching

**Redis** is utilized for distributed caching to improve the performance and scalability of the application by storing frequently accessed data in memory.

### 💎 Azure Blob Storage

**Azure Blob Storage** is employed for file management, allowing the application to store and retrieve its files in a cloud storage.

### 💎 Serilog

**Serilog** is used for logging, providing structured log data that helps in monitoring and troubleshooting the application which helps in tracking application behavior and diagnosing issues. Logs are configured to be written to various sinks, such as console, file, or external systems.

### 💎Database

Azure SQL Database serves as the primary data store for the application. It is used to persist application data, support queries, and ensure data integrity and security.

<a id="project-structure"></a>
## Project Structure

The project follows the Clean Architecture principles, dividing the solution into several distinct layers:

- **Domain**: Contains business logic, domain entities, and interfaces.
- **Application**: Houses application services, commands, and queries.
- **Infrastructure**: Implements the interfaces and hanle technical concerns such as persistence, caching, ... 
- **Repository**: Another infrastructure layer solly for implementing the repository interfaces defined in Doamin
- **WebAPI**: The web API layer that exposes endpoints for client interactions.
- **Messages**: Contains integration messages of the project

<a id="getting-started"></a>
## Getting Started

To get started with this project, tou can use one of these options:

1. **Docker compose**:
   If you prefer a fast and simple approach config your settings in docker-compose.yaml file and run this command:
   ```bash
   docker compose up -d 
2. **Kubernetes**:
  If you like to have the project up and running via Kubernetes orchestration, first set your own configs inside of secret files then run these commands:
  
   ```bash
   kubectl apply -f rabbit-pv-volume.yaml
   kubectl apply -f rabbit-pv-claim.yaml
   kubectl apply -f rabbit-deployment.yaml
   kubectl apply -f rabbit-service.yaml

   kubectl apply -f redis-pv-volume.yaml
   kubectl apply -f redis-pv-claim.yaml
   kubectl apply -f redis-deployment.yaml
   kubectl apply -f redis-service.yaml

   kubectl apply -f webapi-secret.yaml
   kubectl apply -f webapi-deployment.yaml
   kubectl apply -f webapi-service.yaml

   kubectl apply -f ingress.yaml
