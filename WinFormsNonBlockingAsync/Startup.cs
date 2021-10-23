using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsDi.Services;

namespace WinFormsDi
{
    public class Startup : ApplicationStartup
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IWaitService, WaitService>();
        }
    }
}
