using doanC.Component;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using doanC.Screen;

namespace doanC
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
         

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            showLoading();
            
/*            string userName = textBox1.Text;
            string passWord = textBox2.Text;
            if (userName == "admin" && passWord == "admin")
            {
                this.Hide();
                showLoading();
                     
            }
            else
            {
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu");
            }*/
        }
        private void showLoading()
        {
            Loading loading = new Loading();
            loading.ShowDialog();
            this.Close();
        }

       
    }
}
