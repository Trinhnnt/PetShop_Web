using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAn_FW.Models
{
    public class CTHD
    {
        // Properties theo cấu trúc bảng mới
        public int MaHD { get; set; }
        public int MaTTSP { get; set; }
        public long SoLuong { get; set; }
        public int? ThanhTien { get; set; }

        // Properties bổ sung (không lưu trong DB)
        public string TenSP { get; set; }
        public long Gia { get; set; }

        public string ConnectionString { get; set; }

        public CTHD()
        {
            ConnectionString = "Data Source=localhost;Initial Catalog=website_petShop;Integrated Security=True";
        }

        public CTHD(string cs)
        {
            ConnectionString = cs;
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        // Thêm chi tiết hóa đơn
        public int InsertCTHD(CTHD ct)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                // Câu lệnh INSERT này vẫn dựa vào trigger Before_Insert_CTHD để tính ThanhTien cuối cùng được lưu
                // Nếu bạn muốn tự tính ThanhTien ở đây và bỏ qua trigger, bạn cần sửa lại
                var sql = "INSERT INTO cthd(MaHD, MaTTSP, SoLuong, ThanhTien) VALUES(@mahd, @mattsp, @soluong, @thanhtien)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("mahd", ct.MaHD);
                cmd.Parameters.AddWithValue("mattsp", ct.MaTTSP);
                cmd.Parameters.AddWithValue("soluong", ct.SoLuong);

                // Nếu bạn muốn ThanhTien truyền vào được sử dụng thay vì trigger tính:
                // 1. Tính ThanhTien ở Controller khi gọi InsertCTHD (dựa trên giá gốc)
                // 2. Sửa trigger Before_Insert_CTHD để nó không ghi đè ThanhTien nếu đã có giá trị,
                //    hoặc bỏ hoàn toàn phần tính ThanhTien trong trigger nếu bạn luôn tính ở C#.
                // Hiện tại, để đơn giản, cứ để trigger SQL tự tính ThanhTien khi LƯU VÀO DB.
                // Còn khi HIỂN THỊ CHI TIẾT HÓA ĐƠN, ListCTHD sẽ tính lại theo giá gốc.
                cmd.Parameters.AddWithValue("thanhtien", ct.ThanhTien ?? (object)DBNull.Value); // Giá trị này có thể được trigger ghi đè

                return cmd.ExecuteNonQuery();
            }
        }

        // Lấy danh sách chi tiết hóa đơn theo mã hóa đơn
        public List<CTHD> ListCTHD(int mahd)
        {
            List<CTHD> list = new List<CTHD>();
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                // Lấy Gia từ thongtinsp là giá gốc
                var sql = @"SELECT c.MaHD, c.MaTTSP, c.SoLuong, t.TenSP, t.Gia 
                            FROM cthd c 
                            JOIN thongtinsp t ON c.MaTTSP = t.MaTTSP 
                            WHERE c.MaHD = @mahd";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@mahd", mahd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        long donGiaGoc = Convert.ToInt64(reader["Gia"]); // Lấy giá gốc từ thongtinsp
                        long soLuongHienTai = Convert.ToInt64(reader["SoLuong"]);

                        list.Add(new CTHD
                        {
                            MaHD = Convert.ToInt32(reader["MaHD"]),
                            MaTTSP = Convert.ToInt32(reader["MaTTSP"]),
                            SoLuong = soLuongHienTai,
                            TenSP = reader["TenSP"].ToString(),
                            Gia = donGiaGoc, // Lưu đơn giá gốc
                            ThanhTien = (int)(donGiaGoc * soLuongHienTai) // Tính thành tiền dựa trên giá gốc
                        });
                    }
                }
            }
            return list;
        }

        // Cập nhật chi tiết hóa đơn
        public int UpdateCTHD(CTHD ct)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                var sql = "UPDATE cthd SET SoLuong = @soluong, ThanhTien = @thanhtien WHERE MaHD = @mahd AND MaTTSP = @mattsp";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("mahd", ct.MaHD);
                cmd.Parameters.AddWithValue("mattsp", ct.MaTTSP);
                cmd.Parameters.AddWithValue("soluong", ct.SoLuong);
                cmd.Parameters.AddWithValue("thanhtien", ct.ThanhTien ?? (object)DBNull.Value);

                return cmd.ExecuteNonQuery();
            }
        }

        // Xóa chi tiết hóa đơn
        public int DeleteCTHD(int mahd, int mattsp)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                var sql = "DELETE FROM cthd WHERE MaHD = @mahd AND MaTTSP = @mattsp";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("mahd", mahd);
                cmd.Parameters.AddWithValue("mattsp", mattsp);

                return cmd.ExecuteNonQuery();
            }
        }

        // Xóa tất cả chi tiết của một hóa đơn
        public int DeleteAllCTHDByMaHD(int mahd)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                var sql = "DELETE FROM cthd WHERE MaHD = @mahd";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("mahd", mahd);

                return cmd.ExecuteNonQuery();
            }
        }

        // Lấy chi tiết hóa đơn theo mã hóa đơn và mã sản phẩm
        public CTHD GetCTHD(int mahd, int mattsp)
        {
            CTHD ct = null;
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                var sql = @"SELECT c.*, t.TenSP, t.Gia 
                           FROM cthd c 
                           JOIN thongtinsp t ON c.MaTTSP = t.MaTTSP 
                           WHERE c.MaHD = @mahd AND c.MaTTSP = @mattsp";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("mahd", mahd);
                cmd.Parameters.AddWithValue("mattsp", mattsp);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        ct = new CTHD
                        {
                            MaHD = Convert.ToInt32(reader["MaHD"]),
                            MaTTSP = Convert.ToInt32(reader["MaTTSP"]),
                            SoLuong = Convert.ToInt32(reader["SoLuong"]),
                            ThanhTien = reader["ThanhTien"] != DBNull.Value ? Convert.ToInt32(reader["ThanhTien"]) : (int?)null,
                            TenSP = reader["TenSP"].ToString(),
                            Gia = Convert.ToInt32(reader["Gia"])
                        };
                    }
                }
            }
            return ct;
        }

        // Tính tổng tiền của một hóa đơn
        public int TinhTongTien(int mahd)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                var sql = "SELECT SUM(ThanhTien) FROM cthd WHERE MaHD = @mahd";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("mahd", mahd);

                var result = cmd.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    return Convert.ToInt32(result);
                }
                return 0;
            }
        }

        // Cập nhật số lượng sản phẩm sau khi đặt hàng
        public bool CapNhatSoLuongSanPham(int mattsp, int soluong)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                var sql = "UPDATE thongtinsp SET SoLuong = SoLuong - @soluong WHERE MaTTSP = @mattsp AND SoLuong >= @soluong";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("mattsp", mattsp);
                cmd.Parameters.AddWithValue("soluong", soluong);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // Kiểm tra số lượng sản phẩm trong kho
        public bool KiemTraSoLuong(int mattsp, int soluong)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                var sql = "SELECT SoLuong FROM thongtinsp WHERE MaTTSP = @mattsp";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("mattsp", mattsp);

                var result = cmd.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    int soLuongTonKho = Convert.ToInt32(result);
                    return soLuongTonKho >= soluong;
                }
                return false;
            }
        }
    }
}
