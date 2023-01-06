using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SharedRepository;

namespace Services.Models
{
    public class HostedService : BackgroundService
    {
        private readonly ILogger<HostedService> _logger;
        private Timer _timer;
        IServiceProvider _serviceProvider;


        public HostedService(ILogger<HostedService> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service running.");
            _timer = new Timer(async o => await DeleteOldTokens(), null, TimeSpan.Zero,
                TimeSpan.FromSeconds(10));

            return Task.CompletedTask;
        }
        private async Task DeleteOldTokens()
        {
            try
            {
                var scope = _serviceProvider.CreateScope();
                var repo = scope.ServiceProvider.GetRequiredService<IUserTokenRepository>();
                await repo.DeleteExpiredTokens();
                return;
            }
            catch (Exception)
            {
                throw new Exception("Error while deleting tokens.");
            }

        }
    }
}
