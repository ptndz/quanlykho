using Newtonsoft.Json.Linq;
using System;
using System.Data.SqlClient;
namespace WindowsFormsApp3.Class
{
    internal class ClassSQL
    {
        static string pass = "Ptn141122@";
        static string user = "sa";
        static string server = ".";
        static string database = "QuanLyKho";
        string connectionString = "Data Source=" + server + ";Initial Catalog=" + database + ";User ID=" + user + ";Password=" + pass;
        protected SqlConnection conn;
        public ClassSQL()
        {
            conn = new SqlConnection(connectionString);
        }
        public void Open()
        {
            if (conn.State == System.Data.ConnectionState.Closed)
            {
                conn.Open();
            }
        }
        public void Close()
        {
            if (conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
            }
        }
        public int ADD(string sql, JObject data)
        {
            try
            {
                this.Open();
                int newID;
                SqlCommand cmd = new SqlCommand(sql + ";SELECT CAST(scope_identity() AS int)", conn);
                foreach (var item in data)
                {
                    cmd.Parameters.AddWithValue("@" + item.Key, item.Value);
                }
                newID = (int)cmd.ExecuteScalar();
                this.Close();
                return newID;

            }
            catch (Exception ex)
            {
                this.Close();
                return -1;
            }
        }
        public JArray GET(string sql, string[] name)
        {
            try
            {
                this.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                JArray data = new JArray();
                while (reader.Read())
                {
                    JObject obj = new JObject();
                    foreach (string i in name)
                    {
                        obj[i] = reader[i].ToString();
                    }
                    data.Add(obj);
                }
                reader.Close();
                this.Close();
                return data;
            }
            catch (Exception ex)
            {
                this.Close();
                return null;
            }
        }
        public JArray WHERE(string sql, string[] name, JObject value)
        {
            try
            {
                JArray array = new JArray();
                SqlCommand cmd = new SqlCommand(sql, conn);
                foreach (var item in value)
                {
                    cmd.Parameters.AddWithValue("@" + item.Key, item.Value);
                }
                this.Open();
                SqlDataReader reader = cmd.ExecuteReader();
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
            catch (Exception ex)
            {
                this.Close();
                return null;
            }
        }
        public JArray GetTable(string table, string[] column)
        {
            try
            {
                string sql = "SELECT * FROM " + table;

                return this.GET(sql, column);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public JObject GetById(string table, int id, string[] column)
        {
            try
            {
                string sql = "SELECT * FROM " + table + " WHERE id" + table + " = @id";
                JObject value = new JObject();
                value["id"] = id;
                JArray data = this.WHERE(sql, column, value);
                return data[0] as JObject;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public JObject UpdateById(string table, int id, JObject value)
        {
            try
            {
                string sql = "UPDATE " + table + " SET ";
                foreach (var item in value)
                {
                    sql += item.Key + " = @" + item.Key + ", ";
                }
                sql = sql.Substring(0, sql.Length - 2);
                sql += " WHERE id" + table + " = @id";
                value["id"] = id;
                return this.ADD(sql, value) > 0 ? value : null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public bool DeleteById(string table, int id)
        {
            try
            {
                string sql = "DELETE FROM " + table + " WHERE id" + table + " = @id";
                JObject value = new JObject();
                value["id"] = id;
                return this.ADD(sql, value) > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool CheckId(string table, int id)
        {
            try
            {
                string sql = "SELECT * FROM " + table + " WHERE id" + table + " = @id";
                JObject value = new JObject();
                string idTable = "id" + table;
                value[idTable] = id;
                JArray data = this.WHERE(sql, new string[] { idTable }, value);
                return data.Count > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
