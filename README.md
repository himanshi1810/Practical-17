# Practical-18

## Prerequisites

### Connection String Configuration

Add the following connection string to your `web.config` file:

```xml
"ConnectionStrings": {
  "DefaultConnection": "Server=SF-CPU-0226\\SQLEXPRESS;Database=Practical-17;Trusted_Connection=True;TrustServerCertificate=true;"
},
```

> **Note:** Modify the connection string according to your SQL Server configuration if needed.

## Database Migration

For each project, you need to apply the migrations to create the database schema:

1. Open the Package Manager Console (Tools > NuGet Package Manager > Package Manager Console)
2. Select the appropriate project in the "Default project" dropdown
3. Run the following command:
   ```
   Update-Database
   ```
