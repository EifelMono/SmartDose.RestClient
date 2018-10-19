using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using SmartDose.RestClientApp.Globals;

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
            var crudMenuItem = rootMenuItem.Add("CRUD");
            foreach (var modelsVersionGroup in ModelsGlobals.ModelsItems.GroupBy(i => i.Version).OrderBy(g => g.Key))
            {
                var modelsVersionMenuItem = modelsMenueItem.Add(modelsVersionGroup.Key);
                var crudVersionMenuItem = crudMenuItem.Add(modelsVersionGroup.Key);
                foreach (var modelsGroupGroup in modelsVersionGroup.GroupBy(i => i.Group).OrderBy(g => g.Key))
                {
                    var modelsGroupMenuItem = modelsVersionMenuItem.Add(modelsGroupGroup.Key);
                    var crudGroupMenuItem = crudVersionMenuItem.Add(modelsGroupGroup.Key);
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
