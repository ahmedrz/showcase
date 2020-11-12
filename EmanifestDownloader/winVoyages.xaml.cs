using EManifestClient.Core;
using EManifestClient.Model;
using EmanifestDownloader.Helper;
using EmanifestDownloader.Model;
using EManifestServices.DAL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;

namespace EmanifestDownloader
{
    /// <summary>
    /// Interaction logic for fromVoyages.xaml
    /// </summary>
    public partial class winVoyages : RadWindow
    {
        public List<VoyageDetails> Voyages { get; set; }
        public ValidVoyageResult ValidVoyage { get; set; }
        private DataDownloader<Vessels, Users, VoyageDetails, CountryCodes, LocationCodes, UNHSCodes, PackageCodes, Lines, ContainerIsoCodes, UNHazardousCodes> downloader;
        private ObservableCollection<CustomMenuItem> rowContextMenuItems;
        private void InitializeRowContextMenuItems()
        {
            ObservableCollection<CustomMenuItem> items = new ObservableCollection<CustomMenuItem>();
            CustomMenuItem editItem = new CustomMenuItem();
            editItem.Text = "View";
            items.Add(editItem);
            CustomMenuItem deleteItem = new CustomMenuItem();
            deleteItem.Text = "Approve";
            items.Add(deleteItem);
            CustomMenuItem ediItem = new CustomMenuItem();
            ediItem.Text = "Decline";
            items.Add(ediItem);
            this.rowContextMenuItems = items;
        }
        public winVoyages(ValidVoyageResult validVoyage, DataDownloader<Vessels, Users, VoyageDetails, CountryCodes, LocationCodes, UNHSCodes, PackageCodes, Lines, ContainerIsoCodes, UNHazardousCodes> downloader)
        {
            this.ValidVoyage = validVoyage;
            this.downloader = downloader;
            InitializeComponent();
            GetVoyages();
            DataContext = this;
            InitializeRowContextMenuItems();
        }

        private void GetVoyages()
        {
            if (ValidVoyage != null && downloader != null)
            {
                try
                {
                    Voyages = downloader.GetVoyages(ValidVoyage).GetAwaiter().GetResult();
                }
                catch (Exception ex)
                {
                    RadWindow.Alert(new DialogParameters { Owner = this, Content = ex.Message });
                }
            }
        }
        private GridViewRow GetContextMenuClickedRow(RadContextMenu menu)
        {
            return menu.GetClickedElement<GridViewRow>();
        }
        private void GridContextMenu_Opened(object sender, RoutedEventArgs e)
        {
            var contextMenu = sender as RadContextMenu;
            if (contextMenu == null)
                return;
            var clickedRow = GetContextMenuClickedRow(contextMenu);
            if (clickedRow != null)
            {
                this.voyagesGrid.SelectedItem = clickedRow.DataContext;
                foreach (var item in this.rowContextMenuItems)
                {
                    item.IsEnabled = true;
                }
                this.GridContextMenu.ItemsSource = this.rowContextMenuItems;
            }
            else
            {
                foreach (var item in this.rowContextMenuItems)
                {
                    if (item.Text != "Add")
                    {
                        item.IsEnabled = false;
                    }
                }
                this.GridContextMenu.ItemsSource = this.rowContextMenuItems;
            }
        }
        private void GridContextMenu_ItemClick(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            var selectedVoyage = voyagesGrid.SelectedItem as VoyageDetails;
            CustomMenuItem item = (e.OriginalSource as RadMenuItem).DataContext as CustomMenuItem;
            switch (item.Text)
            {
                case "View":
                    {
                        switch (selectedVoyage.ManifestType)
                        {
                            case "GeneralCargo":
                                {
                                    winLCLContainers window = new winLCLContainers(selectedVoyage);
                                    window.Owner = this;
                                    window.ShowDialog();
                                }
                                break;
                            case "FCLContainer":
                                {
                                    winFCLContainersWithRelation window = new winFCLContainersWithRelation(selectedVoyage);
                                    window.Owner = this;
                                    window.ShowDialog();
                                }
                                break;
                            case "LCLContainer":
                                {
                                    winLCLContainers window = new winLCLContainers(selectedVoyage);
                                    window.Owner = this;
                                    window.ShowDialog();
                                }
                                break;
                            case "FCLContainerRelationNotknown":
                                {
                                    winFCLContainersWithoutRelation window = new winFCLContainersWithoutRelation(selectedVoyage);
                                    window.Owner = this;
                                    window.ShowDialog();
                                }
                                break;
                            default:
                                break;
                        }

                    }
                    break;
                case "Approve":
                    {

                        RadWindow.Confirm(
                               new DialogParameters
                               {
                                   Owner = this,
                                   Content = "Are you sure you want to approve this manifest??",
                                   Closed = async (s, args) =>
                                     {
                                         var result = args.DialogResult;
                                         if (result == false)
                                         {
                                             //do nothing
                                         }
                                         else
                                         {
                                             var voyage = voyagesGrid.SelectedItem as VoyageDetails;
                                             try
                                             {
                                                 var succuess = await downloader.ApproveVoyage(voyage.VoyageDetailsId);
                                                 if (succuess)
                                                 {
                                                     RadWindow.Alert(new DialogParameters { Owner = this, Content = "Voyage approved." });
                                                     Close();
                                                 }
                                             }
                                             catch (Exception ex)
                                             {
                                                 RadWindow.Alert(new DialogParameters { Owner = this, Content = ex.Message });
                                             }


                                         }
                                     }
                               });

                    }
                    break;
                case "Decline":
                    {
                        RadWindow.Confirm(
                              new DialogParameters
                              {
                                  Owner = this,
                                  Content = "Are you sure you want to decline this manifest??",
                                  Closed = async (s, args) =>
                                    {
                                        var result = args.DialogResult;
                                        if (result == false)
                                        {
                                            //do nothing
                                        }
                                        else
                                        {
                                            var voyage = voyagesGrid.SelectedItem as VoyageDetails;
                                            winDeclineVoyage winDecline = new winDeclineVoyage();
                                            winDecline.Owner = this;
                                            winDecline.ShowDialog();
                                            var reason = winDecline.DeclineReason;

                                            try
                                            {
                                                var succuess = await downloader.DeclineVoyage(voyage.VoyageDetailsId, reason);
                                                if (succuess)
                                                {
                                                    RadWindow.Alert(new DialogParameters { Owner = this, Content = "Voyage declined." });
                                                    Close();
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                RadWindow.Alert(new DialogParameters { Owner = this, Content = ex.Message });
                                            }
                                        }
                                    }
                              });
                    }
                    break;
            }
        }


    }
}
