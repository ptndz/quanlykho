using doanC.Screen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace doanC.Component
{
    public partial class Nav : UserControl
    {
        public Nav()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Nhapkho nhapkho = new Nhapkho();
            nhapkho.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Xuatkho xuatkho = new Xuatkho();
            xuatkho.ShowDialog();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Vattu vattu = new Vattu();
            vattu.ShowDialog();
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Donvido donvido = new Donvido();
            donvido.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Nhacungcap nhacungcap = new Nhacungcap();
            nhacungcap.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Khachhang khachhang = new Khachhang();
            khachhang.ShowDialog();
        }
    }
}
