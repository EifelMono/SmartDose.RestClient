using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartDose.RestClientApp.Views
{
    class ViewTabItemReadList<T> : ViewTabItem
    {
        public ViewTabItemReadList()
        {
            Header = "Read List";
        }

        public override void ButtonExecute()
        {
            OnButtonExecute?.Invoke(this);
        }
        public Action<ViewTabItemReadList<T>> OnButtonExecute { get; set; }
    }
}
