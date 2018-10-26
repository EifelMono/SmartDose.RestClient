using System.Collections.Generic;
using Models = SmartDose.RestDomain.Models.V2;
using Crud = SmartDose.RestClient.Crud.V2;
using System.Windows;

namespace SmartDose.RestClientApp.Views.V2.Production
{
    public class ViewOrder : ViewCrud
    {
        public ViewOrder() : base()
        {
            var labelName = "Order";
            var labelIdName = "OrderCode";
            var crudInstance = Crud.Production.Order.Instance;
            _labelHeader.Content = crudInstance.UrlClone;

            _tabControl.Items.Add(new ViewTabItemCreate<Models.Production.Order>
            {
                RequestParams = new List<ViewParam>
                {
                    new ViewParam {Name="Create Dummy Medicine" , IsViewObjectJson= false, Value= false },
                    new ViewParam {Name=labelName, IsViewObjectJson= true, Value= new Models.Production.Order() }
                },
                OnButtonExecute = async (self) =>
                {
                    if (self.RequestParamsValueAsBool(0))
                    {
                        MessageBox.Show("Todo Create Medicine");
                        foreach (var medicinesId in self.RequestParamsValueAsT(1).UsedMedicinesIdsAndName)
                        {
                     
                        }
                    }
                    self.ResponseObject = await crudInstance.CreateAsync(self.RequestParamsValueAsT(1));
                },
            });

            _tabControl.Items.Add(new ViewTabItemDelete<Models.Production.Order>
            {
                RequestParams = new List<ViewParam>
                {
                    new ViewParam {Name=labelIdName, IsViewObjectJson= false, Value= "" },
                },
                OnButtonExecute = async (self) => self.ResponseObject = await crudInstance.DeleteAsync(
                                        self.RequestParamsValueAsString(0)),
            });

            _tabControl.Items.Add(new ViewTabItem<Models.Production.Order>
            {
                Header = "OrderResult (Get)",
                RequestParams = new List<ViewParam>
                {
                    new ViewParam {Name=labelIdName, IsViewObjectJson= false, Value= "" },
                },
                OnButtonExecute = async (self) => self.ResponseObject = await crudInstance.GetOrderResult(
                                        self.RequestParamsValueAsString(0)),
            });

            _tabControl.Items.Add(new ViewTabItem<Models.Production.Order>
            {
                Header = "OrderResultByTime (Get)",
                RequestParams = new List<ViewParam>
                {
                    new ViewParam {Name=labelIdName, IsViewObjectJson= false, Value= "" },
                },
                OnButtonExecute = async (self) => self.ResponseObject = await crudInstance.GetOrderResultByTime(
                                        self.RequestParamsValueAsString(0))
            });

            _tabControl.Items.Add(new ViewTabItem<Models.Production.Order>
            {
                Header = "List of OrderStatus (Get)",
                RequestParams = new List<ViewParam>
                {
                },
                OnButtonExecute = async (self) => self.ResponseObject = await crudInstance.GetOrderStatus()
            });

            _tabControl.Items.Add(new ViewTabItem<Models.Production.Order>
            {
                Header = "OrderStatus (Get)",
                RequestParams = new List<ViewParam>
                {
                    new ViewParam {Name=labelIdName, IsViewObjectJson= false, Value= "" },
                },
                OnButtonExecute = async (self) => self.ResponseObject = await crudInstance.GetOrderStatus(
                                        self.RequestParamsValueAsString(0))
            });
        }
    }
}
