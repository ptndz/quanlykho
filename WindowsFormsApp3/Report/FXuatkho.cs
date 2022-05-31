using CrystalDecisions.CrystalReports.Engine;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Windows.Forms;
using WindowsFormsApp3.Class;

namespace WindowsFormsApp3.Report
{
    public partial class FXuatkho : Form
    {
        public FXuatkho()
        {
            InitializeComponent();
        }
        int danhapvao;
        private void FXuatkho_Load(object sender, EventArgs e)
        {
            //Tổng sản phẩm xuất:

            JArray dataCustomer = ClassIndex.SQL.GetTable("Customer", new string[] { "IdCustomer", "DisplayName", "Address", "Phone", "Email", "MoreInfo" });
            JArray dataObject = ClassIndex.SQL.GetTable("Object", new string[] { "IdObject", "DisplayName" });
            JArray dataInputInfo = ClassIndex.SQL.GetTable("InputInfo", new string[] { "IdInputInfo", "IdObject", "IdInput", "Count", "InputPrice", "OutputPrice", "Status" });
            JArray dataIdOutput = ClassIndex.SQL.GetTable("Output", new string[] { "IdOutput", "DateOutput" });
            JArray arryOutputInfo = ClassIndex.SQL.GetTable("OutputInfo", new string[] { "IdOutputInfo", "IdObject", "IdInputInfo", "IdOutput", "IdCustomer", "Count" });
            JArray newarryOutputInfo = new JArray();
            foreach (var item in arryOutputInfo)
            {
                JObject obj = new JObject();
                obj["id"] = item["IdOutputInfo"];
                foreach (var item3 in dataObject)
                {
                    if (item["IdObject"].ToString() == item3["IdObject"].ToString())
                    {
                        obj["DisplayNameObject"] = item3["DisplayName"].ToString();

                    }
                }
                foreach (var item5 in dataIdOutput)
                {
                    if (item["IdOutput"].ToString() == item5["IdOutput"].ToString())
                    {
                        obj["DateOutput"] = item5["DateOutput"].ToString();

                    }
                }
                foreach (var item2 in dataCustomer)
                {
                    if (item["IdCustomer"].ToString() == item2["IdCustomer"].ToString())
                    {
                        obj["DisplayNameCustomer"] = item2["DisplayName"].ToString();

                    }
                }

                foreach (var item4 in dataInputInfo)
                {
                    if (item["IdInputInfo"].ToString() == item4["IdInputInfo"].ToString())
                    {
                        obj["OutputPrice"] = item4["OutputPrice"].ToString();

                    }
                }

                obj["Count"] = item["Count"];
                newarryOutputInfo.Add(obj);

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
            column.ColumnName = "IdInputInfo";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "IdOutput";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "IdCustomer";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "Count";
            table.Columns.Add(column);


            foreach (var item in newarryOutputInfo)
            {
                row = table.NewRow();
                row["IdObject"] = item["DisplayNameObject"];
                row["IdInputInfo"] = item["OutputPrice"];
                row["IdOutput"] = item["DateOutput"];
                row["IdCustomer"] = item["DisplayNameCustomer"];
                row["Count"] = item["Count"];


                table.Rows.Add(row);
            }
            foreach (var item in arryOutputInfo)
            {
                danhapvao += int.Parse(item["Count"].ToString());
            }
            CrystalReportXuatkho reportXuatkho = new CrystalReportXuatkho();
            reportXuatkho.SetDataSource(table);
            int length = arryOutputInfo.Count;

            TextObject txt;
            TextObject txt2;
            txt = (TextObject)reportXuatkho.ReportDefinition.ReportObjects["Text7"];
            txt2 = (TextObject)reportXuatkho.ReportDefinition.ReportObjects["Text8"];
            txt.Text = "Tổng số lượng nhập vào: " + length;
            txt2.Text = "Tổng số lượng hàng: " + danhapvao;
            crystalReportViewer1.ReportSource = reportXuatkho;

        }
    }
}
