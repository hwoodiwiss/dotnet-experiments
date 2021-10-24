using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WinFormsNonBlockingAsync.Config;
using WinFormsNonBlockingAsync.Services;

namespace WinFormsNonBlockingAsync;

public class Startup : ApplicationStartup
{
    public override void Configure(IConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.AddJsonFile("testConfig.json");
    }

    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddOptions<Configuration>().Configure<IConfiguration>((config, configRoot) =>
        {
            configRoot.Bind(config);
        });
        services.AddScoped<IWaitService, WaitService>();
    }
}
