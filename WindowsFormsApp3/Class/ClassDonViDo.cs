using Newtonsoft.Json.Linq;
using System;
namespace WindowsFormsApp3.Class
{
    internal class ClassDonViDo : ClassSQL
    {
        
        public bool AddName(string name)
        {
            try
            {
                string sql = "INSERT INTO Unit(DisplayNameUnit) VALUES('@DisplayNameUnit')";
                JObject obj = new JObject();
                obj["DisplayNameUnit"] = name;
                int id = this.ADD(sql, obj);
                if (id > 0)
                {
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
                string sql = "UPDATE Unit SET DisplayNameUnit = '@DisplayNameUnit' WHERE IdUnit = '@IdUnit'";
                JObject obj = new JObject();
                obj["DisplayNameUnit"] = name;
                obj["IdUnit"] = id;
                int id1 = this.ADD(sql, obj);
                if (id1 > 0)
                {
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
        public JArray GetAll()
        {
            try
            {
                return this.GetTable("Unit", new string[] { "IdUnit", "DisplayNameUnit"});
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
