namespace RestaurantManagement.API.BackgroundServices
{
    public class DateTimeLogWriter : IHostedService, IDisposable
    {
        private Timer _timer;

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine($"{nameof(DateTimeLogWriter)} service started...");

            _timer = new Timer(writeConsoleLog, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));

            return Task.CompletedTask;
        }
        private void writeConsoleLog(object? state)
        {
            Console.WriteLine($"Datetime → {DateTime.Now.ToLongTimeString()}");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            Console.WriteLine($"{nameof(DateTimeLogWriter)} service stopped...");

            return Task.CompletedTask;

        }

        public void Dispose()
        {
            _timer = null;
        }
    }
}
