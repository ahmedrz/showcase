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

namespace EmanifestDownloader
{
    /// <summary>
    /// Interaction logic for winDeclineVoyage.xaml
    /// </summary>
    public partial class winDeclineVoyage : RadWindow
    {
        public string DeclineReason { get; set; }
        public winDeclineVoyage()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
