using System;
using System.Configuration;
using System.Windows;

namespace MagicCardMarket.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            string logFilename = "App_" + Guid.NewGuid().ToString().Substring(0, 5) + ".log";
            Log.Log.Default.Initialize(ConfigurationManager.AppSettings["logpath"], logFilename);

            base.OnStartup(e);
        }
    }
}
