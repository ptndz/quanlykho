using Newtonsoft.Json.Linq;
using System.Windows.Forms;
using WindowsFormsApp3.Class;
using WindowsFormsApp3.Report;

namespace WindowsFormsApp3.Screen
{
    public partial class Khachhang : Form
    {
        public Khachhang()
        {
            InitializeComponent();
        }
        int id;
        private void Khachhang_Load(object sender, System.EventArgs e)
        {
            loadData();
        }
        void loadData()
        {
            dataGridView1.DataSource = ClassIndex.SQL.ArryToList(ClassIndex.SQL.GetTable("Customer", new string[] { "IdCustomer", "DisplayName", "Address", "Phone", "Email", "MoreInfo" }));
            new sizeDGV(dataGridView1);
        }
        void ClearForm()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            richTextBox1.Text = "";

        }
        private void button2_Click(object sender, System.EventArgs e)
        {
            string nameValue = textBox1.Text;
            string addressValue = textBox2.Text;
            string phoneValue = textBox3.Text;
            string emailValue = textBox4.Text;
            string moreInfoValue = richTextBox1.Text;
            string nameButton = (sender as Button).Text;

            if (nameValue.Length > 0 && addressValue.Length > 0 && phoneValue.Length > 0 && emailValue.Length > 0 && moreInfoValue.Length > 0)
            {
                if (nameButton == "Thêm")
                {
                    string sql = "INSERT INTO Customer (DisplayName,Address,Phone,Email,MoreInfo) VALUES(@name,@address,@phone,@email,@note)";
                    JObject obj = new JObject();
                    obj["name"] = nameValue;
                    obj["address"] = addressValue;
                    obj["phone"] = phoneValue;
                    obj["email"] = emailValue;
                    obj["note"] = moreInfoValue;
                    if (ClassIndex.SQL.QUERY(sql, obj))
                    {
                        loadData();
                        ClearForm();
                    }
                    else
                    {
                        MessageBox.Show("Thêm thất bại");
                    }
                }
                else if (nameButton == "Cập nhật")
                {
                    string sql = "UPDATE Customer SET DisplayName = @name,Address = @address,Phone = @phone,Email = @email,MoreInfo = @note WHERE IdCustomer = @id";
                    JObject obj = new JObject();
                    obj["name"] = nameValue;
                    obj["address"] = addressValue;
                    obj["phone"] = phoneValue;
                    obj["email"] = emailValue;
                    obj["note"] = moreInfoValue;
                    obj["id"] = id;
                    if (ClassIndex.SQL.QUERY(sql, obj))
                    {
                        loadData();
                        ClearForm();
                        button2.Text = "Thêm";
                    }
                    else
                    {
                        MessageBox.Show("Sửa thất bại");
                    }
                }

            }
            else
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
            }
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

        private void button3_Click(object sender, System.EventArgs e)
        {
            if (ClassIndex.SQL.DeleteById("Customer", id))
            {
                ClearForm();
                loadData();
                id = 0;
            }

        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            FKHang f = new FKHang();
            f.ShowDialog();
        }
    }
}
