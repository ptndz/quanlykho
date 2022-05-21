using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using doanC.Model;
using Newtonsoft.Json.Linq;

namespace doanC.Sevice
{
    internal class ClassDonvido 
    {
        Mysql mysql = new Mysql();
      
        public bool CreateDonViDo(string tenDonViDo)
        {      
            try
            {
                string sql = "INSERT INTO Unit(DisplayName) VALUES(@tenDonViDo)";
                JObject o = new JObject();
                o["tenDonViDo"] = tenDonViDo;
                mysql.INSERT(sql, o);
             
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
                       
        }
        
        public JArray GetDovido()
        {         
            string sql = "SELECT * FROM `Unit` LIMIT 100;";
            string[] name = { "id", "DisplayName"};
            JArray data = mysql.SELECT(sql, name);
            return data;
        }
    }
}
