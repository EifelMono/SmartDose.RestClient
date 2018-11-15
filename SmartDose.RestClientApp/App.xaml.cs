using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using SmartDose.Core;
using SmartDose.RestClientApp.Globals;
using SmartDose.WcfClient;

namespace SmartDose.RestClientApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            AppGlobals.Init();
            base.OnStartup(e);
        }
    }
}
