using System.Collections.Generic;
using Models = SmartDose.RestDomain.Models.V1;
using Crud = SmartDose.RestClient.Crud.V1;

namespace SmartDose.RestClientApp.Views.V1.MasterData
{
    public class ViewMedicine : ViewCrud
    {
        public ViewMedicine() : base()
        {
            _labelHeader.Content = typeof(ViewMedicine).Namespace;

            _tabControl.Items.Add(new ViewTabItemReadList<Models.MasterData.Medicine>
            {
                OnButtonExecute = async (self) => self.ResponseObject = await Crud.MasterData.Medicine.Instance.ReadListAsync(),
            });

            _tabControl.Items.Add(new ViewTabItemRead<Models.MasterData.Medicine>
            {
                RequestParams = new List<ViewParam>
                {
                    new ViewParam {Name="MedicineCode", IsViewObjectJson= false, Value= "" },
                },
                OnButtonExecute = async (self) => self.ResponseObject = await Crud.MasterData.Medicine.Instance.ReadAsync(self.RequestParamsValueAsString(0)),
            });

            _tabControl.Items.Add(new ViewTabItemCreate<Models.MasterData.Medicine>
            {
                RequestParams = new List<ViewParam>
                {
                    new ViewParam {Name="Medicine", IsViewObjectJson= true, Value= new Models.MasterData.Medicine() }
                },
                OnButtonExecute = async (self) => self.ResponseObject = await Crud.MasterData.Medicine.Instance.CreateAsync(
                                                    self.RequestParams[0].Value as Models.MasterData.Medicine),
            });

            _tabControl.Items.Add(new ViewTabItemUpdate<Models.MasterData.Medicine>
            {
                RequestParams = new List<ViewParam>
                {
                    new ViewParam {Name="MedicineCode", IsViewObjectJson= false, Value= "" },
                    new ViewParam {Name="Medicine", IsViewObjectJson= true, Value= new Models.MasterData.Medicine() }
                },
                OnButtonExecute = async (self) => self.ResponseObject = await Crud.MasterData.Medicine.Instance.UpdateAsync(
                                                    self.RequestParamsValueAsString(0),
                                                    self.RequestParamsValueAsT(1))
            });

            _tabControl.Items.Add(new ViewTabItemDelete<Models.MasterData.Medicine>
            {
                RequestParams = new List<ViewParam>
                {
                    new ViewParam {Name="MedicineCode", IsViewObjectJson= false, Value= "" },
                },
                OnButtonExecute = async (self) => self.ResponseObject = await Crud.MasterData.Medicine.Instance.DeleteAsync(self.RequestParamsValueAsString(0)),
            });
        }
    }
}
