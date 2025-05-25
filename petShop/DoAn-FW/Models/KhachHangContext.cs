using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using PagedList;

namespace DoAn_FW.Models
{
    public class KhachHangContext
    {
        public string ConnectionString { get; set; }
        public KhachHangContext()
        {
            ConnectionString = "Data Source=localhost;Initial Catalog=website_petShop;Integrated Security=True";
        }
        public KhachHangContext(string cs)
        {
            ConnectionString = cs;
        }
        private Microsoft.Data.SqlClient.SqlConnection GetConnection()
        {
            return new Microsoft.Data.SqlClient.SqlConnection(ConnectionString);
        }

        public int ThemTK(KhachHang kh)
        {
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var sql = "INSERT INTO khachhang(TenKH, GioiTinh, SDT, Email, MatKhau, CMND, DiaChi, LoaiKH) VALUES(@tenkh, @gt, @sdt, @email, @matkhau, @cmnd, @diachi, @loaikh)";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("tenkh", kh.TenKH);
                cmd.Parameters.AddWithValue("gt", kh.GioiTinh);
                cmd.Parameters.AddWithValue("sdt", kh.SDT);
                cmd.Parameters.AddWithValue("email", kh.Email);
                cmd.Parameters.AddWithValue("matkhau", kh.MatKhau);
                cmd.Parameters.AddWithValue("cmnd", kh.CMND);
                cmd.Parameters.AddWithValue("diachi", kh.DiaChi ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("loaikh", kh.LoaiKH);
                return (cmd.ExecuteNonQuery());
            }
        }

        public int CapNhatTK(KhachHang kh)
        {
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var sql = "UPDATE khachhang SET TenKH = @tenkh, GioiTinh = @gt, SDT = @sdt, CMND = @cmnd, DiaChi = @diachi WHERE MaKH = @makh";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("tenkh", kh.TenKH);
                cmd.Parameters.AddWithValue("gt", kh.GioiTinh);
                cmd.Parameters.AddWithValue("sdt", kh.SDT);
                cmd.Parameters.AddWithValue("cmnd", kh.CMND);
                cmd.Parameters.AddWithValue("diachi", kh.DiaChi ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("makh", kh.MaKH);

                return (cmd.ExecuteNonQuery());
            }
        }

        public int CapNhatMatKhau(int makh, string matkhaumoi)
        {
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var sql = "UPDATE khachhang SET MatKhau = @matkhau WHERE MaKH = @makh";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("matkhau", matkhaumoi);
                cmd.Parameters.AddWithValue("makh", makh);
                return (cmd.ExecuteNonQuery());
            }
        }

        public KhachHang KHDangNhap(string email, string matkhau)
        {
            KhachHang kh = new KhachHang();
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var sql = "SELECT * FROM khachhang WHERE Email = @email AND MatKhau = @matkhau";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("email", email);
                cmd.Parameters.AddWithValue("matkhau", matkhau);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader.HasRows)
                        {
                            kh.MaKH = int.Parse(reader["MaKH"].ToString());
                            kh.TenKH = reader["TenKH"].ToString();
                            kh.GioiTinh = reader["GioiTinh"].ToString();
                            kh.SDT = reader["SDT"].ToString();
                            kh.Email = reader["Email"].ToString();
                            kh.MatKhau = reader["MatKhau"].ToString();
                            kh.CMND = reader["CMND"].ToString();
                            kh.DiaChi = reader["DiaChi"] != DBNull.Value ? reader["DiaChi"].ToString() : null;
                            kh.LoaiKH = reader["LoaiKH"].ToString();
                        }
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return kh;
        }

        public KhachHang GetKhachHang(int makh)
        {
            KhachHang kh = new KhachHang();
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var sql = "SELECT * FROM khachhang WHERE MaKH = @makh";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("makh", makh);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        kh.MaKH = int.Parse(reader["MaKH"].ToString());
                        kh.TenKH = reader["TenKH"].ToString();
                        kh.GioiTinh = reader["GioiTinh"].ToString();
                        kh.SDT = reader["SDT"].ToString();
                        kh.Email = reader["Email"].ToString();
                        kh.MatKhau = reader["MatKhau"].ToString();
                        kh.CMND = reader["CMND"].ToString();
                        kh.DiaChi = reader["DiaChi"] != DBNull.Value ? reader["DiaChi"].ToString() : null;
                        kh.LoaiKH = reader["LoaiKH"].ToString();
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return kh;
        }

        public bool KiemTraEmail(string email)
        {
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var sql = "SELECT COUNT(*) FROM khachhang WHERE Email = @email";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("email", email);
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        public List<KhachHang> DanhSachKhachHang()
        {
            List<KhachHang> danhSach = new List<KhachHang>();
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var sql = "SELECT * FROM khachhang ORDER BY MaKH DESC";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        KhachHang kh = new KhachHang();
                        kh.MaKH = int.Parse(reader["MaKH"].ToString());
                        kh.TenKH = reader["TenKH"].ToString();
                        kh.GioiTinh = reader["GioiTinh"].ToString();
                        kh.SDT = reader["SDT"].ToString();
                        kh.Email = reader["Email"].ToString();
                        kh.MatKhau = reader["MatKhau"].ToString();
                        kh.CMND = reader["CMND"].ToString();
                        kh.DiaChi = reader["DiaChi"] != DBNull.Value ? reader["DiaChi"].ToString() : null;
                        kh.LoaiKH = reader["LoaiKH"].ToString();
                        danhSach.Add(kh);
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return danhSach;
        }

        public int XoaKhachHang(int makh)
        {
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var sql = "DELETE FROM khachhang WHERE MaKH = @makh";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("makh", makh);
                return cmd.ExecuteNonQuery();
            }
        }

        public int CapNhatLoaiKH(int makh, string loaikh)
        {
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var sql = "UPDATE khachhang SET LoaiKH = @loaikh WHERE MaKH = @makh";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("loaikh", loaikh);
                cmd.Parameters.AddWithValue("makh", makh);
                return cmd.ExecuteNonQuery();
            }
        }
    }
}
