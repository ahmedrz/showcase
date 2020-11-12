using EmanifestParser;
using IQManClient.DAL;
using IQMan.Helpers;
using IQMan.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using System.Threading;

namespace IQMan
{
    /// <summary>
    /// Interaction logic for fromVoyages.xaml
    /// </summary>
    public partial class winVoyages : Window
    {
        bool uploading = false;
        CancellationTokenSource cancelation;
        private DataDeletor deletor;
        private EmanifestContext db;
        private ObservableCollection<CustomMenuItem> rowContextMenuItems;
        private void InitializeRowContextMenuItems()
        {
            ObservableCollection<CustomMenuItem> items = new ObservableCollection<CustomMenuItem>();
            CustomMenuItem addItem = new CustomMenuItem();
            addItem.Text = "Add";
            addItem.SubItems.Add(new CustomMenuItem { Text = "Add General Cargo Manifest" });
            addItem.SubItems.Add(new CustomMenuItem { Text = "Add LCL Containers Manifest" });
            addItem.SubItems.Add(new CustomMenuItem { Text = "Add FCL Containers Manifest" });
            addItem.SubItems.Add(new CustomMenuItem { Text = "Add FCL Containers(Relation not known) Manifest" });
            items.Add(addItem);
            CustomMenuItem editItem = new CustomMenuItem();
            editItem.Text = "Edit";
            items.Add(editItem);
            CustomMenuItem newInstalementItem = new CustomMenuItem();
            newInstalementItem.Text = "Add New Instalement";
            items.Add(newInstalementItem);
            CustomMenuItem deleteItem = new CustomMenuItem();
            deleteItem.Text = "Delete";
            items.Add(deleteItem);
            CustomMenuItem ediItem = new CustomMenuItem();
            ediItem.Text = "Generate EDI";
            items.Add(ediItem);
            CustomMenuItem uploadItem = new CustomMenuItem();
            uploadItem.Text = "Upload Manifest File";
            items.Add(uploadItem);
            this.rowContextMenuItems = items;
        }
        public winVoyages()
        {

            InitializeComponent();

            var dbInitilized = InitilizeDb();
            if (dbInitilized)
            {
                deletor = new DataDeletor(db);
                DataContext = this;
                InitializeRowContextMenuItems();
            }
            cancelation = new CancellationTokenSource();

        }
        private bool InitilizeDb()
        {
            try
            {
                db = new EmanifestContext();
                EntityFrameworkDataSource.DbContext = db;
                return true;

            }
            catch (Exception ex)
            {
                RadWindow.Alert(ex.Message);
                return false;
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
            CustomMenuItem item = (e.OriginalSource as RadMenuItem).DataContext as CustomMenuItem;
            switch (item.Text)
            {
                case "Add General Cargo Manifest":
                    {
                        MainWindow window = new MainWindow(null, db);
                        window.ShowDialog();
                        RefreshData();

                    }
                    break;
                case "Add LCL Containers Manifest":
                    {
                        winLCLContainers window = new winLCLContainers(null, db);
                        window.ShowDialog();
                        RefreshData();
                    }
                    break;
                case "Add FCL Containers Manifest":
                    {
                        winFCLContainersWithRelation window = new winFCLContainersWithRelation(null, db);
                        window.ShowDialog();
                        RefreshData();
                    }
                    break;
                case "Add FCL Containers(Relation not known) Manifest":
                    {
                        winFCLContainersWithoutRelation window = new winFCLContainersWithoutRelation(null, db);
                        window.ShowDialog();
                        RefreshData();
                    }
                    break;
                case "Edit":
                    {
                        var voyage = voyagesGrid.SelectedItem as VoyageDetails;
                        ManifestFileType fileType = (ManifestFileType)Enum.Parse(typeof(ManifestFileType), voyage.ManifestType);
                        switch (fileType)
                        {
                            case ManifestFileType.GeneralCargo:
                                {
                                    MainWindow window = new MainWindow(voyage.VoyageDetailsId, db);
                                    window.ShowDialog();
                                    RefreshData();
                                }
                                break;
                            case ManifestFileType.LCLContainer:
                                {
                                    winLCLContainers window = new winLCLContainers(voyage.VoyageDetailsId, db);
                                    window.ShowDialog();
                                    RefreshData();
                                }
                                break;
                            case ManifestFileType.FCLContainer:
                                {
                                    winFCLContainersWithRelation window = new winFCLContainersWithRelation(voyage.VoyageDetailsId, db);
                                    window.ShowDialog();
                                    RefreshData();
                                }
                                break;
                            case ManifestFileType.FCLContainerRelationNotknown:
                                {
                                    winFCLContainersWithoutRelation window = new winFCLContainersWithoutRelation(voyage.VoyageDetailsId, db);
                                    window.ShowDialog();
                                    RefreshData();
                                }
                                break;
                            default:
                                break;
                        }

                    }
                    break;
                case "Delete":
                    {
                        RadWindow.Confirm(
                               new DialogParameters
                               {
                                   Owner = this,
                                   Content = "Are you sure you want to delete this selected voyage??",
                                   Closed = (s, args) =>
                                   {
                                       var result = args.DialogResult;
                                       if (result == false)
                                       {
                                           //do nothing
                                       }
                                       else
                                       {
                                           var voyage = voyagesGrid.SelectedItem as VoyageDetails;
                                           deletor.DeleteVoyageChildren(voyage);
                                           RefreshData();
                                       }
                                   }
                               });

                    }
                    break;
                case "Generate EDI":
                    {
                        try
                        {
                            var voyage = voyagesGrid.SelectedItem as VoyageDetails;
                            var fullVoyage = db.VoyageDetails.Include("BOLDetails.ContainerDetails.ConsignmentDetails.VehicleDetails")
                    .Include("BOLDetails.ConsignmentDetails.VehicleDetails")
                    .FirstOrDefault(v => v.VoyageDetailsId == voyage.VoyageDetailsId);
                            fullVoyage.GenerateEDI();
                        }
                        catch (Exception ex)
                        {
                            RadWindow.Alert(new DialogParameters { Owner = this, Content = ex.Message });
                        }
                    }
                    break;
                case "Upload Manifest File":
                    {
                        RadWindow.Confirm(
                              new DialogParameters
                              {
                                  Owner = this,
                                  Content = "Are you sure you want to upload this selected voyage??",
                                  Closed = async (s, args) =>
                                  {
                                      var result = args.DialogResult;
                                      if (result == false)
                                      {
                                          //do nothing
                                      }
                                      else
                                      {
                                          try
                                          {
                                              prg.Visibility = lblprg.Visibility = Visibility.Visible;
                                              uploading = true;
                                              var voyage = voyagesGrid.SelectedItem as VoyageDetails;
                                              var fullVoyage = db.VoyageDetails.Include("BOLDetails.ContainerDetails.ConsignmentDetails.VehicleDetails")
                                      .Include("BOLDetails.ConsignmentDetails.VehicleDetails")
                                      .FirstOrDefault(v => v.VoyageDetailsId == voyage.VoyageDetailsId);

                                              await fullVoyage.UploadManifest((filename, progress) => { Dispatcher.Invoke(() => { prg.Value = progress; }); }, cancelation.Token);
                                              if (!cancelation.IsCancellationRequested)
                                              {
                                                  RadWindow.Alert(new DialogParameters { Owner = this, Content = "Manifest Uploaded." });
                                              }


                                          }
                                          catch (Exception ex)
                                          {
                                              if (!cancelation.IsCancellationRequested)
                                              {
                                                  RadWindow.Alert(new DialogParameters { Owner = this, Content = ex.Message });
                                              }

                                          }
                                          finally
                                          {
                                              uploading = false;
                                              prg.Visibility = lblprg.Visibility = Visibility.Hidden;
                                          }
                                      }



                                  }
                              });
                    }
                    break;
                case "Add New Instalement":
                    {
                        var voyage = voyagesGrid.SelectedItem as VoyageDetails;
                        ManifestFileType fileType = (ManifestFileType)Enum.Parse(typeof(ManifestFileType), voyage.ManifestType);
                        switch (fileType)
                        {
                            case ManifestFileType.GeneralCargo:
                                {
                                    MainWindow window = new MainWindow(null, db);
                                    window.Voyage.ExpectedToArriveDate = voyage.ExpectedToArriveDate;
                                    window.Voyage.LineCode = voyage.LineCode;
                                    window.Voyage.AgentVoyageNumber = voyage.AgentVoyageNumber;
                                    window.Voyage.MessageType = voyage.MessageType;
                                    window.Voyage.VoyageAgentCode = voyage.VoyageAgentCode;
                                    window.Voyage.PortCodeOfDischarge = voyage.PortCodeOfDischarge;
                                    window.Voyage.NumberOfInstalment = ++voyage.NumberOfInstalment;
                                    window.Voyage.VesselName = voyage.VesselName;
                                    window.ShowDialog();
                                    RefreshData();
                                }
                                break;
                            case ManifestFileType.LCLContainer:
                                {
                                    winLCLContainers window = new winLCLContainers(null, db);
                                    window.Voyage.ExpectedToArriveDate = voyage.ExpectedToArriveDate;
                                    window.Voyage.LineCode = voyage.LineCode;
                                    window.Voyage.AgentVoyageNumber = voyage.AgentVoyageNumber;
                                    window.Voyage.MessageType = voyage.MessageType;
                                    window.Voyage.VoyageAgentCode = voyage.VoyageAgentCode;
                                    window.Voyage.PortCodeOfDischarge = voyage.PortCodeOfDischarge;
                                    window.Voyage.NumberOfInstalment = ++voyage.NumberOfInstalment;
                                    window.Voyage.VesselName = voyage.VesselName;
                                    window.ShowDialog();
                                    RefreshData();
                                }
                                break;
                            case ManifestFileType.FCLContainer:
                                {
                                    winFCLContainersWithRelation window = new winFCLContainersWithRelation(null, db);
                                    window.Voyage.ExpectedToArriveDate = voyage.ExpectedToArriveDate;
                                    window.Voyage.LineCode = voyage.LineCode;
                                    window.Voyage.AgentVoyageNumber = voyage.AgentVoyageNumber;
                                    window.Voyage.MessageType = voyage.MessageType;
                                    window.Voyage.VoyageAgentCode = voyage.VoyageAgentCode;
                                    window.Voyage.PortCodeOfDischarge = voyage.PortCodeOfDischarge;
                                    window.Voyage.NumberOfInstalment = ++voyage.NumberOfInstalment;
                                    window.Voyage.VesselName = voyage.VesselName;
                                    window.ShowDialog();
                                    RefreshData();
                                }
                                break;
                            case ManifestFileType.FCLContainerRelationNotknown:
                                {
                                    winFCLContainersWithoutRelation window = new winFCLContainersWithoutRelation(null, db);
                                    window.Voyage.ExpectedToArriveDate = voyage.ExpectedToArriveDate;
                                    window.Voyage.LineCode = voyage.LineCode;
                                    window.Voyage.AgentVoyageNumber = voyage.AgentVoyageNumber;
                                    window.Voyage.MessageType = voyage.MessageType;
                                    window.Voyage.VoyageAgentCode = voyage.VoyageAgentCode;
                                    window.Voyage.PortCodeOfDischarge = voyage.PortCodeOfDischarge;
                                    window.Voyage.NumberOfInstalment = ++voyage.NumberOfInstalment;
                                    window.Voyage.VesselName = voyage.VesselName;
                                    window.ShowDialog();
                                    RefreshData();
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    break;
            }
        }
        private void RefreshData()
        {
            //EntityFrameworkDataSource.DbContext = db = new EmanifestContext();
            EntityFrameworkDataSource.Load();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (uploading)
            {
                RadWindow.Confirm(
                               new DialogParameters
                               {
                                   Owner = this,
                                   Content = "Manifest file is uploading do you want to cancel the upload?",
                                   Closed = (s, args) =>
                                   {
                                       var result = args.DialogResult;
                                       if (result == false)
                                       {
                                           //cancel the close of the window
                                           e.Cancel = true;
                                       }
                                       else
                                       {
                                           cancelation.Cancel();
                                       }
                                   }
                               });
            }
        }
    }
}
