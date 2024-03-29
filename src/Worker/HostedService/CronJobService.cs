﻿using Cronos;

namespace Worker.HostedService;

public abstract class CronJobService : IHostedService, IDisposable
{
    private System.Timers.Timer _timer;
    private readonly CronExpression _expression;
    private readonly TimeZoneInfo _timeZoneInfo;

    protected CronJobService(string cronExpression, TimeZoneInfo timeZoneInfo)
    {
        _expression = CronExpression.Parse(cronExpression, CronFormat.IncludeSeconds);
        _timeZoneInfo = timeZoneInfo;
    }

    public virtual async Task StartAsync(CancellationToken cancellationToken) => await ScheduleJob(cancellationToken);

    protected virtual async Task ScheduleJob(CancellationToken cancellationToken)
    {
        var next = _expression.GetNextOccurrence(DateTimeOffset.Now, _timeZoneInfo);
        
        if (next.HasValue)
        {
            var delay = next.Value - DateTimeOffset.Now;
            
            if (delay.TotalMilliseconds <= 0) // prevent non-positive values from being passed into Timer
                await ScheduleJob(cancellationToken);
            
            _timer = new System.Timers.Timer(delay.TotalMilliseconds);
            _timer.Elapsed += async (_, _) =>
            {
                _timer.Dispose();  // reset and dispose timer
                _timer = null;

                if (!cancellationToken.IsCancellationRequested) 
                    await DoWorkAsync(cancellationToken);

                if (!cancellationToken.IsCancellationRequested) 
                    await ScheduleJob(cancellationToken); // reschedule next
            };
            _timer.Start();
        }
        
        await Task.CompletedTask;
    }

    public virtual async Task DoWorkAsync(CancellationToken cancellationToken) => await Task.Delay(5000, cancellationToken);

    public virtual async Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Stop();
        await Task.CompletedTask;
    }

    public virtual void Dispose() => _timer?.Dispose();
}