using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsNonBlockingAsync;

public abstract class ApplicationStartup
{
    public abstract void Configure(IConfigurationBuilder configurationBuilder);
    public abstract void ConfigureServices(IServiceCollection services);

    public static ApplicationStartup[] GetStartups()
    {
        var types = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.BaseType == typeof(ApplicationStartup));

        return types.Select(s => Activator.CreateInstance(s)).Where(w => w != null).Cast<ApplicationStartup>().ToArray();
    }
}
