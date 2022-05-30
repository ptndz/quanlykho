using Newtonsoft.Json.Linq;
using System;
using System.Windows.Forms;
using WindowsFormsApp3.Class;
using WindowsFormsApp3.Report;

namespace WindowsFormsApp3.Screen
{
    public partial class VatTu : Form
    {
        public VatTu()
        {
            InitializeComponent();
        }
        protected int id;
        protected int idUnit;
        protected int idSuplier;
        protected JArray dataUnit;
        protected JArray dataSuplier;
        private void button2_Click(object sender, EventArgs e)
        {
            string nameButton = (sender as Button).Text;
            string nameValue = textBox1.Text;
            string valueUnit = comboBox1.Text;
            string valueSuplier = comboBox2.Text;
            string qrcodeValue = textBox2.Text;
            string barcodeValue = textBox4.Text;
            if (nameValue.Length > 0 && qrcodeValue.Length > 0 && barcodeValue.Length > 0)
            {
                string sqlWhereUint = "SELECT * FROM Unit WHERE DisplayNameUnit=@DisplayNameUnit;";
                string sqlWhereIdSuplier = "SELECT * FROM Suplier WHERE DisplayNameSuplier=@DisplayNameSuplier;";

                JObject objUint = new JObject();
                JObject objSuplier = new JObject();
                objUint["DisplayNameUnit"] = valueUnit;
                objSuplier["DisplayNameSuplier"] = valueSuplier;
                JArray dataUint = ClassIndex.SQL.WHERE(sqlWhereUint, new string[] { "IdUnit" }, objUint);
                JArray dataSuplier = ClassIndex.SQL.WHERE(sqlWhereIdSuplier, new string[] { "IdSuplier" }, objSuplier);
                idUnit = int.Parse(dataUint[0]["IdUnit"].ToString());

                idSuplier = int.Parse(dataSuplier[0]["IdSuplier"].ToString());


                if (nameButton == "Thêm")
                {
                    string sqlAdd = "INSERT INTO Object(DisplayName,IdUnit,IdSuplier,QRCode,BarCode) VALUES(@name,@unit,@suplier,@qrcode,@barcode)";
                    JObject objAdd = new JObject();
                    objAdd["name"] = nameValue;
                    objAdd["unit"] = idUnit;
                    objAdd["suplier"] = idSuplier;
                    objAdd["qrcode"] = qrcodeValue;
                    objAdd["barcode"] = barcodeValue;
                    if (ClassIndex.SQL.QUERY(sqlAdd, objAdd))
                    {
                        LoadData();
                        ClearForm();
                    }
                }
                else if (nameButton == "Cập nhật")
                {

                    string sqlUpdate = "UPDATE Object SET DisplayName=@name,IdUnit=@unit,IdSuplier=@suplier,QRCode=@qrcode,BarCode=@barcode WHERE IdObject=@id;";
                    JObject objUpdate = new JObject();
                    objUpdate["name"] = nameValue;
                    objUpdate["unit"] = idUnit;
                    objUpdate["suplier"] = idSuplier;
                    objUpdate["qrcode"] = qrcodeValue;
                    objUpdate["barcode"] = barcodeValue;
                    objUpdate["id"] = id;
                    if (ClassIndex.SQL.QUERY(sqlUpdate, objUpdate))
                    {
                        button2.Text = "Thêm";
                        id = 0;
                        LoadData();
                        ClearForm();
                    }
                    else
                    {
                        MessageBox.Show("Cập nhập thất bại");
                    }
                }

            }
            else
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
            }
        }

        private void VatTu_Load(object sender, EventArgs e)
        {

            comboBox1.SelectedItem = null;
            comboBox1.SelectedText = "--Đơn vi đo--";
            comboBox2.SelectedItem = null;
            comboBox2.SelectedText = "--Nhà cung cấp--";
            dataUnit = ClassIndex.SQL.GetTable("Unit", new string[] { "IdUnit", "DisplayNameUnit" });
            dataSuplier = ClassIndex.SQL.GetTable("Suplier", new string[] { "IdSuplier", "DisplayNameSuplier" });
            
            foreach (var item in dataUnit)
            {
                comboBox1.Items.Add(item["DisplayNameUnit"]);
            }
            foreach (var item in dataSuplier)
            {
                comboBox2.Items.Add(item["DisplayNameSuplier"]);

            }
            LoadData();
        }
        void LoadData()
        {



            JArray dataObject = ClassIndex.SQL.GetTable("Object", new string[] { "IdObject", "DisplayName", "IdUnit", "IdSuplier", "QRCode", "BarCode" });
            JArray newDataObject = new JArray();


            foreach (var item in dataObject)
            {
                JObject obj = new JObject();
                obj["IdObject"] = item["IdObject"];
                obj["DisplayName"] = item["DisplayName"];


                foreach (var itemUnit in dataUnit)
                {

                    if (item["IdUnit"].ToString() == itemUnit["IdUnit"].ToString())
                    {

                        obj["IdUnit"] = itemUnit["DisplayNameUnit"];
                    }
                }
                foreach (var itemSuplier in dataSuplier)
                {
                    if (item["IdSuplier"].ToString() == itemSuplier["IdSuplier"].ToString())
                    {
                        obj["IdSuplier"] = itemSuplier["DisplayNameSuplier"];
                    }
                }
                obj["QRCode"] = item["QRCode"];
                obj["BarCode"] = item["BarCode"];
                newDataObject.Add(obj);


            }

            dataGridView1.DataSource = ClassIndex.SQL.ArryToList(newDataObject);
            new sizeDGV(dataGridView1);

        }
        void ClearForm()
        {
            textBox1.Text = "";
            textBox2.Text = "";

            textBox4.Text = "";
            comboBox1.SelectedItem = null;
            comboBox1.SelectedText = "--Đơn vi đo--";
            comboBox2.SelectedItem = null;
            comboBox2.SelectedText = "--Nhà cung cấp--";
        }
        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                id = int.Parse(row.Cells[0].Value.ToString());
                textBox1.Text = row.Cells[1].Value.ToString();
                comboBox1.Text = row.Cells[2].Value.ToString();
                comboBox2.Text = row.Cells[3].Value.ToString();
                textBox2.Text = row.Cells[4].Value.ToString();
                textBox4.Text = row.Cells[5].Value.ToString();
                button2.Text = "Cập nhật";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (id > 0)
            {
                bool delete = ClassIndex.SQL.DeleteById("Object", id);

                if (delete)
                {
                    LoadData();
                    ClearForm();
                }
            }
        }

        private void VatTu_Click(object sender, EventArgs e)
        {
            button2.Text = "Thêm";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FVattu f = new FVattu();
            f.ShowDialog();
        }
    }
}
