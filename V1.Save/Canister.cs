namespace SmartDose.RestDomain.V1.Models
{
    public class Canister
    {
        public string CanisterId { get; set; }

        public string Rfid { get; set; }

        public bool Largecanister { get; set; }

        public string RotorId { get; set; }
    }
}
