using IQMan.Helpers;
using EmanifestParser;
using IQManClient.DAL;
using IQMan.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using IQManClient.DAL.Helpers;
using System.Data.Entity.Infrastructure;

namespace IQMan
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        string ManifestType = ManifestFileType.GeneralCargo.ToString();
        private DataDeletor deletor;
        private EmanifestContext db;

        private void FillCombo()
        {
            try
            {
                var lines = db.Lines.ToList().Select(l => new { Text = $"{l.LineCode}-{l.LineName}", Value = l.LineCode }).ToList();
                cmbLine.ItemsSource = lines;
            }
            catch (Exception ex)
            {
                RadWindow.Alert(new DialogParameters
                {
                    Owner = this,
                    Content = new TextBlock
                    {
                        Width = 200,
                        Text = ex.Message,
                        TextWrapping = TextWrapping.WrapWithOverflow
                    }
                });
            }

        }
        private void txtOrigiinPort_SearchTextChanged(object sender, EventArgs e)
        {
            var control = sender as RadAutoCompleteBox;
            var search = control.SearchText;
            if (search == null || search.Length < 2)
            {
                control.ItemsSource = null;
                return;
            }
            FillLocationAutoComplete(sender as RadAutoCompleteBox, search);
        }
        private void FillLocationAutoComplete(RadAutoCompleteBox control, string search)
        {
            Task.Run(() =>
            {
                try
                {
                    using (EmanifestContext db = new EmanifestContext())
                    {

                        var locationCodes = db.LocationCodes.Where(l => l.FullCode.StartsWith(search)).ToList();
                        var source = locationCodes.Where(l => LocationCodesHelper.IsPort(l) == true).Select(l => new { l.FullCode, Name = $"{l.FullCode} - {l.LocationName}" }).ToList();
                        Dispatcher.Invoke(() =>
                        {
                            control.ItemsSource = source;
                            control.Populate(search);
                        });
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });

        }
        private ObservableCollection<CustomMenuItem> rowContextMenuItems;
        //menu items
        public IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);

                    if (child != null && child is T)
                        yield return (T)child;

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                        yield return childOfChild;
                }
            }
        }
        private void InitializeRowContextMenuItems()
        {
            ObservableCollection<CustomMenuItem> items = new ObservableCollection<CustomMenuItem>();
            CustomMenuItem addItem = new CustomMenuItem();
            addItem.Text = "Add";
            items.Add(addItem);
            CustomMenuItem editItem = new CustomMenuItem();
            editItem.Text = "Edit";
            items.Add(editItem);
            CustomMenuItem deleteItem = new CustomMenuItem();
            deleteItem.Text = "Delete";
            items.Add(deleteItem);
            this.rowContextMenuItems = items;
        }
        private GridViewRow GetContextMenuClickedRow(RadContextMenu menu)
        {
            return menu.GetClickedElement<GridViewRow>();
        }
        private GridViewHeaderCell GetContextMenuClickedCell(RadContextMenu menu)
        {
            return menu.GetClickedElement<GridViewHeaderCell>();
        }
        private void GridContextMenu_Opened(object sender, RoutedEventArgs e)
        {
            var contextMenu = sender as RadContextMenu;
            if (contextMenu == null)
                return;
            var clickedRow = GetContextMenuClickedRow(contextMenu);
            if (clickedRow != null)
            {
                this.bolGrid.SelectedItem = clickedRow.DataContext;
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
        private void GridConsignmentContextMenu_Opened(object sender, RoutedEventArgs e)
        {
            var contextMenu = sender as RadContextMenu;
            if (contextMenu == null)
                return;
            var clickedRow = GetContextMenuClickedRow(contextMenu);
            var parentGrid = clickedRow.ParentOfType<RadGridView>();
            if (clickedRow != null && parentGrid.Name == "consignmentGrid")
            {
                parentGrid.SelectedItem = clickedRow.DataContext;
                foreach (var item in this.rowContextMenuItems)
                {
                    item.IsEnabled = true;
                }
                contextMenu.ItemsSource = this.rowContextMenuItems;
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
                contextMenu.ItemsSource = this.rowContextMenuItems;
            }
        }
        private void GridVehicleContextMenu_Opened(object sender, RoutedEventArgs e)
        {
            var contextMenu = sender as RadContextMenu;
            if (contextMenu == null)
                return;
            var clickedRow = GetContextMenuClickedRow(contextMenu);
            var parentGrid = clickedRow.ParentOfType<RadGridView>();
            if (clickedRow != null && parentGrid.Name == "vehiclesGrid")
            {
                parentGrid.SelectedItem = clickedRow.DataContext;
                foreach (var item in this.rowContextMenuItems)
                {
                    item.IsEnabled = true;
                }
                contextMenu.ItemsSource = this.rowContextMenuItems;
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
                contextMenu.ItemsSource = this.rowContextMenuItems;
            }
        }
        private void GridContextMenu_ItemClick(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            CustomMenuItem item = (e.OriginalSource as RadMenuItem).DataContext as CustomMenuItem;
            switch (item.Text)
            {
                case "Add":
                    {
                        winBOLData win = new winBOLData(null, Voyage);
                        win.ShowDialog();
                        if (!win.Saved)
                            break;
                        var bol = win.BOLDetails;
                        bol.BOLDetailsId = Guid.NewGuid();
                        bol.VoyageDetails = Voyage;
                        Voyage.BOLDetails.Add(bol);
                        //bolGrid.Rebind();
                    }
                    break;
                case "Edit":
                    {
                        var bol = this.bolGrid.SelectedItem as BOLDetails;
                        winBOLData win = new winBOLData(bol, Voyage);
                        win.ShowDialog();
                        if (!win.Saved)
                        {
                            if (db.BOLDetails.Local.Any(b => b.BOLDetailsId == bol.BOLDetailsId))
                            {
                                var entry = db.Entry(bol);
                                if (entry.State != EntityState.Added)
                                {
                                    entry.CurrentValues.SetValues(db.Entry(bol).OriginalValues);
                                    //you may also need to set back to unmodified -
                                    //I'm unsure if EF will do this automatically
                                    db.Entry(bol).State = EntityState.Unchanged;
                                }

                            }

                        }
                    }
                    break;
                case "Delete":
                    {
                        RadWindow.Confirm(
                                      new DialogParameters
                                      {
                                          Owner = this,
                                          Content = "Are you sure you want to delete this selected BOLs??",
                                          Closed = (s, args) =>
                                          {
                                              var result = args.DialogResult;
                                              if (result == false)
                                              {
                                                  //do nothing
                                              }
                                              else
                                              {
                                                  var bol = this.bolGrid.SelectedItem as BOLDetails;
                                                  //deletor.DeleteBolChildren(bol);
                                                  foreach (var container in bol.ContainerDetails.ToList())
                                                  {
                                                      foreach (var con in container.ConsignmentDetails.ToList())
                                                      {
                                                          foreach (var veh in con.VehicleDetails.ToList())
                                                          {
                                                              try
                                                              {
                                                                  db.VehicleDetails.Remove(veh);
                                                              }
                                                              catch (InvalidOperationException ex)
                                                              {
                                                                  //ignored
                                                              }
                                                          }
                                                          try
                                                          {

                                                              db.ConsignmentDetails.Remove(con);
                                                          }
                                                          catch (InvalidOperationException ex)
                                                          {
                                                              //ignored
                                                          }
                                                      }
                                                      try
                                                      {
                                                          db.ContainerDetails.Remove(container);
                                                      }
                                                      catch (InvalidOperationException ex)
                                                      {
                                                          //ignored
                                                      }
                                                  }
                                                  foreach (var con in bol.ConsignmentDetails.ToList())
                                                  {
                                                      foreach (var veh in con.VehicleDetails.ToList())
                                                      {
                                                          try
                                                          {
                                                              db.VehicleDetails.Remove(veh);
                                                          }
                                                          catch (InvalidOperationException ex)
                                                          {
                                                              //ignored
                                                          }

                                                      }
                                                      try
                                                      {
                                                          db.ConsignmentDetails.Remove(con);
                                                      }
                                                      catch (InvalidOperationException ex)
                                                      {
                                                          //ignored
                                                      }
                                                  }
                                                  try
                                                  {
                                                      db.BOLDetails.Remove(bol);
                                                  }
                                                  catch (InvalidOperationException ex)
                                                  {
                                                      //ignored
                                                  }
                                                  Voyage.BOLDetails.Remove(bol);
                                                  //bolGrid.Rebind();
                                              }
                                          }
                                      });

                    }
                    break;
            }
        }
        private void consignmentMenu_ItemClick(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            var contextMenu = sender as RadContextMenu;
            if (contextMenu == null)
                return;
            var clickedRow = GetContextMenuClickedRow(contextMenu);
            var clickedCell = GetContextMenuClickedCell(contextMenu);
            RadGridView consignmentGrid = null;
            if (clickedRow != null && clickedRow.GridViewDataControl.Name == "consignmentGrid")
            {
                consignmentGrid = clickedRow.ParentOfType<RadGridView>();
            }
            else
            {
                consignmentGrid = clickedCell.ParentOfType<RadGridView>();
            }
            if (consignmentGrid == null)
                return;
            var bolRow = consignmentGrid.ParentOfType<GridViewRow>();
            var selectedBol = bolRow.DataContext as BOLDetails;
            CustomMenuItem item = (e.OriginalSource as RadMenuItem).DataContext as CustomMenuItem;
            switch (item.Text)
            {
                case "Add":
                    {
                        winConsignmentData win = new winConsignmentData(null, db);
                        win.ShowDialog();
                        if (!win.Saved)
                            break;
                        var con = win.Consignment;
                        con.ConsignmentDetailsId = Guid.NewGuid();
                        con.SerialNumber = selectedBol.ConsignmentDetails.Count + 1;
                        selectedBol.ConsignmentDetails.Add(win.Consignment);
                        //consignmentGrid.Rebind();
                    }

                    break;
                case "Edit":
                    {
                        var con = consignmentGrid.SelectedItem as ConsignmentDetails;
                        winConsignmentData win = new winConsignmentData(con, db);
                        win.ShowDialog();
                        if (!win.Saved)
                        {
                            if (db.ConsignmentDetails.Local.Any(b => b.ConsignmentDetailsId == con.ConsignmentDetailsId))
                            {
                                var entry = db.Entry(con);
                                if (entry.State != EntityState.Added)
                                {
                                    db.Entry(con).CurrentValues.SetValues(db.Entry(con).OriginalValues);
                                    //you may also need to set back to unmodified -
                                    //I'm unsure if EF will do this automatically
                                    db.Entry(con).State = EntityState.Unchanged;
                                }

                            }

                        }
                    }
                    break;
                case "Delete":
                    {
                        RadWindow.Confirm(
                                      new DialogParameters
                                      {
                                          Owner = this,
                                          Content = "Are you sure you want to delete this selected consignments??",
                                          Closed = (s, args) =>
                                          {
                                              var result = args.DialogResult;
                                              if (result == false)
                                              {
                                                  //do nothing
                                              }
                                              else
                                              {
                                                  var con = consignmentGrid.SelectedItem as ConsignmentDetails;
                                                  foreach (var veh in con.VehicleDetails.ToList())
                                                  {
                                                      try
                                                      {
                                                          db.VehicleDetails.Remove(veh);
                                                      }
                                                      catch (InvalidOperationException ex)
                                                      {
                                                          //ignored
                                                      }
                                                  }
                                                  try
                                                  {

                                                      db.ConsignmentDetails.Remove(con);
                                                  }
                                                  catch (InvalidOperationException ex)
                                                  {
                                                      //ignored
                                                  }
                                                  //deletor.DeleteConChildren(con);
                                                  selectedBol.ConsignmentDetails.Remove(con);
                                                  for (int i = 0; i < selectedBol.ConsignmentDetails.Count; i++)
                                                  {
                                                      selectedBol.ConsignmentDetails.ToList()[i].SerialNumber = i + 1;
                                                  }
                                                  //consignmentGrid.Rebind();
                                              }
                                          }
                                      });

                    }
                    break;
            }
        }
        private void vehicleMenu_ItemClick(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            var contextMenu = sender as RadContextMenu;
            if (contextMenu == null)
                return;
            var clickedRow = GetContextMenuClickedRow(contextMenu);
            var clickedCell = GetContextMenuClickedCell(contextMenu);
            RadGridView vehicleGrid = null;
            if (clickedRow != null && clickedRow.GridViewDataControl.Name == "vehiclesGrid")
            {
                vehicleGrid = clickedRow.ParentOfType<RadGridView>();
            }
            else if (clickedCell != null)
            {
                vehicleGrid = clickedCell.ParentOfType<RadGridView>();
            }
            if (vehicleGrid == null)
                return;
            var consignmentRow = vehicleGrid.ParentOfType<GridViewRow>();
            var selectedConsignment = consignmentRow.DataContext as ConsignmentDetails;
            CustomMenuItem item = (e.OriginalSource as RadMenuItem).DataContext as CustomMenuItem;
            switch (item.Text)
            {
                case "Add":
                    {
                        winVehicleData win = new winVehicleData(null);
                        win.ShowDialog();
                        if (!win.Saved)
                            break;
                        var vehicle = win.Vehicle;
                        vehicle.VehicleDetailsId = Guid.NewGuid();
                        vehicle.ConsignmentDetails = selectedConsignment;
                        vehicle.SerialNumber = selectedConsignment.SerialNumber;
                        selectedConsignment.VehicleDetails.Add(vehicle);
                        //vehicleGrid.Rebind();
                    }
                    break;
                case "Edit":
                    {
                        var vehicle = vehicleGrid.SelectedItem as VehicleDetails;
                        winVehicleData win = new winVehicleData(vehicle);
                        win.ShowDialog();
                        if (!win.Saved)
                        {
                            if (db.VehicleDetails.Local.Any(b => b.VehicleDetailsId == vehicle.VehicleDetailsId))
                            {
                                var entry = db.Entry(vehicle);
                                if (entry.State != EntityState.Added)
                                {
                                    entry.CurrentValues.SetValues(db.Entry(vehicle).OriginalValues);
                                    //you may also need to set back to unmodified -
                                    //I'm unsure if EF will do this automatically
                                    db.Entry(vehicle).State = EntityState.Unchanged;
                                }

                            }

                        }
                    }
                    break;
                case "Delete":
                    {
                        RadWindow.Confirm(
                                      new DialogParameters
                                      {
                                          Owner = this,
                                          Content = "Are you sure you want to delete this selected vehicles??",
                                          Closed = (s, args) =>
                                          {
                                              var result = args.DialogResult;
                                              if (result == false)
                                              {
                                                  //do nothing
                                              }
                                              else
                                              {
                                                  var vehicle = vehicleGrid.SelectedItem as VehicleDetails;
                                                  try
                                                  {
                                                      db.VehicleDetails.Remove(vehicle);
                                                  }
                                                  catch (InvalidOperationException ex)
                                                  {
                                                      //ignored
                                                  }

                                                  //deletor.DeleteVehicle(vehicle);
                                                  selectedConsignment.VehicleDetails.Remove(vehicle);
                                                  //vehicleGrid.Rebind();
                                              }
                                          }
                                      });

                    }
                    break;
            }
        }
        //
        private string _fileName = null;

        public event PropertyChangedEventHandler PropertyChanged;
        private void Notify([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private VoyageDetails _voyage;
        public VoyageDetails Voyage { get { return _voyage; } set { _voyage = value; Notify(); } }
        public MainWindow(Guid? voyageDetailsId, EmanifestContext db)
        {
            this.db = db ?? new EmanifestContext();
            deletor = new DataDeletor(db);
            if (voyageDetailsId == null)
            {
                Voyage = new VoyageDetails();
                Voyage.ManifestType = ManifestType;
                if (Settings.User?.Carriers != null)
                {
                    Voyage.VoyageAgentCode = Settings.User.Carriers.CarrierName;
                }
            }
            else
            {
                Voyage = db.VoyageDetails.Include("BOLDetails.ContainerDetails.ConsignmentDetails.VehicleDetails")
                    .Include("BOLDetails.ConsignmentDetails.VehicleDetails")
                    .Where(v => v.VoyageDetailsId == voyageDetailsId).FirstOrDefault();
                if (Voyage == null)
                {
                    RadWindow.Alert(new DialogParameters
                    {
                        Owner = this,
                        Content = new TextBlock
                        {
                            Width = 200,
                            Text = "voyageDetails not found ",
                            TextWrapping = TextWrapping.WrapWithOverflow
                        }
                    });
                    Close();
                }
                else
                {
                    if (Voyage.ManifestType == null)
                    {
                        Voyage.ManifestType = ManifestType;
                    }
                }
            }

            InitializeComponent();
            DataContext = this;
            InitializeRowContextMenuItems();
            //FillCombo();
        }
        private void RadButton_Click(object sender, RoutedEventArgs e)
        {
            SerialNumberHelper.AlterSerialNumbers(Voyage);
            var context = new ValidationContext(Voyage, serviceProvider: null, items: null);
            var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();

            var isValid = Validator.TryValidateObject(Voyage, context, results);

            if (!isValid)
            {
                string error = "";
                foreach (var validationResult in results)
                {
                    error += validationResult.ErrorMessage + Environment.NewLine;
                }
                var owner = (sender as UIElement).ParentOfType<Window>();
                RadWindow.Alert(new DialogParameters
                {
                    Owner = this,
                    Content = new TextBlock
                    {
                        Width = 200,
                        Text = error,
                        TextWrapping = TextWrapping.WrapWithOverflow
                    }
                });
            }
            else
            {
                EmanifestSerializer<VoyageDetails, BOLDetails, ConsignmentDetails, ContainerDetails, VehicleDetails> serializer = new EmanifestSerializer<VoyageDetails, BOLDetails, ConsignmentDetails, ContainerDetails, VehicleDetails>(ManifestFileType.GeneralCargo);
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.DefaultExt = "txt";
                saveFileDialog.AddExtension = true;
                if (saveFileDialog.ShowDialog() == true)
                    serializer.SerializeTofile(saveFileDialog.FileName, Voyage);
            }
        }

        //private void RadGridView_Deleting(object sender, GridViewDeletingEventArgs e)
        //{

        //    RadWindow.Confirm(
        //    new DialogParameters
        //    {
        //        Content = "Are you sure you want to delete this selected BOLs??",
        //        Closed = (s, args) =>
        //        {
        //            var result = args.DialogResult;
        //            if (result == false)
        //            {
        //                e.Cancel = true;
        //            }
        //        }
        //    });
        //    e.Handled = true;
        //}
        //private void Consignment_Deleting(object sender, GridViewDeletingEventArgs e)
        //{
        //    RadWindow.Confirm(
        //    new DialogParameters
        //    {
        //        Content = "Are you sure you want to delete this selected consignments??",
        //        Closed = (s, args) =>
        //        {
        //            var result = args.DialogResult;
        //            if (result == false)
        //            {
        //                e.Cancel = true;
        //            }
        //        }
        //    });
        //    e.Handled = true;
        //}
        //private void Vehicles_Deleting(object sender, GridViewDeletingEventArgs e)
        //{

        //    RadWindow.Confirm(
        //    new DialogParameters
        //    {
        //        Content = "Are you sure you want to delete this selected vehicles??",
        //        Closed = (s, args) =>
        //        {
        //            var result = args.DialogResult;
        //            if (result == false)
        //            {
        //                e.Cancel = true;
        //            }
        //        }
        //    });
        //    e.Handled = true;
        //}
        private void OpenSelectedFile()
        {
            if (_fileName == null)
                return;
            if (Voyage.VoyageDetailsId == Guid.Empty)
            {
                try
                {
                    EmanifestParser<VoyageDetails, BOLDetails, ConsignmentDetails, ContainerDetails, VehicleDetails> parser = new EmanifestParser<VoyageDetails, BOLDetails, ConsignmentDetails, ContainerDetails, VehicleDetails>(File.Open(_fileName, FileMode.Open));
                    var parsedVoyage = parser.ParseStream();
                    if (parsedVoyage != null)
                    {
                        parsedVoyage.VoyageDetailsId = Voyage.VoyageDetailsId;
                        parsedVoyage.ManifestType = ManifestType;
                        Voyage = parsedVoyage;
                    }
                }
                catch (Exception ex)
                {
                    RadWindow.Alert(new DialogParameters
                    {
                        Owner = this,
                        Content = ex.Message,
                    });
                }

            }
            else
            {
                RadWindow.Alert(new DialogParameters
                {
                    Owner = this,
                    Content = new TextBlock
                    {
                        Width = 200,
                        Text = "Cannot choose a file in EditMode",
                        TextWrapping = TextWrapping.WrapWithOverflow
                    }
                });
            }
        }

        private void RadButton_Click_1(object sender, RoutedEventArgs e)
        {
            RadWindow.Confirm(
          new DialogParameters
          {
              Content = "Are you sure you want to open the file you will lose your current work??",
              Closed = (s, args) =>
              {
                  var result = args.DialogResult;
                  if (result == true)
                  {
                      // Create OpenFileDialog
                      Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();

                      // Launch OpenFileDialog by calling ShowDialog method
                      bool? dialogeResult = openFileDlg.ShowDialog();
                      // Get the selected file name and display in a TextBox.
                      // Load content of file in a TextBlock
                      if (dialogeResult == true)
                      {
                          _fileName = openFileDlg.FileName;
                          OpenSelectedFile();
                      }
                  }
              }
          });

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Voyage.VoyageDetailsId == Guid.Empty)
                {

                    Voyage.VoyageDetailsId = Guid.NewGuid();
                    //Voyage.UploadDate = DateTime.Now;
                    db.VoyageDetails.Add(Voyage);
                }

                db.BulkSaveChanges();
                Close();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                var outputLines = new List<string>();
                foreach (var eve in ex.EntityValidationErrors)
                {
                    outputLines.Add(string.Format(
                        "{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:",
                        DateTime.Now, eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        outputLines.Add(string.Format(
                            "- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage));
                    }
                }
                var errors = string.Join(Environment.NewLine, outputLines);
                RadWindow.Alert(new DialogParameters
                {
                    Owner = this,
                    Content = new TextBlock
                    {
                        Width = 200,
                        Text = errors,
                        TextWrapping = TextWrapping.WrapWithOverflow
                    }
                });
            }
            catch (Exception ex)
            {
                RadWindow.Alert(new DialogParameters
                {
                    Owner = this,
                    Content = new TextBlock
                    {
                        Width = 200,
                        Text = ex.Message,
                        TextWrapping = TextWrapping.WrapWithOverflow
                    }
                });
            }

        }

        private void txtVessel_SearchTextChanged(object sender, EventArgs e)
        {
            if (Settings.User.CarrierId == null)
            {
                return;
            }
            var search = txtVessel.SearchText;
            Task.Run(() =>
              {
                  using (EmanifestContext db = new EmanifestContext())
                  {
                      try
                      {
                          var vessels = db.Vessels.Where(v => v.CarrierId == Settings.User.CarrierId).Select(p => new { p.VesselId, p.VesselName }).ToList();
                          var source = vessels.Select(p => new { Code = p.VesselId, Name = p.VesselName });
                          Dispatcher.Invoke(() =>
                          {
                              txtVessel.ItemsSource = source;
                              txtVessel.Populate(search);
                          });
                      }
                      catch (Exception ex)
                      {
                          MessageBox.Show(ex.Message);
                      }
                  }
              });
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (db.IsDirty())
            {
                RadWindow.Confirm(
                              new DialogParameters
                              {
                                  Owner = this,
                                  Content = new TextBlock
                                  {
                                      Width = 200,
                                      Text = "There are unsaved changes are you sure you want to exit you will lose all your changes?",
                                      TextWrapping = TextWrapping.WrapWithOverflow
                                  },
                                  Closed = (s, args) =>
                                  {
                                      var result = args.DialogResult;
                                      if (result == false)
                                      {
                                          e.Cancel = true;
                                      }
                                      else
                                      {
                                          try
                                          {

                                              db.UndoingChangesDbContextLevel();
                                          }
                                          catch (Exception ex)
                                          {

                                              throw;
                                          }
                                      }
                                  }
                              });

            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FillCombo();
        }


        //private void RadGridView_AddingNewDataItem(object sender, Telerik.Windows.Controls.GridView.GridViewAddingNewEventArgs e)
        //{
        //    var control = sender as RadGridView;
        //    if (control.Name == "bolGrid")
        //    {
        //        var parentRow = control.ParentOfType<GridViewRow>();
        //        winBOLData win = new winBOLData(null);
        //        win.ShowDialog();
        //        e.NewObject = win.BOLDetails;
        //    }
        //    else if (control.Name == "consignmentGrid")
        //    {
        //        var parentRow = control.ParentOfType<GridViewRow>();
        //        var bol = parentRow.Item as BOLDetails;
        //        winConsignmentData win = new winConsignmentData(null);
        //        win.ShowDialog();
        //        bol.ConsignmentDetails.Add(win.Consignment);
        //        control.Rebind();
        //        //e.NewObject = win.Consignment;
        //    }
        //    else if (control.Name == "vehiclesGrid")
        //    {
        //        var parentRow = control.ParentOfType<GridViewRow>();
        //        var consignment = parentRow.Item as ConsignmentDetails;
        //        winVehicleData win = new winVehicleData();
        //        win.ShowDialog();
        //        consignment.VehicleDetails.Add(win.Vehicle);
        //        e.NewObject = win.Vehicle;
        //    }

        //}


    }
}
