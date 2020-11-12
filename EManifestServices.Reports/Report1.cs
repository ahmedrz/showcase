namespace EManifestServices.Reports
{
    using EManifestServices.DAL;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for Report1.
    /// </summary>
    public partial class Report1 : Telerik.Reporting.Report
    {
        public Report1()
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();
            this.NeedDataSource += Report1_NeedDataSource;

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        private void Report1_NeedDataSource(object sender, EventArgs e)
        {
            Telerik.Reporting.Processing.Report processingReport = (Telerik.Reporting.Processing.Report)sender;
            if (processingReport.Parameters["voyageDetailsId"].Value != null)
            {
                Guid voyageDetailsId = new Guid(processingReport.Parameters["voyageDetailsId"].Value.ToString());
                LoadDataSource(voyageDetailsId);
            }

        }

        private void LoadDataSource(Guid voyageDetailsId)
        {
            using (EmanifestContext db = new EmanifestContext())
            {
                if (voyageDetailsId == Guid.Empty)
                {

                }
                else
                {

                    var voyages = db.VoyageDetails
                            .Include("BOLDetails.ConsignmentDetails.VehicleDetails")
                            .Include("BOLDetails.ContainerDetails.ConsignmentDetails.VehicleDetails")
                            .FirstOrDefault(v => v.VoyageDetailsId == voyageDetailsId);
                    table1.DataSource = voyages.BOLDetails;
                }
            }




        }
    }
}