using CrystalDecisions.CrystalReports.Engine;
using Newtonsoft.Json.Linq;
using System;
using System.Windows.Forms;
using WindowsFormsApp3.Class;

namespace WindowsFormsApp3.Report
{
    public partial class RNCC : Form
    {
        public RNCC()
        {
            InitializeComponent();
        }

        private void RNCC_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        void LoadData()
        {
            ClassSQL classSQL = new ClassSQL();

            CrystalReport2 report = new CrystalReport2();
            report.SetDataSource(classSQL.Report("Suplier"));



            JArray data = classSQL.GetTable("Suplier", new string[] { "IdSuplier" });
            int length = data.Count;

            TextObject txt;
            txt = (TextObject)report.ReportDefinition.ReportObjects["Text7"];
            txt.Text = "Tổng số lượng nhà cung cấp: " + length;

            crystalReportViewer1.ReportSource = report;
        }
    }
}
