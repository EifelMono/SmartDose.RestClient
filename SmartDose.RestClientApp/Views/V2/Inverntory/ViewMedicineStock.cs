﻿using System.Collections.Generic;
using Models = SmartDose.RestDomain.Models.V2;
using Cruds = SmartDose.RestClient.Cruds.V2;

namespace SmartDose.RestClientApp.Views.V2.Inventory
{
    public class ViewMedicineStock : ViewCruds
    {
        public ViewMedicineStock() : base()
        {
#pragma warning disable CS0219 // Variable is assigned but its value is never used
            var labelName = "MedicineStock";
            var labelIdName = "MedicineStockCode";
#pragma warning restore CS0219 // Variable is assigned but its value is never used
            var crudInstance = Cruds.Inventory.MedicineStock.Instance;
            _labelHeader.Content = crudInstance.UrlClone;

            _tabControl.Items.Add(new ViewTabItemCreate<Models.Inventory.StockBottle>
            {
                Header = "GetStockBottles",
                RequestParams = new List<ViewParam>
                {
                },
                OnButtonExecute = async (self) => self.ResponseObject = await crudInstance.GetMedicineStocksAsync()
            });

            _tabControl.Items.Add(new ViewTabItemCreate<Models.Inventory.StockBottle>
            {
                Header = "GetMedicineStock",
                RequestParams = new List<ViewParam>
                {
                        new ViewParam {Name="MedicineCode", IsViewObjectJson= false, Value= "" },
                },
                OnButtonExecute = async (self) => self.ResponseObject = await crudInstance.GetMedicineStockAsync(self.RequestParamsValueAsString(0))
            });

            _tabControl.Items.Add(new ViewTabItemCreate<Models.Inventory.StockBottle>
            {
                Header = "GetStockBottles",
                RequestParams = new List<ViewParam>
                {
                        new ViewParam {Name="MedicineCode", IsViewObjectJson= false, Value= "" },
                },
                OnButtonExecute = async (self) => self.ResponseObject = await crudInstance.GetStockBottlesAsync(self.RequestParamsValueAsString(0))
            });
        }
    }
}


