using Models = SmartDose.RestDomain.Models.V2;

namespace SmartDose.RestClient.Crud.V2.MasterData
{
    public class Customer : CoreV2Crud<Models.MasterData.Customer>
    {
        public Customer() : base(MasterDataName, nameof(Customer) + "s")
        {
        }

        public static Customer Instance => Instance<Customer>();
    }
}
