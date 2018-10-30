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
        IWindowsFormsEditorService editorService;
        DateTimePicker picker = new DateTimePicker();

        public DateTimePickerEditor(string customFormat)
        {
            picker.Format = DateTimePickerFormat.Custom;
            picker.CustomFormat = customFormat;
            picker.MinDate = DateTimeGlobals.MinValue;
            picker.MaxDate = DateTimeGlobals.MaxValue;
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (provider != null)
            {
                this.editorService = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
            }

            if (this.editorService != null)
            {
                try
                {
                    picker.Value = (DateTime)value;
                }
                catch
                {
                    picker.Value = picker.MinDate;
                }
                this.editorService.DropDownControl(picker);
                value = picker.Value;
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
