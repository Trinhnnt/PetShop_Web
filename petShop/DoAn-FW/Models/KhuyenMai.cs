using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAn_FW.Models
{
    public class KhuyenMai
    {
        public virtual int MaKM { get; set; }
        public virtual byte SoPTKM { get; set; }
        public virtual DateTime TuNgay { get; set; }
        public virtual DateTime DenNgay { get; set; }
        public virtual int? TTienToiThieu { get; set; }

        public string ConnectionString { get; set; }
        public KhuyenMai()
        {
            ConnectionString = "Data Source=localhost;Initial Catalog=website_petShop;Integrated Security=True";
        }
        public KhuyenMai(string cs)
        {
            ConnectionString = cs;
        }
        private Microsoft.Data.SqlClient.SqlConnection GetConnection()
        {
            return new Microsoft.Data.SqlClient.SqlConnection(ConnectionString);
        }

        public List<KhuyenMai> ListKM()
        {
            List<KhuyenMai> list = new List<KhuyenMai>();
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var now = DateTime.Now.ToString("yyyy-MM-dd");
                var sql = "SELECT * FROM khuyenmai WHERE TuNgay <= @datenow AND DenNgay >= @datenow;";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("datenow", now);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new KhuyenMai()
                        {
                            MaKM = int.Parse(reader["MaKM"].ToString()),
                            SoPTKM = byte.Parse(reader["SoPTKM"].ToString()),
                            TuNgay = DateTime.Parse(reader["TuNgay"].ToString()),
                            DenNgay = DateTime.Parse(reader["DenNgay"].ToString()),
                            TTienToiThieu = reader["TTienToiThieu"] != DBNull.Value ? Convert.ToInt32(reader["TTienToiThieu"]) : (int?)null,
                        });
                    }
                }
            }
            return list;
        }

        public KhuyenMai GetKM(int makm)
        {
            KhuyenMai km = new KhuyenMai();
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var sql = "SELECT * FROM khuyenmai WHERE MaKM = @makm";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("makm", makm);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        km.MaKM = int.Parse(reader["MaKM"].ToString());
                        km.SoPTKM = byte.Parse(reader["SoPTKM"].ToString());
                        km.TuNgay = DateTime.Parse(reader["TuNgay"].ToString());
                        km.DenNgay = DateTime.Parse(reader["DenNgay"].ToString());
                        km.TTienToiThieu = reader["TTienToiThieu"] != DBNull.Value ? Convert.ToInt32(reader["TTienToiThieu"]) : (int?)null;
                    }
                }
            }
            return km;
        }

        public List<KhuyenMai> GetAllKM()
        {
            List<KhuyenMai> list = new List<KhuyenMai>();
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var sql = "SELECT * FROM khuyenmai ORDER BY MaKM DESC";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new KhuyenMai()
                        {
                            MaKM = int.Parse(reader["MaKM"].ToString()),
                            SoPTKM = byte.Parse(reader["SoPTKM"].ToString()),
                            TuNgay = DateTime.Parse(reader["TuNgay"].ToString()),
                            DenNgay = DateTime.Parse(reader["DenNgay"].ToString()),
                            TTienToiThieu = reader["TTienToiThieu"] != DBNull.Value ? Convert.ToInt32(reader["TTienToiThieu"]) : (int?)null,
                        });
                    }
                }
            }
            return list;
        }

        public int ThemKM(KhuyenMai km)
        {
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                string sql;
                Microsoft.Data.SqlClient.SqlCommand cmd;

                if (km.TTienToiThieu.HasValue)
                {
                    sql = "INSERT INTO khuyenmai(SoPTKM, TuNgay, DenNgay, TTienToiThieu) VALUES(@soPTKM, @tuNgay, @denNgay, @tTienToiThieu); SELECT SCOPE_IDENTITY()";
                    cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("tTienToiThieu", km.TTienToiThieu);
                }
                else
                {
                    sql = "INSERT INTO khuyenmai(SoPTKM, TuNgay, DenNgay, TTienToiThieu) VALUES(@soPTKM, @tuNgay, @denNgay, NULL); SELECT SCOPE_IDENTITY()";
                    cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, conn);
                }

                cmd.Parameters.AddWithValue("soPTKM", km.SoPTKM);
                cmd.Parameters.AddWithValue("tuNgay", km.TuNgay.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("denNgay", km.DenNgay.ToString("yyyy-MM-dd"));

                return int.Parse(cmd.ExecuteScalar().ToString());
            }
        }

        public int CapNhatKM(KhuyenMai km)
        {
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                string sql;
                Microsoft.Data.SqlClient.SqlCommand cmd;

                if (km.TTienToiThieu.HasValue)
                {
                    sql = "UPDATE khuyenmai SET SoPTKM = @soPTKM, TuNgay = @tuNgay, DenNgay = @denNgay, TTienToiThieu = @tTienToiThieu WHERE MaKM = @maKM";
                    cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("tTienToiThieu", km.TTienToiThieu);
                }
                else
                {
                    sql = "UPDATE khuyenmai SET SoPTKM = @soPTKM, TuNgay = @tuNgay, DenNgay = @denNgay, TTienToiThieu = NULL WHERE MaKM = @maKM";
                    cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, conn);
                }

                cmd.Parameters.AddWithValue("soPTKM", km.SoPTKM);
                cmd.Parameters.AddWithValue("tuNgay", km.TuNgay.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("denNgay", km.DenNgay.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("maKM", km.MaKM);

                return cmd.ExecuteNonQuery();
            }
        }

        public int XoaKM(int maKM)
        {
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var sql = "DELETE FROM khuyenmai WHERE MaKM = @maKM";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("maKM", maKM);
                return cmd.ExecuteNonQuery();
            }
        }

        public List<KhuyenMai> GetKMHopLe(int tongTien)
        {
            List<KhuyenMai> list = new List<KhuyenMai>();
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var now = DateTime.Now.ToString("yyyy-MM-dd");
                var sql = "SELECT * FROM khuyenmai WHERE TuNgay <= @datenow AND DenNgay >= @datenow " +
                          "AND (TTienToiThieu IS NULL OR TTienToiThieu <= @tongTien) ORDER BY SoPTKM DESC";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("datenow", now);
                cmd.Parameters.AddWithValue("tongTien", tongTien);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new KhuyenMai()
                        {
                            MaKM = int.Parse(reader["MaKM"].ToString()),
                            SoPTKM = byte.Parse(reader["SoPTKM"].ToString()),
                            TuNgay = DateTime.Parse(reader["TuNgay"].ToString()),
                            DenNgay = DateTime.Parse(reader["DenNgay"].ToString()),
                            TTienToiThieu = reader["TTienToiThieu"] != DBNull.Value ? Convert.ToInt32(reader["TTienToiThieu"]) : (int?)null,
                        });
                    }
                }
            }
            return list;
        }
    }
}
