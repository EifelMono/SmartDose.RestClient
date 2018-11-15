using System.Diagnostics;
using System.IO;
using System.Windows;
using SmartDose.RestClientApp.Globals;
using ModelsV2 = SmartDose.RestDomain.Models.V2;
using CrudV2 = SmartDose.RestClient.Cruds.V2;
using SmartDose.WcfClient.MasterData;
using System.ServiceModel;
using System;
using SmartDose.WcfClient;

namespace SmartDose.RestClientApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var c = new SmartDose.WcfClient.MasterData.Customer();
            c.ContactAddress = new WcfClient.MasterData.ContactAddress();
            c.ContactPerson = new WcfClient.MasterData.ContactPerson();
            propertyGridView.SelectedObject = c;

        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            AppGlobals.Configuration.Data.WcfClients.ForEach(w => w.Rebuild = true);
            SmartDoseWcfClientGlobals.BuildWcfClientsAssemblies(AppGlobals.Configuration.Data.WcfClients);
            return;

            try
            {
                var client = new MasterDataServiceClient(
                         new NetTcpBinding(SecurityMode.None),
                         new EndpointAddress("net.tcp://LWDEU08DTK2PH2:9002/MasterData"));
                var medicines = await client.GetMedicinesAsync(new SearchFilter[] { }, null, null);
                client.SetMedicinesReceived += (s, a) =>
                {

                };

                // propertyGridView.SelectedObject = c;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
