using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models = SmartDose.RestDomain.Models.V1;

namespace SmartDose.RestGenerator.Generator.V1
{
    public class ExternalOrderGenerator
    {
        public static Models.Production.ExternalOrder CreateNewExternalOrder(
                        List<Models.Production.Medicine> medicine,
                        Models.Production.Customer value,
                        Models.Production.DestinationFacility destinationFacility,
                        int mPatientCounter,
                        int mPatientDays,
                        int mIntakeTimesPerDay,
                        int mMedicationPerIntakeTime,
                        int mPillsPerMedication,
                        bool mRandomValuePatientDays,
                        bool mRandomValueIntakeTimesPerDay,
                        bool mRandomValueMedicationPerIntakeTime,
                        bool mRandomValuePillsPerMedication,
                        bool isYesSelected)
        {
            var random = new Random();
            medicine = medicine.Where(medicine1 => medicine1.Active).ToList();
            var externalOrder = new Models.Production.ExternalOrder
            {
                Customer = value,
                Timestamp = DateTime.Now.ToString("g"),
                OrderDetails = new List<Models.Production.OrderDetail>()
            };
            if (value != null)
                externalOrder.ExternalId = "GO_" + value.Name.Replace(" ", "") + "_" +
                                           DateTime.Now.ToString("yyyyMMdd_HHmmss");
            else
                externalOrder.ExternalId = "GO_NoCustomer_" + DateTime.Now.ToString("yyyyMMdd_HHmmss");
            for (var i = 0; i < mPatientCounter; i++)
            {
                var countOfDay = mRandomValuePatientDays ? random.Next(1, mPatientDays) : mPatientDays;
                var countOfInatkeTimes = mRandomValueIntakeTimesPerDay
                    ? random.Next(1, mIntakeTimesPerDay)
                    : mIntakeTimesPerDay;

                var orderDetail = new Models.Production.OrderDetail
                {
                    Patient = PatientGenerator.New(),
                    Pharmacy = new Models.Production.Customer
                    {
                        Name = "Pharmacy_" + value?.Name,
                        ContactAddress = value?.ContactAddress,
                        ContactPerson = value?.ContactPerson,
                        CustomerId = "Pharmacy_" + value?.CustomerId
                    },
                    IntakeDetails = new List<Models.Production.IntakeDetail> { },
                    DestinationFacility = destinationFacility
                };
                orderDetail.ExternalDetailPrintInfo1 = "EDPI1: " + orderDetail.Patient.ContactPerson.Name + "/" +
                                                       DateTime.Parse(orderDetail.Patient.DateOfBirth,
                                                               CultureInfo.CurrentCulture)
                                                           .ToShortDateString();
                orderDetail.ExternalDetailPrintInfo2 = "EDPI2: " + orderDetail.DestinationFacility.DepartmentName +
                                                       "/" +
                                                       orderDetail.Pharmacy.CustomerId;
                for (var j = 0; j < countOfInatkeTimes; j++)
                {
                    var medicationDetails = new List<Models.Production.MedicationDetail>();
                    var countOfMedsPerIntake = mRandomValueMedicationPerIntakeTime
                        ? random.Next(1, mMedicationPerIntakeTime)
                        : mMedicationPerIntakeTime;
                    for (var k = 0; k < countOfMedsPerIntake; k++)
                    {
                        var medicationDetail = new Models.Production.MedicationDetail();
                        var medicationForIntake = medicine[random.Next(0, medicine.Count)];
                        medicationDetail.MedicineId = isYesSelected
                            ? medicationForIntake.SynonymIds.FirstOrDefault()?.Identifier
                            : medicationForIntake.Identifier;
                        medicationDetail.IntakeAdvice = "IntakeAdvice";
                        medicationDetail.Physician = "Physician";
                        medicationDetail.PhysicianComment = "PhysicianComment";
                        medicationDetail.PrescribedMedicine = medicationForIntake.Name;
                        var countOfPills = mRandomValuePillsPerMedication
                            ? random.Next(1, mPillsPerMedication)
                            : mPillsPerMedication;
                        medicationDetail.Count = countOfPills;
                        medicationDetails.Add(medicationDetail);
                    }

                    var intakeDateAndTime = DateTime.Today + TimeSpan.FromDays(1) +
                                            TimeSpan.FromMinutes(random.Next(1, 1440));
                    for (var k = 0; k < countOfDay; k++)
                    {
                        var intakeDetail = new Models.Production.IntakeDetail
                        {
                            IntakeDateTime = (intakeDateAndTime + TimeSpan.FromDays(k)).ToString("s"),
                            MedicationDetails = new List<Models.Production.MedicationDetail> { },
                        };
                        // intakeDetail.ExternalIntakePrintInfo1 = "EIPI1: " + intakeDetail.IntakeDateTime;
                        intakeDetail.MedicationDetails.AddRange(medicationDetails);
                        // intakeDetail.ExternalIntakePrintInfo2 = "EIPI2: " + intakeDetail.MedicationDetails.Count;
                        orderDetail.IntakeDetails.Add(intakeDetail);
                    }
                }

                externalOrder.OrderDetails.Add(orderDetail);
            }

            return externalOrder;
        }
    }
}
