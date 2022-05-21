using doanC.Component;
using doanC.Sevice;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace doanC.Screen
{
    public partial class Donvido : Form
    {
        public Donvido()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            if (name == "")
            {
                MessageBox.Show("Bạn chưa nhập tên đơn vị đo");
            }
            else
            {
                bool result= ClassIndex.donvido.CreateDonViDo(name);
                if (result)
                {
                    textBox1.Text = "";
                    loadData();
                }
             
            }
        }

        private void Donvido_Load(object sender, EventArgs e)
        {
            loadData();
        }
        void loadData()
        {
            JArray myList = ClassIndex.donvido.GetDovido();
            dataGridView1.DataSource = myList;
            new sizeDGV(dataGridView1);
        }
    }
}
