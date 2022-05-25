using doanC.Component;
using doanC.Sevice;
using Newtonsoft.Json.Linq;
using System;
using System.Windows.Forms;

namespace doanC.Screen
{
    public partial class Donvido : Form
    {
        public Donvido()
        {
            InitializeComponent();
        }
        protected int id;
        private void button1_Click(object sender, EventArgs e)
        {

            string nameButton = (sender as Button).Text;
            string name = textBox1.Text;
            if (name.Length >= 1)
            {
                if (nameButton == "Lưu")
                {
                    bool result = ClassIndex.donvido.CreateDonViDo(name);
                    if (result)
                    {
                        textBox1.Text = "";
                        loadData();
                    }
                }
                else if (nameButton == "Sửa")
                {
                    bool result = ClassIndex.donvido.UpdateDonViDo(id, name);
                    if (result)
                    {
                        textBox1.Text = "";
                        loadData();
                        button1.Text = "Lưu";
                    }
                }
            }
            else
            {

                MessageBox.Show("Bạn chưa nhập tên đơn vị đo");
            }
        }

        private void Donvido_Load(object sender, EventArgs e)
        {
            loadData();
        }
        void loadData()
        {
            try
            {
                JArray myList = ClassIndex.donvido.GetDovido();
                dataGridView1.DataSource = myList;
                new sizeDGV(dataGridView1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                string idValue = row.Cells[0].Value.ToString();
                string nameValue = row.Cells[1].Value.ToString();
                textBox1.Text = nameValue;

                int intid = Convert.ToInt32(idValue);
                id = intid;
                button1.Text = "Sửa";
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                string idValue = row.Cells[0].Value.ToString();
                string nameValue = row.Cells[1].Value.ToString();
                textBox1.Text = nameValue;
                int intid = Convert.ToInt32(idValue);
                id = intid;
                button1.Text = "Sửa";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool result = ClassIndex.donvido.DeleteDonViDo(id);
            if (result)
            {
                textBox1.Text = "";
                loadData();
            }
        }

        private void Donvido_MouseClick(object sender, MouseEventArgs e)
        {
            button1.Text = "Lưu";
        }
    }
}
