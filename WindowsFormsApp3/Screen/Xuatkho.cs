using Newtonsoft.Json.Linq;
using System;
using System.Windows.Forms;
using WindowsFormsApp3.Class;
using WindowsFormsApp3.Report;

namespace WindowsFormsApp3.Screen
{
    public partial class Xuatkho : Form
    {
        public Xuatkho()
        {
            InitializeComponent();
        }
        JArray dataCustomer;
        JArray dataObject;
        JArray dataInputInfo;
        JArray dataIdOutput;
        int id;

        private void button2_Click(object sender, System.EventArgs e)
        {
            string valueIdOutput = comboBox1.Text;
            string valueObject = comboBox2.Text;
            string valueInput = comboBox3.Text;
            string valueCustomer = comboBox4.Text;
            int valueIdOutputid = comboBox1.SelectedIndex;
            int valueObjectid = comboBox2.SelectedIndex;
            int valueInputid = comboBox3.SelectedIndex;
            int valueCustomerid = comboBox4.SelectedIndex;
            if (valueObject.Length > 0 && valueIdOutput.Length > 0 && valueInput.Length > 0 && valueCustomer.Length > 0)
            {

                int IdOutput = int.Parse(dataIdOutput[valueIdOutputid]["IdOutput"].ToString());
                int IdObject = int.Parse(dataObject[valueObjectid]["IdObject"].ToString());
                int IdInputInfo = int.Parse(dataInputInfo[valueInputid]["IdInputInfo"].ToString());
                int IdCustomer = int.Parse(dataCustomer[valueCustomerid]["IdCustomer"].ToString());
                string sqlAdd = "INSERT INTO OutputInfo (IdObject,IdInputInfo,IdOutput,IdCustomer,Count) VALUES (@idObject,@IdInputInfo,@IdOutput,@IdCustomer,@Count);";

                JObject obj = new JObject();
                obj["IdObject"] = IdObject;
                obj["IdInputInfo"] = IdInputInfo;
                obj["IdOutput"] = IdOutput;


                obj["IdCustomer"] = IdCustomer;
                obj["Count"] = textBox1.Text;
                if (ClassIndex.SQL.QUERY(sqlAdd, obj))
                {
                    LoadData();
                }
            }
        }

        private void Xuatkho_Load(object sender, System.EventArgs e)
        {

            dataCustomer = ClassIndex.SQL.GetTable("Customer", new string[] { "IdCustomer", "DisplayName", "Address", "Phone", "Email", "MoreInfo" });
            dataObject = ClassIndex.SQL.GetTable("Object", new string[] { "IdObject", "DisplayName" });
            dataInputInfo = ClassIndex.SQL.GetTable("InputInfo", new string[] { "IdInputInfo", "IdObject", "IdInput", "Count", "InputPrice", "OutputPrice", "Status" });


            /*            comboBox1.SelectedItem = null;
                        comboBox2.SelectedItem = null;
                        comboBox3.SelectedItem = null;
                        comboBox4.SelectedItem = null;
                        comboBox1.Items.Add("--Ngày nhập--");

                        comboBox2.Items.Add("--Vật tư--");

                        comboBox3.Items.Add("--Nhập--");

                        comboBox4.Items.Add("--Khách hàng--");*/


            foreach (var item in dataCustomer)
            {
                comboBox4.Items.Add(item["DisplayName"].ToString());
            }
            foreach (var item in dataObject)
            {
                comboBox2.Items.Add(item["DisplayName"].ToString());
            }

            foreach (var item in dataInputInfo)
            {
                comboBox3.Items.Add(item["IdInputInfo"].ToString());
            }
            LoaddataIdOutput();
            LoadData();

        }
        void LoaddataIdOutput()
        {
            dataIdOutput = ClassIndex.SQL.GetTable("Output", new string[] { "IdOutput", "DateOutput" });
            comboBox1.Items.Clear();
            foreach (var item in dataIdOutput)
            {
                comboBox1.Items.Add(item["DateOutput"].ToString());
            }
        }
        void LoadData()
        {
            JArray arryOutputInfo = ClassIndex.SQL.GetTable("OutputInfo", new string[] { "IdOutputInfo", "IdObject", "IdInputInfo", "IdOutput", "IdCustomer", "Count" });
            JArray newarryOutputInfo = new JArray();
            foreach (var item in arryOutputInfo)
            {
                JObject obj = new JObject();
                obj["id"] = item["IdOutputInfo"];
                foreach (var item3 in dataObject)
                {
                    if (item["IdObject"].ToString() == item3["IdObject"].ToString())
                    {
                        obj["DisplayNameObject"] = item3["DisplayName"].ToString();

                    }
                }
                foreach (var item5 in dataIdOutput)
                {
                    if (item["IdOutput"].ToString() == item5["IdOutput"].ToString())
                    {
                        obj["DateOutput"] = item5["DateOutput"].ToString();

                    }
                }
                foreach (var item2 in dataCustomer)
                {
                    if (item["IdCustomer"].ToString() == item2["IdCustomer"].ToString())
                    {
                        obj["DisplayNameCustomer"] = item2["DisplayName"].ToString();

                    }
                }

                foreach (var item4 in dataInputInfo)
                {
                    if (item["IdInputInfo"].ToString() == item4["IdInputInfo"].ToString())
                    {
                        obj["OutputPrice"] = item4["OutputPrice"].ToString();

                    }
                }

                obj["Count"] = item["Count"];
                newarryOutputInfo.Add(obj);

            }
            dataGridView1.DataSource = ClassIndex.SQL.ArryToList(newarryOutputInfo);
            new sizeDGV(dataGridView1);
        }

        private void dataGridView1_SelectionChanged(object sender, System.EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                id = int.Parse(row.Cells[0].Value.ToString());
                textBox1.Text = row.Cells[1].Value.ToString();

            }
        }

        private void button4_Click(object sender, System.EventArgs e)
        {
            string sql = "INSERT INTO Output (DateOutput) VALUES (@date);";
            JObject obj = new JObject();
            obj["date"] = dateTimePicker1.Value.ToString("MM/dd/yyyy HH:mm:ss");
            if (ClassIndex.SQL.QUERY(sql, obj))
            {
                MessageBox.Show("Thêm thành công");
                LoaddataIdOutput();

            }
            else
            {
                MessageBox.Show("Thêm thất bại");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FXuatkho f = new FXuatkho();
            f.ShowDialog();
        }
    }
}
