using System;
using System.Collections.Generic;
using System.Linq;
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
