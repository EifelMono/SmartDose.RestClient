using System.Collections.Generic;
using Models = SmartDose.RestDomain.Models.V1;
using Cruds = SmartDose.RestClient.Cruds.V1;

namespace SmartDose.RestClientApp.Views.V1.MasterData
{
    public class ViewManufacturer : ViewCruds
    {
        public ViewManufacturer() : base()
        {
            var labelName = "Manufacturer";
            var labelIdName = "Manufacturer";
            var crudInstance = Cruds.MasterData.Manufacturer.Instance;
            _labelHeader.Content = crudInstance.UrlClone;

            _tabControl.Items.Add(new ViewTabItemReadList<Models.MasterData.Manufacturer>
            {
                OnButtonExecute = async (self) => self.ResponseObject = await crudInstance.ReadListAsync(),
            });

            _tabControl.Items.Add(new ViewTabItemRead<Models.MasterData.Manufacturer>
            {
                RequestParams = new List<ViewParam>
                {
                    new ViewParam {Name=labelIdName, IsViewObjectJson= false, Value= "" },
                },
                OnButtonExecute = async (self) => self.ResponseObject = await crudInstance.ReadAsync(
                                                    self.RequestParamsValueAsString(0)),
            });

            _tabControl.Items.Add(new ViewTabItemCreate<Models.MasterData.Manufacturer>
            {
                RequestParams = new List<ViewParam>
                {
                    new ViewParam {Name=labelName, IsViewObjectJson= true, Value= new Models.MasterData.Manufacturer() }
                },
                OnButtonExecute = async (self) => self.ResponseObject = await crudInstance.CreateAsync(
                                                    self.RequestParamsValueAsT(0)),
            });

            _tabControl.Items.Add(new ViewTabItemUpdate<Models.MasterData.Manufacturer>
            {
                RequestParams = new List<ViewParam>
                {
                    new ViewParam {Name=labelName, IsViewObjectJson= true, Value= new Models.MasterData.Manufacturer() }
                },
                OnButtonExecute = async (self) => self.ResponseObject = await crudInstance.UpdateAsync(
                                                    self.RequestParamsValueAsT(0).Identifier,
                                                    self.RequestParamsValueAsT(0))
            });

            _tabControl.Items.Add(new ViewTabItemDelete<Models.MasterData.Manufacturer>
            {
                RequestParams = new List<ViewParam>
                {
                    new ViewParam {Name=labelIdName, IsViewObjectJson= false, Value= "" },
                },
                OnButtonExecute = async (self) => self.ResponseObject = await crudInstance.DeleteAsync(
                                        self.RequestParamsValueAsString(0)),
            });

            _tabControl.Items.Add(new ViewTabItemDelete<Models.MasterData.Manufacturer>
            {
                Header = "Assign Canister",
                RequestParams = new List<ViewParam>
                {
                    new ViewParam {Name=labelIdName, IsViewObjectJson= false, Value= "" },
                    new ViewParam {Name="MedicineId", IsViewObjectJson= false, Value= "" },
                },
                OnButtonExecute = async (self) => self.ResponseObject = await crudInstance.AssignManufacturerToMedicinAsync(
                                        self.RequestParamsValueAsString(0), self.RequestParamsValueAsString(1)),
            });
        }
    }
}
