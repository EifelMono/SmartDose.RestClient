using System.Collections.Generic;
using Models = SmartDose.RestDomain.Models.V1;
using Crud = SmartDose.RestClient.Crud.V1;

namespace SmartDose.RestClientApp.Views.V1.MasterData
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
                OnButtonExecute = async (self) => self.ResponseObject = await Crud.MasterData.Medicine.Instance.CreateAsync(self.RequestParams[0].Value as Models.MasterData.Medicine),
            });

            _tabControl.Items.Add(new ViewTabItemReadList<Models.MasterData.Medicine>
            {
                OnButtonExecute = async (self) => self.ResponseObject = await Crud.MasterData.Medicine.Instance.ReadListAsync(),
            });
        }
    }
}
