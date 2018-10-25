using System.Collections.Generic;
using Models = SmartDose.RestDomain.Models.V2;
using Crud = SmartDose.RestClient.Crud.V2;

namespace SmartDose.RestClientApp.Views.V2.MasterData
{
    public class ViewManufacturer : ViewCrud
    {
        public ViewManufacturer() : base()
        {
            var labelName = "Manufacturer";
            var labelIdName = "ManufacturerCode";
            var crudInstance = Crud.MasterData.Manufacturer.Instance;
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
                    new ViewParam {Name=labelName, IsViewObjectJson= true, Value= new Models.MasterData.Customer() }
                },
                OnButtonExecute = async (self) => self.ResponseObject = await crudInstance.CreateAsync(
                                                    self.RequestParamsValueAsT(0)),
            });

            _tabControl.Items.Add(new ViewTabItemUpdate<Models.MasterData.Manufacturer>
            {
                RequestParams = new List<ViewParam>
                {
                    new ViewParam {Name=labelName, IsViewObjectJson= true, Value= new Models.MasterData.Customer() }
                },
                OnButtonExecute = async (self) => self.ResponseObject = await crudInstance.UpdateAsync(
                                                    self.RequestParamsValueAsT(1).ManufacturerCode,
                                                    self.RequestParamsValueAsT(1))
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

            _tabControl.Items.Add(new ViewTabItem<Models.MasterData.Medicine>
            {
                Header = "AssignManufacturerToMedicineAsync (Get)",
                RequestParams = new List<ViewParam>
                {
                    new ViewParam {Name=labelIdName, IsViewObjectJson= false, Value= "" },
                    new ViewParam {Name="MedicineCode", IsViewObjectJson= false, Value= "" },
                },
                OnButtonExecute = async (self) => self.ResponseObject = await crudInstance.AssignManufacturerToMedicineAsync(
                                        self.RequestParamsValueAsString(0),
                                        self.RequestParamsValueAsString(1)),
            });
        }
    }
}
