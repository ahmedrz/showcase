using IQMan.Model;
using IQManClient.DAL;
using IQManClient.DAL.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
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
    /// Interaction logic for winBOLData.xaml
    /// </summary>
    public partial class winBOLData : Window
    {
        public bool Saved { get; set; }
        List<string> PropertiesToIgnoreValidation = new List<string>() { "ConsignmentDetails" };
        List<string> MessageToIgnoreValidation = new List<string>() { };
        EmanifestContext db;
        public BOLDetails BOLDetails { get; set; }
        private VoyageDetails Voyage { get; set; }

        public winBOLData(BOLDetails bol, VoyageDetails voyage)
        {
            Voyage = voyage;
            BOLDetails = bol;
            FillData();
            db = new EmanifestContext();
            InitializeComponent();
            FillCombo();
            DataContext = BOLDetails;

        }
        private void FillData()
        {
            if (BOLDetails == null)
            {
                BOLDetails = new BOLDetails();
                if (Settings.User?.Carriers != null)
                {
                    BOLDetails.BoxPartneringAgentCode = Settings.User.Carriers.CarrierName;
                }
            }

        }

        private void FillCombo()
        {
            try
            {
                var lines = db.Lines.ToList().Select(l => new { Text = $"{l.LineCode}-{l.LineName}", Value = l.LineCode }).ToList();
                cmbLine.ItemsSource = lines;
                //trade codes
                var tradeCodes = new[] { new { Text = "I - IMPORT", Value = "I" }, new { Text = "T - TRANS-SHIPMENT", Value = "T" } }.ToList();
                cmbTradeCode.ItemsSource = tradeCodes;
                //trans modes
                var transModes = new[] { new { Text = "S - SEA", Value = "S" }, new { Text = "A - AIR", Value = "A" }, new { Text = "R - ROAD", Value = "R" } }.ToList();
                cmbTrasMode.ItemsSource = transModes;

                //cargoCodes
                var cargoCodes = new[] {
                new { Text = "F - FCL CONTAINER", Value = "F" },
                new { Text = "L - LCL CONTAINER", Value = "L" },
                new { Text = "M - EMPTY CONTAINER", Value = "M" },
                new { Text = "B - BULK SOLID", Value = "B" },
                new { Text = "Q - BULK LIQUID", Value = "Q" },
                new { Text = "R - RO-RO UNIT", Value = "R" },
                new { Text = "P - PASSENGER", Value = "P" },
                new { Text = "A - LIVE STICK (ANIMALS)", Value = "A" },
                new { Text = "G - GENERAL CARGO ( BREAK BULK )", Value = "G" },
                }.ToList();
                cmbCargoCode.ItemsSource = cargoCodes;

                //storage request Types
                var storageRequestTypes = new[] {
                new { Text = "D - DIRECT DELIVEY", Value = "D" },
                new { Text = "S - STORAGE IN SHEDS", Value = "S" },
                new { Text = "Y - STORAGE IN YARDS", Value = "Y" },
                }.ToList();
                cmbStorageRequest.ItemsSource = storageRequestTypes;

                //container service types
                var containerServiceTypes = new[] {
                "FCL/FCL", "FCL/LCL", "LCL/LCL", "LCL/FCL"
                }.ToList();
                cmbContainerServiceType.ItemsSource = containerServiceTypes;


                //contract cariage conditions
                var contractCariageConditions = new[] {
                new { Text = "C&F - COST & FREIGHT", Value = "C&F" },
                new { Text = "CIF - COST, INSURANCE AND FREIGHT", Value = "CIF" },
                new { Text = "FOB - FREE ON BOARD", Value = "FOB" },
                new { Text = "D/D - DOOR TO DOOR", Value = "D/D" },
                new { Text = "XXX - OTHERS", Value = "XXX" },
                }.ToList();
                cmbCarriageConditions.ItemsSource = contractCariageConditions;

                var dbPackages = db.PackageCodes.Select(p => new { Value = p.PackageCode, Text = p.PackageDescription }).ToList()
              .Select(p => new { p.Value, p.Text, Display = $"{p.Value} - {p.Text}" }).ToList();
                txtPackageCode.ItemsSource = dbPackages;

                var commodities = db.UNHSCodes.Where(p => p.Classification == "H5").Select(p => new { p.Code, p.Description }).ToList();
                //cmbCommodityCode.ItemsSource = commodities.Select(p => new { p.Code, Name = $"{p.Code} - {p.Description}" });
                var commoditySource = new ObservableCollection<AutoCompleteItem>(commodities.Select(p => new AutoCompleteItem { Code = p.Code, Name = $"{p.Code} - {p.Description}" }).ToList());
                txtCommodityCode.ItemsSource = commoditySource;
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

        private void FillCountryAutoComplete(RadAutoCompleteBox control, string search)
        {
            Task.Run(() =>
                {
                    using (EmanifestContext db = new EmanifestContext())
                    {
                        var countryCodes = db.CountryCodes.Where(c => c.CountryCode.StartsWith(search)).Select(c => new { Code = c.CountryCode, Name = c.CountryName }).ToList();
                        var source = countryCodes.Select(c => new { c.Code, Name = $"{c.Code} - {c.Name}" });
                        Dispatcher.Invoke(() =>
                        {
                            control.ItemsSource = source;
                        });
                    }

                });

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
        private void FillCommodityAutoComplete(RadAutoCompleteBox control, string search)
        {
            Task.Run(() =>
            {
                using (EmanifestContext db = new EmanifestContext())
                {
                    try
                    {
                        var commodities = db.UNHSCodes.Where(p => p.Classification == "H5" && p.Code.Contains(search)).Select(p => new { p.Code, p.Description }).ToList();
                        var source = commodities.Select(p => new { p.Code, Name = $"{p.Code} - {p.Description}", Desc = p.Description });
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
            var owner = (sender as UIElement).ParentOfType<Window>();
            ValidationContext context = new ValidationContext(BOLDetails);
            List<System.ComponentModel.DataAnnotations.ValidationResult> results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
            var isValid = Validator.TryValidateObject(BOLDetails, context, results, true);
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
                    RadWindow.Alert(new DialogParameters
                    {
                        Owner = owner,
                        Content = error
                    });
                    return;
                }
            }
            //global validation 
            if (Voyage != null && BOLDetails != null)
            {
                var checkDublicate = Voyage.BOLDetails
                    .Where(b => b != BOLDetails
                    && b.BillOfLadingNumber == BOLDetails.BillOfLadingNumber)
                    .FirstOrDefault();
                if (checkDublicate != null)
                {
                    RadWindow.Alert(new DialogParameters
                    {
                        Owner = owner,
                        Content = $"Bill of lading number ({checkDublicate.BillOfLadingNumber}) is already entered for this Voyage."
                    });
                    return;
                }
            }
            //all is good
            Saved = true;
            Close();


        }

        private void txtDischargePort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var control = sender as RadAutoCompleteBox;
            if (control.Name == "txtOriginPort")
            {
                BOLDetails.PortCodeOfLoading = control.SearchText;
            }
            if (control.Name == "txtDischargePort")
            {
                BOLDetails.PortCodeOfDestination = control.SearchText;
            }
        }

        private void txtCountryOrigin_SearchTextChanged(object sender, EventArgs e)
        {
            try
            {
                var control = sender as RadAutoCompleteBox;
                var search = control.SearchText;
                if (search == null)
                {
                    control.ItemsSource = null;
                    return;
                }
                FillCountryAutoComplete(control, search);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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

        private void RadAutoCompleteBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = txtPackageCode.SelectedItem;
            if (item == null)
            {
                return;
            }
            var selectedItem = new { Value = "", Text = "", Display = "" };
            selectedItem = Cast(selectedItem, item);
            if (selectedItem != null)
            {
                BOLDetails.PackageType = selectedItem.Text;
            }
        }
        private static T Cast<T>(T typeHolder, Object x)
        {
            // typeHolder above is just for compiler magic
            // to infer the type to cast x to
            return (T)x;
        }

        private void cmbCommodityCode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //var item = cmbCommodityCode.SelectedItem;
            //if (item == null)
            //{
            //    return;
            //}
            //var selectedItem = new { Code = "", Name = "", Desc = "" };
            //selectedItem = Cast(selectedItem, item);
            //if (selectedItem != null)
            //{
            //    BOLDetails.CommodityDescription = selectedItem.Desc;
            //}
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // FillData();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

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

        private void txtOriginPort_LostFocus(object sender, RoutedEventArgs e)
        {

        }
    }

}
