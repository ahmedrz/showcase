using EManifestServices.DAL;
using EManifestServices.Helpers;
using EManifestServices.Models;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EManifestServices.Core
{
    public class FileDataExtractor
    {
        //FileValidator validator;
        //public FileDataExtractor()
        //{
        //    validator = new FileValidator();
        //}
        //private string NullIfEmpty(string input)
        //{
        //    if (!string.IsNullOrEmpty(input) && !string.IsNullOrWhiteSpace(input))
        //    {
        //        return input;
        //    }
        //    return null;
        //}
        //public ManifestModel GetData(IExcelDataReader reader)
        //{
        //    if (validator.Validate(reader))
        //    {
        //        var data = validator.Data;
        //        if (data == null || data.Tables.Count == 0)
        //        {
        //            throw new Exception("Dataset is empty or invalid choose the right file.");
        //        }
        //        var dataTable = data.Tables[0];

        //        ManifestModel manifest = new ManifestModel
        //        {
        //            ManifestId = Guid.NewGuid(),
        //            //PortId = PortHelper.GetPortId(dataTable.Rows[2][6].ToString()),
        //            //VesselName = NullIfEmpty(dataTable.Rows[0][2].ToString()),
        //            //ExpectedArrivalDate = dataTable.Rows[1][2].ToString() != "" ? (DateTime?)Convert.ToDateTime(dataTable.Rows[1][2].ToString()) : null,
        //            //Nationality = NullIfEmpty(dataTable.Rows[2][2].ToString()),
        //            //Voyage = NullIfEmpty(dataTable.Rows[0][4].ToString()),
        //            //ReceivingPlace = NullIfEmpty(dataTable.Rows[1][4].ToString()),
        //            //LoadingPort = NullIfEmpty(dataTable.Rows[2][4].ToString()),
        //            //TransshipPort = NullIfEmpty(dataTable.Rows[0][6].ToString()),
        //            //DischargingPort = NullIfEmpty(dataTable.Rows[1][6].ToString()),
        //            //Destination = NullIfEmpty(dataTable.Rows[2][6].ToString()),
        //            CarrierId = CarrierHelper.GetCarrierId(),
        //            StatusId = new Guid("f2fb713c-8986-44a2-bd3a-a0d64dddeef9"),
        //        };
        //        for (int i = 4; i < dataTable.Rows.Count; i++)
        //        {
        //            ManifestItemModel itemModel = new ManifestItemModel
        //            {
        //                ManifestId = manifest.ManifestId,
        //                ManifestItemId = Guid.NewGuid(),
        //                ItemNo = NullIfEmpty(dataTable.Rows[i][1].ToString()),
        //                ItemSize = dataTable.Rows[i][2].ToString() != "" ? (double?)Convert.ToDouble(dataTable.Rows[i][2].ToString()) : null,
        //                ContainerType = NullIfEmpty(dataTable.Rows[i][3].ToString()),
        //                ItemQuantity = dataTable.Rows[i][4].ToString() != "" ? (double?)Convert.ToDouble(dataTable.Rows[i][4].ToString()) : null,
        //                ItemWeight = dataTable.Rows[i][5].ToString() != "" ? (double?)Convert.ToDouble(dataTable.Rows[i][5].ToString()) : null,
        //                BLNo = NullIfEmpty(dataTable.Rows[i][6].ToString()),
        //                Description = NullIfEmpty(dataTable.Rows[i][7].ToString()),
        //                Consignee = NullIfEmpty(dataTable.Rows[i][8].ToString()),
        //                Notifier = NullIfEmpty(dataTable.Rows[i][9].ToString()),
        //                Shipper = NullIfEmpty(dataTable.Rows[i][10].ToString()),
        //                ContainerStatus = NullIfEmpty(dataTable.Rows[i][11].ToString()),
        //            };
        //            manifest.ManifestItems.Add(itemModel);
        //        }
        //        return manifest;
        //    }
        //    else
        //    {
        //        throw new Exception("the file is not in correct format try downloing the sample file and make the nessecary changes");
        //    }
        //}
    }
}