using EmanifestDownloader.Helper;
using EmanifestDownloader.Model;
using EmanifestDownloader.Models;
using ODataCSharpClient;
using PortSystem.DAL;
using Simple.OData.Client;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Timers;
using System.Windows;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.Data;

namespace EmanifestDownloader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RadWindow, INotifyPropertyChanged
    {
        EmanifestContext db;
        VoyagesCache cache;
        ODataClient client;
        Timer timer;
        private ObservableCollection<CustomMenuItem> rowContextMenuItems;
        private void InitializeRowContextMenuItems()
        {
            ObservableCollection<CustomMenuItem> items = new ObservableCollection<CustomMenuItem>();
            CustomMenuItem editItem = new CustomMenuItem();
            editItem.Text = "View";
            CustomMenuItem downloadItem = new CustomMenuItem();
            downloadItem.Text = "Download";
            items.Add(editItem);
            items.Add(downloadItem);
            this.rowContextMenuItems = items;
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
                    item.IsEnabled = false;
                }
                this.GridContextMenu.ItemsSource = this.rowContextMenuItems;
            }
            if (pnlProgress.Visibility == Visibility.Visible)
            {
                foreach (var item in this.rowContextMenuItems)
                {
                    item.IsEnabled = false;
                }
            }
        }

        private void ShowPanelAndDisableContext(bool flag)
        {
            if (flag)
            {
                pnlProgress.Visibility = Visibility.Visible;

            }
            else
            {
                pnlProgress.Visibility = Visibility.Hidden;
            }
        }
        private async void GridContextMenu_ItemClick(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            try
            {
                CustomMenuItem item = (e.OriginalSource as RadMenuItem).DataContext as CustomMenuItem;
                switch (item.Text)
                {
                    case "View":
                        {
                            var selectedVoyage = voyagesGrid.SelectedItem as VoyageResult;
                            if (selectedVoyage != null)
                            {
                                ShowPanelAndDisableContext(true);
                                var completeVoyage = await cache.GetVoyage(selectedVoyage.VoyageDetailsId);
                                if (completeVoyage.ManifestType == "GeneralCargo")
                                {
                                    winGeneralCargo window = new winGeneralCargo(completeVoyage);
                                    window.Owner = this;
                                    window.ShowInTaskbar("GeneralCargo");
                                }
                                else if (completeVoyage.ManifestType == "FCLContainer")
                                {
                                    winFCLContainersWithRelation window = new winFCLContainersWithRelation(completeVoyage);
                                    window.Owner = this;
                                    window.ShowInTaskbar("FCLContainer");
                                }
                                else if (completeVoyage.ManifestType == "LCLContainer")
                                {
                                    winLCLContainers window = new winLCLContainers(completeVoyage);
                                    window.Owner = this;
                                    window.ShowInTaskbar("LCLContainer");
                                }
                                else if (completeVoyage.ManifestType == "FCLContainerRelationNotknown")
                                {
                                    winFCLContainersWithoutRelation window = new winFCLContainersWithoutRelation(completeVoyage);
                                    window.Owner = this;
                                    window.ShowInTaskbar("FCLContainerRelationNotknown");
                                }
                            }
                        }
                        break;
                    case "Download":
                        {
                            var selectedVoyage = voyagesGrid.SelectedItem as VoyageResult;
                            if (selectedVoyage != null)
                            {
                                ShowPanelAndDisableContext(true);
                                var completeVoyage = await cache.GetVoyage(selectedVoyage.VoyageDetailsId);
                                winDownloadVoyage window = new winDownloadVoyage(completeVoyage);
                                window.Owner = this;
                                window.ShowInTaskbar("Download Manifest Data");
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Alert(new DialogParameters { Owner = this, Content = ex.Message });
            }
            finally
            {
                ShowPanelAndDisableContext(false);
            }

        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
        public ObservableCollection<VoyageResult> Voyages { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            this.ShowInTaskbar("Main Window");
            InitializeRowContextMenuItems();
            timer = new Timer();
            timer.Elapsed += Timer_Elapsed;
            timer.Interval = txtInterval.Value.Value;
            timer.Enabled = chbAutoRefresh.IsChecked == true;
            DataContext = this;
            InitializeOdata();
            db = new EmanifestContext();
        }

        private void InitializeOdata()
        {
            var serviceUrl = ConfigurationManager.AppSettings["ServiceUrl"].ToString();
            string userName = ConfigurationManager.AppSettings["UserName"].ToString();
            string password = ConfigurationManager.AppSettings["Password"].ToString();
            var uri = new Uri($"{serviceUrl}odata");
            client = EmanifestODataHelper.CreateClient(uri, userName, password);
            cache = new VoyagesCache(client);

        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            timer.Enabled = false;
            GetValidVoyages();
        }

        private void GetValidVoyages()
        {
            Dispatcher.Invoke(async () =>
              {
                  try
                  {
                      var portCode = ConfigurationManager.AppSettings["PortCode"].ToString();
                      var queryTask = client.For<VoyageDetails>()
                      .Filter(v => v.PortCodeOfDischarge == portCode && v.ExpectedToArriveDate >= DateTimeOffset.Now && v.StatusId == new Guid("d7b43b80-ea2e-4d6c-a436-b6572ef236ac"));

                      var odataVoyages = await queryTask.FindEntriesAsync();
                      Voyages = new ObservableCollection<VoyageResult>(odataVoyages.Select(v => new VoyageResult
                      {
                          PortCode = v.PortCodeOfDischarge,
                          VoyageNo = v.AgentVoyageNumber,
                          ExpectedArrival = v.ExpectedToArriveDate.Value,
                          VesselName = v.VesselName,
                          Installment = v.NumberOfInstalment.Value,
                          VoyageDetailsId = v.VoyageDetailsId

                      }).ToList());
                      var ides = Voyages.Select(v => v.VoyageDetailsId).ToList();
                      var dbVoyages = db.VoyageDetails.Where(v => ides.Contains(v.VoyageDetailsId)).Select(v => v.VoyageDetailsId).ToList();
                      foreach (var voyage in Voyages)
                      {
                          if (dbVoyages.Contains(voyage.VoyageDetailsId))
                          {
                              voyage.Downloaded = true;
                          }
                      }
                      voyagesGrid.ItemsSource = Voyages;
                      if (!timer.Enabled && chbAutoRefresh.IsChecked == true)
                      {
                          timer.Enabled = true;
                      }
                  }
                  catch (Exception ex)
                  {
                      RadWindow.Alert(new DialogParameters { Owner = this, Content = ex.Message });
                      chbAutoRefresh.IsChecked = false;
                  }
              });


        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            GetValidVoyages();
        }

        private void chbAutoRefresh_Unchecked(object sender, RoutedEventArgs e)
        {
            if (timer != null)
            {
                timer.Enabled = false;
            }
        }

        private void chbAutoRefresh_Checked(object sender, RoutedEventArgs e)
        {
            if (timer != null)
            {
                timer.Enabled = true;

            }
        }

        private void txtInterval_ValueChanged(object sender, RadRangeBaseValueChangedEventArgs e)
        {
            if (timer != null)
            {
                timer.Interval = txtInterval.Value.Value;
            }
        }

        private void RadWindow_Closed(object sender, WindowClosedEventArgs e)
        {
            if (timer != null)
            {
                timer.Stop();
            }
        }
    }
}
