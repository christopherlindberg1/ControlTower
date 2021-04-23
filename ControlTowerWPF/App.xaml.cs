using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AppFeatures;
using DataAccess;

namespace ControlTowerWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider _serviceProvider;

        public App()
        {
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(ServiceCollection services)
        {
            services.AddSingleton<MainWindow>();
            services.AddSingleton(new FlightLogger(
                new XmlFlightLogger(FilePaths.FlightLogFilePath)));
            services.AddSingleton<IFlightLogHandler, FlightLogHandler>();
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            MainWindow mainWindow = _serviceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }
    }
}
