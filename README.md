# MUST Student Activities Portal

Student activities portal for MUST University built with a static frontend and a `.NET` backend API.  
The project helps students explore activities, clubs, competitions, events, and news, while giving admins a dashboard to manage portal content.

## Overview

This project is split into two main parts:

- `Frontend`: HTML, CSS, and JavaScript pages for the public website, authentication flow, club/activity pages, and admin dashboard UI.
- `Backend`: ASP.NET Core Web API with layered structure for authentication, CRUD operations, registrations, and database access.

## Main Features

- Student sign up, login, and OTP verification
- Browse activities, clubs, competitions, events, and news
- Register for activities, events, competitions, and clubs
- Admin dashboard for managing:
  - Events
  - News
  - Menu items
  - Contact messages
  - Slider items
  - Activity categories
  - Activities
  - Clubs
  - Competitions
- JWT-based authentication
- Swagger UI for API testing

## Tech Stack

### Frontend

- HTML5
- CSS3
- JavaScript
- Fetch API
- Bootstrap

### Backend

- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- ASP.NET Core Identity
- JWT Authentication
- Swagger / OpenAPI

## Project Structure

```text
Must/
├── Frontend/
│   ├── index.html
│   ├── signup.html
│   ├── login.html
│   ├── clubs.html
│   ├── registerClub.html
│   ├── admin/
│   │   ├── index.html
│   │   ├── admin.css
│   │   └── admin.js
│   ├── js/
│   │   ├── api.js
│   │   ├── login-api.js
│   │   ├── signup-api.js
│   │   └── index-api.js
│   └── img/
│
└── Backend/
    └── sourc/
        ├── Must/
        ├── Core/
        │   ├── Domain/
        │   ├── Services/
        │   └── Services Abstraction/
        ├── Infrastructure/
        │   ├── Persistence/
        │   └── Presantion/
        └── Shared/
```

## Backend Architecture

The backend follows a layered structure:

- `Core/Domain`: entities and core domain models
- `Core/Services Abstraction`: service interfaces
- `Core/Services`: business logic
- `Infrastructure/Persistence`: Entity Framework context, configurations, migrations, and service implementations
- `Infrastructure/Presantion`: API controllers
- `Shared`: DTOs shared across the application
- `Must`: API startup and application host

## API Base URL

The frontend is configured to use:

- Local: `http://localhost:5184`
- Production: `https://must.runasp.net`

This behavior is defined in:

- [Frontend/js/api.js](/d:/Must%20(final)/Must/Frontend/js/api.js:1)
- [Frontend/admin/admin.js](/d:/Must%20(final)/Must/Frontend/admin/admin.js:1)

## How To Run

### 1. Run the backend

Open the backend project:

```powershell
cd Backend\sourc\Must
dotnet restore
dotnet run
```

By default, the backend runs on:

- `http://localhost:5184`
- `https://localhost:7027`

Swagger should be available at:

- `http://localhost:5184`

### 2. Run the frontend

Open the `Frontend` folder using Live Server or any local static server.

Example with VS Code Live Server:

- Right-click `Frontend/index.html`
- Choose `Open with Live Server`

Example URL:

- `http://127.0.0.1:5500/Frontend/index.html`

## Configuration

The backend uses `appsettings.json` and `appsettings.Development.json` for:

- Database connection string
- JWT settings
- Email settings

Important:

- Do not commit real passwords, email credentials, or production secrets.
- Replace sensitive values with your own local configuration before deployment.
- Prefer using environment variables or user secrets for sensitive data.

Suggested config sections:

- `ConnectionStrings:DefaultConnection`
- `JWT:ValidAudience`
- `JWT:ValidIssuer`
- `JWT:Secret`
- `EmailSettings`

## Admin Access

The backend seeds an admin user in startup code.

Current seed logic is defined in:

- [Backend/sourc/Must/Program.cs](/d:/Must%20(final)/Must/Backend/sourc/Must/Program.cs:1)

For safety in a public repo:

- move admin credentials to configuration
- avoid hardcoding passwords in source code
- rotate any exposed credentials before deployment

## Available Modules

- Authentication
- Events management
- News management
- Clubs management
- Competitions management
- Activity categories
- Activities management
- Participant registrations
- Contact messages
- Slider management
- Dynamic menu management

## Notes

- The frontend consumes the backend through REST API calls using `fetch`.
- Some pages depend on a valid JWT token stored in `localStorage`.
- The admin dashboard checks whether the logged-in user has the `Admin` role.
- If the backend schema changes, migrations and database updates should be kept in sync.

## Future Improvements

- Add role-based access control with finer permissions
- Improve validation and error handling across forms
- Move secrets to environment variables
- Add automated tests
- Add deployment documentation
- Improve database migration workflow
- Add responsive and accessibility refinements

## Authors

Developed as a student activities portal project for MUST University.
