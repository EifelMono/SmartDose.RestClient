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

namespace SmartDose.RestClientApp.Views
{
    /// <summary>
    /// Interaction logic for ViewInput.xaml
    /// </summary>
    public partial class ViewInput : UserControl
    {
        public ViewInput()
        {
            InitializeComponent();
        }

        public string Data { get => textBox.Text; set => textBox.Text = value; }
    }
}
