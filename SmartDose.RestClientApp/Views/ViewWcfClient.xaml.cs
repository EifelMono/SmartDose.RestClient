using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using SmartDose.WcfClient;
using SmartDose.Core;
using SmartDose.WcfClient.Services;
using System;
using System.Windows;
using SmartDose.Core.Extensions;
using System.ComponentModel;

namespace SmartDose.RestClientApp.Views
{

    /// <summary>
    /// Interaction logic for ViewWcfClient.xaml
    /// </summary>
    public partial class ViewWcfClient : UserControl, IDisposable, INotifyPropertyChanged
    {
        public ViewWcfClient()
        {
            InitializeComponent();
            DataContext = this;

            var x = ClassBuilder.NewObject(new ClassBuilderDefinition()
                .AddProperty("Name", "andreas klapperich")
                .AddProperty("Age", 58)
                .AddProperty("List",
                    new List<string> { "a", "b", "c" }, ClassBuilderPropertyCustomAttribute.All));

            var pi = x.GetType().GetProperty("List");
            var pix = pi.GetCustomAttributes(true);

        }

        public void Dispose()
        {
            try
            {
                if (CommunicationService != null)
                {
                    CommunicationService.OnServiceNotifyEvent -= ServiceNotifyEvent;
                    CommunicationService.Stop();
                }
            }
            catch { }
            CommunicationService = null;
        }

        private CommunicationService CommunicationService;
        private WcfItem _WcfItem;

        public WcfItem WcfItem
        {
            get => _WcfItem;
            set
            {
                _WcfItem = value;
                ServiceNotifyEvent(null, new ServiceNotifyEventArgs { Value = WcfClient.Services.ServiceNotifyEvent.ServiceErrorNotConnected });
                CommunicationService = new CommunicationService(_WcfItem, _WcfItem.ConnectionStringUse);
                CommunicationService.OnServiceNotifyEvent += ServiceNotifyEvent;
                CommunicationService.Start();
            }
        }

        public Brush WcfItemStatusColor { get; set; }
        public string WcfItemStatusText { get; set; }

        public List<string> WcfMethods { get; set; } = new List<string> { "a", "b" };

        protected void ServiceNotifyEvent(object sender, ServiceNotifyEventArgs args)
        {
            if (args is null)
                return;
            var color = Brushes.Black;
            var text = args.Value.ToString();
            switch (args.Value)
            {
                case WcfClient.Services.ServiceNotifyEvent.ServiceRunning:
                    {
                        "Service Running".LogInformation();
                    }
                    break;
                case WcfClient.Services.ServiceNotifyEvent.ServiceErrorNotConnected:
                case WcfClient.Services.ServiceNotifyEvent.ServiceErrorAssemblyNotLoaded:
                case WcfClient.Services.ServiceNotifyEvent.ServiceErrorAssemblyBad:
                    {
                        color = Brushes.Red;
                    }
                    break;
            }
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                WcfItemStatusColor = color;
                WcfItemStatusText = text;
                NotifyPropertyChanged(string.Empty);
            }));

        }

        ICommand _commandWcfExecute = null;
        public ICommand CommandWcfExecute
        {
            get => _commandWcfExecute ?? (_commandWcfExecute = new RelayCommand(o =>
            {
            }));
        }

        public void PrepareForStop()
        {
            try
            {
                CommunicationService.Stop();
            }
            catch { }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

    }

}
