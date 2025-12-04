Step 1 — Create folders (project structure)
Inside your project (right-click project → Add → New Folder) create:
Controllers
DTOs
Models
Data
Repository
Services
Interfaces
Middleware
Events
Mappings

Step 2 — Install NuGet packages
Open Package Manager Console (Tools → NuGet Package Manager → Package Manager Console) and run:

Install-Package Microsoft.EntityFrameworkCore
Install-Package Microsoft.EntityFrameworkCore.SqlServer
Install-Package Microsoft.EntityFrameworkCore.Tools
Install-Package AutoMapper.Extensions.Microsoft.DependencyInjection
Install-Package MassTransit


NOTE:

Put secrets in config (appsettings.json) — use env vars in prod

appsettings.json (replace values or use environment variables later):

{
  "Jwt": {
    "Key": "CHANGE_THIS_TO_A_VERY_LONG_SECRET_AT_LEAST_32_CHARS",
    "Issuer": "contactservice",
    "Audience": "contactservice",
    "ExpiresMinutes": 60
  },
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=ContactServiceDB;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}


Important: Do not commit real secrets. Use environment variables or a secret store for production.

MIDDLE WARE

We will implement 4 Middlewares, one by one:

Global Exception Handler

Request Logging Middleware

Action Logging Middleware (Attribute based)

Audit Log Middleware