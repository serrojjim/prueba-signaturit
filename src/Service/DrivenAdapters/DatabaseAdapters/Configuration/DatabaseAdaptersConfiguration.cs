using Microsoft.EntityFrameworkCore;

namespace Service.DrivenAdapters.DatabaseAdapters.Configuration;

public static class DatabaseAdaptersConfiguration
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, string databaseConnection)
    {
       
        return services;
    }
}
