using Worker.HostedService.Config;

namespace Worker.HostedService.Jobs;

public class HelloWorldJob : CronJobService
{
    private readonly ILogger<HelloWorldJob> _logger;
    private readonly IScheduleConfig<HelloWorldJob> _config;
    
    public HelloWorldJob(ILogger<HelloWorldJob> logger, IScheduleConfig<HelloWorldJob> config) : base(config.CronExpression, config.TimeZoneInfo)
    {
        _logger = logger;
        _config = config;
    }
    
    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation($"{nameof(HelloWorldJob)} start. [Cron：{_config.CronExpression}][Timezone：{_config.TimeZoneInfo.DisplayName}]");
       
        await base.StartAsync(cancellationToken);
    }

    public override async Task DoWorkAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation($"{nameof(HelloWorldJob)} working. [DateTime：{TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, _config.TimeZoneInfo)}]");
        
        await Task.CompletedTask;
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation($"{nameof(HelloWorldJob)} stop.");
        
        await base.StopAsync(cancellationToken);
    }
}