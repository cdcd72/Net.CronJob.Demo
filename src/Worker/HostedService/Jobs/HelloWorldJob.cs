using Worker.HostedService.Config;

namespace Worker.HostedService.Jobs;

public class HelloWorldJob(ILogger<HelloWorldJob> logger, IScheduleConfig<HelloWorldJob> config) : CronJobService(config.CronExpression, config.TimeZoneInfo)
{
    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation($"{nameof(HelloWorldJob)} start. [Cron：{config.CronExpression}][Timezone：{config.TimeZoneInfo.DisplayName}]");
       
        await base.StartAsync(cancellationToken);
    }

    public override async Task DoWorkAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation($"{nameof(HelloWorldJob)} working. [DateTime：{TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, config.TimeZoneInfo)}]");
        
        await Task.CompletedTask;
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation($"{nameof(HelloWorldJob)} stop.");
        
        await base.StopAsync(cancellationToken);
    }
}