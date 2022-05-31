using CrystalDecisions.CrystalReports.Engine;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Windows.Forms;
using WindowsFormsApp3.Class;

namespace WindowsFormsApp3.Report
{
    public partial class FVattu : Form
    {
        public FVattu()
        {
            InitializeComponent();
        }

        private void FVattu_Load(object sender, EventArgs e)
        {
            JArray dataUnit = ClassIndex.SQL.GetTable("Unit", new string[] { "IdUnit", "DisplayNameUnit" });
            JArray dataSuplier = ClassIndex.SQL.GetTable("Suplier", new string[] { "IdSuplier", "DisplayNameSuplier" });
            JArray dataObject = ClassIndex.SQL.GetTable("Object", new string[] { "IdObject", "DisplayName", "IdUnit", "IdSuplier", "QRCode", "BarCode" });
            JArray newDataObject = new JArray();

            foreach (var item in dataObject)
            {
                JObject obj = new JObject();
                obj["IdObject"] = item["IdObject"];
                obj["DisplayName"] = item["DisplayName"];


                foreach (var itemUnit in dataUnit)
                {

                    if (item["IdUnit"].ToString() == itemUnit["IdUnit"].ToString())
                    {

                        obj["IdUnit"] = itemUnit["DisplayNameUnit"];
                    }
                }
                foreach (var itemSuplier in dataSuplier)
                {
                    if (item["IdSuplier"].ToString() == itemSuplier["IdSuplier"].ToString())
                    {
                        obj["IdSuplier"] = itemSuplier["DisplayNameSuplier"];
                    }
                }
                obj["QRCode"] = item["QRCode"];
                obj["BarCode"] = item["BarCode"];
                newDataObject.Add(obj);

            }
            DataTable table = new DataTable();
            DataRow row;
            DataColumn column;

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "IdObject";
            table.Columns.Add(column);

            // Create second column.
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "DisplayName";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "IdUnit";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "IdSuplier";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "QRCode";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "BarCode";
            table.Columns.Add(column);
            foreach (var item in newDataObject)
            {
                row = table.NewRow();
                row["IdObject"] = item["IdObject"];
                row["DisplayName"] = item["DisplayName"];
                row["IdUnit"] = item["IdUnit"];
                row["IdSuplier"] = item["IdSuplier"];
                row["QRCode"] = item["QRCode"];
                row["BarCode"] = item["BarCode"];

                table.Rows.Add(row);
            }
            CrystalReportVattu reportVattu = new CrystalReportVattu();

            reportVattu.SetDataSource(table);

            int length = dataUnit.Count;

            TextObject txt;
            txt = (TextObject)reportVattu.ReportDefinition.ReportObjects["Text1"];
            txt.Text = "Tổng số lượng vật tư: " + length;
            crystalReportViewer1.ReportSource = reportVattu;
        }
    }
}
