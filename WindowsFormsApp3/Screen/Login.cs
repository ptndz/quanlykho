using Newtonsoft.Json.Linq;
using System;
using System.Windows.Forms;
using WindowsFormsApp3.Class;

namespace WindowsFormsApp3.Screen
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            try
            {
                string username = textBox1.Text;
                string password = textBox2.Text;
                if (username.Length > 0 && password.Length > 0)
                {
                    string sql = "SELECT * FROM Users WHERE UserName = @username AND Password = @password";
                    JObject obj = new JObject();
                    obj["username"] = username;
                    obj["password"] = password;
                    JArray data = ClassIndex.SQL.WHERE(sql, new string[] { "IdUser", "UserName", "Password" }, obj);
                    int id = int.Parse(data[0]["IdUser"].ToString());
                    if (id > 0)
                    {
                        Main main = new Main();
                        this.Hide();
                        main.ShowDialog();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Tài khoản hoặc mật khẩu không đúng");
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Tài khoản hoặc mật khẩu không đúng");
            }
           
        }

        private void Login_Load(object sender, EventArgs e)
        {
            textBox1.Text = "admin";
            textBox2.Text = "admin";
        }
    }
}
