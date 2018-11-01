using System.Collections.Generic;
using Models = SmartDose.RestDomain.Models.V1;
using Cruds= SmartDose.RestClient.Cruds.V1;

namespace SmartDose.RestClientApp.Views.V1.MasterData
{
    public class ViewCanister : ViewCruds
    {
        public ViewCanister() : base()
        {
            var labelName = "Canister";
            var labelIdName = "CanisterId";
            var crudInstance = Cruds.MasterData.Canister.Instance;
            _labelHeader.Content = crudInstance.UrlClone;

            _tabControl.Items.Add(new ViewTabItemReadList<Models.MasterData.Canister>
            {
                OnButtonExecute = async (self) => self.ResponseObject = await crudInstance.ReadListAsync(),
            });

            _tabControl.Items.Add(new ViewTabItemRead<Models.MasterData.Canister>
            {
                RequestParams = new List<ViewParam>
                {
                    new ViewParam {Name=labelIdName, IsViewObjectJson= false, Value= "" },
                },
                OnButtonExecute = async (self) => self.ResponseObject = await crudInstance.ReadAsync(
                                                    self.RequestParamsValueAsString(0)),
            });

            _tabControl.Items.Add(new ViewTabItemCreate<Models.MasterData.Canister>
            {
                RequestParams = new List<ViewParam>
                {
                    new ViewParam {Name=labelName, IsViewObjectJson= true, Value= new Models.MasterData.Canister() }
                },
                OnButtonExecute = async (self) => self.ResponseObject = await crudInstance.CreateAsync(
                                                    self.RequestParamsValueAsT(0)),
            });

            _tabControl.Items.Add(new ViewTabItemUpdate<Models.MasterData.Canister>
            {
                RequestParams = new List<ViewParam>
                {
                    new ViewParam {Name=labelName, IsViewObjectJson= true, Value= new Models.MasterData.Canister() }
                },
                OnButtonExecute = async (self) => self.ResponseObject = await crudInstance.UpdateAsync(
                                                    self.RequestParamsValueAsT(0).CanisterId,
                                                    self.RequestParamsValueAsT(0))
            });

            _tabControl.Items.Add(new ViewTabItemDelete<Models.MasterData.Canister>
            {
                RequestParams = new List<ViewParam>
                {
                    new ViewParam {Name=labelIdName, IsViewObjectJson= false, Value= "" },
                },
                OnButtonExecute = async (self) => self.ResponseObject = await crudInstance.DeleteAsync(
                                        self.RequestParamsValueAsString(0)),
            });

            _tabControl.Items.Add(new ViewTabItemDelete<Models.MasterData.Canister>
            {
                Header = "Get CanisterStatus",
                RequestParams = new List<ViewParam>
                {
                    new ViewParam {Name=labelIdName, IsViewObjectJson= false, Value= "" },
                },
                OnButtonExecute = async (self) => self.ResponseObject = await crudInstance.GetCanisterStatusAsync(
                                        self.RequestParamsValueAsString(0)),
            });

            _tabControl.Items.Add(new ViewTabItemDelete<Models.MasterData.Canister>
            {
                Header = "Get ListCanisterStatus",
                RequestParams = new List<ViewParam>
                {
                },
                OnButtonExecute = async (self) => self.ResponseObject = await crudInstance.GetListCanisterStatusAsync(),
            });

            _tabControl.Items.Add(new ViewTabItemDelete<Models.MasterData.Canister>
            {
                Header = "Assign Canister",
                RequestParams = new List<ViewParam>
                {
                    new ViewParam {Name="CanisterId", IsViewObjectJson= false, Value= "" },
                    new ViewParam {Name="MedicineId", IsViewObjectJson= false, Value= "" },
                },
                OnButtonExecute = async (self) => self.ResponseObject = await crudInstance.AssignCanisterAsync(
                                        self.RequestParamsValueAsString(0), self.RequestParamsValueAsString(1)),
            });

            _tabControl.Items.Add(new ViewTabItemDelete<Models.MasterData.Canister>
            {
                Header = "Activate Canister",
                RequestParams = new List<ViewParam>
                {
                    new ViewParam {Name="CanisterId", IsViewObjectJson= false, Value= "" },
                    new ViewParam {Name="Status", IsViewObjectJson= false, Value= true },
                },
                OnButtonExecute = async (self) => self.ResponseObject = await crudInstance.ActivateCanisterAsync(
                                        self.RequestParamsValueAsString(0), self.RequestParamsValueAsBool(1)),
            });
        }
    }
}
