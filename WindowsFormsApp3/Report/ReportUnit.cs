using System;
using System.Windows.Forms;
using WindowsFormsApp3.Class;
namespace WindowsFormsApp3.Report
{
    public partial class ReportUnit : Form
    {
        public ReportUnit()
        {
            InitializeComponent();
        }

        private void ReportUnit_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        void LoadData()
        {
            ClassSQL classSQL = new ClassSQL();
            /*            SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM Unit", classSQL.connection());
                        DataTable table = new DataTable();
                        dataAdapter.Fill(table);*/
            CrystalReport1 report = new CrystalReport1();
            report.SetDataSource(classSQL.Report("Unit"));
            crystalReportViewer1.ReportSource = report;
        }
    }
}
