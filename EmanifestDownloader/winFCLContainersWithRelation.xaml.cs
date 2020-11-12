using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using EmanifestDownloader.Model;
using PortSystem.DAL.Enums;
using PortSystem.DAL;

namespace EmanifestDownloader
{
    /// <summary>
    /// Interaction logic for winLCLContainers.xaml
    /// </summary>
    public partial class winFCLContainersWithRelation : RadWindow, INotifyPropertyChanged
    {
        public Sources Sources { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void Notify([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private VoyageDetails _voyage;
        public VoyageDetails Voyage { get { return _voyage; } set { _voyage = value; Notify(); } }

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
            addItem.Text = "View";
            items.Add(addItem);
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
                contextMenu.ItemsSource = this.rowContextMenuItems;
            }
            else
            {
                foreach (var item in this.rowContextMenuItems)
                {
                    item.IsEnabled = false;

                }
                contextMenu.ItemsSource = this.rowContextMenuItems;
            }
        }
        private void GridConsignmentContextMenu_Opened(object sender, RoutedEventArgs e)
        {
            var contextMenu = sender as RadContextMenu;
            if (contextMenu == null)
                return;
            var clickedRow = GetContextMenuClickedRow(contextMenu);
            var rowParentGrid = clickedRow.GridViewDataControl;
            if (clickedRow != null && rowParentGrid.Name == "consignmentGrid")
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
                    item.IsEnabled = false;

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
            var rowParentGrid = clickedRow.GridViewDataControl;
            if (clickedRow != null && rowParentGrid.Name == "containersGrid")
            {
                var containerGrid = clickedRow.ParentOfType<RadGridView>();
                containerGrid.SelectedItem = clickedRow.DataContext;
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
                    item.IsEnabled = false;

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
            var rowParentGrid = clickedRow.GridViewDataControl;
            if (clickedRow != null && rowParentGrid.Name == "vehiclesGrid")
            {
                var vehicleGrid = clickedRow.ParentOfType<RadGridView>();
                vehicleGrid.SelectedItem = clickedRow.DataContext;
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
                    item.IsEnabled = false;

                }
                contextMenu.ItemsSource = this.rowContextMenuItems;
            }
        }
        private void GridContextMenu_ItemClick(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            CustomMenuItem item = (e.OriginalSource as RadMenuItem).DataContext as CustomMenuItem;
            switch (item.Text)
            {
                case "View":
                    {
                    }

                    break;
            }
        }
        private void consignmentMenu_ItemClick(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {

            CustomMenuItem item = (e.OriginalSource as RadMenuItem).DataContext as CustomMenuItem;
            switch (item.Text)
            {
                case "View":
                    {
                    }

                    break;
            }
        }
        private void containerMenu_ItemClick(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            CustomMenuItem item = (e.OriginalSource as RadMenuItem).DataContext as CustomMenuItem;
            switch (item.Text)
            {
                case "View":
                    {
                    }

                    break;
            }
        }
        private void vehicleMenu_ItemClick(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            CustomMenuItem item = (e.OriginalSource as RadMenuItem).DataContext as CustomMenuItem;
            switch (item.Text)
            {
                case "View":
                    {
                    }

                    break;
            }
        }

        public winFCLContainersWithRelation(VoyageDetails voyage)
        {
            Sources = new Sources();
            Voyage = voyage;
            InitializeRowContextMenuItems();
            InitializeComponent();
            DataContext = this;
            ComboBoxColumnsInitialize();

        }
        private void ComboBoxColumnsInitialize()
        {
            ((GridViewComboBoxColumn)this.bolGrid.Columns["TradeCode"]).ItemsSource = Sources.TradeCodesSource;
            ((GridViewComboBoxColumn)this.bolGrid.Columns["TransShipmentMode"]).ItemsSource = Sources.TransshipModesSource;
            ((GridViewComboBoxColumn)this.bolGrid.Columns["CargoCode"]).ItemsSource = Sources.CargoCodesSource;
            ((GridViewComboBoxColumn)this.bolGrid.Columns["StorageRequestCode"]).ItemsSource = Sources.StorageRequestCodesSource;
            ((GridViewComboBoxColumn)this.bolGrid.Columns["ConsolidatedCargoIndicator"]).ItemsSource = Sources.YesNoSource;
            ((GridViewComboBoxColumn)this.bolGrid.Columns["SlacIndicator"]).ItemsSource = Sources.YesNoSource;
            ((GridViewComboBoxColumn)this.bolGrid.Columns["ContractCarriageCondition"]).ItemsSource = Sources.ContractCarriageConditionsSource;
        }

    }
}
