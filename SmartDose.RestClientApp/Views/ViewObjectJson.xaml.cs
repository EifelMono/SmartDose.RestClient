using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
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
                        File.WriteAllText(dlg.FileName, ConvertDev.ToJsonFromObjectDev(DataDev));
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
                    DataDev = ConvertDev.ToObjectDevFromJson(jsonEditor.Text, DataDev.GetType());
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
                FillEmtpyModels(e?.ChangedItem?.Value);
            };

            propertyGridView.SelectedGridItemChanged += (s, e) =>
                {
                    try
                    {
                        listBoxPropertyInfo.Items.Clear();
                        listBoxPropertyInfo.Background = Brushes.White;

                        var pd = e?.NewSelection?.PropertyDescriptor;
                        var pp = pd.ComponentType.GetProperty(pd.Name);
                        foreach (var attribute in pp.GetCustomAttributes().Where(aaa => aaa is ValidationAttribute))
                        {
                            var a = attribute as ValidationAttribute;
                            if (a is null)
                                continue;
                            var isValid = a.IsValid(e?.NewSelection?.Value);
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
                _data = ConvertDev.ToObjectFromObjectDev(DataDev);
                return _data;
            }
            set
            {
                _data = value;
                DataDev = ConvertDev.ToObjectDevFromObject(_data);
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
            propertyGridView.SelectedObject = FillEmtpyModels(_dataDev);
            tabControlMain_SelectionChanged(null, null);
        }

#pragma warning disable IDE1006 // Naming Styles
        private void tabControlMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
#pragma warning restore IDE1006 // Naming Styles
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
                                jsonEditor.Text = ConvertDev.ToJsonFromObjectDev(DataDev);
                        }
                        break;
                    }
                // File seleciton
                case 2:
                    {
                        IsJsonTab = false;
                        lock (this)
                        {
                            JsonFiles = Directory.GetFiles(DataDirectory, "*.json").Select(f=> Path.GetFileName(f)).ToList();
                        }
                        break;
                    }
            }
            NotifyPropertyChanged(string.Empty);
        }

        protected T FillEmtpyModels<T>(T objectValue) where T : class
        {
            if (objectValue is null)
                return objectValue;
            if (objectValue.GetType().IsArray)
                foreach (var item in objectValue as IEnumerable<object>)
                    FillEmtpyModels(item);

            foreach (var property in objectValue.GetType().GetProperties())
            {
                if (property.PropertyType.IsClass)
                {
                    if (property.PropertyType.FullName.StartsWith(RestDomainDev.Models.ModelsGlobals.ModelsNamespace))
                    {
                        var value = property.GetValue(objectValue);
                        if (value is null)
                        {
                            try
                            {
                                if (property.PropertyType.IsArray)
                                {
                                    value = Activator.CreateInstance(property.PropertyType, 0);
                                    property.SetValue(objectValue, value);
                                }
                                else
                                {
                                    value = Activator.CreateInstance(property.PropertyType);
                                    property.SetValue(objectValue, value);
                                    FillEmtpyModels(value);
                                }
                            }
                            catch (Exception ex)
                            {
                                ex.LogException();
                            }
                        }
                    }
                }
            }
            return objectValue;
        }

        public T RemoveEmtpyModels<T>(T thisValue) where T : class
        {
            // todo remove
            return thisValue;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

        private void listBoxJsonFiles_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var filename = (string) listBoxJsonFiles.SelectedItem;
            try
            {
                jsonEditor.Text = File.ReadAllText(Path.Combine(DataDirectory, filename));
                CommandJsonToObject.Execute(null);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
