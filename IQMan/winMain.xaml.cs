using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using System.Diagnostics;
using Telerik.Windows.Controls;
using EManifestClient.Core;
using IQManClient.DAL;

namespace IQMan
{
    /// <summary>
    /// Interaction logic for winMain.xaml
    /// </summary>
    public partial class winMain : Window
    {
        private String updaterModulePath;
        public winMain()
        {
            InitializeComponent();
            // Compute the updater.exe path relative to the application main module path
            updaterModulePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "updater.exe");
            // Start the thread that will launch the updater in silent mode with 10 second delay.
            Thread thread = new Thread(new ThreadStart(StartSilent));
            thread.Start();
        }

        private void StartSilent()
        {
            if (File.Exists(updaterModulePath))
            {
                Thread.Sleep(10000);
                Process process = Process.Start(updaterModulePath, "/silent");
                process.Close();
            }
        }


        private void itmCheckUpdate_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            if (File.Exists(updaterModulePath))
            {
                Process process = Process.Start(updaterModulePath, "/checknow");
                process.Close();
            }
            else
            {
                RadWindow.Alert(new DialogParameters { Owner = this, Content = "Updater file not found" });
            }
        }

        private void itmUpdateConf_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            if (File.Exists(updaterModulePath))
            {
                Process process = Process.Start(updaterModulePath, "/configure");
                process.Close();
            }
            else
            {
                RadWindow.Alert(new DialogParameters { Owner = this, Content = "Updater file not found" });
            }
        }

        private void itmDownloadData_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {

            if (Settings.User == null || Settings.User.CarrierId == null)
            {
                return;
            }
            winUpdate win = new winUpdate();
            win.ShowDialog();

        }

        private void itmVoyages_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            winVoyages window = new winVoyages();
            window.Show();
        }
    }
}
