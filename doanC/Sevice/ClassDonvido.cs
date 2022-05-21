using Newtonsoft.Json.Linq;
using System;

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
            try
            {
                string sql = "SELECT * FROM `Unit` LIMIT 100;";
                string[] name = { "id", "DisplayName" };
                JArray data = mysql.SELECT(sql, name);
                return data;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public bool UpdateDonViDo(int id, string tenDonViDo)
        {
            try
            {
                string sql = "UPDATE `Unit` SET `DisplayName` = @tenDonViDo WHERE `id` = @id";
                JObject o = new JObject();
                o["id"] = id;
                o["tenDonViDo"] = tenDonViDo;
                mysql.INSERT(sql, o);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public JObject GetDonViDoById(int id)
        {
            try
            {
                string sql = "SELECT * FROM `Unit` WHERE `id` = @id";
                string[] name = { "DisplayName" };
                JObject o = new JObject();
                o["id"] = id;
                JObject data = mysql.WHERE(sql, name, o);
                return data;
            }
            catch (Exception)
            {
                return null;
            }

        }
        public bool DeleteDonViDo(int id)
        {
            try
            {
                string sql = "DELETE FROM `Unit` WHERE `id` = @id";
                JObject o = new JObject();
                o["id"] = id;
                mysql.INSERT(sql, o);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
