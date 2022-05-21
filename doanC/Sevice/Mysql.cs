using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;
using System;

namespace doanC
{

    internal class Mysql
    {
        static string pass = "QxKY!otGfAuR";
        static string user = "phamthan_doan";
        static string server = "hs216113.tino.org";
        static string database = "phamthan_doan";

        private string connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + user + ";" + "PASSWORD=" + pass + ";";
        protected MySqlConnection conn;

        public Mysql()
        {
            conn = new MySqlConnection(connectionString);
        }
        public MySqlCommand INSERT(string sql, JObject data)
        {

            this.Open();
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            foreach (var item in data)
            {
                cmd.Parameters.AddWithValue("@" + item.Key, item.Value);
            }
            cmd.ExecuteNonQuery();
            this.Close();
            return cmd;

        }
        public JArray SELECT(string sql, string[] name)
        {
            JArray array = new JArray();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            this.Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                JObject obj = new JObject();
                foreach (string i in name)
                {
                    obj[i] = reader[i].ToString();
                }
                array.Add(obj);
            }
            this.Close();
            return array;
        }
        public JObject WHERE(string sql, string[] name, JObject value)
        {
            JObject obj = new JObject();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            foreach (var item in value)
            {
                cmd.Parameters.AddWithValue("@" + item.Key, item.Value);
            }
            this.Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                foreach (string i in name)
                {
                    obj[i] = reader[i].ToString();
                }
            }
            this.Close();
            return obj;
        }
        public void Open()
        {
            try
            {
                conn.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void Close()
        {
            try
            {
                conn.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

    }
}
