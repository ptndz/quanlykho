using doanC.Screen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace doanC.Component
{
    public partial class Loading : Form
    {
        public Loading()
        {
            InitializeComponent();
        }
        
        private void Loading_Load(object sender, EventArgs e)
        {   
            

            Task task = new Task(() =>
             {
                 Thread.Sleep(2000);
                 this.Invoke(new Action(() =>
                 {
                     showHome();
                              
                 }));
             });
            task.Start();
        }
        
        private void showHome()
        {
            this.Hide();
            Home home = new Home();
            home.ShowDialog();
            this.Close();
        }
    }
}
