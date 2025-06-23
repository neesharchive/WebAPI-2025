# GuestHouse Booking System - Full Stack App (WebAPI_2025)

This is a full-stack web application for managing guest house bookings, built with:

- ASP.NET Core Web API (backend)
- Angular (frontend)
- SQL Server (via Entity Framework Core)

The system supports:
- User and admin login
- Room and bed management
- Bookings with check-in/check-out
- Email-based password resets

---

## Quick Start

1. Clone or unzip the project
2. Ensure `appsettings.json` has valid database and email config
3. Run the backend project using `dotnet run`
4. Navigate to the Angular frontend folder, then run:
   npm install
   ng serve

---

## Default Accounts

Admin:
  Username: admin
  Password: admin123

User:
  Username: user
  Password: user123

These accounts are seeded via `AppDbContext.cs` > `OnModelCreating()` method. You can change them there if needed.

---

## Email Setup Notes

- Password reset emails are sent using `_emailService.SendEmailInBackground(...)`
- Update SMTP credentials and sender email in:
  - `appsettings.json`
  - `EmailService.cs`
---

## Folder Structure Note

- The Angular frontend folder has been copied inside the backend project temporarily to make zip/export easier.
- However, the actual working frontend lives in a separate location.
- If cloning from GitHub, separate frontend and backend properly before running.

---

## Features

- Secure authentication using hashed passwords
- JWT tokens with role-based claims
- Password reset via secure email token
- Role-based dashboards for Admin/User
- Guesthouse, room, bed, and booking management

---

## Before Going Live

- [ ] Replace all personal email addresses with generic/project emails
- [ ] Configure production `appsettings.json` (SMTP, DB, etc.)
- [ ] Separate frontend and backend folders before deploying
- [ ] Disable developer exception page in `Program.cs`

---

## Tech Stack

- Backend: ASP.NET Core 8, Entity Framework Core
- Frontend: Angular 16+, Angular Material
- DB: SQL Server
- Auth: JWT, Identity PasswordHasher
- Email: MailKit + SMTP
