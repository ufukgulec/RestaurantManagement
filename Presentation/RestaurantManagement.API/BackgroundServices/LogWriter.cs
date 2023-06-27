namespace RestaurantManagement.API.BackgroundServices
{
    public class LogWriter : BackgroundService
    {
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine($"{nameof(LogWriter)} service started...");

            return base.StartAsync(cancellationToken);
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine($"{nameof(LogWriter)} service function started...");

            return Task.CompletedTask;
        }
        public override Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine($"{nameof(LogWriter)} service stopped...");

            return base.StopAsync(cancellationToken);
        }
    }
}
