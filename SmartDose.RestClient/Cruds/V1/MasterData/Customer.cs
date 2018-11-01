using Models = SmartDose.RestDomain.Models.V1;

namespace SmartDose.RestClient.Cruds.V1.MasterData
{
    public class Customer : CoreV1Crud<Models.MasterData.Customer>
    {
        public Customer() : base(nameof(Customer) + "s")
        {
        }

        public static Customer Instance => Instance<Customer>();
    }
}
