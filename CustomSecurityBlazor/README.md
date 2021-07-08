# Custom Security for Radzen Blazor Applications

Radzen applications rely on ASP.NET Core Identity for user login and authentication.
ASP.NET Core Identity by default creates a few database tables (AspNetUsers, AspNetRoles etc.) via Entity Framework Core migrations.

In some cases a database comes with its own custom user management implemented - with custom tables for users and roles.

This sample application shows how to make ASP.NET Core Identity understand such cases and allow you to use Radzen with
your custom authentication and user management.

## Features

- The application uses a MSSQL database ([mssql-db.sql](mssql-db.sql) is the SQL CREATE script).
- Users are persisted in the `Users` table.
- Roles are persisted in the `Roles` table.
- The `UserRoles` table links users to roles (one use can have multiple roles).
- The user management and security pages (that Radzen generates automatically) work with the custom setup.

## Implementation

- Most of the implementation is in [CustomSecurity.cs](server/CustomSecurity.cs). You can adapt it for your own use case - check the comments in the source for further info.
- [SecurityService](server/Services/SecurityService.cs) is modified to use the custom tables (Users, Roles) and has been added to Radzen's code generation ignore list.
- The Migrations directory is also added to Radzen's code generation ignore list to avoid creating the default ASP.NET Core Identity tables.
