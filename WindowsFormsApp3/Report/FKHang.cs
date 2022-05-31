using CrystalDecisions.CrystalReports.Engine;
using Newtonsoft.Json.Linq;
using System;
using System.Windows.Forms;
using WindowsFormsApp3.Class;

namespace WindowsFormsApp3.Report
{
    public partial class FKHang : Form
    {
        public FKHang()
        {
            InitializeComponent();
        }

        private void FKHang_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        void LoadData()
        {
            ClassSQL classSQL = new ClassSQL();

            CrystalReportKHang report = new CrystalReportKHang();
            JArray data = classSQL.GetTable("Customer", new string[] { "IdCustomer" });
            int length = data.Count;
            report.SetDataSource(classSQL.Report("Customer"));
            TextObject txt;
            txt = (TextObject)report.ReportDefinition.ReportObjects["Text8"];
            txt.Text = "Tổng số lượng khách hàng: " + length;
            crystalReportViewer1.ReportSource = report;

        }
    }
}
