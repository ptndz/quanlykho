using Newtonsoft.Json.Linq;
using System;

namespace doanC.Sevice
{
    internal class ClassNhacungcap
    {
        Mysql mysql = new Mysql();

        public bool CreateNCC(string tenNhacungcap, string diachi, string sdt, string email, string thongtin)
        {
            try
            {
                string sql = "INSERT INTO `Suplier`(DisplayName,Address,Phone,Email,MoreInfo) VALUES(@tenNhacungcap,@diachi,@sodienthoai,@email,@thongtinchitiet)";
                JObject o = new JObject();
                o["tenNhacungcap"] = tenNhacungcap;
                o["diachi"] = diachi;
                o["sodienthoai"] = sdt;
                o["email"] = email;
                o["thongtinchitiet"] = thongtin;
                mysql.INSERT(sql, o);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public JArray GetAllNCC()
        {
            try
            {
                string sql = "SELECT * FROM `Suplier`";
                string[] name = { "id", "DisplayName", "Address", "Phone", "Email", "MoreInfo" };

                JArray data = mysql.SELECT(sql, name);
                return data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public JObject GetNCCById(int id)
        {
            try
            {
                string sql = "SELECT * FROM `Suplier` WHERE id = @id";
                string[] name = { "id", "DisplayName", "Address", "Phone", "Email", "MoreInfo" };
                JObject o = new JObject();
                o["id"] = id;
                JArray data = mysql.WHERE(sql, name, o);
                return (JObject)data[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public bool UpdateNCC(int id, string tenNhacungcap, string diachi, string sdt, string email, string thongtin)
        {
            try
            {
                string sql = "UPDATE `Suplier` SET DisplayName=@tenNhacungcap,Address=@diachi,Phone=@sodienthoai,Email=@email,MoreInfo=@thongtinchitiet WHERE ID=@id";
                JObject o = new JObject();
                o["tenNhacungcap"] = tenNhacungcap;
                o["diachi"] = diachi;
                o["sodienthoai"] = sdt;
                o["email"] = email;
                o["thongtinchitiet"] = thongtin;
                o["id"] = id;
                mysql.INSERT(sql, o);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool DeleteNCC(int id)
        {
            try
            {
                string sql = "DELETE FROM `Suplier` WHERE ID=@id";
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
