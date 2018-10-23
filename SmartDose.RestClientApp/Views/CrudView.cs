using System.Windows;
using System.Windows.Controls;

namespace SmartDose.RestClientApp.Views
{
    public class CruidView : Grid
    {
        protected TabControl _tabControl { get; set; }
        protected Label _labelHeader { get; set; }
        public CruidView()
        {
            RowDefinitions.Add(new RowDefinition { Height = new System.Windows.GridLength(1, System.Windows.GridUnitType.Auto) });
            RowDefinitions.Add(new RowDefinition { Height = new System.Windows.GridLength(1, System.Windows.GridUnitType.Star) });

            Children.Add(_labelHeader = new Label { FontWeight = FontWeights.Black });
            Grid.SetRow(_labelHeader, 0);

            _tabControl = new TabControl();
            Children.Add(_tabControl = new TabControl());
            Grid.SetRow(_tabControl, 1);
        }
    }
}
