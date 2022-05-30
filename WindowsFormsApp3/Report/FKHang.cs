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
            report.SetDataSource(classSQL.Report("Customer"));
            crystalReportViewer1.ReportSource = report;
        }
    }
}
