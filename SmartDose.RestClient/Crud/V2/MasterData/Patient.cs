using Models = SmartDose.RestDomain.Models.V2;

namespace SmartDose.RestClient.Crud.V2.MasterData
{
    public class Patient : CoreV2Crud<Models.MasterData.Patient>
    {
        public Patient() : base(MasterDataName, nameof(Patient) + "s")
        {
        }

        public static Patient Instance => Instance<Patient>();
    }
}
