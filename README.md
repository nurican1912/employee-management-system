# Employee Management System

A web-based employee management application built with ASP.NET Core MVC, featuring department and position-based organization.

## Tech Stack

- **Backend:** ASP.NET Core 9 MVC, Entity Framework Core 9
- **Database:** SQL Server Express
- **Frontend:** Bootstrap 5, Bootstrap Icons
- **Auth:** Cookie Authentication + BCrypt password hashing

## Features

- Secure user authentication (cookie-based)
- Full CRUD operations for employees
- Search by name/email, filter by department
- Department and position management
- Dashboard with live statistics (employee, department, position counts)
- Toast notifications and modal delete confirmations
- Clean service layer architecture (interface + implementation)

## Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- SQL Server Express

### 1. Clone the repository

```bash
git clone https://github.com/username/repo-name.git
cd repo-name/web_final
```

### 2. Create the database

Create a new database in SQL Server:

```sql
CREATE DATABASE EmployeeManagementDb;
```

Then run the setup script:

```bash
sqlcmd -S ".\SQLEXPRESS" -i "veritabanı.sql/mainquery.sql"
```

### 3. Update the connection string

Edit `appsettings.json` and replace the server name with your SQL Server instance:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.\\SQLEXPRESS;Database=EmployeeManagementDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

### 4. Run the application

```bash
dotnet run
```

### 5. Initialize passwords

Navigate to the following URL on first run only:

```
https://localhost:{PORT}/Account/ResetPasswords
```

## Default Credentials

| Username | Password |
|---|---|
| `admin` | `1234` |

## Project Structure

```
web_final/
├── Controllers/        # MVC Controllers
├── Models/             # Entity and ViewModel classes
├── Services/           # Business logic layer (Interface + Implementation)
├── Views/              # Razor view files
└── wwwroot/            # Static assets (CSS, JS)
```
