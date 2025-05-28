using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAn_FW.Models
{
    public class HoaDon
    {
        public virtual int MaHD { get; set; }
        public virtual int MaKH { get; set; }
        public virtual int? MaKM { get; set; }
        public virtual string DiaChiGH { get; set; }
        public virtual int TongTienTT { get; set; }
        public virtual DateTime NgayLapHD { get; set; }
        public virtual int TinhTrangHD { get; set; }
        public virtual int? TinhTrangTT { get; set; }
        public virtual int? SoTienNhan { get; set; }
        public virtual int? SoTienTra { get; set; }

        public string ConnectionString { get; set; }
        public HoaDon()
        {
            ConnectionString = "Data Source=localhost;Initial Catalog=website_petShop;Integrated Security=True";
        }
        public HoaDon(string cs)
        {
            ConnectionString = cs;
        }
        private Microsoft.Data.SqlClient.SqlConnection GetConnection()
        {
            return new Microsoft.Data.SqlClient.SqlConnection(ConnectionString);
        }

        public int InsertHD(int makh, int? makm, string dchi)
        {
            int last_id = 0;
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                try
                {
                    var now = DateTime.Now.ToString("yyyy-MM-dd");
                    conn.Open();
                    if (makm != null && makm > 0)
                    {
                        var sql = "INSERT INTO hoadon(MaKH, MaKM, DiaChiGH, NgayLapHD, TinhTrangTT, SoTienNhan, SoTienTra, TongTienTT, TinhTrangHD) VALUES(@makh, @makm, @dchi, @ngaylapHD, 0, 0, 0, @tongTienTT, 0); SELECT SCOPE_IDENTITY()";
                        Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("makh", makh);
                        cmd.Parameters.AddWithValue("makm", makm);
                        cmd.Parameters.AddWithValue("dchi", dchi);
                        cmd.Parameters.AddWithValue("ngaylapHD", now);
                        cmd.Parameters.AddWithValue("tongTienTT", 0);
                        last_id = int.Parse(cmd.ExecuteScalar().ToString());
                    }
                    else
                    {
                        var sql = "INSERT INTO hoadon(MaKH, MaKM, DiaChiGH, NgayLapHD, TinhTrangTT, SoTienNhan, SoTienTra, TongTienTT, TinhTrangHD) VALUES(@makh, NULL, @dchi, @ngaylapHD, 0, 0, 0, @tongTienTT, 0); SELECT SCOPE_IDENTITY()";
                        Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("makh", makh);
                        cmd.Parameters.AddWithValue("makm", DBNull.Value);
                        cmd.Parameters.AddWithValue("dchi", dchi);
                        cmd.Parameters.AddWithValue("ngaylapHD", now);
                        cmd.Parameters.AddWithValue("tongTienTT", 0);
                        last_id = int.Parse(cmd.ExecuteScalar().ToString());
                    }
                }
                finally
                {
                    conn.Close();
                }
            }
            return last_id;
        }

        public List<HoaDon> ListHD(int makh)
        {
            List<HoaDon> list = new List<HoaDon>();
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var sql = "SELECT * FROM hoadon WHERE MaKH = @makh ORDER BY NgayLapHD DESC";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("makh", makh);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        HoaDon hd = new HoaDon();
                        hd.MaHD = int.Parse(reader["MaHD"].ToString());
                        hd.MaKH = int.Parse(reader["MaKH"].ToString());
                        hd.MaKM = reader["MaKM"] != DBNull.Value ? (int?)int.Parse(reader["MaKM"].ToString()) : null;
                        hd.DiaChiGH = reader["DiaChiGH"].ToString();
                        hd.TongTienTT = int.Parse(reader["TongTienTT"].ToString());
                        hd.NgayLapHD = DateTime.Parse(reader["NgayLapHD"].ToString());
                        hd.TinhTrangHD = int.Parse(reader["TinhTrangHD"].ToString());
                        hd.TinhTrangTT = reader["TinhTrangTT"] != DBNull.Value ? (int?)int.Parse(reader["TinhTrangTT"].ToString()) : null;
                        hd.SoTienNhan = reader["SoTienNhan"] != DBNull.Value ? (int?)int.Parse(reader["SoTienNhan"].ToString()) : null;
                        hd.SoTienTra = reader["SoTienTra"] != DBNull.Value ? (int?)int.Parse(reader["SoTienTra"].ToString()) : null;
                        list.Add(hd);
                    }
                }
                conn.Close();
            }
            return list;
        }

        public int CapNhatHD(int mahd, int tthd)
        {
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var sql = "UPDATE hoadon SET TinhTrangHD = @tthd WHERE MaHD = @mahd";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("tthd", tthd);
                cmd.Parameters.AddWithValue("mahd", mahd);
                return (cmd.ExecuteNonQuery());
            }
        }

        public int CapNhatThanhToan(int mahd, int tinhtrangtt, int sotiennhan, int sotientra)
        {
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var sql = "UPDATE hoadon SET TinhTrangTT = @tinhtrangtt, SoTienNhan = @sotiennhan, SoTienTra = @sotientra WHERE MaHD = @mahd";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("tinhtrangtt", tinhtrangtt);
                cmd.Parameters.AddWithValue("sotiennhan", sotiennhan);
                cmd.Parameters.AddWithValue("sotientra", sotientra);
                cmd.Parameters.AddWithValue("mahd", mahd);
                return (cmd.ExecuteNonQuery());
            }
        }

        public int CapNhatTongTien(int mahd, int tongtien)
        {
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var sql = "UPDATE hoadon SET TongTienTT = @tongtien WHERE MaHD = @mahd";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("tongtien", tongtien);
                cmd.Parameters.AddWithValue("mahd", mahd);
                return (cmd.ExecuteNonQuery());
            }
        }

        public HoaDon GetHD(int mahd)
        {
            HoaDon hd = new HoaDon();
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var sql = "SELECT * FROM hoadon WHERE MaHD = @mahd";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("mahd", mahd);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        hd.MaHD = int.Parse(reader["MaHD"].ToString());
                        hd.MaKH = int.Parse(reader["MaKH"].ToString());
                        hd.MaKM = reader["MaKM"] != DBNull.Value ? (int?)int.Parse(reader["MaKM"].ToString()) : null;
                        hd.DiaChiGH = reader["DiaChiGH"].ToString();
                        hd.TongTienTT = int.Parse(reader["TongTienTT"].ToString());
                        hd.NgayLapHD = DateTime.Parse(reader["NgayLapHD"].ToString());
                        hd.TinhTrangHD = int.Parse(reader["TinhTrangHD"].ToString());
                        hd.TinhTrangTT = reader["TinhTrangTT"] != DBNull.Value ? (int?)int.Parse(reader["TinhTrangTT"].ToString()) : null;
                        hd.SoTienNhan = reader["SoTienNhan"] != DBNull.Value ? (int?)int.Parse(reader["SoTienNhan"].ToString()) : null;
                        hd.SoTienTra = reader["SoTienTra"] != DBNull.Value ? (int?)int.Parse(reader["SoTienTra"].ToString()) : null;
                    }
                }
            }
            return hd;
        }

        public List<HoaDon> GetAllHD()
        {
            List<HoaDon> list = new List<HoaDon>();
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var sql = "SELECT * FROM hoadon ORDER BY NgayLapHD DESC";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        HoaDon hd = new HoaDon();
                        hd.MaHD = int.Parse(reader["MaHD"].ToString());
                        hd.MaKH = int.Parse(reader["MaKH"].ToString());
                        hd.MaKM = reader["MaKM"] != DBNull.Value ? (int?)int.Parse(reader["MaKM"].ToString()) : null;
                        hd.DiaChiGH = reader["DiaChiGH"].ToString();
                        hd.TongTienTT = int.Parse(reader["TongTienTT"].ToString());
                        hd.NgayLapHD = DateTime.Parse(reader["NgayLapHD"].ToString());
                        hd.TinhTrangHD = int.Parse(reader["TinhTrangHD"].ToString());
                        hd.TinhTrangTT = reader["TinhTrangTT"] != DBNull.Value ? (int?)int.Parse(reader["TinhTrangTT"].ToString()) : null;
                        hd.SoTienNhan = reader["SoTienNhan"] != DBNull.Value ? (int?)int.Parse(reader["SoTienNhan"].ToString()) : null;
                        hd.SoTienTra = reader["SoTienTra"] != DBNull.Value ? (int?)int.Parse(reader["SoTienTra"].ToString()) : null;
                        list.Add(hd);
                    }
                }
                conn.Close();
            }
            return list;
        }
    }
}
