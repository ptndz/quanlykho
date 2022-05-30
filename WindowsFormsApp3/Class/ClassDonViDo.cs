using Newtonsoft.Json.Linq;
using System;
using System.Windows.Forms;

namespace WindowsFormsApp3.Class
{
    internal class ClassDonViDo : ClassSQL
    {
        public bool AddName(string name)
        {
            try
            {
                string sql = "INSERT INTO Unit (DisplayNameUnit) VALUES (@DisplayNameUnit);";
                JObject obj = new JObject();
                obj["DisplayNameUnit"] = name;
                int id = this.ADD(sql, obj);
                if (id > 0)
                {
                    MessageBox.Show("" + id);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public bool UpdateName(int id, string name)
        {
            try
            {
                string sql = "UPDATE Unit SET DisplayNameUnit=@DisplayNameUnit WHERE IdUnit=@IdUnit";
                JObject obj = new JObject();
                obj["DisplayNameUnit"] = name;
                obj["IdUnit"] = id;
                return this.QUERY(sql, obj);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public bool DeleteName(int id)
        {
            try
            {
                return this.DeleteById("Unit", id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public BindingSource GetAll()
        {
            try
            {
                return ArryToList(this.GetTable("Unit", new string[] { "IdUnit", "DisplayNameUnit" }));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

    }
}
