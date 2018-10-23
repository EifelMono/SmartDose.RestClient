using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartDose.RestClientApp.Views
{
    class ViewTabItemCreate<T> : ViewTabItem
    {
        private ViewObjectJson _requestViewObjectJson;
        public ViewTabItemCreate()
        {
            Header = "Create";
            _requestViewObjectJson = new ViewObjectJson();
            _gridRequest.Children.Add(_requestViewObjectJson);
        }

        public T RequestObject { get => (T)_requestViewObjectJson.Data; set => _requestViewObjectJson.Data = value; }

        public override void ButtonExecute()
        {
            OnButtonExecute?.Invoke(this);
        }
        public Action<ViewTabItemCreate<T>> OnButtonExecute { get; set; }
        protected ViewObjectJson RequestViewObjectJson { get => _requestViewObjectJson; set => _requestViewObjectJson = value; }
    }
}
