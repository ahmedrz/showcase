using IQManClient.DAL;
using IQMan.Helpers;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for winUpdate.xaml
    /// </summary>
    public partial class winUpdate : Window
    {
        DownloadInfoHelper helper;
        public winUpdate()
        {
            InitializeComponent();
        }

        private async void btnDownload_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                progress.Visibility = Visibility.Visible;
                using (EmanifestContext db = new EmanifestContext())
                {
                    helper = new DownloadInfoHelper(db);
                    if (chbVessels.IsChecked == true)
                        await helper.DownloadAndProcessVessels();
                    if (chbCountry.IsChecked == true)
                        await helper.DownloadAndProcessCountryCodes();
                    if (chbLocation.IsChecked == true)
                        await helper.DownloadAndProcessLocationCodes();
                    if (chbHSCodes.IsChecked == true)
                        await helper.DownloadAndProcessHSCodes();
                    if (chbPackages.IsChecked == true)
                        await helper.DownloadAndProcessPackagesCodes();
                    if (chbLines.IsChecked == true)
                        await helper.DownloadAndProcessLines();
                    if (chbContainerCodes.IsChecked == true)
                        await helper.DownloadAndProcessContainerIsoCodes();
                    if (chbUNHazardousCodes.IsChecked == true)
                        await helper.DownloadAndProcessUNHazardousCodes();
                    RadWindow.Alert(new DialogParameters { Owner = this, Content = "Finished" });
                }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                var innerException = ex.InnerException;
                while (innerException != null)
                {
                    errorMessage += Environment.NewLine;
                    errorMessage = innerException.Message;
                    innerException = innerException.InnerException;
                }
                RadWindow.Alert(new DialogParameters
                {
                    Owner = this,
                    Content = new TextBlock { Text = errorMessage, TextWrapping = TextWrapping.Wrap, Width = 250 }
                });

            }
            finally
            {
                progress.Visibility = Visibility.Hidden;
            }

        }
    }
}
