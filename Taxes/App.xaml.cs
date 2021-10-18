using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Serilog;
using System.Windows;
using Taxes.Services;

namespace Taxes
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {

            ILogger log = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.File(path: "./logs/.txt", restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information, rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true)
                .CreateLogger();

            var ioc = new WindsorContainer();

            ioc.Register(Component.For<ILogger>().Instance(log));

            ioc.Register(Component.For<IExchangeRatesService>().ImplementedBy<ExchangeRatesService>());
            ioc.Register(Component.For<ITaxesService>().ImplementedBy<TaxesService>());

            ioc.Register(Component.For<MainWindow>().ImplementedBy<MainWindow>());

            var window = ioc.Resolve<MainWindow>();
            window.Show();
        }
    }
}
