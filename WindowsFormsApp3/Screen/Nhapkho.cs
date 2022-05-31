using Newtonsoft.Json.Linq;
using System;
using System.Windows.Forms;
using WindowsFormsApp3.Class;
namespace WindowsFormsApp3.Screen
{
    public partial class Nhapkho : Form
    {
        public Nhapkho()
        {
            InitializeComponent();
        }
        protected int id;
        protected JArray dataObject;
        int idObject;
        int idInput;
        JArray indexIdDate;
        private void button2_Click(object sender, System.EventArgs e)
        {
            int dateValue = comboBox1.SelectedIndex;
            int indexOb = comboBox2.SelectedIndex;
            if (dateValue == -1 && indexOb == -1)
            {
                MessageBox.Show("Chưa chọn ngày nhập");
                return;
            }
        
            string valueObject = comboBox2.Text;
            string coutValue = textBox1.Text;
            string priceValue = textBox2.Text;
            string outPriceValue = textBox3.Text;
            foreach (var item in dataObject)
            {
                if (item["DisplayName"].ToString() == valueObject)
                {
                    idObject = int.Parse(item["IdObject"].ToString());
                }
            }
            idInput = int.Parse(indexIdDate[dateValue]["IdInput"].ToString());
            if ( coutValue.Length > 0 && priceValue.Length > 0 && outPriceValue.Length > 0)
            {
                string sqlAdd = "INSERT INTO InputInfo (IdObject,IdInput,Count,InputPrice,OutputPrice) VALUES (@idObject,@idInput,@count,@price,@outputPrice);";
                JObject obj = new JObject();
                MessageBox.Show("" + idInput+"dddd"+ idObject);
                obj["IdObject"] = idObject;
                obj["idInput"] = idInput;
                obj["count"] = coutValue;
                obj["price"] = priceValue;
                obj["outputPrice"] = outPriceValue;

                if (ClassIndex.SQL.QUERY(sqlAdd, obj)){
                    MessageBox.Show("Thêm thành công");
                    LodaData();
                }
                else
                {
                    MessageBox.Show("Thêm thất bại");
                }
            }
            else
            {
                MessageBox.Show("Chưa nhập đủ thông tin");
            }



        }

        private void Nhapkho_Load(object sender, System.EventArgs e)
        {
            comboBox1.SelectedItem = null;
            comboBox1.SelectedText = "--Ngày nhập--";
            comboBox2.SelectedItem = null;
            comboBox2.SelectedText = "--Vật tư--";
            dataObject = ClassIndex.SQL.GetTable("Object", new string[] { "IdObject", "DisplayName" });
            foreach (var item in dataObject)
            {
                comboBox2.Items.Add(item["DisplayName"]);
            }
            LodaData();
        }
        void LodaData()
        {
            dateTimePicker1.Value = DateTime.Now;
            LoadDateInput();
            JArray dataInputInfo = ClassIndex.SQL.GetTable("InputInfo", new string[] { "IdInputInfo", "IdObject", "IdInput", "Count", "InputPrice", "OutputPrice", "Status" });
            JArray newDataObject = new JArray();


            foreach (var item in dataInputInfo)
            {
                JObject obj = new JObject();
                obj["IdInputInfo"] = item["IdInputInfo"];
                foreach (var itemObject in dataObject)
                {
                    if (itemObject["IdObject"].ToString() == item["IdObject"].ToString())
                    {
                        obj["IdObject"] = itemObject["DisplayName"];
                    }
                }

                obj["IdInput"] = item["IdInput"];
                obj["Count"] = item["Count"];
                obj["InputPrice"] = item["InputPrice"];
                obj["OutputPrice"] = item["OutputPrice"];
                obj["Status"] = item["Status"];
                newDataObject.Add(obj);
            }
            dataGridView1.DataSource = ClassIndex.SQL.ArryToList(newDataObject);
            new sizeDGV(dataGridView1);

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

            string sql = "INSERT INTO Input (DateInput) VALUES (@date);";
            JObject obj = new JObject();
            obj["date"] = dateTimePicker1.Value.ToString("MM/dd/yyyy HH:mm:ss");
            if (ClassIndex.SQL.QUERY(sql, obj))
            {
                MessageBox.Show("Thêm thành công");
                LoadDateInput();
            }
            else
            {
                MessageBox.Show("Thêm thất bại");
            }
        }
        void LoadDateInput()
        {
            comboBox1.Items.Clear();

            comboBox1.SelectedItem = null;
            comboBox1.Items.Add("--Ngày nhập--");
            indexIdDate = new JArray();
            int idIndex = 0;
            JArray dataInput = ClassIndex.SQL.GetTable("Input", new string[] { "IdInput", "DateInput" });
            foreach (var item in dataInput)
            {
                comboBox1.Items.Add(item["DateInput"]);
                JObject obj = new JObject();
                obj["IdInput"] = item["IdInput"];
                obj["DateInput"] = item["DateInput"];
                obj["idIndex"] = idIndex;
                indexIdDate.Add(obj);
                idIndex++;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
