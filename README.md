# 🗂️ Task Management API

A RESTful Web API built with **ASP.NET Core** and **Entity Framework Core** for managing projects and their associated tasks.

## 🚀 Features

- Manage `Projects` and `Tasks`
- Associate multiple tasks with a single project
- Create, retrieve, update, and delete resources (CRUD)
- EF Core database integration with code-first migrations
- Clean architecture using DTOs and services

## 🛠️ Tech Stack

- ASP.NET Core 8 Web API
- Entity Framework Core (SQL Server)
- Swagger / Swashbuckle for API documentation
- LINQ and async/await for data access

## 📦 Endpoints Overview

### 📁 Projects

| Method | Endpoint              | Description                   |
|--------|-----------------------|-------------------------------|
| GET    | `/api/projects`       | Get all projects              |
| GET    | `/api/projects/{id}`  | Get a single project by ID    |
| POST   | `/api/projects`       | Create a new project          |
| PUT    | `/api/projects/{id}`  | Update an existing project    |
| DELETE | `/api/projects/{id}`  | Delete a project              |

### ✅ Tasks (per project)

| Method | Endpoint                           | Description                     |
|--------|------------------------------------|---------------------------------|
| GET    | `/api/projects/{id}/tasks`         | Get all tasks for a project     |
| POST   | `/api/projects/{id}/tasks`         | Add a task to a project         |
| PUT    | `/api/tasks/{id}`                  | Update a task                   |
| DELETE | `/api/tasks/{id}`                  | Delete a task                   |

## 🧱 Project Structure

/Controllers → API controllers
/Services → Business logic
/DTOs → Request/response models
/Models → EF Core entity models
/Data → AppDbContext
