using Microsoft.Data.SqlClient;
using System.Data;

namespace QLPhongKH.Models
{
    static class DataAccess
    {
        public static string Path = @"Data Source=DESKTOP-E7BVGP7;Initial Catalog=kh;Integrated Security=True";
        //pt tạo kết nối
        public static SqlConnection TaoKetNoi()
        {
            return new SqlConnection(Path);
        }

        //Phương thức lấy ra một table/view
        public static DataTable getTable(string sql)
        {
            SqlConnection con = TaoKetNoi();
            con.Open();
            SqlDataAdapter ad = new SqlDataAdapter(sql, con);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();
            ad.Dispose();
            return dt;
        }

        //phương thức thêm 
        public static void inSertEditDelete(string sql)
        {
            SqlConnection con = TaoKetNoi();
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
            cmd.Dispose();
        }
    }
}
