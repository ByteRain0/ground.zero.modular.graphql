TODO describe:
1. Add new migrations command and aspire setup.
2. Decision to go with postgresql.
3. Usage of DbContext inside the handlers to omit one level of useless abstraction.
4. How to apply migrations in local development and on production via CI/CD and Migration service.
5. Model configurations.
6. Data seeding.
7. 



https://khalidabuhakmeh.com/add-ef-core-migrations-to-dotnet-aspire-solutions
dotnet ef migrations add UserSettings --project src/Modules/Manga/Manga.Service/Manga.Service.csproj --startup-project src/Host/App.Host/App.Host.csproj --context MangaDbContext