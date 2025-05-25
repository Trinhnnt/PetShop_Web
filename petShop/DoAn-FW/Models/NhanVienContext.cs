using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAn_FW.Models
{
    public class NhanVienContext
    {
        public string ConnectionString { get; set; }
        public NhanVienContext()
        {
            ConnectionString = "Data Source=localhost;Initial Catalog=website_petShop;Integrated Security=True";
        }
        public NhanVienContext(string cs)
        {
            ConnectionString = cs;
        }
        private Microsoft.Data.SqlClient.SqlConnection GetConnection()
        {
            return new Microsoft.Data.SqlClient.SqlConnection(ConnectionString);
        }

        public NhanVien KHDangNhap(string email, string matkhau)
        {
            NhanVien NV = new NhanVien();
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var sql = "SELECT * FROM nhanvien WHERE Email = @email AND MatKhau = @matkhau";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("email", email);
                cmd.Parameters.AddWithValue("matkhau", matkhau);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader.HasRows)
                        {
                            NV.MaNV = Convert.ToInt32(reader["MaNV"]);
                            NV.TenNV = reader["TenNV"].ToString();
                            NV.NgayVL = Convert.ToDateTime(reader["NgayVL"]);
                            NV.Luong = Convert.ToInt32(reader["Luong"]);
                            NV.SDT = reader["SDT"].ToString();
                            NV.Email = reader["Email"].ToString();
                            NV.CMND = reader["CMND"].ToString();
                            NV.DiaChi = reader["DiaChi"].ToString();
                            NV.MatKhau = reader["MatKhau"].ToString();
                            NV.LoaiNV = reader["LoaiNV"].ToString();
                        }
                    }
                }
            }
            return NV;
        }

        public List<NhanVien> DanhSachNhanVien()
        {
            List<NhanVien> danhSach = new List<NhanVien>();
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var sql = "SELECT * FROM nhanvien ORDER BY MaNV DESC";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        NhanVien nv = new NhanVien
                        {
                            MaNV = Convert.ToInt32(reader["MaNV"]),
                            TenNV = reader["TenNV"].ToString(),
                            NgayVL = Convert.ToDateTime(reader["NgayVL"]),
                            Luong = Convert.ToInt32(reader["Luong"]),
                            SDT = reader["SDT"].ToString(),
                            Email = reader["Email"].ToString(),
                            CMND = reader["CMND"].ToString(),
                            DiaChi = reader["DiaChi"].ToString(),
                            MatKhau = reader["MatKhau"].ToString(),
                            LoaiNV = reader["LoaiNV"].ToString()
                        };
                        danhSach.Add(nv);
                    }
                }
            }
            return danhSach;
        }

        public NhanVien GetNhanVien(int maNV)
        {
            NhanVien nv = new NhanVien();
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var sql = "SELECT * FROM nhanvien WHERE MaNV = @maNV";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("maNV", maNV);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        nv.MaNV = Convert.ToInt32(reader["MaNV"]);
                        nv.TenNV = reader["TenNV"].ToString();
                        nv.NgayVL = Convert.ToDateTime(reader["NgayVL"]);
                        nv.Luong = Convert.ToInt32(reader["Luong"]);
                        nv.SDT = reader["SDT"].ToString();
                        nv.Email = reader["Email"].ToString();
                        nv.CMND = reader["CMND"].ToString();
                        nv.DiaChi = reader["DiaChi"].ToString();
                        nv.MatKhau = reader["MatKhau"].ToString();
                        nv.LoaiNV = reader["LoaiNV"].ToString();
                    }
                }
            }
            return nv;
        }

        public int ThemNhanVien(NhanVien nv)
        {
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var sql = "INSERT INTO nhanvien(TenNV, NgayVL, Luong, SDT, Email, MatKhau, CMND, DiaChi, LoaiNV) " +
                          "VALUES(@tenNV, @ngayVL, @luong, @sdt, @email, @matKhau, @cmnd, @diaChi, @loaiNV); SELECT SCOPE_IDENTITY();";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("tenNV", nv.TenNV);
                cmd.Parameters.AddWithValue("ngayVL", nv.NgayVL);
                cmd.Parameters.AddWithValue("luong", nv.Luong);
                cmd.Parameters.AddWithValue("sdt", nv.SDT);
                cmd.Parameters.AddWithValue("email", nv.Email);
                cmd.Parameters.AddWithValue("matKhau", nv.MatKhau);
                cmd.Parameters.AddWithValue("cmnd", nv.CMND);
                cmd.Parameters.AddWithValue("diaChi", nv.DiaChi);
                cmd.Parameters.AddWithValue("loaiNV", nv.LoaiNV);

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int CapNhatNhanVien(NhanVien nv)
        {
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var sql = "UPDATE nhanvien SET TenNV = @tenNV, NgayVL = @ngayVL, Luong = @luong, SDT = @sdt, " +
                          "Email = @email, CMND = @cmnd, DiaChi = @diaChi, LoaiNV = @loaiNV WHERE MaNV = @maNV";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("tenNV", nv.TenNV);
                cmd.Parameters.AddWithValue("ngayVL", nv.NgayVL);
                cmd.Parameters.AddWithValue("luong", nv.Luong);
                cmd.Parameters.AddWithValue("sdt", nv.SDT);
                cmd.Parameters.AddWithValue("email", nv.Email);
                cmd.Parameters.AddWithValue("cmnd", nv.CMND);
                cmd.Parameters.AddWithValue("diaChi", nv.DiaChi);
                cmd.Parameters.AddWithValue("loaiNV", nv.LoaiNV);
                cmd.Parameters.AddWithValue("maNV", nv.MaNV);

                return cmd.ExecuteNonQuery();
            }
        }

        public int CapNhatMatKhau(int maNV, string matKhauMoi)
        {
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var sql = "UPDATE nhanvien SET MatKhau = @matKhau WHERE MaNV = @maNV";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("matKhau", matKhauMoi);
                cmd.Parameters.AddWithValue("maNV", maNV);

                return cmd.ExecuteNonQuery();
            }
        }

        public int XoaNhanVien(int maNV)
        {
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var sql = "DELETE FROM nhanvien WHERE MaNV = @maNV";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("maNV", maNV);

                return cmd.ExecuteNonQuery();
            }
        }

        public bool KiemTraEmail(string email)
        {
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var sql = "SELECT COUNT(*) FROM nhanvien WHERE Email = @email";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("email", email);

                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        public List<NhanVien> TimKiemNhanVien(string tuKhoa)
        {
            List<NhanVien> danhSach = new List<NhanVien>();
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var sql = "SELECT * FROM nhanvien WHERE TenNV LIKE @tuKhoa OR Email LIKE @tuKhoa OR SDT LIKE @tuKhoa ORDER BY MaNV DESC";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("tuKhoa", "%" + tuKhoa + "%");

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        NhanVien nv = new NhanVien
                        {
                            MaNV = Convert.ToInt32(reader["MaNV"]),
                            TenNV = reader["TenNV"].ToString(),
                            NgayVL = Convert.ToDateTime(reader["NgayVL"]),
                            Luong = Convert.ToInt32(reader["Luong"]),
                            SDT = reader["SDT"].ToString(),
                            Email = reader["Email"].ToString(),
                            CMND = reader["CMND"].ToString(),
                            DiaChi = reader["DiaChi"].ToString(),
                            MatKhau = reader["MatKhau"].ToString(),
                            LoaiNV = reader["LoaiNV"].ToString()
                        };
                        danhSach.Add(nv);
                    }
                }
            }
            return danhSach;
        }
    }
}
