using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WinFormsNonBlockingAsync;

internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        var configBuilder = new ConfigurationBuilder();
        var startups = ApplicationStartup.GetStartups();

        foreach (var startup in startups)
        {
            startup.Configure(configBuilder);
        }

        var services = new ServiceCollection();

        foreach (var startup in startups)
        {
            startup.ConfigureServices(services);
        }

        services.AddSingleton<frmMain>();
        var config = configBuilder.Build();
        services.AddSingleton<IConfiguration>(config);

        var sp = services.BuildServiceProvider();
        var form = sp.GetRequiredService<frmMain>();
        Application.Run(form);
    }
}
