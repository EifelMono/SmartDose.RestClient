using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ICSharpCode.AvalonEdit;
using Newtonsoft.Json;
using SmartDose.RestClient.Extensions;
using SmartDose.RestClientApp.Editor;

namespace SmartDose.RestClientApp.Views
{
    public class ViewTabItem : TabItem
    {
        protected Grid _gridTabItem;
        protected Grid _gridRequest;
        protected Button _buttonExecute;

        protected TabControl _tabControlResponse;
        protected ViewObjectJson _viewObjectJsonResponse;
        protected TabItem _tabItemJsonEditorResponse;
        protected JsonEditor _jsonEditorResponse;
        protected System.Windows.Forms.PropertyGrid _propertyGridResponse;

        protected List<ViewParam> _requestParams = new List<ViewParam>();
        public List<ViewParam> RequestParams
        {
            get => _requestParams;
            set
            {
                _gridRequest.Children.Clear();
                _gridRequest.RowDefinitions.Clear();
                foreach (var item in value)
                {
                    _gridRequest.RowDefinitions.Add(new RowDefinition { Height = new System.Windows.GridLength(1, item.IsViewObjectJson ? System.Windows.GridUnitType.Star : System.Windows.GridUnitType.Auto) });
                    var label = new Label { Content = item.Name };
                    _gridRequest.Children.Add(label);
                    Grid.SetColumn(label, 0);
                    Grid.SetRow(label, _gridRequest.RowDefinitions.Count - 1);
                    if (item.IsViewObjectJson)
                    {
                        var viewObjectJson = new ViewObjectJson();
                        _gridRequest.Children.Add(viewObjectJson);
                        Grid.SetColumn(viewObjectJson, 1);
                        Grid.SetRow(viewObjectJson, _gridRequest.RowDefinitions.Count - 1);
                        viewObjectJson.Data = item.Value;
                        item.View = viewObjectJson;
                    }
                    else
                    {
                        var viewInput = new ViewInput();
                        _gridRequest.Children.Add(viewInput);
                        Grid.SetColumn(viewInput, 1);
                        Grid.SetRow(viewInput, _gridRequest.RowDefinitions.Count - 1);
                        viewInput.Data = (string)item.Value;
                        item.View = viewInput;
                    }
                }
                _requestParams = value;
            }
        }


        public ViewTabItem()
        {
            Content = _gridTabItem = new Grid();
            _gridTabItem.RowDefinitions.Add(new RowDefinition { Height = new System.Windows.GridLength(1, System.Windows.GridUnitType.Star) });
            _gridTabItem.RowDefinitions.Add(new RowDefinition { Height = new System.Windows.GridLength(1, System.Windows.GridUnitType.Auto) });
            _gridTabItem.RowDefinitions.Add(new RowDefinition { Height = new System.Windows.GridLength(1, System.Windows.GridUnitType.Auto) });
            _gridTabItem.RowDefinitions.Add(new RowDefinition { Height = new System.Windows.GridLength(1, System.Windows.GridUnitType.Star) });


            _gridRequest = new Grid();
            _gridRequest.ColumnDefinitions.Add(new ColumnDefinition { Width = new System.Windows.GridLength(1, System.Windows.GridUnitType.Auto) });
            _gridRequest.ColumnDefinitions.Add(new ColumnDefinition { Width = new System.Windows.GridLength(1, System.Windows.GridUnitType.Star) });
            _gridTabItem.Children.Add(_gridRequest);
            Grid.SetRow(_gridRequest, 0);

            _buttonExecute = new Button
            {
                Margin = new System.Windows.Thickness(5),
                Padding = new System.Windows.Thickness(5),
                Foreground = Brushes.Green,
                Content = "Execute"
            };
            _buttonExecute.Click += (s, e) => InternalButtonExecute();
            _gridTabItem.Children.Add(_buttonExecute);
            Grid.SetRow(_buttonExecute, 1);

            var gridSplitter = new GridSplitter
            {
                Height = 3,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch,
                VerticalAlignment = System.Windows.VerticalAlignment.Stretch,
                ShowsPreview = false
            };
            _gridTabItem.Children.Add(gridSplitter);
            Grid.SetRow(gridSplitter, 2);

            _tabControlResponse = new TabControl();
            _gridTabItem.Children.Add(_tabControlResponse);
            Grid.SetRow(_tabControlResponse, 3);

            _viewObjectJsonResponse = new ViewObjectJson(false);
            _tabControlResponse.Items.Add(new TabItem
            {
                Header = "Data",
                Content = _viewObjectJsonResponse
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

        protected void InternalButtonExecute()
        {
            try
            {
                this.Cursor = Cursors.Wait;
                foreach(var item in RequestParams)
                {
                    if (item.IsViewObjectJson)
                        item.Value = (item.View as ViewObjectJson).Data;
                    else
                        item.Value = (item.View as ViewInput).Data;
                }
                ButtonExecute();
            }
            finally
            {
                this.Cursor = null;
            }
        }

        public Action<ViewTabItem> OnButtonExecute { get; set; }

        public virtual void ButtonExecute()
        {
            OnButtonExecute?.Invoke(this);
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
                    _viewObjectJsonResponse.Data = response.Data;
                    _tabControlResponse.SelectedIndex = 0;
                }
                else
                {
                    _viewObjectJsonResponse.Data = null;
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

