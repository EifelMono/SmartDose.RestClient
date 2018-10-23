﻿using System;
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
            DataContext = this;
            CreateObjectTree();
        }


        private void CreateObjectTree()
        {
            var rootMenuItem = new MenuItem { Title = "SmartDose.Rest" };
            var modelsMenueItem = rootMenuItem.Add("Models");

            foreach (var modelsVersionGroup in RestDomain.Models.ModelsGlobals.ModelsItems.GroupBy(i => i.Version).OrderBy(g => g.Key))
            {
                var modelsVersionMenuItem = modelsMenueItem.Add(modelsVersionGroup.Key);
                foreach (var modelsGroupGroup in modelsVersionGroup.GroupBy(i => i.Group).OrderBy(g => g.Key))
                {
                    var modelsGroupMenuItem = modelsVersionMenuItem.Add(modelsGroupGroup.Key);
                    foreach (var value in modelsGroupGroup.OrderBy(i => i.Name))
                        modelsGroupMenuItem.Add(value.Name, value);
                }
            }

            var crudMenuItem = rootMenuItem.Add("CRUD");
            var crudMenuV1Item = crudMenuItem.Add("V1");
            var crudMenuV1MasterDataItem = crudMenuV1Item.Add("MasterData");
            crudMenuV1MasterDataItem.Add("Medicine", new RestDomain.Models.ModelsItem { Type = typeof(V1.MasterData.ViewMedicine) });


            var crudMenuV2Item = crudMenuItem.Add("V2");
            var crudMenuV2MasterDataItem = crudMenuV2Item.Add("MasterData");
            crudMenuV2MasterDataItem.Add("Medicine", new RestDomain.Models.ModelsItem { Type = typeof(V2.MasterData.ViewMedicine) });


            treeViewModels.Items.Add(rootMenuItem);
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.NewValue is MenuItem menuItem)
            {
                if (menuItem.ModelsItem is RestDomain.Models.ModelsItem modelsItem)
                {
                    if (modelsItem.Type.FullName.Contains(".Models."))
                    {
                        if (modelsItem.Value is null)
                            modelsItem.Value = Activator.CreateInstance(modelsItem.Type);
                        objectJsonView.Data = modelsItem.Value;
                        if (!gridContent.Children.Contains(objectJsonView))
                        {
                            gridContent.Children.Clear();
                            gridContent.Children.Add(objectJsonView);
                        }
                    }
                    else
                    if (modelsItem.Type.FullName.Contains(".Views."))
                    {
                        if (modelsItem.Value is null)
                            modelsItem.Value = Activator.CreateInstance(modelsItem.Type);
                        if (!gridContent.Children.Contains(modelsItem.Value as UIElement))
                        {
                            gridContent.Children.Clear();
                            gridContent.Children.Add(modelsItem.Value as UIElement);
                        }
                    }
                    else gridContent.Children.Clear();
                }
            }
            else
                gridContent.Children.Clear();
        }
    }

    public class MenuItem
    {
        public string Title { get; set; }

        public RestDomain.Models.ModelsItem ModelsItem { get; set; }

        public List<MenuItem> Items { get; set; } = new List<MenuItem>();

        public MenuItem Add(string title, RestDomain.Models.ModelsItem modelsItem = null)
        {
            var menuItem = new MenuItem { Title = title, ModelsItem = modelsItem };
            Items.Add(menuItem);
            return menuItem;
        }
        public override string ToString()
                => $"{Title}";
    }
}
