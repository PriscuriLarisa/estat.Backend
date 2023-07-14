using eStat.BLL.Jobs.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace eStat.BLL.Jobs
{
    public class NotificationScheduledService : IHostedService, IDisposable
    {
        private readonly ILogger _logger;
        private Timer? _timer;
        private readonly int _frequency = 1;
        private readonly INotificationSystemHub _hub;
        private readonly INotificationGenerator _notificationGenerator;

        public NotificationScheduledService(ILogger<NotificationScheduledService> logger, INotificationSystemHub hub, INotificationGenerator notificationGenerator)
        {
            _logger = logger;
            _hub = hub;
            this._notificationGenerator = notificationGenerator;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Service is starting.");
            _timer = new Timer(MyTask, null, TimeSpan.FromHours(1),
                TimeSpan.FromHours(_frequency));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public async void MyTask(object state)
        {
            //Dictionary<string, string> notifications = _notificationGenerator.GenerateNotificationForPriceChanges();
            //foreach(KeyValuePair<string, string> kvp in notifications)
            //{
            //    await _hub.NotifyUser(kvp.Key, kvp.Value);
            //}
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

    }
}