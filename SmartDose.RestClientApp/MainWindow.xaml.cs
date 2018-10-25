using System.Diagnostics;
using System.IO;
using System.Windows;
using SmartDose.RestClientApp.Globals;
using ModelsV2 = SmartDose.RestDomain.Models.V2;
using CrudV2 = SmartDose.RestClient.Crud.V2;


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
            var u1 = AppGlobals.Configuration.Data.UrlV1;
            var u2 = AppGlobals.Configuration.Data.UrlV1;
        }


        private async void MedicineCreate_Click(object sender, RoutedEventArgs e)
        {
            if (await CrudV2.MasterData.Medicine.Instance.CreateAsync(new ModelsV2.MasterData.Medicine { }) is var response && response.Ok)
            {
                Debug.WriteLine("Created");
                // created!
            }
            else
                Debug.WriteLine(response.Message);
        }

        private async void MedicineGet_Click(object sender, RoutedEventArgs e)
        {
            if (await CrudV2.MasterData.Medicine.Instance.ReadListAsync() is var response && response.Ok)
            {
                Debug.WriteLine("Medicine List");
                Debug.WriteLine(response.Data.ToJson());
            }
            else
                Debug.WriteLine(response.Message);
        }

        private async void MedicineGetList_Click(object sender, RoutedEventArgs e)
        {
            if (await CrudV2.MasterData.Medicine.Instance.ReadAsync("me") is var response && response.Ok)
            {
                Debug.WriteLine("Medicine");
                Debug.WriteLine(response.Data.ToJson());
            }
            else
                Debug.WriteLine(response.Message);
        }
    }


}
