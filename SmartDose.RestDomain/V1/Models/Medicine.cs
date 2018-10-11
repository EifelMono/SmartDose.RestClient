using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SmartDose.RestDomain.V1.Models
{
    public class Medicine
    {
        public Medicine()
        {
            SynonymIds = new List<Synonym>();
            PrintDetails = new List<PrintDetail>();
            Pictures = new List<MedicinePicture>();
        }

        [JsonProperty("Identifier")]
        public string MedicineId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Comment { get; set; }

        public bool Active { get; set; }

        public virtual bool TrayFillOnly { get; set; }

        public virtual PouchMode PouchMode { get; set; }

        public List<Synonym> SynonymIds { get; set; }

        public List<PrintDetail> PrintDetails { get; set; }

        public List<MedicinePicture> Pictures { get; set; }

        public SpecialHandling SpecialHandling { get; set; }

        public override string ToString()
            => $"Medicine MedicineId={MedicineId} Name={Name}";
    }
}
