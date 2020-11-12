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
using System.Windows.Shapes;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using IQManClient.DAL.Helpers;

namespace IQMan
{
    /// <summary>
    /// Interaction logic for winLCLContainers.xaml
    /// </summary>
    public partial class winLCLContainers : Window, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        private void Notify([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private VoyageDetails _voyage;
        public VoyageDetails Voyage { get { return _voyage; } set { _voyage = value; Notify(); } }
        string ManifestType = ManifestFileType.LCLContainer.ToString();
        private DataDeletor deletor;
        private EmanifestContext db;
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
        private void FillLocationAutoComplete(RadAutoCompleteBox control, string search)
        {
            Task.Run(() =>
            {
                using (EmanifestContext db = new EmanifestContext())
                {
                    try
                    {
                        var locationCodes = db.LocationCodes.Where(l => l.FullCode.StartsWith(search)).ToList();
                        var source = locationCodes.Where(l => LocationCodesHelper.IsPort(l) == true).Select(l => new { l.FullCode, Name = $"{l.FullCode} - {l.LocationName}" }).ToList();
                        Dispatcher.Invoke(() =>
                        {
                            control.ItemsSource = source;
                            control.Populate(search);
                        });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
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
                var conGrid = clickedRow.ParentOfType<RadGridView>();
                conGrid.SelectedItem = clickedRow.DataContext;
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
        private void GridcontainerContextMenu_Opened(object sender, RoutedEventArgs e)
        {
            var contextMenu = sender as RadContextMenu;
            if (contextMenu == null)
                return;
            var clickedRow = GetContextMenuClickedRow(contextMenu);
            var parentGrid = clickedRow.ParentOfType<RadGridView>();
            if (clickedRow != null && parentGrid.Name == "containersGrid")
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
                                    db.Entry(bol).CurrentValues.SetValues(db.Entry(bol).OriginalValues);
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
                                                      db.ConsignmentDetails.Remove(con);
                                                  }
                                                  try
                                                  {
                                                      db.BOLDetails.Remove(bol);
                                                  }
                                                  catch (InvalidOperationException ex)
                                                  {
                                                      //ignored
                                                  }
                                                  //deletor.DeleteBolChildren(bol);
                                                  Voyage.BOLDetails.Remove(bol);
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
            var containerRow = consignmentGrid.ParentOfType<GridViewRow>();
            var selectedContainer = containerRow.DataContext as ContainerDetails;
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
                        if (selectedContainer.BOLDetails != null)
                        {
                            win.Consignment.SerialNumber = selectedContainer.BOLDetails.ConsignmentDetails.Count + 1;
                        }
                        selectedContainer.ConsignmentDetails.Add(win.Consignment);

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
                                                  selectedContainer.ConsignmentDetails.Remove(con);
                                                  if (selectedContainer.BOLDetails != null)
                                                  {
                                                      var consignments = selectedContainer.BOLDetails.ConsignmentDetails.ToList();
                                                      for (int i = 0; i < consignments.Count; i++)
                                                      {
                                                          consignments.ToList()[i].SerialNumber = i + 1;
                                                      }
                                                  }

                                              }
                                          }
                                      });

                    }
                    break;
            }
        }
        private void containerMenu_ItemClick(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            var contextMenu = sender as RadContextMenu;
            if (contextMenu == null)
                return;
            var clickedRow = GetContextMenuClickedRow(contextMenu);
            var clickedCell = GetContextMenuClickedCell(contextMenu);
            RadGridView containerGrid = null;
            if (clickedRow != null && clickedRow.GridViewDataControl.Name == "containersGrid")
            {
                containerGrid = clickedRow.ParentOfType<RadGridView>();
            }
            else if (clickedCell != null)
            {
                containerGrid = clickedCell.ParentOfType<RadGridView>();
            }
            if (containerGrid == null)
                return;
            var bolRow = containerGrid.ParentOfType<GridViewRow>();
            var selectedBol = bolRow.DataContext as BOLDetails;
            CustomMenuItem item = (e.OriginalSource as RadMenuItem).DataContext as CustomMenuItem;
            switch (item.Text)
            {
                case "Add":
                    {
                        winContainerData win = new winContainerData(null, Voyage);
                        win.ShowDialog();
                        if (!win.Saved)
                            break;
                        var container = win.Container;
                        container.ContainerDetailsId = Guid.NewGuid();
                        container.BOLDetails = selectedBol;
                        selectedBol.ContainerDetails.Add(win.Container);
                    }

                    break;
                case "Edit":
                    {
                        var container = containerGrid.SelectedItem as ContainerDetails;
                        winContainerData win = new winContainerData(container, Voyage);
                        win.ShowDialog();
                        if (!win.Saved)
                        {
                            if (db.ContainerDetails.Local.Any(b => b.ContainerDetailsId == container.ContainerDetailsId))
                            {
                                var entry = db.Entry(container);
                                if (entry.State != EntityState.Added)
                                {
                                    db.Entry(container).CurrentValues.SetValues(db.Entry(container).OriginalValues);
                                    //you may also need to set back to unmodified -
                                    //I'm unsure if EF will do this automatically
                                    db.Entry(container).State = EntityState.Unchanged;
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
                                          Content = "Are you sure you want to delete this selected containers??",
                                          Closed = (s, args) =>
                                          {
                                              var result = args.DialogResult;
                                              if (result == false)
                                              {
                                                  //do nothing
                                              }
                                              else
                                              {
                                                  var container = containerGrid.SelectedItem as ContainerDetails;
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

                                                  //deletor.DeleteContainerChildren(container);
                                                  selectedBol.ContainerDetails.Remove(container);
                                              }
                                          }
                                      });

                    }
                    break;
            }
        }
        //
        private string _fileName = null;
        public winLCLContainers(Guid? voyageDetailsId, EmanifestContext db)
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
                    RadWindow.Alert(new DialogParameters { Owner = this, Content = "voyageDetails not found " });
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
            InitializeRowContextMenuItems();
            InitializeComponent();
            DataContext = this;
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
                RadWindow.Alert(error);
            }
            else
            {
                var serializer = new EmanifestSerializer<VoyageDetails, BOLDetails, ConsignmentDetails, ContainerDetails, VehicleDetails>(ManifestFileType.LCLContainer);
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.DefaultExt = "txt";
                saveFileDialog.AddExtension = true;
                if (saveFileDialog.ShowDialog() == true)
                    serializer.SerializeTofile(saveFileDialog.FileName, Voyage);
            }
        }

        private void RadGridView_Deleting(object sender, GridViewDeletingEventArgs e)
        {

            RadWindow.Confirm(
            new DialogParameters
            {
                Content = "Are you sure you want to delete this selected BOLs??",
                Closed = (s, args) =>
                {
                    var result = args.DialogResult;
                    if (result == false)
                    {
                        e.Cancel = true;
                    }
                }
            });
            e.Handled = true;
        }
        private void Consignment_Deleting(object sender, GridViewDeletingEventArgs e)
        {

            RadWindow.Confirm(
            new DialogParameters
            {
                Content = "Are you sure you want to delete this selected consignments??",
                Closed = (s, args) =>
                {
                    var result = args.DialogResult;
                    if (result == false)
                    {
                        e.Cancel = true;
                    }
                }
            });
            e.Handled = true;
        }
        private void Containers_Deleting(object sender, GridViewDeletingEventArgs e)
        {

            RadWindow.Confirm(
            new DialogParameters
            {
                Content = "Are you sure you want to delete this selected containers??",
                Closed = (s, args) =>
                {
                    var result = args.DialogResult;
                    if (result == false)
                    {
                        e.Cancel = true;
                    }
                }
            });
            e.Handled = true;
        }
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
                    Content = "Cannot choose a file in EditMode"
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

        private void containersGrid_AddingNewDataItem(object sender, GridViewAddingNewEventArgs e)
        {
            //e.NewObject = new ContainerDetails();
            //var item = e.NewObject as ContainerDetails;
            //winContainerData window = new winContainerData(item as ContainerDetails);
            //window.ShowDialog();

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
    }
}
