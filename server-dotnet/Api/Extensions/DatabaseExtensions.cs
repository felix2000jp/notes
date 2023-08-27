using Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Api.Extensions;

public static class DatabaseExtensions
{
    public static WebApplication MigrateDatabase(this WebApplication webApp)
    {
        using var scope = webApp.Services.CreateScope();
        using var appContext = scope.ServiceProvider.GetRequiredService<DataContext>();
        
        if (appContext.Database.GetPendingMigrations().Any())
        {
            appContext.Database.Migrate();
        }
        
        return webApp;
    }
}