using Domain.Ports.Driving;
using Domain.UseCases;

namespace Service.DrivingAdapters.Configuration;

public static class UseCaseConfiguration
{
    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        services.AddTransient<IGetWinner, GetWinner>();
        services.AddTransient<IGetClosest, GetClosest>();
        services.AddTransient<IGetMissing, GetMissing>();

        return services;
    }
}
