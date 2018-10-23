using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartDose.RestClientApp.Views
{
    class ViewTabItemReadList<T> : ViewTabItem
    {
        protected ObjectJsonView _requestObjectJsonView;
        public ViewTabItemReadList()
        {
            Header = "Read List";
            _requestObjectJsonView = new ObjectJsonView();
            _gridRequest.Children.Add(_requestObjectJsonView);
        }

        public T RequestObject { get => (T)_requestObjectJsonView.Data; set => _requestObjectJsonView.Data = value; }

        public override void ButtonExecute()
        {
            OnButtonExecute?.Invoke(this);
        }
        public Action<ViewTabItemReadList<T>> OnButtonExecute { get; set; }
    }
}
