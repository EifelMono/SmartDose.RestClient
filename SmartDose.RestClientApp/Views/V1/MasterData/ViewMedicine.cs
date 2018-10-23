using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models = SmartDose.RestDomain.Models.V1;
using Crud = SmartDose.RestClient.Crud.V1;

namespace SmartDose.RestClientApp.Views.V1.MasterData
{
    public class ViewMedicine : CruidView
    {
        public ViewMedicine() : base()
        {
            _labelHeader.Content = typeof(ViewMedicine).Namespace;
            _tabControl.Items.Add(new ViewTabItemCreate<Models.MasterData.Medicine>
            {
                RequestObject = new Models.MasterData.Medicine(),
                OnButtonExecute = async (self) => self.ResponseObject = await Crud.MasterData.Medicine.Instance.CreateAsync(self.RequestObject),
            });

            _tabControl.Items.Add(new ViewTabItemReadList<Models.MasterData.Medicine>
            {
                OnButtonExecute = async (self) => self.ResponseObject = await Crud.MasterData.Medicine.Instance.ReadListAsync(),
            });
        }
    }
}
