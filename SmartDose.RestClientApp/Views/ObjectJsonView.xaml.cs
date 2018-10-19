using System.Collections.Generic;
using System.IO;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml;
using Flurl.Http;
using ICSharpCode.AvalonEdit.Folding;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using ICSharpCode.AvalonEdit.Search;
using SmartDose.RestClientApp.Globals;

namespace SmartDose.RestClientApp.Views
{
    /// <summary>
    /// Interaction logic for ObjectJsonView.xaml
    /// </summary>
    public partial class ObjectJsonView : UserControl
    {
        public FoldingManager FoldingManager;
        public JsonFoldingStrategy JsonFoldingStrategy = new JsonFoldingStrategy();
        public ObjectJsonView()
        {
            InitializeComponent();

            using (var stream = new FileStream("json.xshd", FileMode.Open))
            {
                using (var reader = new XmlTextReader(stream))
                {
                    textEditor.SyntaxHighlighting = HighlightingLoader.Load(reader, HighlightingManager.Instance);
                }
            }
            textEditor.ShowLineNumbers = true;
            textEditor.WordWrap = false;

            SearchPanel.Install(textEditor);

            textEditor.ContextMenu = new System.Windows.Controls.ContextMenu();
            textEditor.ContextMenu.Items.Add(new System.Windows.Controls.MenuItem
            {
                Header = "Select all",
                Command = ApplicationCommands.SelectAll
            });
            textEditor.ContextMenu.Items.Add(new System.Windows.Controls.Separator());
            textEditor.ContextMenu.Items.Add(new System.Windows.Controls.MenuItem
            {
                Header = "Copy",
                Command = ApplicationCommands.Copy
            });




            propertyView.PropertyValueChanged += (s, e) =>
            {
                e?.ChangedItem?.Value?.FillEmtpyModels();
            };

            propertyView.SelectedGridItemChanged += (s, e) =>
            {
            };

            tabControlMain.SelectionChanged += (s, e) =>
            {
            };
        }

        private void ActivateCodeFolding()
        {
            if (FoldingManager == null)
                FoldingManager = FoldingManager.Install(textEditor.TextArea);

            JsonFoldingStrategy.UpdateFoldings(FoldingManager, textEditor.Document);
        }

        ObjectJsonData _Data;
        public ObjectJsonData Data
        {
            get => _Data;
            set
            {
                _Data = value;
                propertyView.SelectedObject = _Data.Value.FillEmtpyModels();
                textEditor.Text = Data?.Value.ToJson();
                ActivateCodeFolding();
            }
        }

        private void tabControlMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            textEditor.Text = Data?.Value.ToJson();
            ActivateCodeFolding();
        }

        private async void buttonClick_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var o = await "http://127.0.0.1:56040/SmartDose/V2.0/MasterData/Medicines".GetJsonAsync<List<RestDomain.Models.V2.MasterData.Medicine>>();
            "x".LogInformation();
        }
    }
}
