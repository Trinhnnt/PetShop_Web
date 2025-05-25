using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace DoAn_FW.Models
{
    public class DSSP_Mua
    {
        public string ConnectionString { get; set; }

        public DSSP_Mua()
        {
            ConnectionString = "Data Source=localhost;Initial Catalog=website_petShop;Integrated Security=True";
        }

        public DSSP_Mua(string cs)
        {
            ConnectionString = cs;
        }

        private Microsoft.Data.SqlClient.SqlConnection GetConnection()
        {
            return new Microsoft.Data.SqlClient.SqlConnection(ConnectionString);
        }

        public int MaSPM { get; set; }
        public int MaHD { get; set; }
        public int MaKH { get; set; }
        public int MaTTSP { get; set; }
        public int SoLuong { get; set; }
        public int ThanhTien { get; set; }
        public SanPham SanPham { get; set; }

        // Lấy danh sách các sản phẩm đã mua dựa trên MaHD
        public List<DSSP_Mua> ListDSSPMua(int makh)
        {
            List<DSSP_Mua> list = new List<DSSP_Mua>();
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var sql = "SELECT * FROM dssanpham_mua WHERE MaKH = @makh";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("makh", makh);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new DSSP_Mua()
                        {
                            MaSPM = int.Parse(reader["MaSPM"].ToString()),
                            MaHD = int.Parse(reader["MaHD"].ToString()),
                            MaKH = int.Parse(reader["MaKH"].ToString()),
                            MaTTSP = int.Parse(reader["MaTTSP"].ToString()),
                            SoLuong = int.Parse(reader["SoLuong"].ToString()),
                            ThanhTien = int.Parse(reader["ThanhTien"].ToString())
                        });
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return list;
        }

        public List<object> ListCTSPMua(int makh)
        {
            List<object> list = new List<object>();
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var sql = @"SELECT t.MaTTSP, t.MaLoaiSP, t.TenSP, t.HinhAnh, t.MauSac, t.DoTuoi, t.XuatXu, dsp.SoLuong, dsp.ThanhTien, t.Gia, t.GiaKM 
                    FROM dssanpham_mua dsp 
                    JOIN thongtinsp t ON dsp.MaTTSP = t.MaTTSP 
                    WHERE dsp.MaKH = @makh";
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
                            TenSP = reader["TenSP"].ToString(),
                            HinhAnh = reader["HinhAnh"].ToString(),
                            MauSac = reader["MauSac"].ToString(),
                            DoTuoi = reader["DoTuoi"].ToString(),
                            XuatXu = reader["XuatXu"].ToString(),
                            SoLuong = int.Parse(reader["SoLuong"].ToString()),
                            ThanhTien = int.Parse(reader["ThanhTien"].ToString()),
                            Gia = long.Parse(reader["Gia"].ToString()),
                            GiaKM = long.Parse(reader["GiaKM"].ToString())
                        });
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return list;
        }


        // Thêm sản phẩm vào DSSP_Mua
        public int InsertDSSPMua(DSSP_Mua dspmua)
        {
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    var sql = @"INSERT INTO dssanpham_mua (MaHD, MaTTSP, SoLuong, ThanhTien, MaKH) 
                        VALUES (@mahd, @mattsp, @soluong, @thanhtien, @makh)";

                    Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@mahd", dspmua.MaHD);
                    cmd.Parameters.AddWithValue("@mattsp", dspmua.MaTTSP);
                    cmd.Parameters.AddWithValue("@soluong", dspmua.SoLuong);
                    cmd.Parameters.AddWithValue("@thanhtien", dspmua.ThanhTien);

                    // Thêm tham số MaKH nếu có trong model DSSP_Mua
                    if (dspmua.GetType().GetProperty("MaKH") != null)
                    {
                        cmd.Parameters.AddWithValue("@makh", dspmua.MaKH);
                    }
                    else
                    {
                        // Nếu không có thuộc tính MaKH, sử dụng giá trị null hoặc mặc định
                        cmd.Parameters.AddWithValue("@makh", DBNull.Value);
                    }

                    return cmd.ExecuteNonQuery();
                }
                catch (Microsoft.Data.SqlClient.SqlException ex)
                {
                    // Xử lý ngoại lệ SQL
                    Console.WriteLine($"SQL Error: {ex.Message}");
                    throw;
                }
                catch (Exception ex)
                {
                    // Xử lý các ngoại lệ khác
                    Console.WriteLine($"Error: {ex.Message}");
                    throw;
                }
            }
        }


        // Cập nhật thông tin sản phẩm đã mua
        public int UpdateDSSPMua(DSSP_Mua dspmua)
        {
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    var sql = @"UPDATE dssanpham_mua 
                        SET SoLuong = @soluong, 
                            ThanhTien = @thanhtien 
                        WHERE MaHD = @mahd AND MaTTSP = @mattsp";

                    Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@mahd", dspmua.MaHD);
                    cmd.Parameters.AddWithValue("@mattsp", dspmua.MaTTSP);
                    cmd.Parameters.AddWithValue("@soluong", dspmua.SoLuong);
                    cmd.Parameters.AddWithValue("@thanhtien", dspmua.ThanhTien);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected;
                }
                catch (Microsoft.Data.SqlClient.SqlException ex)
                {
                    // Xử lý ngoại lệ SQL
                    Console.WriteLine($"SQL Error: {ex.Message}");
                    throw;
                }
                catch (Exception ex)
                {
                    // Xử lý các ngoại lệ khác
                    Console.WriteLine($"Error: {ex.Message}");
                    throw;
                }
            }
        }

    }
}