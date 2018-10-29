using System.Windows;
using System.Windows.Controls;

namespace SmartDose.RestClientApp.Views
{
    public class ViewCrud: Grid
    {
        protected TabControl _tabControl { get; set; }
        protected Label _labelHeader { get; set; }
        public ViewCrud()
        {
            RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
            RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

            Children.Add(_labelHeader = new Label { FontWeight = FontWeights.Black });
            Grid.SetRow(_labelHeader, 0);

            _tabControl = new TabControl();
            Children.Add(_tabControl = new TabControl());
            Grid.SetRow(_tabControl, 1);
        }
    }
}
