using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Newtonsoft.Json;
using SmartDose.RestClientApp.Globals;
using SmartDose.RestDomainDev;
using SmartDose.RestDomain.Converter;

namespace SmartDose.RestClientApp.Views
{
    /// <summary>
    /// Interaction logic for ObjectJsonView.xaml
    /// </summary>
    public partial class ViewObjectJson : UserControl, INotifyPropertyChanged
    {
        public bool CheckBoxAutoConvertObjectToJson { get; set; } = true;
        public bool IsDataObject { get; set; } = false;
        public bool IsJsonTab { get; set; } = false;
        private ICommand _commandSaveObject;
        public ICommand CommandSaveObject
        {
            get => _commandSaveObject ?? (_commandSaveObject = new RelayCommand(o =>
            {
                var dlg = new Microsoft.Win32.SaveFileDialog
                {
                    FileName = "default.json",
                    InitialDirectory = DataDirectory,
                    DefaultExt = ".json",
                    Filter = "Json documents (.json)|*.json"
                };

                var result = dlg.ShowDialog();
                if (result == true)
                {
                    try
                    {
                        File.WriteAllText(dlg.FileName, RestDomainDevGlobals.ToJsonFromObjectDev(DataDev));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }));
        }

        private ICommand _commandJsonToObject;
        public ICommand CommandJsonToObject
        {
            get => _commandJsonToObject ?? (_commandJsonToObject = new RelayCommand(o =>
            {
                try
                {
                    DataDev = RestDomainDevGlobals.ToObjectDevFromJson(jsonEditor.Text, DataDev.GetType());
                    tabControlMain.SelectedIndex = 0;
                }
                catch (Exception ex)
                {
                    var save = CheckBoxAutoConvertObjectToJson;
                    CheckBoxAutoConvertObjectToJson = false;
                    try
                    {
                        tabControlMain.SelectedIndex = 1;
                    }
                    finally
                    {
                        CheckBoxAutoConvertObjectToJson = save;
                    }
                    MessageBox.Show(ex.Message);
                    // Message = "After parsing a value an unexpected character was encountered: d. Path 'UniqueId', line 2, position 21."
                    var line = -1;
                    var position = -1;
                    foreach (var text in ex.Message.Split(','))
                    {
                        if (text.Contains(" line "))
                            int.TryParse(text.Replace("line", "").Trim(), out line);
                        if (text.Contains(" position "))
                            int.TryParse(text.Replace("position", "").Replace(".", "").Trim(), out position);
                    }
                    if (line >= 0 && position >= 0)
                    {
                        jsonEditor.Focus();
                        jsonEditor.TextArea.Caret.Location = new ICSharpCode.AvalonEdit.Document.TextLocation(line, position);
                        jsonEditor.TextArea.Caret.BringCaretToView();
                        NotifyPropertyChanged(string.Empty);
                    }
                }
            }));
        }
        public List<string> JsonFiles { get; private set; } = new List<string>();


        public string DataDirectory { get; set; }

        public ViewObjectJson()
        {
            InitializeComponent();
            DataContext = this;

            propertyGridView.HelpVisible = false;

            propertyGridView.PropertyValueChanged += (s, e) =>
            {
                propertyGridView.Refresh();
            };

            propertyGridView.SelectedGridItemChanged += (s, e) =>
                {
                    try
                    {
                        listBoxPropertyInfo.Items.Clear();
                        listBoxPropertyInfo.Background = Brushes.White;

                        var pd = e?.NewSelection?.PropertyDescriptor;
                        var pp = pd.ComponentType.GetProperty(pd.Name);
                        JsonConverterAttribute jcA = null;
                        foreach (var attribute in pp.GetCustomAttributes().Where(aaa => aaa is JsonConverterAttribute))
                        {
                            jcA = attribute as JsonConverterAttribute;
                            if (jcA != null)
                                break;
                        }
                        foreach (var attribute in pp.GetCustomAttributes().Where(aaa => aaa is ValidationAttribute))
                        {
                            var a = attribute as ValidationAttribute;
                            if (a is null)
                                continue;
                            var obj = e?.NewSelection?.Value;
                            if (jcA != null)
                            {
                                try
                                {
                                    if (Activator.CreateInstance(jcA.ConverterType) is JsonConverter jc)
                                    {
                                        if (jc is SmartDose.RestDomain.Converter.DateTimeConverter dtc)
                                        {
                                            obj = ((DateTime)obj).ToString(dtc.CustomFormat);
                                        }
                                    }
                                }
                                catch { }
                            }
                            var isValid = a.IsValid(obj);
                            listBoxPropertyInfo.Background = isValid ? Brushes.White : Brushes.Red;
                            listBoxPropertyInfo.Items.Add($"IsValid {isValid} {a.GetType().FullName}");
                            if (!string.IsNullOrEmpty(a.FormatErrorMessage(pd.Name)))
                                listBoxPropertyInfo.Items.Add(a.FormatErrorMessage(pd.Name));
                        }
                    }
                    catch { }
                };
            tabControlMain.SelectionChanged += (s, e) =>
                {
                    switch (tabControlMain.SelectedIndex)
                    {
                        // Property Editor
                        case 0:
                            {
                                IsJsonTab = false;
                                break;
                            }
                        // Json Editor
                        case 1:
                            {
                                if (IsDataObject)
                                    IsJsonTab = true;
                                if (CheckBoxAutoConvertObjectToJson)
                                {
                                    if (!IsPlainData)
                                        jsonEditor.Text = RestDomainDevGlobals.ToJsonFromObjectDev(DataDev);
                                }
                                break;
                            }
                        // File seleciton
                        case 2:
                            {
                                IsJsonTab = false;
                                lock (this)
                                {
                                    JsonFiles = Directory.GetFiles(DataDirectory, "*.json").Select(f => Path.GetFileName(f)).ToList();
                                }
                                break;
                            }
                    }
                    NotifyPropertyChanged(string.Empty);
                };
        }

        public ViewObjectJson(bool showBottomListBox) : this()
        {
            listBoxPropertyInfo.Visibility = showBottomListBox ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
        }
        private object _data;
        public object Data
        {
            get
            {
                _data = RestDomainDevGlobals.ToObjectFromObjectDev(DataDev);
                return _data;
            }
            set
            {
                _data = value;
                DataDev = RestDomainDevGlobals.ToObjectDevFromObject(_data);
                IsDataObject = true;
                IsPlainData = false;
                try
                {
                    DataDirectory = AppGlobals.DataBinObjectJsonDirectory(DataDev.GetType());
                }
                catch
                {
                    // What shall I do?
                }
                CheckBoxAutoConvertObjectToJson = true;
                NotifyPropertyChanged(string.Empty);
            }
        }

        public bool IsPlainData { get; set; } = false;
        public object PlainData
        {
            get => _dataDev;
            set
            {
                DataDirectory = "";
                IsPlainData = true;
                IsDataObject = false;
                NotifyPropertyChanged(string.Empty);
                DataDev = value;
            }
        }

        private object _dataDev;
        public object DataDev
        {
            get => _dataDev;
            set
            {
                _dataDev = value;
                UpdateView(DataDev);
            }
        }

        protected void UpdateView(object objectValue)
        {
            propertyGridView.SelectedObject = _dataDev;
            tabControlMain.RaiseEvent(new SelectionChangedEventArgs(TabControl.SelectionChangedEvent, new List<TabItem> { }, new List<TabItem> { }));
        }

        public T RemoveEmtpyModels<T>(T thisValue) where T : class
        {
            // todo remove
            return thisValue;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

#pragma warning disable IDE1006 // Naming Styles
        private void listBoxJsonFiles_MouseDoubleClick(object sender, MouseButtonEventArgs e)
#pragma warning restore IDE1006 // Naming Styles
        {
            var filename = (string)listBoxJsonFiles.SelectedItem;
            try
            {
                jsonEditor.Text = File.ReadAllText(Path.Combine(DataDirectory, filename));
                CommandJsonToObject.Execute(null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
