# User Management API

A RESTful User Management API built using ASP.NET Core Web API.

The project demonstrates CRUD operations, input validation, error handling, logging middleware, and API documentation using Swagger.

## Features

- Create users
- Retrieve all users
- Retrieve a user by ID
- Update users
- Delete users
- Input validation
- Duplicate email validation
- Centralized exception handling
- Request logging middleware
- Swagger/OpenAPI documentation
- In-memory data storage

## Technologies Used

- C#
- ASP.NET Core Web API
- .NET 9
- REST API
- Swagger/OpenAPI
- Data Annotations
- Dependency Injection
- Custom Middleware

## Project Structure

```text
UserManagementAPI/
│
├── Controllers/
│   └── UsersController.cs
│
├── Middleware/
│   ├── ExceptionHandlingMiddleware.cs
│   └── RequestLoggingMiddleware.cs
│
├── Models/
│   └── User.cs
│
├── Services/
│   ├── IUserService.cs
│   └── UserService.cs
│
├── Properties/
│   └── launchSettings.json
│
├── appsettings.json
├── appsettings.Development.json
├── Program.cs
├── UserManagementAPI.csproj
└── README.md
