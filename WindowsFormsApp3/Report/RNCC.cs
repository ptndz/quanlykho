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
            crystalReportViewer1.ReportSource = report;
        }
    }
}
