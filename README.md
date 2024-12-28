# ThrivoHR API

## Overview

The **ThrivoHR API** is a backend service for managing human resources-related functionality, such as employee contracts, attendance, overtime, projects, and more. It is built using .NET 8, with a modular architecture based on Clean Architecture principles. The API supports versioning, authentication using JWT tokens, and integrates with a variety of services like face detection and image uploads.

## Features

- **Employee Management**: Create, update, and manage employee records and contracts.
- **Face Detection**: Use facial recognition for attendance and face registration.
- **Project and Task Management**: Create and manage projects, tasks, and track their progress.
- **Forms Management**: Handle employee forms such as absentee, application, and resignation forms.
- **Authentication & Authorization**: Secure the API with JWT-based authentication.
- **Swagger API Documentation**: Interactive API documentation for ease of use.

## Project Structure

The project is structured into several layers, ensuring separation of concerns:

- **API Layer**: Contains controllers and routes for handling HTTP requests and responses.
- **Application Layer**: Contains business logic, use cases, and services.
- **Domain Layer**: Contains core business entities and interfaces.
- **Infrastructure Layer**: Contains implementations for database access, caching, and external services.
  
## Technologies Used

- **.NET 8**: Framework for building the API.
- **JWT Authentication**: For securing endpoints.
- **Swashbuckle**: For generating Swagger API documentation.
- **Face Detection**: For attendance verification and face registration.
- **Entity Framework Core**: For ORM-based database access.
- **Redis**: For caching.
- **Docker**: For containerization.
- **Azure**: For cloud storage and database.

## Setup Instructions

### Prerequisites

Before setting up the project, ensure you have the following installed:

- **Docker**: For containerization.
- **.NET SDK 8**: For building and running the API.
- **Azure**: For database and cloud services.
  
### Local Setup

1. Clone the repository:

   ```bash
   git clone https://github.com/your-repository-url.git
   cd your-repository

2. Build and run the application using Docker:

   ```bash
   docker-compose up --build

3. The application will be available at http://localhost:8080.
4. Alternatively, if you prefer to run it locally without Docker, use the following command:
   ```bash
   dotnet run --project EXE201_BE_ThrivoHR.API

### Configuration
Modify the following files to suit your environment:
- `EXE201_BE_ThrivoHR.API/appsettings.Development.json:` Database connection strings and JWT settings.
- `EXE201_BE_ThrivoHR.API/appsettings.Production.json:` Production settings for live deployment.
### Environment Variables
Ensure the following environment variables are set:
- `CLOUDINARY_URL:` For image storage integration.

## Usage Examples

### Authentication

1. **Login**: POST request to `/api/v1/authenticate` with `employeeCode` and `password`.
   ```json
   {
    "employeeCode": "EMP123",
    "password": "password123"  
    }
2. **Refresh** Token: POST request to `/api/v1/authenticate/refresh` with a valid refresh token.

### Employee Management
1. **Create Employee Contract**: POST request to `/api/v1/contracts` with employee contract details.
   ```json
   {
    "employeeId": "EMP123",
    "contractType": "Full-Time",
    "startDate": "2024-01-01",
    "endDate": "2025-01-01"
    }
2. **Get Employee Contracts**: GET request to `/api/v1/contracts` to retrieve a list of employee contracts.

### Face Detection
1. **Detect Face**: POST request to `/api/v1/facedetection/detect` with an image file.

2. **Register Face**: POST request to `/api/v1/facedetection/register` with an image file for face registration.

### Running Tests
1. To run unit tests for the project, use the following command:
    ```bash
    dotnet test


### Docker Support
To deploy using Docker, follow the steps below:

1. **Build Docker Image**:

    ```bash
    docker build -t thrivohr:latest .
    
2. **Run Docker Compose**:
   ```bash
    docker-compose up -d
   
## API Documentation
The API is documented using Swagger. Once the application is running, navigate to http://localhost:8080/swagger to view the interactive API documentation.
