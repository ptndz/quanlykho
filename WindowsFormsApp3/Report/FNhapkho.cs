using CrystalDecisions.CrystalReports.Engine;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Windows.Forms;
using WindowsFormsApp3.Class;

namespace WindowsFormsApp3.Report
{
    public partial class FNhapkho : Form
    {
        public FNhapkho()
        {
            InitializeComponent();
        }
        int danhapvao;
        private void FNhapkho_Load(object sender, EventArgs e)
        {

            JArray dataInputInfo = ClassIndex.SQL.GetTable("InputInfo", new string[] { "IdInputInfo", "IdObject", "IdInput", "Count", "InputPrice", "OutputPrice", "Status" });

            JArray dataInput = ClassIndex.SQL.GetTable("Input", new string[] { "IdInput", "DateInput" });

            JArray dataObject = ClassIndex.SQL.GetTable("Object", new string[] { "IdObject", "DisplayName" });
            JArray newDataObject = new JArray();
            foreach (var item in dataInputInfo)
            {
                JObject obj = new JObject();
                obj["IdInputInfo"] = item["IdInputInfo"];
                foreach (var itemObject in dataObject)
                {
                    if (itemObject["IdObject"].ToString() == item["IdObject"].ToString())
                    {
                        obj["IdObject"] = itemObject["DisplayName"];
                    }
                }
                foreach (var itemInput in dataInput)
                {
                    if (itemInput["IdInput"].ToString() == item["IdInput"].ToString())
                    {
                        obj["IdInput"] = itemInput["DateInput"];
                    }
                }

                obj["Count"] = item["Count"];
                obj["InputPrice"] = item["InputPrice"];
                obj["OutputPrice"] = item["OutputPrice"];

                newDataObject.Add(obj);
            }
            DataTable table = new DataTable();
            DataRow row;
            DataColumn column;

            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "IdObject";
            table.Columns.Add(column);

            // Create second column.
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "IdInput";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "Count";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "InputPrice";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "OutputPrice";
            table.Columns.Add(column);


            foreach (var item in newDataObject)
            {
                row = table.NewRow();
                row["IdObject"] = item["IdObject"];
                row["IdInput"] = item["IdInput"];
                row["Count"] = item["Count"];
                row["InputPrice"] = item["InputPrice"];
                row["OutputPrice"] = item["OutputPrice"];


                table.Rows.Add(row);
            }
            foreach (var item in dataInputInfo)
            {
                danhapvao += int.Parse(item["Count"].ToString());
            }
            CrystalReportNhapKho reportNhapKho = new CrystalReportNhapKho();
            reportNhapKho.SetDataSource(table);
            int length = dataInputInfo.Count;

            TextObject txt;
            TextObject txt2;
            txt = (TextObject)reportNhapKho.ReportDefinition.ReportObjects["Text7"];
            txt2 = (TextObject)reportNhapKho.ReportDefinition.ReportObjects["Text10"];
            txt.Text = "Tổng sản phẩm xuất: " + length;
            txt2.Text = "Tổng số lượng hàng: " + danhapvao;
            crystalReportViewer1.ReportSource = reportNhapKho;
        }
    }
}
