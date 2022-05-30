using Newtonsoft.Json.Linq;
using System.Windows.Forms;
using WindowsFormsApp3.Class;
using WindowsFormsApp3.Report;

namespace WindowsFormsApp3.Screen
{
    public partial class Nhacungcap : Form
    {
        public Nhacungcap()
        {
            InitializeComponent();
        }
        int id;
        private void button2_Click(object sender, System.EventArgs e)
        {
            string nameButton = (sender as Button).Text;

            string nameValue = textBox1.Text;
            string addressValue = textBox2.Text;
            string phoneValue = textBox3.Text;
            string emailValue = textBox4.Text;
            string noteValue = richTextBox1.Text;
            if (nameValue.Length > 0 && addressValue.Length > 0 && phoneValue.Length > 0 && emailValue.Length > 0)
            {
                if (nameButton == "Thêm")
                {

                    string sql = "INSERT INTO Suplier (DisplayNameSuplier,Address,Phone,Email,MoreInfo) VALUES(@name,@address,@phone,@email,@note)";
                    JObject obj = new JObject();
                    obj["name"] = nameValue;
                    obj["address"] = addressValue;
                    obj["phone"] = phoneValue;
                    obj["email"] = emailValue;
                    obj["note"] = noteValue;
                    if (ClassIndex.SQL.QUERY(sql, obj))
                    {
                        ClearForm();
                        loadData();
                    }

                }
                else if (nameButton == "Cập nhật")
                {
                    string sql = "UPDATE Suplier SET DisplayNameSuplier=@name,Address=@address,Phone=@phone,Email=@email,MoreInfo=@note WHERE IdSuplier=@id";
                    JObject obj = new JObject();
                    obj["name"] = nameValue;
                    obj["address"] = addressValue;
                    obj["phone"] = phoneValue;
                    obj["email"] = emailValue;
                    obj["note"] = noteValue;
                    obj["id"] = id;
                    if (ClassIndex.SQL.QUERY(sql, obj))
                    {
                        button2.Text = "Thêm";
                        ClearForm();
                        loadData();
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
            }


        }
        void loadData()
        {
            JArray data = ClassIndex.SQL.GetTable("Suplier", new string[] { "IdSuplier", "DisplayNameSuplier", "Address", "Phone", "Email", "MoreInfo" });

            dataGridView1.DataSource = ClassIndex.SQL.ArryToList(data);
            new sizeDGV(dataGridView1);
        }

        private void Nhacungcap_Load(object sender, System.EventArgs e)
        {
            loadData();
        }
        void ClearForm()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            richTextBox1.Text = "";
        }

        private void Nhacungcap_Click(object sender, System.EventArgs e)
        {
            button2.Text = "Thêm";
        }

        private void dataGridView1_SelectionChanged(object sender, System.EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                id = int.Parse(row.Cells[0].Value.ToString());
                textBox1.Text = row.Cells[1].Value.ToString();
                textBox2.Text = row.Cells[2].Value.ToString();
                textBox3.Text = row.Cells[3].Value.ToString();
                textBox4.Text = row.Cells[4].Value.ToString();
                richTextBox1.Text = row.Cells[5].Value.ToString();
                button2.Text = "Cập nhật";
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex > 0)
            {
                id = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                richTextBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            }
        }

        private void button3_Click(object sender, System.EventArgs e)
        {
            if (ClassIndex.SQL.DeleteById("Suplier", id))
            {
                ClearForm();
                loadData();
                id = 0;
            }


        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            RNCC rncc = new RNCC();
            rncc.ShowDialog();
        }
    }
}
