using eBookStoreAPI.Application.ApiUtilities.Interfaces;

namespace eBookStoreAPI.Presentation.Extensions;

public static class DatabaseInitializationExtensions
{
    public static async Task InitializeDatabaseAsync(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var databaseInitializer = scope.ServiceProvider.GetRequiredService<IDatabaseInitializer>();
            await databaseInitializer.InitializeAsync();
        }
    }
}
