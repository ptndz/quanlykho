using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using System;

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
        private void button2_Click(object sender, System.EventArgs e)
        {

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
    }
}
