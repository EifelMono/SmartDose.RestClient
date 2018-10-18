using System.Diagnostics;
using System.Windows;
using SmartDose.RestDomain;

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
            Globals.Init();
            var u1 = Globals.Configuration.Data.UrlRestV1;
            var u2 = Globals.Configuration.Data.UrlRestV1;

            foreach (var type in typeof(ModelExtensions).Assembly.GetTypes())
            {
                Debug.WriteLine(type.FullName);
            }

            var x = ModelExtensions.ModelItems;

            var customerV1 = new RestDomain.V1.Models.MasterData.Customer().CreateAllSubModels();
            var customerV2 = new RestDomain.V2.Models.MasterData.Customer().CreateAllSubModels();
            propertyGridCustomerV1.SelectedObject = customerV1;
            propertyGridCustomerV2.SelectedObject = customerV2;

            var medicineV1 = new RestDomain.V1.Models.MasterData.Medicine().CreateAllSubModels();
            var medicineV2 = new RestDomain.V2.Models.MasterData.Medicine().CreateAllSubModels();
            propertyGridMedicineV1.SelectedObject = medicineV1;
            propertyGridMedicineV2.SelectedObject = medicineV2;

            var orderV1 = new RestDomain.V1.Models.Production.RestExternalOrder().CreateAllSubModels();
            var orderV2 = new RestDomain.V2.Models.Production.Order().CreateAllSubModels();
            propertyGridOrderV1.SelectedObject = orderV1;
            propertyGridOrderV2.SelectedObject = orderV2;
            propertyGridOrderV2.PropertyValueChanged += PropertyGridOrderV2_PropertyValueChanged; ; 
        }

        private void PropertyGridOrderV2_PropertyValueChanged(object s, System.Windows.Forms.PropertyValueChangedEventArgs e)
        {
            Debug.WriteLine("x");
        }
    }
}
