using System;
using System.Windows.Controls;
using System.Windows.Media;
using ICSharpCode.AvalonEdit;
using Newtonsoft.Json;
using SmartDose.RestClient.Extensions;
using SmartDose.RestClientApp.Editor;

namespace SmartDose.RestClientApp.Views
{
    public class ViewTabItem : TabItem
    {
        protected Grid _gridItem;
        protected Grid _gridRequest;
        protected Button _buttonExecute;

        protected TabControl _tabControlResponse;
        protected ObjectJsonView _objectObjectJsonViewResponse;
        protected TabItem _tabItemJsonEditorResponse;
        protected JsonEditor _jsonEditorResponse;
        protected System.Windows.Forms.PropertyGrid _propertyGridResponse;


        public ViewTabItem()
        {
            Content = _gridItem = new Grid();
            _gridItem.RowDefinitions.Add(new RowDefinition { Height = new System.Windows.GridLength(1, System.Windows.GridUnitType.Star) });
            _gridItem.RowDefinitions.Add(new RowDefinition { Height = new System.Windows.GridLength(1, System.Windows.GridUnitType.Auto) });
            _gridItem.RowDefinitions.Add(new RowDefinition { Height = new System.Windows.GridLength(1, System.Windows.GridUnitType.Auto) });
            _gridItem.RowDefinitions.Add(new RowDefinition { Height = new System.Windows.GridLength(1, System.Windows.GridUnitType.Star) });


            _gridRequest = new Grid();
            _gridItem.Children.Add(_gridRequest);
            Grid.SetRow(_gridRequest, 0);

            _buttonExecute = new Button
            {
                Margin = new System.Windows.Thickness(5),
                Padding= new System.Windows.Thickness(5),
                Foreground= Brushes.Green,
                Content = "Execute"
            };
            _buttonExecute.Click += (s, e) => ButtonExecute();
            _gridItem.Children.Add(_buttonExecute);
            Grid.SetRow(_buttonExecute, 1);

            var gridSplitter = new GridSplitter
            {
                Height = 3,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch,
                VerticalAlignment = System.Windows.VerticalAlignment.Stretch,
                ShowsPreview = false
            };
            _gridItem.Children.Add(gridSplitter);
            Grid.SetRow(gridSplitter, 2);

            _tabControlResponse = new TabControl();
            _gridItem.Children.Add(_tabControlResponse);
            Grid.SetRow(_tabControlResponse, 3);

            _objectObjectJsonViewResponse = new ObjectJsonView();
            _tabControlResponse.Items.Add(new TabItem
            {
                Header = "Data",
                Content = _objectObjectJsonViewResponse
            });
            _jsonEditorResponse = new JsonEditor();
            _tabControlResponse.Items.Add(_tabItemJsonEditorResponse = new TabItem
            {
                Header = "Result Message",
                Content = _jsonEditorResponse
            });
            var host = new System.Windows.Forms.Integration.WindowsFormsHost();
            _propertyGridResponse = new System.Windows.Forms.PropertyGrid() { HelpVisible = false };
            _tabControlResponse.Items.Add(new TabItem
            {
                Header = "Result Data",
                Content = new System.Windows.Forms.Integration.WindowsFormsHost { Child = _propertyGridResponse }
            });

        }

        public virtual void ButtonExecute()
        {
        }
        public string ButtonExecuteText { get => (string)_buttonExecute.Content; set => _buttonExecute.Content = value; }

        private object _responseObject;
        public object ResponseObject
        {
            get => _responseObject;
            set
            {
                _responseObject = value;
                Brush resultColor = Brushes.Black;
                if (value is SdrcFlurHttpResponse response && response.Ok)
                {
                    _objectObjectJsonViewResponse.Data = response.Data;
                    _tabControlResponse.SelectedIndex = 0;
                }
                else
                {
                    _objectObjectJsonViewResponse.Data = null;
                    _tabControlResponse.SelectedIndex = 1;
                    resultColor = Brushes.Red;
                }
                _propertyGridResponse.SelectedObject = _responseObject;
                _jsonEditorResponse.Text = ((_responseObject as SdrcFlurHttpResponse)?.Message ?? "").Replace("\\r", "\r").Replace("\\n", "\n");
                // _tabItemJsonEditorResponse.Background = resultColor;
            }
        }

    }
}

