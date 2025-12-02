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
