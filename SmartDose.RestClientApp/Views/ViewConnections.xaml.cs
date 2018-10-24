﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SmartDose.RestClientApp.Globals;

namespace SmartDose.RestClientApp.Views
{
    /// <summary>
    /// Interaction logic for ViewConnection.xaml
    /// </summary>
    public partial class ViewConnections : UserControl
    {
        public ViewConnections()
        {
            InitializeComponent();
            DataContext = this;
        }

        public MenuItem RootMenuItem { get; set; } = new MenuItem();

        public ConfigurationData ConfigurationData { get => AppGlobals.Configuration.Data; }

        ICommand _commandOpenSwaggerV1 = null;
        public ICommand CommandOpenSwaggerV1
        {
            get => _commandOpenSwaggerV1 ?? (_commandOpenSwaggerV1 = new RelayCommand(o =>
            {
            }));
        }

        ICommand _commandOpenSwaggerV2 = null;
        public ICommand CommandOpenSwaggerV2
        {
            get => _commandOpenSwaggerV2 ?? (_commandOpenSwaggerV2 = new RelayCommand(o =>
            {
                try
                {
                    Process.Start(AppGlobals.Configuration.Data.UrlRestV2 + "/swagger/ui/index");
                }
                catch { }
            }));
        }

        ICommand _commandSaveConfiguration = null;
        public ICommand CommandSaveConfiguration
        {
            get => _commandSaveConfiguration ?? (_commandSaveConfiguration = new RelayCommand(o =>
            {

                try
                {
                    RestClient.UrlConfig.UrlV1 = ConfigurationData.UrlRestV1;
                    RestClient.UrlConfig.UrlV2 = ConfigurationData.UrlRestV2;
                    RestClient.UrlConfig.ClearUrls();
                    RemoveViews(RootMenuItem);
                    AppGlobals.Configuration.Save();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                void RemoveViews(MenuItem menuItem)
                {
                    if (menuItem != null)
                    {
                        if (menuItem.ModelsItem != null)
                            if (menuItem.ModelsItem.Value is ViewCrud viewCrud)
                                menuItem.ModelsItem.Value = null;
                        foreach (var m in menuItem.Items)
                            RemoveViews(m);
                    }
                };

            }));
        }



        ICommand _commandOpenFolder = null;
        public ICommand CommandOpenDataFolder
        {
            get => _commandOpenFolder ?? (_commandOpenFolder = new RelayCommand(o =>
            {
                try
                {
                    Process.Start(Globals.AppGlobals.DataBinDirectory);
                }
                catch { }
            }));
        }
    }
}