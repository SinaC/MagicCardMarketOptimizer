using System;
using System.Configuration;
using System.Windows;
using MagicCardMarket.APIHelpers;

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

            Initialization init = new Initialization();
            init.InitializeTokens();

            base.OnStartup(e);
        }
    }
}
