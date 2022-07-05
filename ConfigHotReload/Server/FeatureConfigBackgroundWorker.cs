using ConfigHotReload.Server.Hubs;
using ConfigHotReload.Shared;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;

namespace ConfigHotReload.Server
{
    public class FeatureConfigBackgroundWorker : BackgroundService
    {
        private readonly IHubContext<FeatureConfigHub> _hubContext;
        private readonly IOptionsMonitor<FeatureConfig> _optionsMonitor;

        public FeatureConfigBackgroundWorker(IHubContext<FeatureConfigHub> hubContext, IOptionsMonitor<FeatureConfig> optionsMonitor)
        {
            _hubContext = hubContext;
            _optionsMonitor = optionsMonitor;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var listener = _optionsMonitor.OnChange(f => _hubContext.Clients.All.SendAsync("GetFeatureConfig", f).Wait());

            while(stoppingToken.IsCancellationRequested == false)
            {
                await Task.Delay(TimeSpan.FromMinutes(100), stoppingToken);
            }

            listener.Dispose();
        }
    }
}
