using System.Windows.Forms;
using WindowsFormsApp3.Class;
using WindowsFormsApp3.Report;

namespace WindowsFormsApp3.Screen
{
    public partial class Donvido : Form
    {
        public Donvido()
        {
            InitializeComponent();
        }
        int id;
        private void Donvido_Load(object sender, System.EventArgs e)
        {
            GetData();
        }

        void GetData()
        {
            dataGridView1.DataSource = ClassIndex.donViDo.GetAll();
            new sizeDGV(dataGridView1);
        }

        private void textBox1_TextChanged(object sender, System.EventArgs e)
        {

        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            string nameButton = (sender as Button).Text;

            if (textBox1.Text.Length > 0)
            {
                if (nameButton == "Thêm")
                {
                    if (ClassIndex.donViDo.AddName(textBox1.Text))
                    {
                        GetData();
                    }
                }
                else if (nameButton == "Cập nhật")
                {
                    MessageBox.Show(id.ToString());
                    if (ClassIndex.donViDo.UpdateName(id, textBox1.Text))
                    {
                        button1.Text = "Thêm";
                        GetData();
                    }
                }

            }
            else
            {
                MessageBox.Show("Bạn chưa nhập tên đơn vị đo");
            }
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            ReportUnit reportUnit = new ReportUnit();
            reportUnit.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex > 0)
            {
                id = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                button1.Text = "Cập nhật";
            }
        }

        private void dataGridView1_SelectionChanged(object sender, System.EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                id = int.Parse(row.Cells[0].Value.ToString());
                textBox1.Text = row.Cells[1].Value.ToString();
                button1.Text = "Cập nhật";
            }
        }


        private void Donvido_MouseClick(object sender, MouseEventArgs e)
        {
            textBox1.Text = "";
            id = 0;
            button1.Text = "Thêm";
        }

        private void dataGridView1_Click(object sender, System.EventArgs e)
        {

        }

        private void button3_Click(object sender, System.EventArgs e)
        {
            if (id > 0)
            {
                bool delete = ClassIndex.donViDo.DeleteName(id);

                if (delete)
                {
                    textBox1.Text = "";
                    GetData();
                }
            }
        }
    }
}
