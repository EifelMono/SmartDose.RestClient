using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SmartDose.Core;
using SmartDose.Core.Extensions;
using SmartDose.RestClientApp.Globals;
using SmartDose.WcfClient;

namespace SmartDose.RestClientApp.Views
{
    /// <summary>
    /// Interaction logic for ViewConnection.xaml
    /// </summary>
    public partial class ViewConnections : UserControl
    {
        public ViewConnections()
        {
            InitializeComponent();
            DataContext = this;
  
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            propertyGridView.SelectedObject = ConfigurationData;
            propertyGridView.HelpVisible = false;
            propertyGridView.PropertyValueChanged += (s1, e1) =>
            {
                propertyGridView.Refresh();
            };
            propertyGridView.SelectedGridItemChanged += (s2, e2) =>
            {
                propertyGridView.Refresh();
            };
        }

        public string Version => $"Version {Assembly.GetExecutingAssembly().GetName().Version.ToString()}";
        public MenuItem RootMenuItem { get; set; } = new MenuItem();

        public ConfigurationData ConfigurationData { get => AppGlobals.Configuration.Data; }

        ICommand _commandOpenSwaggerV1 = null;
        public ICommand CommandOpenSwaggerV1
        {
            get => _commandOpenSwaggerV1 ?? (_commandOpenSwaggerV1 = new RelayCommand(o =>
            {
            }));
        }

        ICommand _commandOpenSwaggerV2 = null;
        public ICommand CommandOpenSwaggerV2
        {
            get => _commandOpenSwaggerV2 ?? (_commandOpenSwaggerV2 = new RelayCommand(o =>
            {
                try
                {
                    Process.Start(AppGlobals.Configuration.Data.UrlV2 + "/swagger/ui/index");
                }
                catch { }
            }));
        }

        ICommand _CommandBuildWcfClientsAssemblies = null;
        public ICommand CommandBuildWcfClientsAssemblies
        {
            get => _CommandBuildWcfClientsAssemblies ?? (_CommandBuildWcfClientsAssemblies = new RelayCommand(o =>
            {
                try
                {
                    SmartDoseWcfClientGlobals.BuildWcfClientsAssemblies(ConfigurationData.WcfClients);
                }
                catch { }
            }));
        }

        ICommand _CommandDotNetDownload = null;
        public ICommand CommandDotNetDownload
        {
            get => _CommandDotNetDownload ?? (_CommandDotNetDownload = new RelayCommand(o =>
            {
                try
                {
                    Process.Start("https://www.microsoft.com/net/download");
                }
                catch { }
            }));
        }

        ICommand _CommandDotNet = null;
        public ICommand CommandDotNet
        {
            get => _CommandDotNet ?? (_CommandDotNet = new RelayCommand(o =>
            {
                try
                {
                    Process.Start("http://dot.net");
                }
                catch { }
            }));
        }


        ICommand _commandSaveConfiguration = null;
        public ICommand CommandSaveConfiguration
        {
            get => _commandSaveConfiguration ?? (_commandSaveConfiguration = new RelayCommand(o =>
            {
                try
                {
                    RestClient.RestClientGlobals.UrlV1 = ConfigurationData.UrlV1;
                    RestClient.RestClientGlobals.UrlV2 = ConfigurationData.UrlV2;
                    RestClient.RestClientGlobals.UrlTimeSpan = ConfigurationData.UrlTimeSpan;
                    RestClient.RestClientGlobals.ClearUrls();
                    RemoveViews(RootMenuItem);
                    AppGlobals.Configuration.Save();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                void RemoveViews(MenuItem menuItem)
                {
                    if (menuItem != null)
                    {
                        if (menuItem.ModelsItem != null)
                            if (menuItem.ModelsItem.Value is ViewCruds viewCrud)
                                menuItem.ModelsItem.Value = null;
                        foreach (var m in menuItem.Items)
                            RemoveViews(m);
                    }
                };

            }));
        }

        ICommand _commandOpenFolder = null;
        public ICommand CommandOpenDataFolder
        {
            get => _commandOpenFolder ?? (_commandOpenFolder = new RelayCommand(o =>
            {
                try
                {
                    Process.Start(SmartDoseCoreGlobals.DataBinDirectory);
                }
                catch { }
            }));
        }


    }
}
