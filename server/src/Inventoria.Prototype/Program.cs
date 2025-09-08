using Inventoria.Infra.Data;

InventoriaSqLite inventoriaSqLite = new();

inventoriaSqLite.ApplyMigrations();

Console.WriteLine("Database migrations applied.");