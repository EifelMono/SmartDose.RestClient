using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using SmartDose.RestDomain.Converter;

namespace SmartDose.RestDomainDev.PropertyEditorThings
{
    public class DateTimePickerEditor : UITypeEditor
    {
        IWindowsFormsEditorService _editorService;
        DateTimePicker _picker = new DateTimePicker();

        public DateTimePickerEditor(string customFormat)
        {
            _picker.Format = DateTimePickerFormat.Custom;
            _picker.CustomFormat = customFormat;
            _picker.MinDate = DateTimeGlobals.MinValue;
            _picker.MaxDate = DateTimeGlobals.MaxValue;
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (provider != null)
            {
                _editorService = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
            }

            if (_editorService != null)
            {
                try
                {
                    _picker.Value = (DateTime)value;
                }
                catch
                {
                    _picker.Value = _picker.MinDate;
                }
                _editorService.DropDownControl(_picker);
                value = _picker.Value;
            }

            return value;
        }
    }

    public class DateTime_yyyy_MM_dd_Editor : DateTimePickerEditor
    {
        public DateTime_yyyy_MM_dd_Editor() : base(DateTimeGlobals.DateTime_yyyy_MM_dd) { }
    }

    public class DateTime_yyyy_MM_ddTHH_mm_ssZ_Editor : DateTimePickerEditor
    {
        public DateTime_yyyy_MM_ddTHH_mm_ssZ_Editor() : base(DateTimeGlobals.DateTime_yyyy_MM_ddTHH_mm_ssZ) { }
    }
}
