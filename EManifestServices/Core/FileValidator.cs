using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace EManifestServices.Core
{
    public class FileValidator
    {
        public DataSet Data { get; set; }
        public bool Validate(IExcelDataReader reader)
        {
            bool isValid = true;
            var data = reader.AsDataSet();
            var table = data.Tables[0];
            isValid = isValid && table.Rows[0][1].ToString() == "VESSEL";
            isValid = isValid && table.Rows[0][3].ToString() == "VOYAGE";
            isValid = isValid && table.Rows[0][5].ToString() == "TRANSHIP PORT";

            isValid = isValid && table.Rows[1][1].ToString() == "EXPECTED ARRIVAL";
            isValid = isValid && table.Rows[1][3].ToString() == "RECEIVING PLACE";
            isValid = isValid && table.Rows[1][5].ToString() == "DISCHARGING PORT";

            isValid = isValid && table.Rows[2][1].ToString() == "NATIONALITY";
            isValid = isValid && table.Rows[2][3].ToString() == "LOADING PORT";
            isValid = isValid && table.Rows[2][5].ToString() == "DESTINATION";

            //items header
            isValid = isValid && table.Rows[3][1].ToString() == "Contr. No./Item No";
            isValid = isValid && table.Rows[3][2].ToString() == "Container Size/Item Size";
            isValid = isValid && table.Rows[3][3].ToString() == "Container_Type";
            isValid = isValid && table.Rows[3][4].ToString() == "Item Quantity";
            isValid = isValid && table.Rows[3][5].ToString() == "Container Weight/Item Weight";
            isValid = isValid && table.Rows[3][6].ToString() == "BL Number";
            isValid = isValid && table.Rows[3][7].ToString() == "Commodity Code/Description";
            isValid = isValid && table.Rows[3][8].ToString() == "Consignee Fullname";
            isValid = isValid && table.Rows[3][9].ToString() == "Notifier";
            isValid = isValid && table.Rows[3][10].ToString() == "Shipper Co";
            isValid = isValid && table.Rows[3][11].ToString() == "Container_Status";
            if (isValid)
            {
                Data = data;
            }
            return isValid;
        }
    }
}