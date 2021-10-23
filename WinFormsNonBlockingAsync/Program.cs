using Microsoft.Extensions.DependencyInjection;

namespace WinFormsDi
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            var services = new ServiceCollection();
            services.AddSingleton<frmMain>();
            foreach(var startup in ApplicationStartup.GetStartups())
            {
                startup.ConfigureServices(services);
            }

            var sp = services.BuildServiceProvider();

            var form = sp.GetRequiredService<frmMain>();
            Application.Run(form);
        }
    }
}