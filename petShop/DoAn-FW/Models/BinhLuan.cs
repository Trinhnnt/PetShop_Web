using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAn_FW.Models
{
    public class BinhLuan
    {
        // Properties theo cấu trúc bảng mới
        public int MaBL { get; set; }
        public int MaTTSP { get; set; }
        public int MaKH { get; set; }
        public string NoiDung { get; set; }

        public string ConnectionString { get; set; }

        public BinhLuan()
        {
            ConnectionString = "Data Source=localhost;Initial Catalog=website_petShop;Integrated Security=True";
        }

        public BinhLuan(string cs)
        {
            ConnectionString = cs;
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        // Thêm bình luận mới
        public int InsertBL(BinhLuan bl)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                var sql = "INSERT INTO binhluan(MaTTSP, MaKH, NoiDung) VALUES(@mattsp, @makh, @noidung); SELECT SCOPE_IDENTITY();";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("mattsp", bl.MaTTSP);
                cmd.Parameters.AddWithValue("makh", bl.MaKH);
                cmd.Parameters.AddWithValue("noidung", bl.NoiDung ?? (object)DBNull.Value);

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        // Lấy danh sách bình luận theo sản phẩm
        public List<BinhLuan> GetBinhLuanBySanPham(int maTTSP)
        {
            List<BinhLuan> list = new List<BinhLuan>();
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                var sql = "SELECT * FROM binhluan WHERE MaTTSP = @mattsp ORDER BY MaBL DESC";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("mattsp", maTTSP);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new BinhLuan
                        {
                            MaBL = Convert.ToInt32(reader["MaBL"]),
                            MaTTSP = Convert.ToInt32(reader["MaTTSP"]),
                            MaKH = Convert.ToInt32(reader["MaKH"]),
                            NoiDung = reader["NoiDung"]?.ToString()
                        });
                    }
                }
            }
            return list;
        }

        // Lấy danh sách bình luận theo khách hàng
        public List<BinhLuan> GetBinhLuanByKhachHang(int maKH)
        {
            List<BinhLuan> list = new List<BinhLuan>();
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                var sql = "SELECT * FROM binhluan WHERE MaKH = @makh ORDER BY MaBL DESC";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("makh", maKH);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new BinhLuan
                        {
                            MaBL = Convert.ToInt32(reader["MaBL"]),
                            MaTTSP = Convert.ToInt32(reader["MaTTSP"]),
                            MaKH = Convert.ToInt32(reader["MaKH"]),
                            NoiDung = reader["NoiDung"]?.ToString()
                        });
                    }
                }
            }
            return list;
        }

        // Lấy chi tiết bình luận theo mã
        public BinhLuan GetBinhLuanByID(int maBL)
        {
            BinhLuan bl = new BinhLuan();
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                var sql = "SELECT * FROM binhluan WHERE MaBL = @mabl";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("mabl", maBL);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        bl.MaBL = Convert.ToInt32(reader["MaBL"]);
                        bl.MaTTSP = Convert.ToInt32(reader["MaTTSP"]);
                        bl.MaKH = Convert.ToInt32(reader["MaKH"]);
                        bl.NoiDung = reader["NoiDung"]?.ToString();
                    }
                }
            }
            return bl;
        }

        // Cập nhật bình luận
        public int UpdateBinhLuan(BinhLuan bl)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                var sql = "UPDATE binhluan SET NoiDung = @noidung WHERE MaBL = @mabl";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("mabl", bl.MaBL);
                cmd.Parameters.AddWithValue("noidung", bl.NoiDung ?? (object)DBNull.Value);

                return cmd.ExecuteNonQuery();
            }
        }

        // Xóa bình luận
        public int DeleteBinhLuan(int maBL)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                var sql = "DELETE FROM binhluan WHERE MaBL = @mabl";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("mabl", maBL);

                return cmd.ExecuteNonQuery();
            }
        }

        // Lấy danh sách bình luận kèm thông tin khách hàng
        public List<object> GetBinhLuanWithKhachHang(int maTTSP)
        {
            List<object> list = new List<object>();
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                var sql = @"SELECT bl.*, kh.HoTen, kh.Hinh 
                           FROM binhluan bl 
                           JOIN khachhang kh ON bl.MaKH = kh.MaKH 
                           WHERE bl.MaTTSP = @mattsp 
                           ORDER BY bl.MaBL DESC";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("mattsp", maTTSP);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new
                        {
                            MaBL = Convert.ToInt32(reader["MaBL"]),
                            MaTTSP = Convert.ToInt32(reader["MaTTSP"]),
                            MaKH = Convert.ToInt32(reader["MaKH"]),
                            NoiDung = reader["NoiDung"]?.ToString(),
                            HoTen = reader["HoTen"]?.ToString(),
                            Hinh = reader["Hinh"]?.ToString()
                        });
                    }
                }
            }
            return list;
        }

        // Đếm số lượng bình luận theo sản phẩm
        public int CountBinhLuanBySanPham(int maTTSP)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                var sql = "SELECT COUNT(*) FROM binhluan WHERE MaTTSP = @mattsp";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("mattsp", maTTSP);

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
    }
}
