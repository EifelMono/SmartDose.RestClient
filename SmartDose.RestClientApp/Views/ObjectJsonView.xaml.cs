using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Media;
using SmartDose.RestClientApp.Globals;
using SmartDose.RestDomainDev;
using ModelsV2 = SmartDose.RestDomain.Models.V2;
using CrudV2 = SmartDose.RestClient.Crud.V2;

namespace SmartDose.RestClientApp.Views
{
    /// <summary>
    /// Interaction logic for ObjectJsonView.xaml
    /// </summary>
    public partial class ObjectJsonView : UserControl
    {
        public ObjectJsonView()
        {
            InitializeComponent();
            DataContext = this;

            propertyView.HelpVisible = false;

            propertyView.PropertyValueChanged += (s, e) =>
            {
                FillEmtpyModels(e?.ChangedItem?.Value);
            };

            propertyView.SelectedGridItemChanged += (s, e) =>
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

        public bool Enabled { get; set; } = false;
        private object _data;
        public object Data
        {
            get => _data;
            set
            {
                Enabled = false;
                try
                {
                    _data = value;
                    DataDev = ConvertDev.ToObjectDevFromObject(Data);
                }
                finally
                {
                    Enabled = true;
                }

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
            propertyView.SelectedObject = FillEmtpyModels(_dataDev);
            jsonEditor.Text = ConvertDev.ToJsonFromObjectDev(objectValue);
        }

        private void tabControlMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!Enabled)
                return;
            switch (tabControlMain.SelectedIndex)
            {
                // Property Editor
                case 0:
                    {
                        break;
                    }
                // Json Editor
                case 1:
                    {
                        jsonEditor.Text = ConvertDev.ToJsonFromObjectDev(DataDev);
                        break;
                    }
                // File seleciton
                case 2:
                    {
                        break;
                    }
            }
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



        private async void buttonClick_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            {
                var medicine = CrudV2.MasterData.Medicine.Instance;
                if (await medicine.CreateAsync(new ModelsV2.MasterData.Medicine { }) is var response1 && response1.Ok)
                {
                    "1".LogInformation();
                }
                "2".LogInformation();


                if (await medicine.ReadListAsync() is var response2 && response2.Ok)
                {
                    "1".LogInformation();
                }
                "2".LogInformation();
            }

        }
    }
}
