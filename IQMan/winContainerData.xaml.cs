using IQMan.Model;
using IQManClient.DAL;
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
    /// Interaction logic for winContainerData.xaml
    /// </summary>
    public partial class winContainerData : Window
    {
        public bool Saved { get; set; }
        List<string> PropertiesToIgnoreValidation = new List<string>() { };
        List<string> MessageToIgnoreValidation = new List<string>() { };
        public ContainerDetails Container { get; set; }
        private VoyageDetails Voyage { get; set; }
        EmanifestContext db;

        private void FillCombo()
        {
            var containerCodes = db.ContainerIsoCodes.ToList().Select(c => new { Code = c.IsoTypeCode, Name = $"{c.IsoTypeCode}-{c.IsoTypeDescription}" });
            var containerCodesSource = new ObservableCollection<AutoCompleteItem>(containerCodes.Select(p => new AutoCompleteItem { Code = p.Code, Name = $"{p.Code} - {p.Name}" }).ToList());
            txtContainerCodes.ItemsSource = containerCodesSource;

            //container owner types
            var containerOwnerTypes = new[] { new { Text = "COC - CARRIER OWNED CONTAINER", Value = "COC" }, new { Text = "SOC - SHIPPER OWNED CONTAINER", Value = "SOC" } }.ToList();
            cmbContainerOwnerType.ItemsSource = containerOwnerTypes;
        }

        public winContainerData(ContainerDetails container, VoyageDetails voyage)
        {
            Voyage = voyage;
            Container = container;
            if (Container == null)
            {
                Container = new ContainerDetails();
            }
            InitializeComponent();
            db = new EmanifestContext();
            DataContext = Container;
            FillCombo();
        }
        private void RadButton_Click(object sender, RoutedEventArgs e)
        {
            var owner = (sender as UIElement).ParentOfType<Window>();
            ValidationContext context = new ValidationContext(Container);
            List<System.ComponentModel.DataAnnotations.ValidationResult> results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
            var isValid = Validator.TryValidateObject(Container, context, results, true);
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
                        Content = new TextBlock
                        {
                            Width = 200,
                            Text = error,
                            TextWrapping = TextWrapping.WrapWithOverflow
                        }
                    });
                    return;
                }
            }
            //global validation 
            if (Voyage != null && Container != null)
            {
                var checkDublicate = Voyage.BOLDetails
                    .SelectMany(b => b.ContainerDetails)
                    .Where(c => c != Container
                    && c.ContainerNo == Container.ContainerNo)
                    .FirstOrDefault();
                if (checkDublicate != null)
                {
                    RadWindow.Alert(new DialogParameters
                    {
                        Owner = owner,
                        Content = new TextBlock
                        {
                            Width = 200,
                            Text = $"Container number ({checkDublicate.ContainerNo}) is already entered for this Voyage in BOL ({checkDublicate.BOLDetails?.BillOfLadingNumber}).",
                            TextWrapping = TextWrapping.WrapWithOverflow
                        }
                    });

                    return;
                }
            }
            //all is good
            Saved = true;
            Close();

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void txtContainerCodes_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtContainerCodes.SelectedItem == null)
            {
                var Items = txtContainerCodes.ItemsSource.Cast<AutoCompleteItem>().ToList();
                var chosenItems = Items.FirstOrDefault(i => i.Code == txtContainerCodes.SearchText);
                if (chosenItems != null)
                {
                    txtContainerCodes.SelectedItem = chosenItems;
                }
                else
                {
                    txtContainerCodes.SearchText = null;
                }

            }
        }
    }
}
