﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using SmartDose.RestClientApp.Globals;
using SmartDose.WcfClient;
using SmartDose.Core.Extensions;
using static SmartDose.Core.SafeExecuter;
using System.Windows.Input;

namespace SmartDose.RestClientApp.Views
{
    /// <summary>
    /// Interaction logic for ObjectJsonWindow.xaml
    /// </summary>
    public partial class WindowObjectJson : Window, INotifyPropertyChanged
    {
        public WindowObjectJson()
        {
            InitializeComponent();
            DataContext = this;
            CreateObjectTree();
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));


        protected MenuItem RootMenuItem;
        protected MenuItem RestClientsMenuItem;
        protected MenuItem WcfClientsMenuItem;

        private void CreateObjectTree()
        {
            ViewConnections viewConnections;
            RootMenuItem = new MenuItem
            {
                Title = "SmartDose",
                IsExpanded = true,
                IsSelected = true,
                ModelsItem = new RestDomain.Models.ModelsItem
                {
                    Type = typeof(ViewConnections),
                    Value = new ViewConnections()
                }
            };
            viewConnections = new ViewConnections { RootMenuItem = RootMenuItem, RefreshMain = LocalRefreshMain };
            RootMenuItem.ModelsItem.Value = viewConnections;

            #region Rest
            RestClientsMenuItem = RootMenuItem.Add(new MenuItem
            {
                Title = "Rest clients",
                IsExpanded = true,
                IsSelected = true,
                ModelsItem = new RestDomain.Models.ModelsItem
                {
                    Type = typeof(ViewConnections),
                    Value = viewConnections
                }
            });

            #region Models
            var modelsMenuItem = RestClientsMenuItem.Add("Models");

            foreach (var modelsVersionGroup in RestDomain.Models.ModelsGlobals.ModelsItems.GroupBy(i => i.Version).OrderBy(g => g.Key))
            {
                var modelsVersionMenuItem = modelsMenuItem.Add(modelsVersionGroup.Key);
                foreach (var modelsGroupGroup in modelsVersionGroup.GroupBy(i => i.Group).OrderBy(g => g.Key))
                {
                    var modelsGroupMenuItem = modelsVersionMenuItem.Add(modelsGroupGroup.Key);
                    foreach (var value in modelsGroupGroup.OrderBy(i => i.Name))
                        modelsGroupMenuItem.Add(value.Name, value);
                }
            }

            #endregion

            #region Cruds
            var crudMenuItem = RestClientsMenuItem.Add("Cruds", true);
            var crudMenuV1Item = crudMenuItem.Add("V1", true);
            var crudMenuV1InverntoryItem = crudMenuV1Item.Add("Inventory", true);
            crudMenuV1InverntoryItem.Add("MedicineContainer", new RestDomain.Models.ModelsItem { Type = typeof(V1.Inventory.ViewMedicineContainer) });
            var crudMenuV1MasterDataItem = crudMenuV1Item.Add("MasterData", true);
            crudMenuV1MasterDataItem.Add("Canister", new RestDomain.Models.ModelsItem { Type = typeof(V1.MasterData.ViewCanister) });
            crudMenuV1MasterDataItem.Add("Customer", new RestDomain.Models.ModelsItem { Type = typeof(V1.MasterData.ViewCustomer) });
            crudMenuV1MasterDataItem.Add("Medicine", new RestDomain.Models.ModelsItem { Type = typeof(V1.MasterData.ViewMedicine) });
            crudMenuV1MasterDataItem.Add("Manufacturer", new RestDomain.Models.ModelsItem { Type = typeof(V1.MasterData.ViewManufacturer) });
            crudMenuV1MasterDataItem.Add("RefillCanister", new RestDomain.Models.ModelsItem { Type = typeof(V1.MasterData.ViewRefillCanister) });
            var crudMenuV1ProdcutionItem = crudMenuV1Item.Add("Production", true);
            crudMenuV1ProdcutionItem.Add("Order", new RestDomain.Models.ModelsItem { Type = typeof(V1.Production.ViewOrder) });

            var crudMenuV2Item = crudMenuItem.Add("V2", true);
            var crudMenuV2InverntoryItem = crudMenuV2Item.Add("Inventory", true);
            crudMenuV2InverntoryItem.Add("MedicineStock", new RestDomain.Models.ModelsItem { Type = typeof(V2.Inventory.ViewMedicineStock) });
            crudMenuV2InverntoryItem.Add("StockBottle", new RestDomain.Models.ModelsItem { Type = typeof(V2.Inventory.ViewStockBottle) });
            var crudMenuV2MasterDataItem = crudMenuV2Item.Add("MasterData", true);
            crudMenuV2MasterDataItem.Add("Customer", new RestDomain.Models.ModelsItem { Type = typeof(V2.MasterData.ViewCustomer) });
            crudMenuV2MasterDataItem.Add("Medicine", new RestDomain.Models.ModelsItem { Type = typeof(V2.MasterData.ViewMedicine) });
            crudMenuV2MasterDataItem.Add("DestinationFacility", new RestDomain.Models.ModelsItem { Type = typeof(V2.MasterData.ViewDestinationFacility) });
            crudMenuV2MasterDataItem.Add("Manufacturer", new RestDomain.Models.ModelsItem { Type = typeof(V2.MasterData.ViewManufacturer) });
            crudMenuV2MasterDataItem.Add("Patient", new RestDomain.Models.ModelsItem { Type = typeof(V2.MasterData.ViewPatient) });
            crudMenuV2MasterDataItem.Add("Pharmacy", new RestDomain.Models.ModelsItem { Type = typeof(V2.MasterData.ViewPharmacy) });
            var crudMenuV2ProdcutionItem = crudMenuV2Item.Add("Production", true);
            crudMenuV2ProdcutionItem.Add("Order", new RestDomain.Models.ModelsItem { Type = typeof(V2.Production.ViewOrder) });
            #endregion

            #endregion

            #region Wcf
            WcfClientsMenuItem = RootMenuItem.Add(new MenuItem
            {
                Title = "Wcf clients",
                IsExpanded = true,
                IsSelected = false,
                ModelsItem = new RestDomain.Models.ModelsItem
                {
                    Type = typeof(ViewConnections),
                    Value = viewConnections
                }
            });
            AddWcfClientsMenues();
            #endregion

            treeViewModels.Items.Add(RootMenuItem);
        }

        protected void LocalRefreshMain()
        {
            AddWcfClientsMenues();
        }

        public void AddWcfClientsMenues()
        {
            Cursor = Cursors.Wait;
            foreach (var group in WcfClientsMenuItem.Items)
            {
                foreach (var wcfMenuItem in group.Items)
                {
                    if (wcfMenuItem?.ModelsItem?.Value is ViewWcfClient view)
                    {
                        Catcher(() =>
                        {
                            view.PrepareForStop();
                            wcfMenuItem.ModelsItem.Value = null;
                        });
                    }
                    wcfMenuItem.IsSelected = false;
                }
                group.IsSelected = false;
                group.Items.Clear();
            }
            WcfClientsMenuItem.Items.Clear();

            foreach (var group in AppGlobals.Configuration.Data.WcfClients
                .Where(wc=> wc.Active).OrderBy(wc=> wc.Group).Select(wc => wc.Group).Distinct())
            {
                var grouItem = new MenuItem
                {
                    Title = group,
                    IsExpanded = true,
                    IsSelected = false,
                };
                WcfClientsMenuItem.Add(grouItem);
                foreach (var wcfItem in AppGlobals.Configuration.Data.WcfClients
                    .Where(wc => wc.Group == group).OrderBy(wc => wc.ConnectionName))
                {
                    if (wcfItem.Active)
                        grouItem.Add(new MenuItem
                        {
                            Title = string.IsNullOrEmpty(wcfItem.ConnectionName) ? $"[{wcfItem.ConnectionString}]" : wcfItem.ConnectionName,
                            IsExpanded = true,
                            IsSelected = false,
                            ModelsItem = new RestDomain.Models.ModelsItem
                            {
                                Type = typeof(ViewWcfClient),
                                Value = new ViewWcfClient().Pipe(v => v.WcfItem = wcfItem)
                            }
                        });
                }
            }
            NotifyPropertyChanged(string.Empty);
            Cursor = null;
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

        public MenuItem Add(MenuItem menuItem)
        {
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
