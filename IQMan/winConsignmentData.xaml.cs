using IQMan.Model;
using IQManClient.DAL;
using IQManClient.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
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

namespace IQMan
{
    /// <summary>
    /// Interaction logic for winConsignmentData.xaml
    /// </summary>
    public partial class winConsignmentData : Window
    {
        EmanifestContext _db;
        Sources sources;
        public bool Saved { get; set; }
        List<string> PropertiesToIgnoreValidation = new List<string>() { "SerialNumber" };
        List<string> MessageToIgnoreValidation = new List<string>() { };
        public ConsignmentDetails Consignment { get; set; }
        public winConsignmentData(ConsignmentDetails consignment, EmanifestContext db)
        {
            if (db != null)
            {
                _db = db;
            }
            else
            {
                _db = new EmanifestContext();
            }
            Consignment = consignment;
            if (Consignment == null)
            {
                Consignment = new ConsignmentDetails();
            }
            InitializeComponent();
            DataContext = Consignment;
            sources = new Sources();
            FillCombo();

        }
        private void FillCombo()
        {
            //trade codes
            var usedOrNew = new[] { new { Text = "U - USED", Value = "U" }, new { Text = "N - NEW", Value = "N" } }.ToList();
            cmbUsedOrNew.ItemsSource = usedOrNew;

            //flash temperatures
            var temperatures = new[] { new { Text = "C - CENTIGRADE", Value = "C" }, new { Text = "F - FARENHEIT", Value = "F" } }.ToList();
            cmbFlashTemperature.ItemsSource = temperatures;

            //ref temperatures
            var refTemperatures = new[] { new { Text = "C - CENTIGRADE", Value = "C" }, new { Text = "F - FARENHEIT", Value = "F" } }.ToList();
            cmbrefTemperature.ItemsSource = refTemperatures;

            //storage request Types
            var storageRequestTypes = new[] {
                new { Text = "D - DIRECT DELIVEY", Value = "D" },
                new { Text = "S - STORAGE IN SHEDS", Value = "S" },
                new { Text = "Y - STORAGE IN YARDS", Value = "Y" },
                }.ToList();
            cmbStorageRequest.ItemsSource = storageRequestTypes;
            //imo class
            txtImoClass.ItemsSource = sources.IMOClassSource.Select(s => new { Code = s.Value, Name = s.Text, FullCode = $"{s.Value} - {s.Text}" });


            var dbPackages = _db.PackageCodes.Select(p => new { Value = p.PackageCode, Text = p.PackageDescription }).ToList()
                .Select(p => new { p.Value, p.Text, Display = $"{p.Value} - {p.Text}" }).ToList();
            txtPackageCode.ItemsSource = dbPackages;

            var codes = _db.UNHazardousCodes.Select(p => new { p.Code, Name = p.Description }).ToList();
            var dangerousCodesSource = new ObservableCollection<AutoCompleteItem>(codes.Select(p => new AutoCompleteItem { Code = p.Code, Name = $"{p.Code} - {p.Name}" }).ToList());
            txtUNHazardousCodes.ItemsSource = dangerousCodesSource;

            var commodities = _db.UNHSCodes.Where(p => p.Classification == "H5").Select(p => new { p.Code, p.Description }).ToList();


            var commoditySource = new ObservableCollection<AutoCompleteItem>(commodities.Select(p => new AutoCompleteItem { Code = p.Code, Name = $"{p.Code} - {p.Description}" }).ToList());
            txtCommodityCode.ItemsSource = commoditySource;

        }
        private void RadAutoCompleteBox_SearchTextChanged_1(object sender, EventArgs e)
        {
            var control = sender as RadAutoCompleteBox;
            var search = control.SearchText;
            if (search == null || search.Length < 2)
            {
                control.ItemsSource = null;
                return;
            }
            FillCommodityAutoComplete(control, search);
        }
        private void FillCommodityAutoComplete(RadAutoCompleteBox control, string search)
        {
            Task.Run(() =>
            {
                using (EmanifestContext db = new EmanifestContext())
                {
                    try
                    {
                        var commodities = db.UNHSCodes.Where(p => p.Classification == "H5" && p.Code.Contains(search)).Select(p => new { p.Code, p.Description }).ToList();
                        var source = commodities.Select(p => new { p.Code, Name = $"{p.Code} - {p.Description}" });
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
        private void FillHazardousCodesAutoComplete(RadAutoCompleteBox control, string search)
        {
            Task.Run(() =>
            {
                using (EmanifestContext db = new EmanifestContext())
                {
                    try
                    {
                        var codes = db.UNHazardousCodes.Where(p => p.Code.StartsWith(search)).Select(p => new { p.Code, Name = p.Description }).ToList();
                        var source = codes.Select(c => new { c.Code, Name = $"{c.Code}-{c.Name}" }).ToList();

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
        private void RadAutoCompleteBox_SearchTextChanged(object sender, EventArgs e)
        {

            var control = sender as RadAutoCompleteBox;
            var search = control.SearchText;
            if (search == null)
            {
                control.ItemsSource = null;
                return;
            }
            FillPackagesAutoComplete(control, search);
        }
        private void FillPackagesAutoComplete(RadAutoCompleteBox control, string search)
        {
            Task.Run(() =>
            {
                using (EmanifestContext db = new EmanifestContext())
                {
                    try
                    {
                        var packages = db.PackageCodes.Where(p => p.PackageCode.StartsWith(search)).Select(p => new { p.PackageCode, p.PackageDescription }).ToList();
                        var source = packages.Select(p => new { Code = p.PackageCode, Name = $"{p.PackageCode} - {p.PackageDescription}", Desc = p.PackageDescription });
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

        private void RadButton_Click(object sender, RoutedEventArgs e)
        {
            ValidationContext context = new ValidationContext(Consignment);
            List<System.ComponentModel.DataAnnotations.ValidationResult> results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
            var isValid = Validator.TryValidateObject(Consignment, context, results, true);
            results.RemoveAll(r => r.MemberNames.Any(m => PropertiesToIgnoreValidation.Contains(m)));
            results.RemoveAll(r => MessageToIgnoreValidation.Contains(r.ErrorMessage));
            if (!isValid)
            {
                if (results.Any())
                {
                    string error = "";
                    foreach (var validationResult in results)
                    {
                        error += validationResult.ErrorMessage + Environment.NewLine;
                    }
                    var owner = (sender as UIElement).ParentOfType<Window>();
                    RadWindow.Alert(new DialogParameters
                    {
                        Owner = owner,
                        Content = error
                    });
                }
                else
                {
                    Saved = true;
                    Close();
                    //all good
                }
            }
            else
            {
                Saved = true;
                Close();
            }

        }
        private void RadAutoCompleteBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = txtPackageCode.SelectedItem;
            if (item == null)
            {
                return;
            }
            var selectedItem = new { Value = "", Text = "", Display = "" };
            selectedItem = Cast(selectedItem, txtPackageCode.SelectedItem);
            if (selectedItem != null)
            {
                Consignment.PackageType = selectedItem.Text;
            }
        }
        private static T Cast<T>(T typeHolder, Object x)
        {
            // typeHolder above is just for compiler magic
            // to infer the type to cast x to
            return (T)x;
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void txtUNHazardousCodes_SearchTextChanged(object sender, EventArgs e)
        {
            var control = sender as RadAutoCompleteBox;
            var search = control.SearchText;
            if (search == null)
            {
                control.ItemsSource = null;
                return;
            }
            FillHazardousCodesAutoComplete(control, search);
        }

        private void txtCommodityCode_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtCommodityCode.SelectedItem == null)
            {
                var Items = txtCommodityCode.ItemsSource.Cast<AutoCompleteItem>().ToList();
                var chosenItems = Items.FirstOrDefault(i => i.Code == txtCommodityCode.SearchText);
                if (chosenItems != null)
                {
                    txtCommodityCode.SelectedItem = chosenItems;
                }
                else
                {
                    txtCommodityCode.SearchText = null;
                }

            }
        }

        private void txtUNHazardousCodes_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtUNHazardousCodes.SelectedItem == null)
            {
                var Items = txtUNHazardousCodes.ItemsSource.Cast<AutoCompleteItem>().ToList();
                var chosenItems = Items.FirstOrDefault(i => i.Code == txtUNHazardousCodes.SearchText);
                if (chosenItems != null)
                {
                    txtUNHazardousCodes.SelectedItem = chosenItems;
                }
                else
                {
                    txtUNHazardousCodes.SearchText = null;
                }

            }
        }
    }


}

