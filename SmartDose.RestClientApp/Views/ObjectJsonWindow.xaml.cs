using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SmartDose.RestClientApp.Globals;
using SmartDose.RestDomain.V2.Models.MasterData;

namespace SmartDose.RestClientApp.Views
{
    /// <summary>
    /// Interaction logic for ObjectJsonWindow.xaml
    /// </summary>
    public partial class ObjectJsonWindow : Window
    {
        public ObjectJsonWindow()
        {
            InitializeComponent();
            var rootMenuItem = new MenuItem { Title = "SmartDose.Rest" };
            var modelsMenueItem = rootMenuItem.Add("Models");
            var callsMenuItem = rootMenuItem.Add("Calls");
            foreach (var modelsVersionGroup in ModelsGlobals.ModelsItems.GroupBy(i => i.Version).OrderBy(g => g.Key))
            {
                var modelsVersionMenuItem = modelsMenueItem.Add(modelsVersionGroup.Key);
                var callsVersionMenuItem = callsMenuItem.Add(modelsVersionGroup.Key);
                foreach (var modelsGroupGroup in modelsVersionGroup.GroupBy(i => i.Group).OrderBy(g => g.Key))
                {
                    var modelsGroupMenuItem = modelsVersionMenuItem.Add(modelsGroupGroup.Key);
                    var callsGroupMenuItem = callsVersionMenuItem.Add(modelsGroupGroup.Key);
                    foreach (var value in modelsGroupGroup.OrderBy(i => i.Name))
                        modelsGroupMenuItem.Add(value.Name, value);
                }
            }
            treeViewModels.Items.Add(rootMenuItem);
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var data = new ObjectJsonData { };
            if (e.NewValue is MenuItem menuItem)
            {
                if (menuItem.ModelsItem is ModelsItem modelsItem)
                {
                    if (modelsItem.Value is null)
                        modelsItem.Value = Activator.CreateInstance(modelsItem.Type);
                    data.Value = modelsItem.Value;
                    data.Directory = data.Value.ModelsDirectory();
                }
            }

            objectJsonView.Data = data;
        }
    }



    public class MenuItem
    {
        public string Title { get; set; }

        public ModelsItem ModelsItem { get; set; }

        public List<MenuItem> Items { get; set; } = new List<MenuItem>();

        public MenuItem Add(string title, ModelsItem modelsItem = null)
        {
            var menuItem = new MenuItem { Title = title, ModelsItem = modelsItem };
            Items.Add(menuItem);
            return menuItem;
        }
    }
}
