using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Newtonsoft.Json;

#if RestDomainDev
using SmartDose.Core;
using SmartDose.RestDomainDev.PropertyEditorThings;
namespace SmartDose.RestDomainDev.Models.V1.Production
#else
namespace SmartDose.RestDomain.Models.V1.Production
#endif
{
#if RestDomainDev
    [TypeConverter(typeof(ExpandableObjectConverter))]
#endif
    public class RestExternalOrder
    {
        public RestExternalOrder()
        {

        }

        /// <summary>
        /// Gets or sets the external id.
        /// </summary>
        [Required(ErrorMessage = "An ExternalId is required")]
        public string ExternalId { get; set; }

        /// <summary>
        /// Gets or sets the timestamp.
        /// </summary>
        // ? [DateTimeValidation("yyyy-MM-ddTHH:mm:ss", "Timestamp requires this format yyyy-MM-ddTHH:mm:ss")]
        public string Timestamp { get; set; }

        /// <summary>
        /// Gets or sets the customer.
        /// </summary>
        public RestExternalCustomer Customer { get; set; } = new RestExternalCustomer();

        /// <summary>
        /// Gets or sets the order details.
        /// </summary>
#if RestDomainDev
        [TypeConverter(typeof(ListConverter))]
#endif
        public List<RestOrderDetail> OrderDetails { get; set; } = new List<RestOrderDetail>();

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        public OrderState State { get; set; } = OrderState.FixNull;

        [Browsable(false)]
        [JsonIgnore]
        public IEnumerable<MedicationDetail> UsedMedicines
            =>  OrderDetails == null
                    ? new List<MedicationDetail>()
                    : OrderDetails.Where(od => od != null)
                        .SelectMany(od => od.IntakeDetails)
                            .Where(id => id != null)
                                .SelectMany(id => id.MedicationDetails)
                                    .Where(md => md != null && md.MedicineId != null)
                                            .GroupBy(md => md.MedicineId)
                                                .Select(g => g.First());

        [Browsable(false)]
        [JsonIgnore]
        public IEnumerable<string> UsedMedicineIds
            => UsedMedicines.Select(md => md.MedicineId).Distinct();

        [Browsable(false)]
        [JsonIgnore]
        public IEnumerable<(string Id, string Name)> UsedMedicinesIdsAndName
            => UsedMedicines.Select(md => (md.MedicineId, md.PrescribedMedicine));
    }
}
