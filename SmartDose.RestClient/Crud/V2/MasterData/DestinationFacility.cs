using Models = SmartDose.RestDomain.Models.V2;

namespace SmartDose.RestClient.Crud.V2.MasterData
{
    public class DestinationFacility : CoreV2Crud<Models.MasterData.DestinationFacility>
    {
        public DestinationFacility() : base(MasterDataName, "DestinationFacilities" )
        {
        }

        public static DestinationFacility Instance => Instance<DestinationFacility>();
    }
}
