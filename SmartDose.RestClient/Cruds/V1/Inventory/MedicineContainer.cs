using Models = SmartDose.RestDomain.Models.V1;

namespace SmartDose.RestClient.Cruds.V1.Inventory
{
    public class MedicineContainer : CoreV1Crud<Models.Inventory.MedicineContainer>
    {
        public MedicineContainer() : base("MedContainers")
        {
        }

        public static MedicineContainer Instance => Instance<MedicineContainer>();
    }
}
