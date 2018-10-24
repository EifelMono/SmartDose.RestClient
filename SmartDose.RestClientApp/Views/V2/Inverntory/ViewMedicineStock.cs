using System.Collections.Generic;
using Models = SmartDose.RestDomain.Models.V2;
using Crud = SmartDose.RestClient.Crud.V2;

namespace SmartDose.RestClientApp.Views.V2.Inventory
{
    public class ViewMedicineStock : ViewCrud
    {
        public ViewMedicineStock() : base()
        {
#pragma warning disable CS0219 // Variable is assigned but its value is never used
            var labelName = "MedicineStock";
            var labelIdName = "MedicineStockCode";
#pragma warning restore CS0219 // Variable is assigned but its value is never used
            var crudInstance = Crud.Inventory.MedicineStock.Instance;

            _labelHeader.Content = GetType().Namespace;

            _tabControl.Items.Add(new ViewTabItemCreate<Models.Inventory.StockBottle>
            {
                Header = "GetStockBottles",
                RequestParams = new List<ViewParam>
                {
                },
                OnButtonExecute = async (self) => self.ResponseObject = await crudInstance.GetMedicineStocks()
            });

            _tabControl.Items.Add(new ViewTabItemCreate<Models.Inventory.StockBottle>
            {
                Header = "GetMedicineStock",
                RequestParams = new List<ViewParam>
                {
                        new ViewParam {Name="MedicineCode", IsViewObjectJson= false, Value= "" },
                },
                OnButtonExecute = async (self) => self.ResponseObject = await crudInstance.GetMedicineStock(self.RequestParamsValueAsString(0))
            });

            _tabControl.Items.Add(new ViewTabItemCreate<Models.Inventory.StockBottle>
            {
                Header = "GetStockBottles",
                RequestParams = new List<ViewParam>
                {
                        new ViewParam {Name="MedicineCode", IsViewObjectJson= false, Value= "" },
                },
                OnButtonExecute = async (self) => self.ResponseObject = await crudInstance.GetStockBottles(self.RequestParamsValueAsString(0))
            });

        }
    }
}


