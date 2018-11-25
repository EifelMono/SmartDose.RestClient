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
using System.Collections.ObjectModel;
using System.Text;
using System.Diagnostics;
using SmartDose.RestClientApp.Globals;

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
                    CommunicationService.OnEvents = null;
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
                    CommunicationService = new CommunicationService(_WcfItem, _WcfItem.ConnectionStringUse)
                    {
                        WcfTimeOuts = new WcfTimeouts
                        {
                            Open= TimeSpan.FromSeconds(5),
                            Close= TimeSpan.FromSeconds(5),
                            Receive = AppGlobals.Configuration.Data.WcfTimeSpan,
                            Send = AppGlobals.Configuration.Data.WcfTimeSpan,
                        }
                    };
                    CommunicationService.OnServiceNotifyEvent += ServiceNotifyEvent;
                    CommunicationService.OnEvents = (s, e) =>
                    {
                        Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            WcfEvents.Add(new WcfEvent
                            {
                                Name = e == null ? "" : e.GetType().Name,
                                Value = e
                            });
                            NotifyPropertyChanged(string.Empty);
                        }));

                    };
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

        public ObservableCollection<WcfEvent> WcfEvents { get; set; } = new ObservableCollection<WcfEvent>();

        protected async void ServiceNotifyEvent(object sender, ServiceNotifyEventArgs args)
        {
            if (args is null)
                return;
            var color = Brushes.Black;
            var text = args.Value.ToString();
            $"{WcfItem.ConnectionName} {text}".LogInformation();
            switch (args.Value)
            {
                case WcfClient.Services.ServiceNotifyEvent.ServiceInited:
                    await Task.Delay(200);
                    CommunicationService.Start();
                    break;
                case WcfClient.Services.ServiceNotifyEvent.ServiceRunning:
                    {
                        WcfMethods = CommunicationService.WcfMethods.OrderBy(m => m.Name).ToList();
                        if (WcfMethods.Count > 0)
                            Application.Current.Dispatcher.Invoke(new Action(() =>
                            {
                                comboBoxMethod.SelectedIndex = 0;
                                WcfItemStatusText = text;
                                GenerateDoc();
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
                            wcfMethod.Output = new WcfError("Error on Execute", result.Value as Exception);
                        viewObjectJsonWcfOutput.PlainData = wcfMethod.Output;
                    }
                    else
                        viewObjectJsonWcfOutput.PlainData = new WcfError("Error App");
                }
                catch (Exception ex)
                {
                    viewObjectJsonWcfOutput.PlainData = new WcfError(ex.Message, ex);
                }
                finally
                {
                    ButtonExecuteState(true);
                }
                NotifyPropertyChanged(string.Empty);
                Catcher(() => $"After Execute Client Status {CommunicationService.Client.State.ToString()}".LogInformation());
            }));
        }

        ICommand _commandWcfEventsClear = null;
        public ICommand CpmmandWcfEventsClear
        {
            get => _commandWcfEventsClear ?? (_commandWcfEventsClear = new RelayCommand(async o =>
            {
                WcfEvents.Clear();
            }));
        }

        public void PrepareForStop()
        {
            try
            {
                CommunicationService.OnEvents = null;
                CommunicationService.Stop();
            }
            catch { }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

        private string _SelectedWcfMethod;
        public string SelectedWcfMethod { get=> _SelectedWcfMethod; set
            {
                if (_SelectedWcfMethod!= value)
                {
                    _SelectedWcfMethod = value;
                    NotifyPropertyChanged(nameof(SelectedWcfMethod));
                }
            }
        }
        private void comboBoxMethod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxMethod.SelectedItem is WcfMethod wcfMethod)
                viewObjectJsonWcfInput.PlainData = wcfMethod.Input;
            NotifyPropertyChanged(string.Empty);
        }

        private void listViewEvents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listViewEvents.SelectedItem is WcfEvent wcfEvent)
                viewObjectJsonEvent.PlainData = wcfEvent.Value;
            NotifyPropertyChanged(string.Empty);
        }

        public void GenerateDoc()
        {
            try
            {
                var sb = new StringBuilder()
                    .H1($"{WcfItem.ConnectionName} [{WcfItem.Group}]")
                    .br();

                foreach (var method in WcfMethods)
                {
                    try
                    {
                        sb.H2(method.Name)
                            .hr()
                            .H4("Parameter and return")
                            .TableOpen("Name", "Type");

                        foreach (var parameter in method.Method.GetParameters())
                            sb.TableLine(parameter.Name, parameter.ParameterType.Name);
                        var returnType = method.Method.ReturnParameter.ParameterType.Name;
                        if (method.Method.ReturnParameter.ParameterType.IsGenericType)
                        {
                            returnType = "";
                            foreach (var type in method.Method.ReturnParameter.ParameterType.GenericTypeArguments)
                                returnType += type.Name + " ";
                        }
                        var returnText = returnType.ToString();
                        if (returnText == "Task")
                            returnText = " ";
                        sb.TableLine("return", returnText);

                        sb.TableClose().br(2);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex);
                    }
                }
                webBrowserDoc.NavigateToString(sb.ToString());
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }

    internal static class HtmlExtensions
    {
        public static StringBuilder Frame(this StringBuilder sb, string name, string text)
        {
            sb.AppendLine($"<{name}>{text}</{name}>");
            return sb;
        }
        public static StringBuilder H1(this StringBuilder sb, string text)
            => sb.Frame("h1", text);

        public static StringBuilder H2(this StringBuilder sb, string text)
            => sb.Frame("h2", text);
        public static StringBuilder H4(this StringBuilder sb, string text)
            => sb.Frame("h4", text);

        public static StringBuilder br(this StringBuilder sb, int count = 1)
        {
            for (var i = 0; i < count; i++)
                sb.AppendLine("<br>");
            return sb;
        }

        public static StringBuilder hr(this StringBuilder sb)
        {
            sb.AppendLine($"<hr>");
            return sb;
        }

        public static StringBuilder TableOpen(this StringBuilder sb, params string[] thtexts)
        {
            sb.AppendLine("<table border=\"1\" cellpadding=\"6\"><tr bgcolor=\"silver\" >");
            foreach (var thtext in thtexts)
                sb.Frame("th", thtext);
            return sb.Append("</tr>");
        }

        public static StringBuilder TableLine(this StringBuilder sb, params string[] tdtexts)
        {
            sb.AppendLine("<tr>");
            foreach (var tdtext in tdtexts)
                sb.Frame("td", tdtext);
            return sb.Append("</tr>");
        }

        public static StringBuilder TableClose(this StringBuilder sb)
        {
            sb.AppendLine("</table>");
            return sb;
        }
    }

}
