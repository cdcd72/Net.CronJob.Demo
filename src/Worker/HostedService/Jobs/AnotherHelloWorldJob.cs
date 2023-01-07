using Worker.HostedService.Config;

namespace Worker.HostedService.Jobs;

public class AnotherHelloWorldJob : CronJobService
{
    private readonly ILogger<AnotherHelloWorldJob> _logger;
    private readonly IScheduleConfig<AnotherHelloWorldJob> _config;
    
    public AnotherHelloWorldJob(ILogger<AnotherHelloWorldJob> logger, IScheduleConfig<AnotherHelloWorldJob> config) : base(config.CronExpression, config.TimeZoneInfo)
    {
        _logger = logger;
        _config = config;
    }
    
    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation($"{nameof(AnotherHelloWorldJob)} start. [Cron：{_config.CronExpression}][Timezone：{_config.TimeZoneInfo.DisplayName}]");
       
        await base.StartAsync(cancellationToken);
    }

    public override async Task DoWorkAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation($"{nameof(AnotherHelloWorldJob)} working. [DateTime：{TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, _config.TimeZoneInfo)}]");
        
        await Task.CompletedTask;
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation($"{nameof(AnotherHelloWorldJob)} stop.");
        
        await base.StopAsync(cancellationToken);
    }
}