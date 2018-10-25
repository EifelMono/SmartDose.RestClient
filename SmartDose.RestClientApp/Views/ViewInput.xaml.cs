using System.Windows;
using System.Windows.Controls;

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

        private object _data;
        public object Data
        {
            get
            {
                switch (_data)
                {
                    case bool b:
                        return checkBox.IsChecked;
                    case string s:
                        return textBox.Text;
                }
                return null;
            }
            set
            {
                _data = value;
                switch (_data)
                {
                    case bool b:
                        textBox.Visibility = Visibility.Collapsed;
                        checkBox.Visibility = Visibility.Visible;
                        checkBox.IsChecked = b;
                        break;
                    case string s:
                        textBox.Visibility = Visibility.Visible;
                        checkBox.Visibility = Visibility.Collapsed;
                        textBox.Text = s;
                        break;
                }
            }
        }
    }
}
