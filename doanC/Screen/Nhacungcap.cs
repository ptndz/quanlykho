using doanC.Sevice;
using Newtonsoft.Json.Linq;
using System;
using System.Windows.Forms;

namespace doanC.Screen
{
    public partial class Nhacungcap : Form
    {
        public Nhacungcap()
        {
            InitializeComponent();
        }
        protected int id;
        protected string ten;
        protected string diachi;
        protected string sdt;
        protected string email;
        protected string ghichu;
        
        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void Nhacungcap_Load(object sender, EventArgs e)
        {
            loadData();
        }
        void loadData()
        {
            JArray myList = ClassIndex.nhacungcap.GetAllNCC();
            dataGridView1.DataSource = myList;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex > 0)
            {
                id = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                ten = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                diachi = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                sdt = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                email = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                ghichu = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                textBox1.Text = ten;
                textBox2.Text = diachi;
                textBox3.Text = sdt;
                textBox4.Text = email;
                richTextBox1.Text = ghichu;
                button1.Text = "Sửa";

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {     
            if (CheckForm())
            {
                string nameButton = (sender as Button).Text;

                if (nameButton == "Lưu")
                {
                    SetData();
                    ClassIndex.nhacungcap.CreateNCC(ten, diachi, sdt, email, ghichu);
                    ClearForm();
                    loadData();
                }
                else if (nameButton == "Sửa")
                {
                    SetData();
                    ClassIndex.nhacungcap.UpdateNCC(id, ten, diachi, sdt, email, ghichu);
                    ClearForm();
                    button1.Text = "Lưu";
                    loadData();
                }

            }
            else
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
            }

        }
         bool CheckForm()
        {
            string nameValue = textBox1.Text;
            string addressValue = textBox2.Text;
            string phoneValue = textBox3.Text;
            string emailValue = textBox4.Text;
            string noteValue = richTextBox1.Text;
            if (nameValue.Length >= 1 && addressValue.Length >= 1 && phoneValue.Length >= 1 && emailValue.Length >= 1 && noteValue.Length >= 1)
            {
                return true;
            }
            return false;
        }
        void ClearForm()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            richTextBox1.Text = "";
        }
        void SetData()
        {
            ten= textBox1.Text;
            diachi = textBox2.Text;
            sdt = textBox3.Text;
            email = textBox4.Text;
            ghichu = richTextBox1.Text;
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                id = int.Parse(row.Cells[0].Value.ToString());
                ten = row.Cells[1].Value.ToString();
                diachi = row.Cells[2].Value.ToString();
                sdt = row.Cells[3].Value.ToString();
                email = row.Cells[4].Value.ToString();
                ghichu = row.Cells[5].Value.ToString();
                textBox1.Text = ten;
                textBox2.Text = diachi;
                textBox3.Text = sdt;
                textBox4.Text = email;
                richTextBox1.Text = ghichu;
                button1.Text = "Sửa";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (id != 0)
            {
                ClassIndex.nhacungcap.DeleteNCC(id);
                ClearForm();
                loadData();
            }
        }

        private void Nhacungcap_Click(object sender, EventArgs e)
        {
            button1.Text = "Lưu";
        }
    }
}
