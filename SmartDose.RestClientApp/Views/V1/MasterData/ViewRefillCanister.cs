using System.Collections.Generic;
using Models = SmartDose.RestDomain.Models.V1;
using Crud = SmartDose.RestClient.Crud.V1;

namespace SmartDose.RestClientApp.Views.V1.MasterData
{
    public class ViewRefillCanister : ViewCrud
    {
        public ViewRefillCanister() : base()
        {
#pragma warning disable CS0219 // Variable is assigned but its value is never used
            var labelName = "RefillCanister";
            var labelIdName = "RefillCanisterId";
#pragma warning restore CS0219 // Variable is assigned but its value is never used
            var crudInstance = Crud.MasterData.RefillCanister.Instance;
            _labelHeader.Content = crudInstance.UrlClone;

            _tabControl.Items.Add(new ViewTabItemDelete<Models.MasterData.Canister>
            {
                Header = "Refill Canister ",
                RequestParams = new List<ViewParam>
                {
                    new ViewParam {Name="CanisterId", IsViewObjectJson= false, Value= "" },
                },
                OnButtonExecute = async (self) => self.ResponseObject = await crudInstance.RefillCanisterAsync(
                                            self.RequestParamsValueAsString(0)),
            });

            _tabControl.Items.Add(new ViewTabItemDelete<Models.MasterData.Canister>
            {
                Header = "Refill Canister From Stockbottle",
                RequestParams = new List<ViewParam>
                {
                    new ViewParam {Name="CanisterId", IsViewObjectJson= false, Value= "" },
                    new ViewParam {Name="ContainerId", IsViewObjectJson= false, Value= "" },
                    new ViewParam {Name="Amout", IsViewObjectJson= false, Value= "" },
                },
                OnButtonExecute = async (self) => self.ResponseObject = await crudInstance.RefillCanisterFromStockbottleAsync(
                                            self.RequestParamsValueAsString(0), self.RequestParamsValueAsString(1), self.RequestParamsValueAsString(1)),
            });

            _tabControl.Items.Add(new ViewTabItemDelete<Models.MasterData.Canister>
            {
                Header = "Refill Canister From Stockbottle",
                RequestParams = new List<ViewParam>
                {
                    new ViewParam {Name="CanisterId", IsViewObjectJson= false, Value= "" },
                    new ViewParam {Name="MedicineId", IsViewObjectJson= false, Value= "" }

                },
                OnButtonExecute = async (self) => self.ResponseObject = await crudInstance.AssignCanisterAsync(
                                            self.RequestParamsValueAsString(0), self.RequestParamsValueAsString(1)),
            });
        }
    }
}
