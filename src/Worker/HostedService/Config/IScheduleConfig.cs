namespace Worker.HostedService.Config;

public interface IScheduleConfig<T>
{
    string CronExpression { get; set; }
    
    TimeZoneInfo TimeZoneInfo { get; set; }
}