using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartDose.RestClientApp.Views
{
    class CrudTabItemCreate<T> : CruidTabItem
    {
        protected ObjectJsonView _requestObjectJsonView;
        public CrudTabItemCreate()
        {
            Header = "Create";
            _requestObjectJsonView = new ObjectJsonView();
            _gridRequest.Children.Add(_requestObjectJsonView);
        }

        public T RequestObject { get => (T)_requestObjectJsonView.Data; set => _requestObjectJsonView.Data = value; }

        public override void ButtonExecute()
        {
            OnButtonExecute?.Invoke(this);
        }
        public Action<CrudTabItemCreate<T>> OnButtonExecute { get; set; }
    }
}
