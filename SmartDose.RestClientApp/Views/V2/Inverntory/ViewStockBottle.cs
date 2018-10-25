using System.Collections.Generic;
using Models = SmartDose.RestDomain.Models.V2;
using Crud = SmartDose.RestClient.Crud.V2;

namespace SmartDose.RestClientApp.Views.V2.Inventory
{
    public class ViewStockBottle : ViewCrud
    {
        public ViewStockBottle() : base()
        {
            var labelName = "StockBottel";
            var labelIdName = "StockBottelCode";
            var crudInstance = Crud.Inventory.StockBottle.Instance;
            _labelHeader.Content = crudInstance.UrlClone;

            _tabControl.Items.Add(new ViewTabItemCreate<Models.Inventory.StockBottle>
            {
                Header="Create StockBottel",
                RequestParams = new List<ViewParam>
                {
                    new ViewParam {Name=labelName, IsViewObjectJson= true, Value= new Models.Inventory.StockBottle() }
                },
                OnButtonExecute = async (self) => self.ResponseObject = await crudInstance.CreateAsync(
                                                    self.RequestParamsValueAsT(0)),
            });
            _tabControl.Items.Add(new ViewTabItemCreate<Models.Inventory.StockBottle>
            {
                Header = "Read StockBottel",
                RequestParams = new List<ViewParam>
                {
                    new ViewParam {Name=labelIdName, IsViewObjectJson= false, Value= "" }
                },
                OnButtonExecute = async (self) => self.ResponseObject = await crudInstance.ReadAsync(
                                                    self.RequestParamsValueAsString(0)),
            });
            _tabControl.Items.Add(new ViewTabItemCreate<Models.Inventory.StockBottle>
            {
                Header = "Update StockBottel",
                RequestParams = new List<ViewParam>
                {
                    new ViewParam {Name=labelName, IsViewObjectJson= true, Value= new Models.Inventory.StockBottle() }
                },
                OnButtonExecute = async (self) => self.ResponseObject = await crudInstance.UpdateAsync(
                                                    self.RequestParamsValueAsT(0)),
            });
            _tabControl.Items.Add(new ViewTabItemCreate<Models.Inventory.StockBottle>
            {
                Header = "Delete StockBottel",
                RequestParams = new List<ViewParam>
                {
                    new ViewParam {Name=labelIdName, IsViewObjectJson= false, Value= "" }
                },
                OnButtonExecute = async (self) => self.ResponseObject = await crudInstance.DeleteAsync(
                                                    self.RequestParamsValueAsString(0)),
            });
        }
    }
}
