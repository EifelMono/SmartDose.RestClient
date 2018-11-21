using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SmartDose.Core;
using System.Linq;
using SmartDose.RestClientApp.Globals;
using SmartDose.WcfClient;
using static SmartDose.Core.SafeExecuter;
using System.ComponentModel;

namespace SmartDose.RestClientApp.Views
{
    /// <summary>
    /// Interaction logic for ViewConnection.xaml
    /// </summary>
    public partial class ViewConnections : UserControl, INotifyPropertyChanged
    {
        public ViewConnections()
        {
            InitializeComponent();
            DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

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

            propertyGridView.GotFocus += (s3, e3) =>
            {
                propertyGridView.Refresh();
            };
        }

        public string Version => $"Version {Assembly.GetExecutingAssembly().GetName().Version.ToString()}";
        public MenuItem RootMenuItem { get; set; } = new MenuItem();

        public Action RefreshMain { get; set; } = null;

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
                    WcfClientGlobals.BuildWcfClientsAssemblies(ConfigurationData.WcfClients);
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

        protected void WcfClientFlags(bool value, bool all = false, bool group = false, bool item = false, bool active = false, bool build = false)
        {
            var groupText = "";
            if (!all)
            {
                if (propertyGridView.SelectedGridItem?.Value is WcfItem wc)
                {
                    groupText = wc.Group;
                    if (item)
                    {
                        if (active)
                            wc.Active = value;
                        if (build)
                            wc.Build = value;
                        NotifyPropertyChanged(string.Empty);
                        propertyGridView.Refresh();
                        return;
                    }
                }
                if (string.IsNullOrEmpty(groupText))
                    return;
            }
            foreach (var wcfClient in ConfigurationData.WcfClients)
            {
                if (groupText == "" || groupText == wcfClient.Group)
                {
                    if (active)
                        wcfClient.Active = value;
                    if (build)
                        wcfClient.Build = value;
                }
            }
            NotifyPropertyChanged(string.Empty);
            propertyGridView.Refresh();
        }

        ICommand _CommandAllWcfClientsActiveOn = null;
        public ICommand CommandAllWcfClientsActiveOn
        {
            get => _CommandAllWcfClientsActiveOn ?? (_CommandAllWcfClientsActiveOn = new RelayCommand(o =>
            {
                WcfClientFlags(all: true, active: true, value: true);
            }));
        }

        ICommand _CommandAllWcfClientsActiveOff = null;
        public ICommand CommandAllWcfClientsActiveOff
        {
            get => _CommandAllWcfClientsActiveOff ?? (_CommandAllWcfClientsActiveOff = new RelayCommand(o =>
            {
                WcfClientFlags(all: true, active: true, value: false);
            }));
        }

        ICommand _CommandAllWcfClientsBuildOn = null;
        public ICommand CommandAllWcfClientsBuildOn
        {
            get => _CommandAllWcfClientsBuildOn ?? (_CommandAllWcfClientsBuildOn = new RelayCommand(o =>
            {
                WcfClientFlags(all: true, build: true, value: true);
            }));
        }

        ICommand _CommandAllWcfClientsBuildOff = null;
        public ICommand CommandAllWcfClientsBuildOff
        {
            get => _CommandAllWcfClientsBuildOff ?? (_CommandAllWcfClientsBuildOff = new RelayCommand(o =>
            {
                WcfClientFlags(all: true, build: true, value: false);
            }));
        }

        ICommand _CommandGroupWcfClientsActiveOn = null;
        public ICommand CommandGroupWcfClientsActiveOn
        {
            get => _CommandGroupWcfClientsActiveOn ?? (_CommandGroupWcfClientsActiveOn = new RelayCommand(o =>
            {
                WcfClientFlags(group: true, active: true, value: true);
            }));
        }

        ICommand _CommandGroupWcfClientsActiveOff = null;
        public ICommand CommandGroupWcfClientsActiveOff
        {
            get => _CommandGroupWcfClientsActiveOff ?? (_CommandGroupWcfClientsActiveOff = new RelayCommand(o =>
            {
                WcfClientFlags(group: true, active: true, value: false);
            }));
        }

        ICommand _CommandGroupWcfClientsBuildOn = null;
        public ICommand CommandGroupWcfClientsBuildOn
        {
            get => _CommandGroupWcfClientsBuildOn ?? (_CommandGroupWcfClientsBuildOn = new RelayCommand(o =>
            {
                WcfClientFlags(group: true, build: true, value: true);
            }));
        }

        ICommand _CommandGroupWcfClientsBuildOff = null;
        public ICommand CommandGroupWcfClientsBuildOff
        {
            get => _CommandGroupWcfClientsBuildOff ?? (_CommandGroupWcfClientsBuildOff = new RelayCommand(o =>
            {
                WcfClientFlags(group: true, build: true, value: false);
            }));
        }

        ICommand _CommandItemWcfClientsActiveOn = null;
        public ICommand CommandItemWcfClientsActiveOn
        {
            get => _CommandItemWcfClientsActiveOn ?? (_CommandItemWcfClientsActiveOn = new RelayCommand(o =>
            {
                WcfClientFlags(item: true, active: true, value: true);
            }));
        }

        ICommand _CommandItemWcfClientsActiveOff = null;
        public ICommand CommandItemWcfClientsActiveOff
        {
            get => _CommandItemWcfClientsActiveOff ?? (_CommandItemWcfClientsActiveOff = new RelayCommand(o =>
            {
                WcfClientFlags(item: true, active: true, value: false);
            }));
        }

        ICommand _CommandItemWcfClientsBuildOn = null;
        public ICommand CommandItemWcfClientsBuildOn
        {
            get => _CommandItemWcfClientsBuildOn ?? (_CommandItemWcfClientsBuildOn = new RelayCommand(o =>
            {
                WcfClientFlags(item: true, build: true, value: true);
            }));
        }

        ICommand _CommandItemWcfClientsBuildOff = null;
        public ICommand CommandItemWcfClientsBuildOff
        {
            get => _CommandItemWcfClientsBuildOff ?? (_CommandItemWcfClientsBuildOff = new RelayCommand(o =>
            {
                WcfClientFlags(item: true, build: true, value: false);
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
                    RefreshMain?.Invoke();
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
                    Process.Start(CoreGlobals.DataBinDirectory);
                }
                catch { }
            }));
        }
    }
}
