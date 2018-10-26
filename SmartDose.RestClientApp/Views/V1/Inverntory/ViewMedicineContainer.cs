using System.Collections.Generic;
using Models = SmartDose.RestDomain.Models.V1;
using Crud = SmartDose.RestClient.Crud.V1;

namespace SmartDose.RestClientApp.Views.V1.Inventory
{
    public class ViewMedicineContainer : ViewCrud
    {
        public ViewMedicineContainer() : base()
        {
            var labelName = "MedicineContainer";
            var labelIdName = "MedicineContainerId";
            var crudInstance = Crud.Inventory.MedicineContainer.Instance;
            _labelHeader.Content = crudInstance.UrlClone;

            _tabControl.Items.Add(new ViewTabItemReadList<Models.Inventory.MedicineContainer>
            {
                OnButtonExecute = async (self) => self.ResponseObject = await crudInstance.ReadListAsync(),
            });

            _tabControl.Items.Add(new ViewTabItemRead<Models.Inventory.MedicineContainer>
            {
                RequestParams = new List<ViewParam>
                {
                    new ViewParam {Name=labelIdName, IsViewObjectJson= false, Value= "" },
                },
                OnButtonExecute = async (self) => self.ResponseObject = await crudInstance.ReadAsync(
                                                    self.RequestParamsValueAsString(0)),
            });

            _tabControl.Items.Add(new ViewTabItemCreate<Models.Inventory.MedicineContainer>
            {
                RequestParams = new List<ViewParam>
                {
                    new ViewParam {Name=labelName, IsViewObjectJson= true, Value= new Models.Inventory.MedicineContainer() }
                },
                OnButtonExecute = async (self) => self.ResponseObject = await crudInstance.CreateAsync(
                                                    self.RequestParamsValueAsT(0)),
            });

            _tabControl.Items.Add(new ViewTabItemUpdate<Models.Inventory.MedicineContainer>
            {
                RequestParams = new List<ViewParam>
                {
                    new ViewParam {Name=labelName, IsViewObjectJson= true, Value= new Models.Inventory.MedicineContainer() }
                },
                OnButtonExecute = async (self) => self.ResponseObject = await crudInstance.UpdateAsync(
                                                    self.RequestParamsValueAsT(0).MedicineId,
                                                    self.RequestParamsValueAsT(0))
            });

            _tabControl.Items.Add(new ViewTabItemDelete<Models.Inventory.MedicineContainer>
            {
                RequestParams = new List<ViewParam>
                {
                    new ViewParam {Name=labelIdName, IsViewObjectJson= false, Value= "" },
                },
                OnButtonExecute = async (self) => self.ResponseObject = await crudInstance.DeleteAsync(
                                        self.RequestParamsValueAsString(0)),
            });
        }
    }
}
