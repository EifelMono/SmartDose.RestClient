﻿using System.Collections.Generic;
using Models = SmartDose.RestDomain.Models.V2;
using Cruds = SmartDose.RestClient.Cruds.V2;

namespace SmartDose.RestClientApp.Views.V2.MasterData
{
    public class ViewCustomer : ViewCruds
    {
        public ViewCustomer() : base()
        {
            var labelName = "Customer";
            var labelIdName = "CustomerCode";
            var crudInstance = Cruds.MasterData.Customer.Instance;
            _labelHeader.Content = crudInstance.UrlClone;

            _tabControl.Items.Add(new ViewTabItemReadList<Models.MasterData.Customer>
            {
                OnButtonExecute = async (self) => self.ResponseObject = await crudInstance.ReadListAsync(),
            });

            _tabControl.Items.Add(new ViewTabItemRead<Models.MasterData.Customer>
            {
                RequestParams = new List<ViewParam>
                {
                    new ViewParam {Name=labelIdName, IsViewObjectJson= false, Value= "" },
                },
                OnButtonExecute = async (self) => self.ResponseObject = await crudInstance.ReadAsync(
                                                    self.RequestParamsValueAsString(0)),
            });

            _tabControl.Items.Add(new ViewTabItemCreate<Models.MasterData.Customer>
            {
                RequestParams = new List<ViewParam>
                {
                    new ViewParam {Name=labelName, IsViewObjectJson= true, Value= new Models.MasterData.Customer() }
                },
                OnButtonExecute = async (self) => self.ResponseObject = await crudInstance.CreateAsync(
                                                    self.RequestParamsValueAsT(0)),
            });

            _tabControl.Items.Add(new ViewTabItemUpdate<Models.MasterData.Customer>
            {
                RequestParams = new List<ViewParam>
                {
                    new ViewParam {Name=labelName, IsViewObjectJson= true, Value= new Models.MasterData.Customer() }
                },
                OnButtonExecute = async (self) => self.ResponseObject = await crudInstance.UpdateAsync(
                                                    self.RequestParamsValueAsT(0).CustomerCode,
                                                    self.RequestParamsValueAsT(0))
            });

            _tabControl.Items.Add(new ViewTabItemDelete<Models.MasterData.Customer>
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
