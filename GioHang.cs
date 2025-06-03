using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace DoAn_FW.Models
{
    public class GioHang
    {
        public string ConnectionString { get; set; }

        public GioHang()
        {
            ConnectionString = @"Data Source=localhost\HAGIANHU;Initial Catalog=website_PetShop;Integrated Security=True";
        }

        public GioHang(string cs)
        {
            ConnectionString = cs;
        }

        private Microsoft.Data.SqlClient.SqlConnection GetConnection()
        {
            return new Microsoft.Data.SqlClient.SqlConnection(ConnectionString);
        }

        public int MaTTSP { get; set; }
        public int MaKH { get; set; }
        public int SoLuong { get; set; }

        // Lấy danh sách sản phẩm trong giỏ hàng của khách hàng
        public List<GioHang> ListGioHang(int makh)
        {
            List<GioHang> list = new List<GioHang>();
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var sql = "SELECT * FROM giohang WHERE MaKH = @makh";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("makh", makh);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new GioHang()
                        {
                            MaTTSP = int.Parse(reader["MaTTSP"].ToString()),
                            MaKH = int.Parse(reader["MaKH"].ToString()),
                            SoLuong = int.Parse(reader["SoLuong"].ToString())
                        });
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return list;
        }

        // Lấy chi tiết sản phẩm trong giỏ hàng kèm thông tin sản phẩm
        public List<object> ListCTSP(int makh)
        {
            List<object> list = new List<object>();
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var sql = @"SELECT t.MaTTSP, t.MaLoaiSP, g.MaKH, g.SoLuong, t.TenSP, t.HinhAnh, t.Gia, t.GiaKM, 
           t.MauSac, t.KhoiLuong, t.DoTuoi, t.XuatXu, t.KichThuoc, t.ThanhPhan, 
           t.CongDung, t.HuongDanSD, t.TenLoaiSP, t.TenTH, t.SoLuong AS SoLuongTonKho
           FROM giohang g 
           JOIN thongtinsp t ON g.MaTTSP = t.MaTTSP 
           WHERE g.MaKH = @makh";

                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("makh", makh);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new
                        {
                            MaTTSP = int.Parse(reader["MaTTSP"].ToString()),
                            MaLoaiSP = int.Parse(reader["MaLoaiSP"].ToString()),
                            MaKH = int.Parse(reader["MaKH"].ToString()),
                            SoLuong = int.Parse(reader["SoLuong"].ToString()),
                            TenSP = reader["TenSP"].ToString(),
                            HinhAnh = reader["HinhAnh"].ToString(),
                            Gia = long.Parse(reader["Gia"].ToString()),
                            GiaKM = long.Parse(reader["GiaKM"].ToString()),
                            MauSac = reader["MauSac"]?.ToString(),
                            KhoiLuong = reader["KhoiLuong"] != DBNull.Value ? Convert.ToDouble(reader["KhoiLuong"]) : (double?)null,
                            DoTuoi = reader["DoTuoi"]?.ToString(),
                            XuatXu = reader["XuatXu"]?.ToString(),
                            KichThuoc = reader["KichThuoc"]?.ToString(),
                            ThanhPhan = reader["ThanhPhan"]?.ToString(),
                            CongDung = reader["CongDung"]?.ToString(),
                            HuongDanSD = reader["HuongDanSD"]?.ToString(),
                            TenLoaiSP = reader["TenLoaiSP"].ToString(),
                            TenTH = reader["TenTH"].ToString(),
                            SoLuongTonKho = int.Parse(reader["SoLuongTonKho"].ToString())

                        });
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return list;
        }

        // Thêm sản phẩm vào giỏ hàng
        public int InsertGH(GioHang gh)
        {
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var sql = "INSERT INTO giohang(MaTTSP, SoLuong, MaKH) VALUES(@mattsp, @soluong, @makh)";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("mattsp", gh.MaTTSP);
                cmd.Parameters.AddWithValue("soluong", gh.SoLuong);
                cmd.Parameters.AddWithValue("makh", gh.MaKH);
                return (cmd.ExecuteNonQuery());
            }
        }

        // Cập nhật số lượng sản phẩm trong giỏ hàng
        public int UpdateGH(GioHang gh)
        {
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var sql = "UPDATE giohang SET SoLuong = @soluong WHERE MaTTSP = @mattsp AND MaKH = @makh";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("mattsp", gh.MaTTSP);
                cmd.Parameters.AddWithValue("soluong", gh.SoLuong);
                cmd.Parameters.AddWithValue("makh", gh.MaKH);
                return (cmd.ExecuteNonQuery());
            }
        }

        // Xóa sản phẩm khỏi giỏ hàng
        public int DeleteGH(int maTTSP, int maKH)
        {
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var sql = "DELETE FROM giohang WHERE MaTTSP = @mattsp AND MaKH = @makh";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("mattsp", maTTSP);
                cmd.Parameters.AddWithValue("makh", maKH);
                return cmd.ExecuteNonQuery();
            }
        }

        // Xóa toàn bộ giỏ hàng của khách hàng
        public int ClearGioHang(int maKH)
        {
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var sql = "DELETE FROM giohang WHERE MaKH = @makh";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("makh", maKH);
                return cmd.ExecuteNonQuery();
            }
        }


        // Kiểm tra sản phẩm đã có trong giỏ hàng chưa
        public bool CheckProductInCart(int maTTSP, int maKH)
        {
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var sql = "SELECT COUNT(*) FROM giohang WHERE MaTTSP = @mattsp AND MaKH = @makh";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("mattsp", maTTSP);
                cmd.Parameters.AddWithValue("makh", maKH);
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }
        public int GetProductStock(int maTTSP)
        {
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var sql = "SELECT SoLuong FROM thongtinsp WHERE MaTTSP = @mattsp";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("mattsp", maTTSP);
                var result = cmd.ExecuteScalar();
                return result != null ? Convert.ToInt32(result) : 0;
            }
        }

        public int XoaGioHangTheoKH(int maKH)
        {
            int result = 0;
            using (var conn = GetConnection())
            {
                conn.Open();
                var sql = "DELETE FROM giohang WHERE MaKH = @makh";
                var cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@makh", maKH);
                result = cmd.ExecuteNonQuery();
            }
            return result;
        }
    }
}
