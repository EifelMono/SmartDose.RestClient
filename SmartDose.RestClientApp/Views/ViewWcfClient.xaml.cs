using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using SmartDose.RestDomainDev.PropertyEditorThings;
using SmartDose.WcfClient;
using SmartDose.Core.Extensions;

namespace SmartDose.RestClientApp.Views
{

    /// <summary>
    /// Interaction logic for ViewWcfClient.xaml
    /// </summary>
    public partial class ViewWcfClient : UserControl
    {
        public ViewWcfClient()
        {
            InitializeComponent();
            DataContext = this;

            dynamic o = "{\"Karl\":\"karl\"}".ToExpandableObjectFromJson<object>();
            viewObjectJsonWcfInput.PlainData = o;
        }


        private WcfItem _WcfItem;

        public WcfItem WcfItem
        {
            get => _WcfItem;
            set
            {
                _WcfItem = value;
                WcfItemStatus();
            }
        }

        public Brush WcfItemStatusColor { get; set; }
        public string WcfItemStatusText { get; set; }

        public List<string> WcfMethods { get; set; } = new List<string> { "a", "b" };

        protected void WcfItemStatus()
        {
            WcfItemStatusColor = Brushes.Red;
            WcfItemStatusText = "NotConnected";
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

        }
    }

}
