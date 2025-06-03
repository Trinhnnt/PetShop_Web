using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient; // QUAN TRỌNG: Thêm dòng này

namespace DoAn_FW.Models
{
    public class ThongTinSP
    {
        // Các thuộc tính bạn đã định nghĩa
        public virtual int MaTTSP { get; set; }
        public virtual string TenSP { get; set; }
        public virtual string HinhAnh { get; set; }
        public virtual int MaLoaiSP { get; set; }
        public virtual int MaTH { get; set; }
        public virtual int Gia { get; set; } // Đây là giá gốc
        public virtual int GiaKM { get; set; } // Giá khuyến mãi riêng của sản phẩm (nếu có)
        public virtual int SoLuong { get; set; }
        public virtual string MauSac { get; set; }
        public virtual float KhoiLuong { get; set; } // Nên cân nhắc dùng decimal cho các giá trị tiền tệ và float/double cho khối lượng
        public virtual string DoTuoi { get; set; }
        public virtual string XuatXu { get; set; }
        public virtual string KichThuoc { get; set; }
        public virtual string ThanhPhan { get; set; }
        public virtual string CongDung { get; set; }
        public virtual string HuongDanSD { get; set; }

        // Thêm TenLoaiSP và TenTH nếu bạn muốn lấy chúng khi truy vấn ThongTinSP
        // Tuy nhiên, trong SQL gốc của bảng thongtinsp đã có 2 cột này rồi
        // public string TenLoaiSP { get; set; } // (Lấy từ join với bảng loaisp)
        // public string TenTH { get; set; } // (Lấy từ join với bảng thuonghieu)


        // Thêm ConnectionString và GetConnection tương tự các model khác
        public string ConnectionString { get; set; }

        public ThongTinSP()
        {
            // Chuỗi kết nối của bạn
            ConnectionString = "Data Source=localhost;Initial Catalog=website_PetShop;Integrated Security=True;TrustServerCertificate=True";
        }

        // Constructor với tham số connection string (nếu cần)
        public ThongTinSP(string cs)
        {
            ConnectionString = cs;
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        // Phương thức để lấy thông tin một sản phẩm dựa trên MaTTSP
        public ThongTinSP GetThongTinSPByMa(int maTTSP)
        {
            ThongTinSP sp = null;
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                // Lấy tất cả các cột cần thiết, bao gồm Gia (giá gốc)
                var sql = "SELECT MaTTSP, TenSP, HinhAnh, MaLoaiSP, MaTH, TenLoaiSP, TenTH, Gia, GiaKM, SoLuong, MauSac, KhoiLuong, DoTuoi, XuatXu, KichThuoc, ThanhPhan, CongDung, HuongDanSD FROM thongtinsp WHERE MaTTSP = @maTTSP";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@maTTSP", maTTSP);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        sp = new ThongTinSP()
                        {
                            MaTTSP = Convert.ToInt32(reader["MaTTSP"]),
                            TenSP = reader["TenSP"].ToString(),
                            HinhAnh = reader["HinhAnh"].ToString(),
                            MaLoaiSP = Convert.ToInt32(reader["MaLoaiSP"]),
                            MaTH = Convert.ToInt32(reader["MaTH"]),
                            // TenLoaiSP = reader["TenLoaiSP"].ToString(), // Lấy từ cột đã có sẵn trong bảng
                            // TenTH = reader["TenTH"].ToString(),       // Lấy từ cột đã có sẵn trong bảng
                            Gia = Convert.ToInt32(reader["Gia"]), // Giá gốc
                            GiaKM = Convert.ToInt32(reader["GiaKM"]),
                            SoLuong = Convert.ToInt32(reader["SoLuong"]),
                            MauSac = reader["MauSac"] != DBNull.Value ? reader["MauSac"].ToString() : null,
                            KhoiLuong = reader["KhoiLuong"] != DBNull.Value ? Convert.ToSingle(reader["KhoiLuong"]) : 0,
                            DoTuoi = reader["DoTuoi"] != DBNull.Value ? reader["DoTuoi"].ToString() : null,
                            XuatXu = reader["XuatXu"] != DBNull.Value ? reader["XuatXu"].ToString() : null,
                            KichThuoc = reader["KichThuoc"] != DBNull.Value ? reader["KichThuoc"].ToString() : null,
                            ThanhPhan = reader["ThanhPhan"] != DBNull.Value ? reader["ThanhPhan"].ToString() : null,
                            CongDung = reader["CongDung"] != DBNull.Value ? reader["CongDung"].ToString() : null,
                            HuongDanSD = reader["HuongDanSD"] != DBNull.Value ? reader["HuongDanSD"].ToString() : null
                            // Lưu ý: SQL của bạn cho bảng thongtinsp có cột TenLoaiSP và TenTH. 
                            // Nếu bạn muốn lấy trực tiếp từ đó thì không cần join.
                            // Nếu các cột này không tự cập nhật mà bạn muốn lấy tên từ bảng loaisp và thuonghieu thì cần JOIN.
                            // Hiện tại, tôi giả định bạn lấy từ các cột TenLoaiSP, TenTH đã có trong bảng thongtinsp.
                        };
                    }
                }
            }
            return sp;
        }

        // Bạn có thể thêm các phương thức khác để lấy danh sách sản phẩm, cập nhật, v.v... nếu cần
        // Ví dụ:
        public List<ThongTinSP> GetDanhSachSanPham()
        {
            List<ThongTinSP> danhSach = new List<ThongTinSP>();
            // Viết code truy vấn để lấy tất cả sản phẩm
            return danhSach;
        }
    }
}