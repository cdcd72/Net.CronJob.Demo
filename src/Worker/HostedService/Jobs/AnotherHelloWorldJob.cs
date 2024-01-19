using Worker.HostedService.Config;

namespace Worker.HostedService.Jobs;

public class AnotherHelloWorldJob(ILogger<AnotherHelloWorldJob> logger, IScheduleConfig<AnotherHelloWorldJob> config) : CronJobService(config.CronExpression, config.TimeZoneInfo)
{
    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation($"{nameof(AnotherHelloWorldJob)} start. [Cron：{config.CronExpression}][Timezone：{config.TimeZoneInfo.DisplayName}]");
       
        await base.StartAsync(cancellationToken);
    }

    public override async Task DoWorkAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation($"{nameof(AnotherHelloWorldJob)} working. [DateTime：{TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, config.TimeZoneInfo)}]");
        
        await Task.CompletedTask;
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation($"{nameof(AnotherHelloWorldJob)} stop.");
        
        await base.StopAsync(cancellationToken);
    }
}