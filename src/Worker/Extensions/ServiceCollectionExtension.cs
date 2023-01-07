using Worker.HostedService;
using Worker.HostedService.Config;

namespace Worker.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddCronJob<T>(this IServiceCollection services, Action<IScheduleConfig<T>> options) where T : CronJobService
    {
        if (options is null)
            throw new ArgumentNullException(nameof(options), "Please provide schedule configurations.");
        
        var config = new ScheduleConfig<T>();
        options.Invoke(config);
        
        if (string.IsNullOrWhiteSpace(config.CronExpression))
            throw new ArgumentNullException(nameof(ScheduleConfig<T>.CronExpression), "Empty cron expression is not allowed.");

        services.AddSingleton<IScheduleConfig<T>>(config);
        services.AddHostedService<T>();
        return services;
    }
}