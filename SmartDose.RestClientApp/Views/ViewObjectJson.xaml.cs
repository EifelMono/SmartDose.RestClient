﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Media;
using SmartDose.RestClientApp.Globals;
using SmartDose.RestDomainDev;

namespace SmartDose.RestClientApp.Views
{
    /// <summary>
    /// Interaction logic for ObjectJsonView.xaml
    /// </summary>
    public partial class ViewObjectJson : UserControl
    {
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
        public bool Enabled { get; set; } = false;
        public bool IsModel { get; set; } = false;
        private object _data;
        public object Data
        {
            get
            {
                if (IsModel)
                    _data = ConvertDev.ToObjectFromObjectDev(DataDev);
                else
                    _data = DataDev;
                return _data;
            }
            set
            {
                Enabled = false;
                try
                {
                    _data = value;
                    try
                    {
                        IsModel = true;
                        DataDev = ConvertDev.ToObjectDevFromObject(_data);
                    }
                    catch
                    {
                        IsModel = false;
                        DataDev = _data;
                    }
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
            propertyGridView.SelectedObject = FillEmtpyModels(_dataDev);
            jsonEditor.Text = ConvertDev.ToJsonFromObjectDev(objectValue);
        }

#pragma warning disable IDE1006 // Naming Styles
        private void tabControlMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
#pragma warning restore IDE1006 // Naming Styles
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
    }
}
