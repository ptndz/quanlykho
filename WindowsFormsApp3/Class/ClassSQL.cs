using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

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

            string connectionString2 = Laychuoiketnoi();
            conn = new SqlConnection(connectionString2);
        }
        public string Laychuoiketnoi()
        {
            string tentaptin = "Chuoi.txt";
            string s;
            StreamReader sr = new StreamReader(tentaptin);
            s = sr.ReadToEnd();
            sr.Close();
            return s;
        }
        public SqlConnection connection()
        {
            this.OpenConn();
            return conn;
        }
        public void OpenConn()
        {
            if (conn.State == System.Data.ConnectionState.Closed)
            {
                conn.Open();
            }

        }
        public void CloseConn()
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

                int newID;
                SqlCommand cmd = new SqlCommand(sql + "SELECT CAST(scope_identity() AS int)", conn);

                foreach (var item in data)
                {
                    cmd.Parameters.AddWithValue("@" + item.Key.ToString(), item.Value.ToString());
                }

                this.OpenConn();

                newID = (int)cmd.ExecuteScalar();

                this.CloseConn();
                return newID;

            }
            catch (Exception ex)
            {
                this.CloseConn();
                return -1;
            }
        }
        public bool QUERY(string sql, JObject data)
        {
            try
            {

                int newID;
                SqlCommand cmd = new SqlCommand(sql, conn);

                foreach (var item in data)
                {
                    cmd.Parameters.AddWithValue("@" + item.Key.ToString(), item.Value.ToString());
                }

                this.OpenConn();

                cmd.ExecuteNonQuery();

                this.CloseConn();
                return true;

            }
            catch (Exception ex)
            {
                this.CloseConn();
                return false;
            }
        }
        public JArray GET(string sql, string[] name)
        {
            try
            {
                this.OpenConn();
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
                this.CloseConn();
                return data;
            }
            catch (Exception ex)
            {
                this.CloseConn();
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
                    cmd.Parameters.AddWithValue("@" + item.Key.ToString(), item.Value.ToString());
                }
                this.OpenConn();
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
                this.CloseConn();
                return array;

            }
            catch (Exception ex)
            {
                this.CloseConn();
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
        public bool UpdateById(string table, int id, JObject value)
        {
            try
            {
                string sql = "UPDATE " + table + " SET ";
                foreach (var item in value)
                {
                    sql += item.Key + " = @" + item.Key + ", ";
                }
                sql = sql.Substring(0, sql.Length - 2);
                sql += " WHERE id" + table + " = @id;";
                value["id"] = id;
                return this.QUERY(sql, value);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool DeleteById(string table, int id)
        {
            try
            {
                string sql = "DELETE FROM " + table + " WHERE Id" + table + "=@id";
                JObject value = new JObject();
                value["id"] = id;

                return this.QUERY(sql, value);
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
                string sql = "SELECT * FROM " + table + " WHERE Id" + table + " = @id";
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
        public BindingSource ArryToList(JArray data)
        {
            List<JObject> list = new List<JObject>();
            foreach (var item in data)
            {
                list.Add((JObject)item);
            }
            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = list;
            return bindingSource;
        }
        public DataTable Report(string table)
        {
            try
            {
                string sql = "SELECT * FROM " + table;

                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                this.OpenConn();
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                return dataTable;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
