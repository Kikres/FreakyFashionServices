# Microservices Examination Project
This project is a microservices-based application demonstrating the use of Docker, Docker Compose, and database migrations.

## Installation
1. Start the services:
   Run the following command to start all microservices using Docker Compose:
   ```
    docker-compose -f .\docker-compose.yml -f .\docker-compose.override.yml up -d
   ```
2. Update the databases:
   Run update-database for each service to apply database migrations:
   - Stock.API
   - Catalog.API
   - Order.Data

Your microservices should now be up and running!
