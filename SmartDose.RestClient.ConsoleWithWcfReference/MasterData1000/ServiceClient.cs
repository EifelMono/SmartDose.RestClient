﻿using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using SmartDose.RestClient.ConsoleWithWcfReference;

namespace MasterData1000
{
    public class ServiceClient : ServiceClientBase, IMasterDataService, IMasterDataServiceCallback
    {
        public ServiceClient(string endpointAddress, SecurityMode securityMode = SecurityMode.None) : base(endpointAddress, securityMode)
        {
        }

        public new MasterDataServiceClient Client { get => (MasterDataServiceClient)base.Client; set => base.Client = value; }

        public override void CreateClient()
        {
            Binding binding = new NetTcpBinding(SecurityMode)
            {
                OpenTimeout = TimeSpan.FromSeconds(1),
                ReceiveTimeout = TimeSpan.FromSeconds(30),
                SendTimeout = TimeSpan.FromSeconds(30),
                CloseTimeout = TimeSpan.FromSeconds(1)
            };
            if (EndpointAddress.ToLower().StartsWith("http"))
                binding = new NetHttpBinding();

            Client = new MasterDataServiceClient(binding, new EndpointAddress(EndpointAddress));
        }

        #region Query

        public ServiceResultQueryResponse Query(QueryRequest queryRequest)
            => QueryAsync(queryRequest).Result;

        public async Task<ServiceResultQueryResponse> QueryAsync(QueryRequest queryRequest)
            => await CatcherAsync(() => Client.QueryAsync(queryRequest)).ConfigureAwait(false);

        public async override Task<ServiceResult> ExecuteQueryBuilderAsync(QueryBuilder queryBuilder)
        {
            var queryRequest = new QueryRequest
            {
                ModelName = queryBuilder.ModelType.Name,
                ModelNamespace = queryBuilder.ModelType.Namespace,
                WhereAsJson = queryBuilder.WhereAsJson,
                OrderByAsJson = queryBuilder.OrderByAsJson,
                OrderByAsc = queryBuilder.OrderByAsc,
                OrderByAs = (QueryRequestOrderByAs)queryBuilder.OrderByAs,
                Page = queryBuilder.Page,
                PageSize = queryBuilder.PageSize,
                ResultAs = (QueryRequestResultAs)queryBuilder.ResultAs,
            };
            var serviceResult = await QueryAsync(queryRequest).ConfigureAwait(false);
            var newServiceResult = serviceResult.CastByClone<ServiceResult<string>>();
            newServiceResult.Data = serviceResult?.Data?.ZippedJsonData;
            return newServiceResult;
        }
        #endregion

        #region Wrapped abstract Client Calls

        public async override Task OpenAsync()
        {
            await (CatcherAsyncIgnore(() => Client.OpenAsync()).ConfigureAwait(false));
        }

        public async override Task CloseAsync()
        {
            await (CatcherAsyncIgnore(() => Client.CloseAsync()).ConfigureAwait(false));
        }

        public async override Task SubscribeForCallbacksAsync()
        {
            await (CatcherAsyncIgnore(() => Client.SubscribeForCallbacksAsync()).ConfigureAwait(false));
        }

        public async override Task UnsubscribeForCallbacksAsync()
        {
            await (CatcherAsyncIgnore(() => Client.UnsubscribeForCallbacksAsync()).ConfigureAwait(false));
        }

        #endregion

        #region Wrapped Client Calls 

        #region Old Stuff
        public Task<Medicine> GetMedicineByIdentifierAsync(string medicineIdentifier)
        {
            throw new NotImplementedException();
        }
        #endregion

        public ServiceResultLong MedicinesGetIdByIdentifier(string medicineIdentifier)
            => MedicinesGetIdByIdentifierAsync(medicineIdentifier).Result;

        public async Task<ServiceResultLong> MedicinesGetIdByIdentifierAsync(string medicineIdentifier)
            => await CatcherAsync(() => Client.MedicinesGetIdByIdentifierAsync(medicineIdentifier))
                    .ConfigureAwait(false);

        public async Task<ServiceResultBool> MedicinesDeleteByIdentifierAsync(string medicineIdentifier)
             => await CatcherAsync(() => Client.MedicinesDeleteByIdentifierAsync(medicineIdentifier))
                    .ConfigureAwait(false);

        public async Task<ServiceResultMedicine> MedicinesGetMedcineByIdentifierAsync(string medicineIdentifier)
            => await CatcherAsync(() => Client.MedicinesGetMedcineByIdentifierAsync(medicineIdentifier))
                    .ConfigureAwait(false);

        public async Task<ServiceResultMedicineList> MedicinesGetMedcinesByIdentifiersAsync(string[] medicineIdentifiers, int page, int pageSize)
            => await CatcherAsync(() => Client.MedicinesGetMedcinesByIdentifiersAsync(medicineIdentifiers, page, pageSize))
                    .ConfigureAwait(false);

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
        #endregion

        #region Wrapped Callbacks

        public event Action<Medicine[]> OnMedicinesChanged;
        public virtual void MedicinesChanged(Medicine[] medicines)
            => CatcherAsTask(() => OnMedicinesChanged?.Invoke(medicines));


        public event Action<string[]> OnMedicinesDeleted;
        public virtual void MedicinesDeleted(string[] medicineIdentifiers)
            => CatcherAsTask(() => OnMedicinesDeleted?.Invoke(medicineIdentifiers));
        #endregion
    }
}
