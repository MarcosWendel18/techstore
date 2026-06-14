using TechStore.Models;
using TechStore.Data;

public static class DbInitializer

{
    public static void Seed(AppDbContext context)
    {
        if (!context.Usuarios.Any())
        {
            context.Usuarios.Add(new Usuario
            {
                Nome = "Administrador",
                Email = "admin@techstore.com",
                Senha = "123456",
                Administrador = true
            });

            context.SaveChanges();
        }
    }
}