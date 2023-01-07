using Worker.Extensions;
using Worker.HostedService.Jobs;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddCronJob<HelloWorldJob>(scheduleConfig =>
        {
            scheduleConfig.CronExpression = @"*/10 * * * * *"; // period of 10 seconds
            scheduleConfig.TimeZoneInfo = TimeZoneInfo.Local;
        });
        services.AddCronJob<AnotherHelloWorldJob>(scheduleConfig =>
        {
            scheduleConfig.CronExpression = @"*/20 * * * * *"; // period of 20 seconds
            scheduleConfig.TimeZoneInfo = TimeZoneInfo.Local;
        });
    })
    .Build();

host.Run();
