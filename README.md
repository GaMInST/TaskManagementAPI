# Task Management API

## Overview
This is a simple .NET Core Web API for basic task management, built for assessment purposes.

It allows:
- Creating tasks
- Fetching tasks by ID
- Fetching tasks assigned to a specific user
- JWT-based Authentication
- Basic role-based Authorization (Admin / User)
- Swagger UI for easy testing

---

## Technologies Used
- ASP.NET Core 8
- Entity Framework Core
- **InMemory Database** (no external SQL Server needed)
- Swagger (OpenAPI)
- JWT Authentication

---

##How to Run the Project Locally

1. **Clone the Repository**
   ```bash
   git clone https://github.com/GaMInST/TaskManagementAPI.git
## Database Schema

The application uses three main tables:

- **Users**
- **Tasks**
- **TaskComments**

---

## ER Diagram

![TaskMangementAPI Diagram](https://github.com/user-attachments/assets/b998d6f3-7e7e-46e0-b71e-4de16dd17dee)

## 📑 Sample SQL Queries

### 1. Get all tasks assigned to a user
```sql
SELECT * FROM TaskItem WHERE UserId = @UserId;
```
### 1. 2. Get all comments on a task
```sql
SELECT * FROM TaskComment WHERE TaskId = @TaskId;
```

---

## Unit Testing

### Test Frameworks Used
- **NUnit**
- **Moq**
- **EF Core InMemory** for isolated data

### How to Run Tests (Visual Studio)
1. Open the solution in Visual Studio.
2. Open **Test Explorer** (Test → Test Explorer).
3. Click **Run All**.

### Test Cases Included
- CreateTask_ShouldReturnOkAndPersist_WhenValid  
- GetTask_ShouldReturnOk_WhenExists  
- GetTask_ShouldReturnNotFound_WhenMissing  
- GetTasksByUser_ShouldReturnOnlyThatUserTasks  




