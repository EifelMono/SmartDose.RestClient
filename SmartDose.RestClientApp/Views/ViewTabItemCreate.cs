using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartDose.RestClientApp.Views
{
    class ViewTabItemCreate : ViewTabItem 
    {
        public ViewTabItemCreate()
        {
            Header = "Create";
        }

        //public override void ButtonExecute()
        //{
        //    OnButtonExecute?.Invoke(this);
        //}
        //public Action<ViewTabItemCreate> OnButtonExecute { get; set; }
    }
}
