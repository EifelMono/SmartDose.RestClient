using System.Collections.Generic;
using Models = SmartDose.RestDomain.Models.V1;
using Crud = SmartDose.RestClient.Crud.V1;
using System.Windows;

namespace SmartDose.RestClientApp.Views.V1.Production
{
    public class ViewOrder : ViewCrud
    {
        public ViewOrder() : base()
        {
            var labelName = "Order";
            var labelIdName = "Identifier";
            var crudInstance = Crud.Production.Order.Instance;
            _labelHeader.Content = crudInstance.UrlClone;

            _tabControl.Items.Add(new ViewTabItemCreate<Models.Production.RestExternalOrder>
            {
                Header = "Create Order",
                RequestParams = new List<ViewParam>
                {
                    new ViewParam {Name="Create Dummy Medicine" , IsViewObjectJson= false, Value= false },
                    new ViewParam {Name="Check Medicine" , IsViewObjectJson= false, Value= true },
                    new ViewParam {Name=labelName, IsViewObjectJson= true, Value= new Models.Production.RestExternalOrder() }
                },
                OnButtonExecute = async (self) =>
                {
                    if (self.RequestParamsValueAsBool(0))
                    {
                        foreach (var medicine in self.RequestParamsValueAsT(2).UsedMedicinesIdsAndName)
                            await Crud.MasterData.Medicine.Instance.CreateAsync(Models.MasterData.Medicine.Dummy(medicine.Id, medicine.Name));
                    }
                    self.ResponseObject = await crudInstance.CreateAsync(self.RequestParamsValueAsT(2), self.RequestParamsValueAsBool(1));
                },
            });

            _tabControl.Items.Add(new ViewTabItemDelete<Models.Production.RestExternalOrder>
            {
                RequestParams = new List<ViewParam>
                {
                    new ViewParam {Name=labelIdName, IsViewObjectJson= false, Value= "" },
                },
                OnButtonExecute = async (self) => self.ResponseObject = await crudInstance.DeleteAsync(
                                        self.RequestParamsValueAsString(0)),
            });

            _tabControl.Items.Add(new ViewTabItemCreate<Models.Production.ExternalOrder>
            {
                Header = "GetOrderResultByIdentifier",
                RequestParams = new List<ViewParam>
                {
                    new ViewParam {Name=labelIdName, IsViewObjectJson= false, Value= "" }
                },
                OnButtonExecute = async (self) => self.ResponseObject = await crudInstance.GetOrderResultByIdentifierAsync(
                                                    self.RequestParamsValueAsString(0)),
            });

            _tabControl.Items.Add(new ViewTabItemCreate<Models.Production.ExternalOrder>
            {
                Header = "GetBasicOrderInfoByIdentifier",
                RequestParams = new List<ViewParam>
                {
                    new ViewParam {Name=labelIdName, IsViewObjectJson= false, Value= "" }
                },
                OnButtonExecute = async (self) => self.ResponseObject = await crudInstance.GetBasicOrderInfoByIdentifierAsync(
                                                    self.RequestParamsValueAsString(0)),
            });

            _tabControl.Items.Add(new ViewTabItemCreate<Models.Production.ExternalOrder>
            {
                Header = "GetResultUsageByCustomerId",
                RequestParams = new List<ViewParam>
                {
                    new ViewParam {Name="CustomerId", IsViewObjectJson= false, Value= "" },
                    new ViewParam {Name="Start (yyyy-mm-dd)", IsViewObjectJson= false, Value= "" },
                    new ViewParam {Name="End (yyyy-mm-dd)", IsViewObjectJson= false, Value= "" }
                },
                OnButtonExecute = async (self) => self.ResponseObject = await crudInstance.GetResultUsageByCustomerIdAsync(
                                                    self.RequestParamsValueAsString(0), self.RequestParamsValueAsString(1), self.RequestParamsValueAsString(2)),
            });

            _tabControl.Items.Add(new ViewTabItemCreate<Models.Production.ExternalOrder>
            {
                Header = "GetLifeOrders",
                RequestParams = new List<ViewParam>
                {
                },
                OnButtonExecute = async (self) => self.ResponseObject = await crudInstance.GetLifeOrdersAsync(),
            });

            _tabControl.Items.Add(new ViewTabItemCreate<Models.Production.ExternalOrder>
            {
                Header = "GetArchivedOrders",
                RequestParams = new List<ViewParam>
                {
                },
                OnButtonExecute = async (self) => self.ResponseObject = await crudInstance.GetArchivedOrdersAsync(),
            });

            _tabControl.Items.Add(new ViewTabItemCreate<Models.Production.ExternalOrder>
            {
                Header = "GetOrders",
                RequestParams = new List<ViewParam>
                {
                },
                OnButtonExecute = async (self) => self.ResponseObject = await crudInstance.GetOrdersAsync(),
            });


        }
    }
}
