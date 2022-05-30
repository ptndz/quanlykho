using System;
using System.Windows.Forms;

namespace WindowsFormsApp3.Screen
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Nhapkho nhapkho = new Nhapkho();
                nhapkho.ShowDialog();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Xuatkho xuatkho = new Xuatkho();
                xuatkho.ShowDialog();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                Donvido donvido = new Donvido();
                donvido.ShowDialog();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                Khachhang khachhang = new Khachhang();
                khachhang.ShowDialog();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                Nhacungcap nhacungcap = new Nhacungcap();
                nhacungcap.ShowDialog();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                VatTu vatTu = new VatTu();
                vatTu.ShowDialog();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
