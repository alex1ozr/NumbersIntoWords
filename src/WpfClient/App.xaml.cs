using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Windows;
using AErmilov.NumbersIntoWords.Api.Client.Extensions;

namespace WpfClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public sealed partial class App : Application
    {
        private IServiceProvider? serviceProvider;
        private IConfiguration? configuration;

        public IServiceProvider ServiceProvider => serviceProvider
            ?? throw new ArgumentNullException(nameof(serviceProvider));

        public IConfiguration Configuration => configuration 
            ?? throw new ArgumentNullException(nameof(configuration));

        protected override void OnStartup(StartupEventArgs e)
        {
            SetupConfigurationAndServiceProvider();

            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        private void SetupConfigurationAndServiceProvider()
        {
            configuration = GetConfiguration();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection, Configuration);

            serviceProvider = serviceCollection.BuildServiceProvider();
        }

        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(configuration);
            services.AddTransient(typeof(MainWindow));
            services.AddNumbersIntoWordsHttpClient();
        }
        private static IConfiguration GetConfiguration()
            => new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
             .Build();
    }
}
