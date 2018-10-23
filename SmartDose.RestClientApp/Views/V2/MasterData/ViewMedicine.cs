using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsV2 = SmartDose.RestDomain.Models.V2;
using CrudV2 = SmartDose.RestClient.Crud.V2;

namespace SmartDose.RestClientApp.Views.V2.MasterData
{
    public class ViewMedicine : ViewTabControl
    {
        public ViewMedicine() : base()
        {
            Items.Add(new ViewTabItemCreate<ModelsV2.MasterData.Medicine>
            {
                RequestObject = new ModelsV2.MasterData.Medicine(),
                OnButtonExecute = async (self) => self.ResponseObject = await CrudV2.MasterData.Medicine.Instance.CreateAsync(self.RequestObject),
            });
        }
    }
}
