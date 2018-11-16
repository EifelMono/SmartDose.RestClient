using System.Windows.Controls;
using System.Windows.Media;
using SmartDose.WcfClient;

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

        protected void WcfItemStatus()
        {
            WcfItemStatusColor = Brushes.Red;
            WcfItemStatusText = "NotConnected";
        }


        public void PrepareForStop()
        {

        }
    }
}
