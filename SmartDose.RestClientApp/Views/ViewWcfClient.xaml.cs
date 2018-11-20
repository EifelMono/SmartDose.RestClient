using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using SmartDose.WcfClient;
using SmartDose.WcfClient.Services;
using System;
using System.Windows;
using SmartDose.Core.Extensions;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Linq;
using static SmartDose.Core.SafeExecuter;

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
                try
                {
                    _WcfItem = value;
                    ServiceNotifyEvent(null, new ServiceNotifyEventArgs { Value = WcfClient.Services.ServiceNotifyEvent.ServiceErrorNotConnected });
                    CommunicationService = new CommunicationService(_WcfItem, _WcfItem.ConnectionStringUse);
                    CommunicationService.OnServiceNotifyEvent += ServiceNotifyEvent;

                }
                catch (Exception ex)
                {
                    ex.LogException();
                    ServiceNotifyEvent(null, new ServiceNotifyEventArgs { Value = WcfClient.Services.ServiceNotifyEvent.Error });
                }
            }
        }

        public Brush WcfItemStatusColor { get; set; }
        public string WcfItemStatusText { get; set; }

        public List<WcfMethod> WcfMethods { get; set; } = new List<WcfMethod>();

        protected async void ServiceNotifyEvent(object sender, ServiceNotifyEventArgs args)
        {
            if (args is null)
                return;
            var color = Brushes.Black;
            var text = args.Value.ToString();
            switch (args.Value)
            {
                case WcfClient.Services.ServiceNotifyEvent.ServiceInited:
                    await Task.Delay(100);
                    CommunicationService.Start();
                    break;
                case WcfClient.Services.ServiceNotifyEvent.ServiceRunning:
                    {
                        "Service Running".LogInformation();
                        WcfMethods = CommunicationService.WcfMethods.OrderBy(m => m.Name).ToList();
                        if (WcfMethods.Count > 0)
                            Application.Current.Dispatcher.Invoke(new Action(() =>
                            {
                                comboBoxMethod.SelectedIndex = 0;
                                WcfItemStatusText = text;
                                NotifyPropertyChanged(string.Empty);
                            }));
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

        public void ButtonExecuteState(bool enable)
        {
            if (enable)
            {
                buttonExecute.Background = Brushes.Black;
                buttonExecute.Foreground = Brushes.White;
                buttonExecute.Cursor = null;
                buttonExecute.Content = "Execute";
            }
            else
            {
                buttonExecute.Background = Brushes.Yellow;
                buttonExecute.Foreground = Brushes.Red;
                buttonExecute.Cursor = Cursors.Wait;
                buttonExecute.Content = "Executing .........................................";
            }
        }

        public bool IsButtonExecuteEnabled => buttonExecute.Cursor == null;

        ICommand _commandWcfExecute = null;
        public ICommand CommandWcfExecute
        {
            get => _commandWcfExecute ?? (_commandWcfExecute = new RelayCommand(async o =>
            {
                if (!IsButtonExecuteEnabled)
                    return;
                Catcher(() => $"Before Execute Client Status {CommunicationService.Client.State.ToString()}".LogInformation());
                ButtonExecuteState(false);
                try
                {

                    if (comboBoxMethod.SelectedItem is WcfMethod wcfMethod)
                    {
                        $"Execute  {wcfMethod.Name}".LogInformation();

                        if (await wcfMethod.CallMethodAsync(CommunicationService.Client) is var result && result.Ok)
                        {
                            wcfMethod.Output = result.Value;
                        }
                        else
                            wcfMethod.Output = new WcfErrorObject("Error on Execute", result.Value as Exception);
                        viewObjectJsonWcfOutput.PlainData = wcfMethod.Output;
                    }
                    else
                        viewObjectJsonWcfOutput.PlainData = new WcfErrorObject("Error App");
                }
                catch (Exception ex)
                {
                    viewObjectJsonWcfOutput.PlainData = new WcfErrorObject(ex.Message, ex);
                }
                finally
                {
                    ButtonExecuteState(true);
                }
                NotifyPropertyChanged(string.Empty);
                Catcher(() => $"After Execute Client Status {CommunicationService.Client.State.ToString()}".LogInformation());
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

        private void comboBoxMethod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxMethod.SelectedItem is WcfMethod wcfMethod)
                viewObjectJsonWcfInput.PlainData = wcfMethod.Input;
            NotifyPropertyChanged(string.Empty);
        }
    }

}
