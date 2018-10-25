using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ICSharpCode.AvalonEdit;
using Newtonsoft.Json;
using SmartDose.RestClient.Extensions;
using SmartDose.RestClientApp.Editor;
using SmartDose.RestClientApp.Globals;

namespace SmartDose.RestClientApp.Views
{
    public class ViewTabItem : TabItem
    {
        protected Grid _gridTabItem;
        protected Grid _gridRequestMain;
        protected Grid _gridRequest;
        protected Button _buttonExecute;

        protected Grid _gridResponse;
        protected Label _labelResponse;
        protected TabControl _tabControlResponse;
        protected ViewObjectJson _viewObjectJsonResponse;
        protected TabItem _tabItemJsonEditorResponse;
        protected JsonEditor _jsonEditorResponse;
        protected System.Windows.Forms.PropertyGrid _propertyGridResponse;

        protected List<ViewParam> _requestParams = new List<ViewParam>();

        public string RequestParamsValueAsString(int index) => (string)RequestParams[index].Value ?? "";
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

            _gridTabItem.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            _gridTabItem.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
            _gridTabItem.RowDefinitions.Add(new RowDefinition { Height = new GridLength(300, GridUnitType.Pixel) });

            _gridRequestMain = new Grid();
            _gridRequestMain.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            _gridRequestMain.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
            _gridTabItem.Children.Add(_gridRequestMain);
            Grid.SetRow(_gridRequestMain, 0);

            _gridRequest = new Grid();
            _gridRequest.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
            _gridRequest.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            _gridRequestMain.Children.Add(_gridRequest);
            Grid.SetRow(_gridRequest, 0);

            _buttonExecute = new Button
            {
                Margin = new Thickness(5),
                Padding = new Thickness(5),
                Foreground = Brushes.Green,
                Content = "Execute"
            };
            _buttonExecute.Click += (s, e) => InternalButtonExecute();
            _gridRequestMain.Children.Add(_buttonExecute);
            Grid.SetRow(_buttonExecute, 1);

            var gridSplitter = new GridSplitter
            {
                Height = 3,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Center,
                ResizeDirection = GridResizeDirection.Rows,
                ShowsPreview = false
            };
            _gridTabItem.Children.Add(gridSplitter);
            Grid.SetRow(gridSplitter, 1);

            _gridResponse = new Grid();
            _gridResponse.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
            _gridResponse.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            _gridTabItem.Children.Add(_gridResponse);
            Grid.SetRow(_gridResponse, 2);

            _labelResponse = new Label { VerticalContentAlignment = VerticalAlignment.Center, FontWeight = FontWeights.Bold };
            _gridResponse.Children.Add(_labelResponse);
            Grid.SetRow(_labelResponse, 0);

            _tabControlResponse = new TabControl();
            _gridResponse.Children.Add(_tabControlResponse);
            Grid.SetRow(_tabControlResponse, 1);

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
                Cursor = Cursors.Wait;
                foreach (var item in RequestParams)
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
                Cursor = null;
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
                _labelResponse.Content = "";
                Brush resultColor = Brushes.Black;
                if (value is SdrcFlurHttpResponse response)
                {
                    if (response.Ok)
                    {
                        try
                        {
                            if (response.Data is null)
                                _viewObjectJsonResponse.PlainData = _responseObject;
                            else
                                _viewObjectJsonResponse.Data = response.Data;
                        }
                        catch
                        {
                            _viewObjectJsonResponse.PlainData = _responseObject;
                        }
                        _tabControlResponse.SelectedIndex = 0;
                    }
                    else
                    {
                        _viewObjectJsonResponse.PlainData = _responseObject;
                        _tabControlResponse.SelectedIndex = 0;
                        resultColor = Brushes.Red;
                    }
                    _labelResponse.Content = $"Status={response.StatusCode.ToString()}";
                    _labelResponse.Foreground = resultColor;
                    _jsonEditorResponse.Text = $"\r\n// Timestamp={response.ReceivedOn}" +
                                               $"\r\n// Status={response.StatusCode.ToString()}\r\n" +
                                               $"\r\n// Data\r\n" +
                                               (response.Data?.ToString() ?? "").Replace("\\r", "\r").Replace("\\n", "\n") +
                                               $"\r\n// Message\r\n" +
                                               (response.Message ?? "").Replace("\\r", "\r").Replace("\\n", "\n") +
                                               $"\r\n// Exception\r\n" +
                                               (response.Exception?.ToString() ?? "").Replace("\\r", "\r").Replace("\\n", "\n");
                }
                else
                {
                    _labelResponse.Content = "$No known response";
                    _jsonEditorResponse.Text = $"// Timestamp={DateTime.Now}\r\n" +
                                               $"// No known response";
                }
                _propertyGridResponse.SelectedObject = _responseObject;
                // _tabItemJsonEditorResponse.Background = resultColor;
            }
        }

    }

    public class ViewTabItem<T> : ViewTabItem
    {
        public T RequestParamsValueAsT(int index) => (T)RequestParams[index].Value;

        public new Action<ViewTabItem<T>> OnButtonExecute { get; set; }

        public override void ButtonExecute()
        {
            try
            {
                OnButtonExecute?.Invoke(this);
            }
            catch (Exception ex)
            {
                ex.LogException();
            }
        }
    }

    class ViewTabItemCreate<T> : ViewTabItem<T> { public ViewTabItemCreate() { Header = "Create (Post)"; } }

    class ViewTabItemReadList<T> : ViewTabItem<T> { public ViewTabItemReadList() { Header = "Read List (Get)"; } }

    class ViewTabItemRead<T> : ViewTabItem<T> { public ViewTabItemRead() { Header = "Read (Get)"; } }

    class ViewTabItemUpdate<T> : ViewTabItem<T> { public ViewTabItemUpdate() { Header = "Update (Put)"; } }

    class ViewTabItemDelete<T> : ViewTabItem<T> { public ViewTabItemDelete() { Header = "Delete (Delete)"; } }
}

