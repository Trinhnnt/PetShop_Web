using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Web_projectframeword_admin.Models
{
    public class ThongKe
    {
        public ThongKe()
        {
            ConnectionString = "Data Source=localhost;Initial Catalog=website_petShop;Integrated Security=True";
        }
        public string ConnectionString { get; set; }//biết thành viên
        public ThongKe(string connectionString) //phuong thuc khoi tao
        {
            this.ConnectionString = connectionString;
        }
        private Microsoft.Data.SqlClient.SqlConnection GetConnection() //lấy connection 
        {
            return new Microsoft.Data.SqlClient.SqlConnection(ConnectionString);
        }
        public long DoanhThuThang(int thang, int nam)
        {
            long DT = 0;
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                // Câu truy vấn đã phù hợp với cấu trúc bảng, chỉ cần điều chỉnh cách xử lý kết quả
                string str = "SELECT MONTH(NgayLapHD) AS Thang, SUM(TongTienTT) AS Tong FROM hoadon WHERE YEAR(NgayLapHD) = @nam AND MONTH(NgayLapHD) = @thang AND TinhTrangTT = 1 GROUP BY MONTH(NgayLapHD)";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(str, conn);
                cmd.Parameters.AddWithValue("nam", nam);
                cmd.Parameters.AddWithValue("thang", thang);

                using (var reader = cmd.ExecuteReader())
                {
                    // Nếu có dữ liệu trả về, lấy giá trị tổng
                    if (reader.Read())
                    {
                        // Kiểm tra nếu giá trị không phải là DBNull
                        if (reader["Tong"] != DBNull.Value)
                        {
                            DT = Convert.ToInt64(reader["Tong"]);
                        }
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return DT;
        }
        public long DoanhThuNam(int nam)
        {
            long DT = 0;
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                // Câu truy vấn đã phù hợp với cấu trúc bảng
                string str = "SELECT YEAR(NgayLapHD) AS Nam, SUM(TongTienTT) AS Tong FROM hoadon WHERE YEAR(NgayLapHD) = @nam AND TinhTrangTT = 1 GROUP BY YEAR(NgayLapHD)";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(str, conn);
                cmd.Parameters.AddWithValue("nam", nam);

                using (var reader = cmd.ExecuteReader())
                {
                    // Thay đổi từ while sang if vì chỉ có một kết quả duy nhất
                    if (reader.Read())
                    {
                        // Kiểm tra nếu giá trị không phải là DBNull
                        if (reader["Tong"] != DBNull.Value)
                        {
                            DT = Convert.ToInt64(reader["Tong"]);
                        }
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return DT;
        }
        
        public List<HoaDon> GetHDsTheoThang(int thang, int nam)
        {
            List<HoaDon> list = new List<HoaDon>();
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "SELECT h.*, k.TenKH FROM hoadon h JOIN khachhang k ON h.MaKH = k.MaKH WHERE YEAR(NgayLapHD) = @nam AND MONTH(NgayLapHD) = @thang AND TinhTrangTT = 1";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(str, conn);
                cmd.Parameters.AddWithValue("nam", nam);
                cmd.Parameters.AddWithValue("thang", thang);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new HoaDon()
                        {
                            MaHD = Convert.ToInt32(reader["MaHD"]),
                            TenKH = reader["TenKH"].ToString(),
                            TongTienTT = Convert.ToInt32(reader["TongTienTT"]),
                            NgayLapHD = Convert.ToDateTime(reader["NgayLapHD"]),
                            // Thêm các thuộc tính khác nếu cần thiết
                            TinhTrangHD = Convert.ToInt32(reader["TinhTrangHD"]),
                            TinhTrangTT = reader["TinhTrangTT"] != DBNull.Value ? Convert.ToInt32(reader["TinhTrangTT"]) : 0,
                            MaKH = Convert.ToInt32(reader["MaKH"]),
                            MaKM = reader["MaKM"] != DBNull.Value ? Convert.ToInt32(reader["MaKM"]) : (int?)null,
                            DiaChiGH = reader["DiaChiGH"] != DBNull.Value ? reader["DiaChiGH"].ToString() : null,
                            SoTienNhan = reader["SoTienNhan"] != DBNull.Value ? Convert.ToInt32(reader["SoTienNhan"]) : 0,
                            SoTienTra = reader["SoTienTra"] != DBNull.Value ? Convert.ToInt32(reader["SoTienTra"]) : 0
                        });
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return list;
        }
        /// <summary>
        /// Lấy danh sách phiếu nhập theo tháng và năm
        /// </summary>
        public List<NhapHang> GetPNsTheoThang(int thang, int nam)
        {
            List<NhapHang> list = new List<NhapHang>();
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "SELECT p.*, n.TenNCC FROM phieunhap p JOIN nhacc n ON p.MaNCC = n.MaNCC WHERE YEAR(NgayLapPN) = @nam AND MONTH(NgayLapPN) = @thang";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(str, conn);
                cmd.Parameters.AddWithValue("nam", nam);
                cmd.Parameters.AddWithValue("thang", thang);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new NhapHang()
                        {
                            MAPN = Convert.ToInt32(reader["MaPN"]),
                            TENNCC = reader["TenNCC"].ToString(),
                            TONGTIENTT = Convert.ToInt32(reader["TongTienTT"]),
                            NGAYLAPPN = Convert.ToDateTime(reader["NgayLapPN"]),
                            TINHTRANGTT = Convert.ToInt32(reader["TinhTrangTT"]),
                            MANCC = Convert.ToInt32(reader["MaNCC"]),
                            MANV = Convert.ToInt32(reader["MaNV"])
                        });
                    }
                }
                conn.Close();
            }
            return list;
        }

        /// <summary>
        /// Tính tổng chi phí nhập hàng theo tháng
        /// </summary>
        public decimal ChiPhiThang(int thang, int nam)
        {
            decimal tongChiPhi = 0;
            List<NhapHang> dsPhieuNhap = GetPNsTheoThang(thang, nam);

            foreach (var phieuNhap in dsPhieuNhap)
            {
                // Chỉ tính các phiếu nhập đã thanh toán (TinhTrangTT = 1)
                if (phieuNhap.TINHTRANGTT == 1)
                {
                    tongChiPhi += phieuNhap.TONGTIENTT;
                }
            }

            return tongChiPhi;
        }

        /// <summary>
        /// Tính tổng chi phí nhập hàng theo năm
        /// </summary>
        public decimal ChiPhiNam(int nam)
        {
            decimal tongChiPhi = 0;

            for (int thang = 1; thang <= 12; thang++)
            {
                tongChiPhi += ChiPhiThang(thang, nam);
            }

            return tongChiPhi;
        }

        public List<object> Top3KH(int nam)
        {
            List<object> list = new List<object>();
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                // Thay đổi LIMIT 3 thành TOP 3 vì SQL Server sử dụng TOP thay vì LIMIT
                string str = "SELECT TOP 3 k.MaKH, k.TenKH, SUM(h.TongTienTT) AS TongTien " +
                             "FROM hoadon h JOIN khachhang k ON h.MaKH = k.MaKH " +
                             "WHERE YEAR(NgayLapHD) = @nam AND TinhTrangTT = 1 " +
                             "GROUP BY k.MaKH, k.TenKH " +
                             "ORDER BY TongTien DESC";

                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(str, conn);
                cmd.Parameters.AddWithValue("nam", nam);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new
                        {
                            MaKH = Convert.ToInt32(reader["MaKH"]),
                            TenKH = reader["TenKH"].ToString(),
                            TongTien = Convert.ToInt64(reader["TongTien"]),
                        });
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return list;
        }
        public List<object> DSSP(int nam)
        {
            List<object> list = new List<object>();
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                // Sửa câu truy vấn để phù hợp với cấu trúc bảng và cú pháp SQL Server
                string str = "SELECT t.MaTTSP, t.TenSP, COUNT(*) AS SL " +
                             "FROM cthd ct JOIN hoadon h ON ct.MaHD = h.MaHD " +
                             "JOIN thongtinsp t ON ct.MaTTSP = t.MaTTSP " +
                             "WHERE YEAR(h.NgayLapHD) = @nam AND h.TinhTrangHD = 1 " +
                             "GROUP BY t.MaTTSP, t.TenSP " +
                             "ORDER BY SL DESC";

                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(str, conn);
                cmd.Parameters.AddWithValue("nam", nam);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new
                        {
                            MaTTSP = Convert.ToInt32(reader["MaTTSP"]),
                            TenSP = reader["TenSP"].ToString(),
                            SL = Convert.ToInt64(reader["SL"]),
                        });
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return list;
        }
        public List<NhanVien> GetDanhSachNhanVien()
        {
            List<NhanVien> list = new List<NhanVien>();
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "SELECT * FROM nhanvien ORDER BY Luong DESC";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(str, conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new NhanVien()
                        {
                            MaNV = Convert.ToInt32(reader["MaNV"]),
                            TenNV = reader["TenNV"].ToString(),
                            NgayVL = Convert.ToDateTime(reader["NgayVL"]),
                            Luong = Convert.ToInt32(reader["Luong"]),
                            SDT = reader["SDT"].ToString(),
                            Email = reader["Email"].ToString(),
                            LoaiNV = reader["LoaiNV"].ToString(),
                            DiaChi = reader["DiaChi"].ToString()
                        });
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return list;
        }

    }
}
