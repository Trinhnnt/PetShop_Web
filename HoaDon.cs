using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAn_FW.Models
{
    public class HoaDon
    {
        public virtual int MaHD { get; set; }         // Mã hóa đơn (số duy nhất)
        //virtual: truy cập bất kì đâu
        public virtual int MaKH { get; set; }         // Mã khách hàng liên kết với hóa đơn này
        public virtual int? MaKM { get; set; }        // Mã khuyến mãi (nếu có, nếu không thì là null - dấu ? cho phép điều này)
        public virtual string DiaChiGH { get; set; }   // Địa chỉ giao hàng
        public virtual int TongTienTT { get; set; }   // Tổng số tiền khách phải thanh toán
        public virtual DateTime NgayLapHD { get; set; }// Ngày giờ hóa đơn được tạo
        public virtual int TinhTrangHD { get; set; }  // Tình trạng của hóa đơn (ví dụ: đang chờ xử lý, đang giao, đã giao...)
                                                      // Chúng ta cần tìm hiểu xem các con số ở đây (0, 1, 2...) có ý nghĩa cụ thể là gì.
        public virtual int? TinhTrangTT { get; set; }// Tình trạng thanh toán (ví dụ: chưa thanh toán, đã thanh toán)
                                                     // Cũng là số, và có thể là null.
        public virtual int? SoTienNhan { get; set; }  // Số tiền thực tế cửa hàng nhận từ khách (khi thanh toán tiền mặt)
        public virtual int? SoTienTra { get; set; }   // Số tiền thối lại cho khách

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

        public int InsertHD(int makh, int makm_da_apdung, string diaChiGiaoHang) // Đổi tên tham số cho rõ ràng
        {
            int newHoaDonID = 0;
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                // Sử dụng @MaKH, @MaKM, @DiaChiGH, @NgayLapHD trong câu SQL
                var sql = @"INSERT INTO hoadon (MaKH, MaKM, DiaChiGH, TongTienTT, NgayLapHD, TinhTrangHD, TinhTrangTT) 
                        VALUES (@MaKH, @MaKM, @DiaChiGH, 0, @NgayLapHD, 0, 0); 
                        SELECT SCOPE_IDENTITY();";
                // Giả sử TinhTrangHD = 0 là 'Chờ xử lý', TinhTrangTT = 0 là 'Chưa thanh toán'
                // TongTienTT ban đầu có thể là 0, sau đó trigger After_Insert_CTHD sẽ cập nhật

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@MaKH", makh);

                if (makm_da_apdung > 0) // Kiểm tra MaKM hợp lệ
                {
                    cmd.Parameters.AddWithValue("@MaKM", makm_da_apdung);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@MaKM", DBNull.Value); // Nếu không có KM thì truyền DBNull
                }

                cmd.Parameters.AddWithValue("@DiaChiGH", diaChiGiaoHang); // Tên tham số khớp với SQL
                cmd.Parameters.AddWithValue("@NgayLapHD", DateTime.Now.Date); // Ngày lập hóa đơn hiện tại

                object result = cmd.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    newHoaDonID = Convert.ToInt32(result);
                }
            }
            return newHoaDonID;
        }

        public List<HoaDon> ListHD(int makh)
        {
            List<HoaDon> list = new List<HoaDon>();
            // Viết code để lấy danh sách hóa đơn theo MaKH
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                var sql = "SELECT * FROM hoadon WHERE MaKH = @makh ORDER BY NgayLapHD DESC";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@makh", makh);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new HoaDon
                        {
                            MaHD = Convert.ToInt32(reader["MaHD"]),
                            MaKH = Convert.ToInt32(reader["MaKH"]),
                            MaKM = reader["MaKM"] != DBNull.Value ? Convert.ToInt32(reader["MaKM"]) : (int?)null,
                            DiaChiGH = reader["DiaChiGH"]?.ToString(),
                            TongTienTT = Convert.ToInt32(reader["TongTienTT"]),
                            NgayLapHD = Convert.ToDateTime(reader["NgayLapHD"]),
                            TinhTrangHD = Convert.ToInt32(reader["TinhTrangHD"]),
                            TinhTrangTT = reader["TinhTrangTT"] != DBNull.Value ? Convert.ToInt32(reader["TinhTrangTT"]) : (int?)null
                            // Các trường SoTienNhan, SoTienTra nếu có
                        });
                    }
                }
            }
            return list;
        }

        public int CapNhatHD(int mahd, int tthd)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                var sql = "UPDATE hoadon SET TinhTrangHD = @tthd WHERE MaHD = @mahd";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@tthd", tthd);
                cmd.Parameters.AddWithValue("@mahd", mahd);
                return cmd.ExecuteNonQuery();
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
            HoaDon hd = null;
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                string sql = "SELECT * FROM hoadon WHERE MaHD = @MaHD";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaHD", mahd);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        hd = new HoaDon
                        {
                            MaHD = Convert.ToInt32(reader["MaHD"]),
                            MaKH = Convert.ToInt32(reader["MaKH"]),
                            MaKM = reader["MaKM"] != DBNull.Value ? Convert.ToInt32(reader["MaKM"]) : (int?)null,
                            DiaChiGH = reader["DiaChiGH"]?.ToString(),
                            TongTienTT = Convert.ToInt32(reader["TongTienTT"]),
                            NgayLapHD = Convert.ToDateTime(reader["NgayLapHD"]),
                            TinhTrangHD = Convert.ToInt32(reader["TinhTrangHD"]),
                            TinhTrangTT = reader["TinhTrangTT"] != DBNull.Value ? Convert.ToInt32(reader["TinhTrangTT"]) : (int?)null,
                            SoTienNhan = reader["SoTienNhan"] != DBNull.Value ? Convert.ToInt32(reader["SoTienNhan"]) : (int?)null,
                            SoTienTra = reader["SoTienTra"] != DBNull.Value ? Convert.ToInt32(reader["SoTienTra"]) : (int?)null
                        };
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
