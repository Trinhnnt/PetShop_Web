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
            ConnectionString = @"Data Source=QUYNHNHULEE\SQLEXPRESS;Initial Catalog=website_PetShop;Integrated Security=True;Encrypt=False";
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

        // Lấy danh sách các sản phẩm đã mua dựa trên MaKH
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

        // Lấy chi tiết các sản phẩm đã mua (chỉ hóa đơn đã duyệt và đã thanh toán)
        public List<object> ListCTSPMua(int makh)
        {
            List<object> list = new List<object>();
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var sql = @"
                    SELECT dsp.MaHD, t.MaTTSP, t.MaLoaiSP, t.TenSP, t.HinhAnh, t.MauSac, t.DoTuoi, t.XuatXu, dsp.SoLuong, dsp.ThanhTien, t.Gia, t.GiaKM 
                    FROM dssanpham_mua dsp 
                    JOIN thongtinsp t ON dsp.MaTTSP = t.MaTTSP 
                    JOIN hoadon hd ON dsp.MaHD = hd.MaHD
                    WHERE dsp.MaKH = @makh AND hd.TinhTrangHD = 1 AND hd.TinhTrangTT = 1";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("makh", makh);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new
                        {
                            MaHD = int.Parse(reader["MaHD"].ToString()),
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

        // Thêm hoặc cộng dồn sản phẩm vào DSSP_Mua
        public int InsertOrUpdateDSSPMua(DSSP_Mua dspmua)
        {
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                // Kiểm tra đã có sản phẩm này trong hóa đơn chưa
                var checkSql = @"SELECT SoLuong, ThanhTien FROM dssanpham_mua WHERE MaHD = @mahd AND MaTTSP = @mattsp";
                var checkCmd = new SqlCommand(checkSql, conn);
                checkCmd.Parameters.AddWithValue("@mahd", dspmua.MaHD);
                checkCmd.Parameters.AddWithValue("@mattsp", dspmua.MaTTSP);

                using (var reader = checkCmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Đã có, cộng dồn số lượng và thành tiền
                        int oldSoLuong = int.Parse(reader["SoLuong"].ToString());
                        int oldThanhTien = int.Parse(reader["ThanhTien"].ToString());
                        reader.Close();

                        var updateSql = @"UPDATE dssanpham_mua SET SoLuong = @soluong, ThanhTien = @thanhtien WHERE MaHD = @mahd AND MaTTSP = @mattsp";
                        var updateCmd = new SqlCommand(updateSql, conn);
                        updateCmd.Parameters.AddWithValue("@soluong", oldSoLuong + dspmua.SoLuong);
                        updateCmd.Parameters.AddWithValue("@thanhtien", oldThanhTien + dspmua.ThanhTien);
                        updateCmd.Parameters.AddWithValue("@mahd", dspmua.MaHD);
                        updateCmd.Parameters.AddWithValue("@mattsp", dspmua.MaTTSP);

                        return updateCmd.ExecuteNonQuery();
                    }
                }

                // Nếu chưa có thì thêm mới
                var insertSql = @"INSERT INTO dssanpham_mua (MaHD, MaTTSP, SoLuong, ThanhTien, MaKH)
                                  VALUES (@mahd, @mattsp, @soluong, @thanhtien, @makh)";
                var insertCmd = new SqlCommand(insertSql, conn);
                insertCmd.Parameters.AddWithValue("@mahd", dspmua.MaHD);
                insertCmd.Parameters.AddWithValue("@mattsp", dspmua.MaTTSP);
                insertCmd.Parameters.AddWithValue("@soluong", dspmua.SoLuong);
                insertCmd.Parameters.AddWithValue("@thanhtien", dspmua.ThanhTien);
                insertCmd.Parameters.AddWithValue("@makh", dspmua.MaKH);

                return insertCmd.ExecuteNonQuery();
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