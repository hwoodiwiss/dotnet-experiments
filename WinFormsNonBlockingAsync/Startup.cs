using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsDi.Services;
using WinFormsNonBlockingAsync.Config;

namespace WinFormsDi
{
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
}
