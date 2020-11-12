using EmanifestDownloader.Helper;
using EmanifestDownloader.Models;
using PortSystem.DAL;
using PortSystem.DAL.Core;
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
    /// Interaction logic for winDownloadVoyage.xaml
    /// </summary>
    public partial class winDownloadVoyage : RadWindow
    {
        ManifestSaver saver;
        VoyageDetails _voyage;
        EmanifestContext db;
        public winDownloadVoyage(VoyageDetails voyage)
        {
            InitializeComponent();
            db = new EmanifestContext();
            saver = new ManifestSaver();
            _voyage = voyage;
            this.ShowInTaskbar("Download Voyage");
        }
        private void FillCombo()
        {
            try
            {
                var lines = _voyage.BOLDetails.Select(b => b.BoxPartneringLineCode).Distinct().ToList();
                cmbLines.ItemsSource = lines;
                var ports = db.Ports.Where(p => p.PortCode == _voyage.PortCodeOfDischarge).Select(p => p.PortId).ToList();
                if (ports.Any())
                {
                    var department = db.Departments.Include("Ports").Where(d => ports.Contains(d.PortId)).ToList();
                    cmbDeps.ItemsSource = department.Select(d => new ComboBoxSource { Id = d.DepartmentId, Text = $"{d.DepartmentName} - {d.Ports.PortName}" }).ToList();
                    ((GridViewComboBoxColumn)this.bolGrid.Columns["depColumn"]).ItemsSource = department.ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        private void FillGrid()
        {
            try
            {
                bolGrid.ItemsSource = _voyage.BOLDetails;
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        private void RadWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (_voyage != null)
            {
                FillCombo();
                FillGrid();
            }
            else
            {
                Alert(new DialogParameters { Content = "Voyage is null", Owner = this });
                Close();
            }
        }

        private void btnchoose_Click(object sender, RoutedEventArgs e)
        {
            var chosenLines = cmbLines.SelectedItems.Cast<string>().ToList();
            var chosenDep = cmbDeps.SelectedItem as ComboBoxSource;
            if (!chosenLines.Any() && chosenDep == null)
            {
                return;
            }
            if (!chosenLines.Any())
            {
                foreach (var bol in _voyage.BOLDetails)
                {
                    bol.DepartmentId = chosenDep.Id;
                }
            }
            else
            {
                foreach (var bol in _voyage.BOLDetails.Where(b => chosenLines.Contains(b.BoxPartneringLineCode)))
                {
                    bol.DepartmentId = chosenDep.Id;
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            RadWindow.Confirm(
                              new DialogParameters
                              {
                                  Owner = this,
                                  Content = "Are you sure you want to leave this window you will lose your work??",
                                  Closed = (s, args) =>
                                  {
                                      var result = args.DialogResult;
                                      if (result == false)
                                      {
                                          //do nothing
                                      }
                                      else
                                      {
                                          Close();
                                      }
                                  }
                              });
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var emptyDeps = _voyage.BOLDetails.Any(b => b.DepartmentId == null);
            if (emptyDeps)
            {
                Alert(new DialogParameters
                {
                    Owner = this,
                    Content = "You have to choose a department for all the Bill Of Ladings.",

                });
                return;
            }
            RadWindow.Confirm(
                             new DialogParameters
                             {
                                 Owner = this,
                                 Content = "Are you sure you want to save your changes to the database??",
                                 Closed = (s, args) =>
                                 {
                                     var result = args.DialogResult;
                                     if (result == false)
                                     {
                                         //do nothing
                                     }
                                     else
                                     {
                                         try
                                         {

                                             saver.Save(_voyage);
                                         }
                                         catch (Exception ex)
                                         {
                                             Alert(new DialogParameters
                                             {
                                                 Owner = this,
                                                 Content = ex.Message

                                             });
                                         }
                                     }
                                 }
                             });
        }
    }
}
