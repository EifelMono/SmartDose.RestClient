using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml;
using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Folding;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using ICSharpCode.AvalonEdit.Search;

namespace SmartDose.RestClientApp.Editor
{
    public class JsonEditor : TextEditor
    {
        public FoldingManager FoldingManager { get; set; }
        public JsonFoldingStrategy JsonFoldingStrategy { get; set; } = new JsonFoldingStrategy();
        public JsonEditor()
        {
            ShowLineNumbers = true;
            WordWrap = false;
            FontFamily = new FontFamily("Consolas");
            FontSize = 11;

            using (var stream = Assembly.GetAssembly(typeof(JsonEditor)).GetManifestResourceStream($"{typeof(JsonEditor).Namespace}.json.xshd"))
            {
                using (var reader = new XmlTextReader(stream))
                {
                    SyntaxHighlighting = HighlightingLoader.Load(reader, HighlightingManager.Instance);
                }
            }

            SearchPanel.Install(this);

            ContextMenu = new System.Windows.Controls.ContextMenu();
            ContextMenu.Items.Add(new System.Windows.Controls.MenuItem
            {
                Header = "Select all",
                Command = ApplicationCommands.SelectAll
            });
            ContextMenu.Items.Add(new System.Windows.Controls.Separator());
            ContextMenu.Items.Add(new System.Windows.Controls.MenuItem
            {
                Header = "Copy",
                Command = ApplicationCommands.Copy
            });
        }

        public new string Text
        {
            get => base.Text; set
            {
                base.Text = value;
                JsonFolding();
            }
        }

        private void JsonFolding()
        {
            if (FoldingManager == null)
                FoldingManager = FoldingManager.Install(TextArea);

            JsonFoldingStrategy.UpdateFoldings(FoldingManager, Document);
        }
    }
}
