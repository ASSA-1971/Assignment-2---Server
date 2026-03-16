# Bookstore - COMP 2084 Assignment 2

An ASP.NET Core MVC web app that manages authors and books using a SQL Server database.

## What it does

This is a bookstore management site where you can create, read, update, and delete both Authors and Books. Books belong to Authors (one-to-many relationship).

## How to run it

1. Clone the repo
2. Open in Visual Studio 2022
3. Update `appsettings.json` with your SQL Server connection string
4. Run migrations to create the database tables (see below)
5. Press F5 to run

## Database setup

Replace the placeholder values in `appsettings.json`:

```json
"DefaultConnection": "Server=YOUR_SERVER;Database=BookstoreDb;User Id=YOUR_USER;Password=YOUR_PASSWORD;TrustServerCertificate=True;"
```

Then run this in the Package Manager Console:

```
Add-Migration InitialCreate
Update-Database
```

This creates two tables: Authors and Books with a foreign key between them.

## Packages required

```
Microsoft.EntityFrameworkCore.SqlServer
Microsoft.EntityFrameworkCore.Tools
```

Install via NuGet Package Manager or the Package Manager Console.

## Live site

*(add your MonsterASP or Azure URL here once deployed)*
