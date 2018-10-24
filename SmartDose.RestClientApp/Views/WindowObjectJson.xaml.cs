﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using SmartDose.RestClientApp.Globals;

namespace SmartDose.RestClientApp.Views
{
    /// <summary>
    /// Interaction logic for ObjectJsonWindow.xaml
    /// </summary>
    public partial class WindowObjectJson : Window
    {
        public WindowObjectJson()
        {
            InitializeComponent();
            DataContext = this;
            CreateObjectTree();
        }


        private void CreateObjectTree()
        {
            var rootMenuItem = new MenuItem
            {
                Title = "SmartDose.Rest",
                IsExpanded = true,
                IsSelected = true,
                ModelsItem = new RestDomain.Models.ModelsItem
                {
                    Type = typeof(ViewConnections),
                    Value = new ViewConnections()
                }
            };
            rootMenuItem.ModelsItem.Value = new ViewConnections { RootMenuItem = rootMenuItem };

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

            var crudMenuItem = rootMenuItem.Add("CRUD", true);
            var crudMenuV1Item = crudMenuItem.Add("V1", true);
            var crudMenuV1MasterDataItem = crudMenuV1Item.Add("MasterData", true);
            crudMenuV1MasterDataItem.Add("Medicine", new RestDomain.Models.ModelsItem { Type = typeof(V1.MasterData.ViewMedicine) });
            crudMenuV1MasterDataItem.Add("Customer", new RestDomain.Models.ModelsItem { Type = typeof(V1.MasterData.ViewCustomer) });

            var crudMenuV2Item = crudMenuItem.Add("V2", true);
            var crudMenuV2MasterDataItem = crudMenuV2Item.Add("MasterData", true);
            crudMenuV2MasterDataItem.Add("Medicine", new RestDomain.Models.ModelsItem { Type = typeof(V2.MasterData.ViewMedicine) });
            crudMenuV2MasterDataItem.Add("Customer", new RestDomain.Models.ModelsItem { Type = typeof(V2.MasterData.ViewCustomer) });

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
                        viewObjectJson.Data = modelsItem.Value;
                        if (!gridContent.Children.Contains(viewObjectJson))
                        {
                            gridContent.Children.Clear();
                            gridContent.Children.Add(viewObjectJson);
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
                else
                    gridContent.Children.Clear();
            }
            else
                gridContent.Children.Clear();
        }
    }

    public class MenuItem : INotifyPropertyChanged
    {
        public string Title { get; set; }

        public RestDomain.Models.ModelsItem ModelsItem { get; set; }

        public ObservableCollection<MenuItem> Items { get; set; } = new ObservableCollection<MenuItem>();

        public MenuItem Add(string title, RestDomain.Models.ModelsItem modelsItem = null, bool isExpanded = false)
        {
            var menuItem = new MenuItem { Title = title, ModelsItem = modelsItem, IsExpanded = isExpanded };
            Items.Add(menuItem);
            return menuItem;
        }

        public MenuItem Add(string title, bool isExpanded)
            => Add(title, null, isExpanded);

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                if (value != _isSelected)
                {
                    _isSelected = value;
                    NotifyPropertyChanged("IsSelected");
                }
            }
        }

        private bool _isExpanded;
        public bool IsExpanded
        {
            get => _isExpanded;
            set
            {
                if (value != _isExpanded)
                {
                    _isExpanded = value;
                    NotifyPropertyChanged("IsExpanded");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

        public override string ToString()
                => $"{Title}";
    }
}