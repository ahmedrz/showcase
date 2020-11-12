using IQManClient.DAL;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for winVehicleData.xaml
    /// </summary>
    public partial class winVehicleData : Window
    {
        public bool Saved { get; set; }
        List<string> PropertiesToIgnoreValidation = new List<string>() { "SerialNumber" };
        List<string> MessageToIgnoreValidation = new List<string>() { };
        public VehicleDetails Vehicle { get; set; }
        public winVehicleData(VehicleDetails vehicle)
        {
            Vehicle = vehicle;
            if (Vehicle == null)
            {
                Vehicle = new VehicleDetails();
            }
            InitializeComponent();
            DataContext = Vehicle;
            FillCombo();
        }
        private void FillCombo()
        {
            //used or new
            var usedOrNew = new[] { new { Text = "U - USED", Value = "U" }, new { Text = "N - NEW", Value = "N" } }.ToList();
            cmbUsedOrNew.ItemsSource = usedOrNew;

            //rolling or static
            var rollingStatis = new[] { new { Text = "R - ROLLING", Value = "R" }, new { Text = "S - STATIC", Value = "S" } }.ToList();
            cmbRollingOrStatic.ItemsSource = rollingStatis;
            //vehicleIndicators
            var vehicleIndicators = new[] { new { Text = "V - VEHICLE", Value = "V" }, new { Text = "E - EQUIPMENT", Value = "E" }, new { Text = "X - OTHERS", Value = "X" } }.ToList();
            cmbVehicleIndicator.ItemsSource = vehicleIndicators;


        }
        private void RadButton_Click(object sender, RoutedEventArgs e)
        {
            ValidationContext context = new ValidationContext(Vehicle);
            List<System.ComponentModel.DataAnnotations.ValidationResult> results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
            var isValid = Validator.TryValidateObject(Vehicle, context, results, true);
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
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
