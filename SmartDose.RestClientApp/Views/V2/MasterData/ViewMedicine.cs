using System.Collections.Generic;
using Models = SmartDose.RestDomain.Models.V2;
using Crud = SmartDose.RestClient.Crud.V2;

namespace SmartDose.RestClientApp.Views.V2.MasterData
{
    public class ViewMedicine : CruidView
    {
        public ViewMedicine() : base()
        {
            _labelHeader.Content = typeof(ViewMedicine).Namespace;
            _tabControl.Items.Add(new ViewTabItemCreate
            {
                RequestParams = new List<ViewParam>
                {
                    new ViewParam {Name="Medicine", IsViewObjectJson= true, Value= new Models.MasterData.Medicine() }
                },
                OnButtonExecute = async (self) => self.ResponseObject = await Crud.MasterData.Medicine.Instance.CreateAsync(
                                                    self.RequestParams[0].Value as Models.MasterData.Medicine),
            });

            _tabControl.Items.Add(new ViewTabItemUpdate
            {
                RequestParams = new List<ViewParam>
                {
                    new ViewParam {Name="MedicineCode", IsViewObjectJson= false, Value= "" },
                    new ViewParam {Name="Medicine", IsViewObjectJson= true, Value= new Models.MasterData.Medicine() }
                },
                OnButtonExecute = async (self) => self.ResponseObject = await Crud.MasterData.Medicine.Instance.UpdateAsync(
                                                    self.RequestParams[0].Value as string,
                                                    self.RequestParams[1].Value as Models.MasterData.Medicine),
            });

            _tabControl.Items.Add(new ViewTabItemReadList<Models.MasterData.Medicine>
            {
                OnButtonExecute = async (self) => self.ResponseObject = await Crud.MasterData.Medicine.Instance.ReadListAsync(),
            });


        }
    }
}
