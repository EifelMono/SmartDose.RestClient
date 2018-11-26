using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using MasterData1000;

namespace SmartDose.RestClient.ConsoleWithWcfReference
{

    public class ServiceClient : ServiceClientCore, IMasterDataService, IMasterDataServiceCallback
    {
        public ServiceClient(string endpointAddress, SecurityMode securityMode = SecurityMode.None) : base(endpointAddress, securityMode)
        {
        }

        public new MasterDataServiceClient Client { get => (MasterDataServiceClient)base.Client; }

        #region Query


        public async Task<ServiceResultQueryResponse> QueryAsync(QueryRequest queryRequest)
            => await SafeExecuteAsync(() => Client.QueryAsync(queryRequest)).ConfigureAwait(false);

        #endregion

        #region Wrapped Client Calls

        public async Task<ServiceResultLong> MedicinesGetIdByIdentifierAsync(string medicineIdentifier)
            => await SafeExecuteAsync(() => Client.MedicinesGetIdByIdentifierAsync(medicineIdentifier)).ConfigureAwait(false);

        public async Task<ServiceResultBool> MedicinesDeleteByIdentifierAsync(string medicineIdentifier)
             => await SafeExecuteAsync(() => Client.MedicinesDeleteByIdentifierAsync(medicineIdentifier)).ConfigureAwait(false);

        public async Task<ServiceResultMedicine> MedicinesGetMedcineByIdentifierAsync(string medicineIdentifier)
            => await SafeExecuteAsync(() => Client.MedicinesGetMedcineByIdentifierAsync(medicineIdentifier)).ConfigureAwait(false);

        public async Task<ServiceResultMedicineList> MedicinesGetMedcinesByIdentifiersAsync(string[] medicineIdentifiers, int page, int pageSize)
            => await SafeExecuteAsync(() => Client.MedicinesGetMedcinesByIdentifiersAsync(medicineIdentifiers, page, pageSize)).ConfigureAwait(false);

        public async override Task<ServiceResult> ExecuteQueryBuilderAsync(QueryBuilder queryBuilder) 
        {
            var queryRequest = new QueryRequest();
            var queryResponse = await SafeExecuteAsync(() => Client.QueryAsync(queryRequest)).ConfigureAwait(false);
            return new ServiceResult
            {

            };
        }

        public Task SubscribeForCallbacksAsync()
        {
            throw new NotImplementedException();
        }

        public Task UnsubscribeForCallbacksAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Medicine> GetMedicineByIdentifierAsync(string medicineIdentifier)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResultLong> CanistersGetIdByIdentifierAsync(string identifier)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResultBool> CanistersDeleteByIdentifierAsync(string identifier)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResultCanister> CanistersGetCanisterByIdentifierAsync(string identifier)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResultCanisterList> CanistersGetCanistersByIdentifiersAsync(string[] identifiers, int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResultLong> CustomersGetIdByIdentifierAsync(string customerIdentifier)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResultBool> CustomersDeleteByIdentifierAsync(string customerIdentifier)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResultCustomer> CustomersGetCustomerByIdentifierAsync(string customerIdentifier)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResultCustomerList> CustomersGetCustomersByIdentifiersAsync(string[] customerIdentifiers, int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResultLong> DestinationFacilitiesGetIdByIdentifierAsync(string destinationFacilityIdentifier)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResultBool> DestinationFacilitiesDeleteByIdentifierAsync(string destinationFacilityIdentifier)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResultDestinationFacility> DestinationFacilitiesGetDestinationFacilityByIdentifierAsync(string destinationFacilityIdentifier)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResultDestinationFacilityList> DestinationFacilitiesGetDestinationFacilitiesByIdentifiersAsync(string[] destinationFacilityIdentifiers, int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResultLong> ManufacturersGetIdByIdentifierAsync(string manufacturerIdentifier)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResultBool> ManufacturersDeleteByIdentifierAsync(string manufacturerIdentifier)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResultManufacturer> ManufacturersGetManufacturerByIdentifierAsync(string manufacturerIdentifier)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResultManufacturerList> ManufacturersGetManufacturersByIdentifiersAsync(string[] manufacturerIdentifiers, int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResultLong> PatientsGetIdByIdentifierAsync(string patientIdentifier)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResultBool> PatientsDeleteByIdentifierAsync(string patientIdentifier)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResultPatient> PatientsGetPatientByIdentifierAsync(string patientIdentifier)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResultPatientList> PatientsGetPatientsByIdentifiersAsync(string[] patientIdentifiers, int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResultLong> TraysGetIdByIdentifierAsync(string identifier)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResultBool> TraysDeleteByIdentifierAsync(string identifier)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResultTray> TraysGetTrayByIdentifierAsync(string identifier)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResultTrayList> TraysGetTraysByIdentifiersAsync(string[] identifiers, int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        public void MedicinesChanged(Medicine[] medicines)
        {
            throw new NotImplementedException();
        }

        public void MedicinesDeleted(string[] medicineIdentifiers)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
