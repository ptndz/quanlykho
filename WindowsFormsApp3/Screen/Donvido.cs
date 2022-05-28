using System.Windows.Forms;
using WindowsFormsApp3.Class;
namespace WindowsFormsApp3.Screen 
{
    public partial class Donvido : Form
    {
        public Donvido()
        {
            InitializeComponent();
        }
        
        private void Donvido_Load(object sender, System.EventArgs e)
        {
            GetData();
        }
        
        void GetData()
        {
            dynamic data = ClassIndex.donViDo.GetAll();
            dataGridView1.DataSource = data;
            
        }
    }
}
