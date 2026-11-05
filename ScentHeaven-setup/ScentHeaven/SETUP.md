# Project setup (step 1)

Requires the .NET 8 SDK (`dotnet --version` should start with 8).

## First run
```bash
cd ScentHeaven.Api
dotnet restore
dotnet tool install --global dotnet-ef      # once per machine, needed in step 2
dotnet run --launch-profile ScentHeaven.Api
```
- Swagger UI: https://localhost:7100/swagger
- Frontend: https://localhost:7100/  (should show "API status: ok")
- If HTTPS is not trusted yet: `dotnet dev-certs https --trust`

## Secrets (never commit them)
```bash
cd ScentHeaven.Api
dotnet user-secrets set "Jwt:Key" "<random string of 32+ characters>"
dotnet user-secrets set "Stripe:SecretKey" "sk_test_..."
dotnet user-secrets set "Stripe:WebhookSecret" "whsec_..."
```

## Folder layout
- `Controllers/` thin API controllers
- `Services/` business rules (cart, orders, stock, payment)
- `Models/` EF Core entities (from the ER diagram)
- `Dtos/` request/response shapes
- `Data/` AppDbContext, migrations, seeder
- `wwwroot/` HTML, CSS, JS frontend

## Git
```bash
git add . && git commit -m "Project setup: ASP.NET Core API, SQLite, static frontend"
```
