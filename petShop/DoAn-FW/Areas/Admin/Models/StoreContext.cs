using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoAn_FW.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace Web_projectframeword_admin.Models
{
    public class StoreContext
    {
        public string ConnectionString { get; set; }//biết thành viên 

        public StoreContext(string connectionString) //phuong thuc khoi tao
        {
            this.ConnectionString = connectionString;
        }

        private Microsoft.Data.SqlClient.SqlConnection GetConnection() //lấy connection 
        {
            return new Microsoft.Data.SqlClient.SqlConnection(ConnectionString);
        }

        //Hóa Đơn
        public (List<HoaDonViewModel> hd, int pages, int page) GetDSHD(int page)
        {
            List<HoaDonViewModel> list = new List<HoaDonViewModel>();
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "SELECT * FROM hoadon hd, khachhang kh WHERE hd.MaKH = kh.MaKH order by TinhTrangHD;";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new HoaDonViewModel()
                        {
                            MaHD = Convert.ToInt32(reader["MaHD"]),
                            MaKM = (reader["MaKM"] != DBNull.Value) ? Convert.ToInt32(reader["MaKM"]) : 0,
                            TenKH = reader["TenKH"].ToString(),
                            DiaChiGH = reader["DiaChiGH"].ToString(),
                            TongTienTT = Convert.ToInt32(reader["TongTienTT"]),
                            NgayLapHD = Convert.ToDateTime(reader["NgayLapHD"]),
                            TinhTrangTT = Convert.ToInt32(reader["TinhTrangTT"]),
                            TinhTrangHD = Convert.ToInt32(reader["TinhTrangHD"]),
                            SoTienNhan = Convert.ToInt32(reader["SoTienNhan"]),
                            SoTienTra = Convert.ToInt32(reader["SoTienTra"]),
                        });
                    }
                    reader.Close();
                }
                conn.Close();
            }
            int Size = 10;
            int pages = (int)Math.Ceiling((double)list.Count / Size);
            List<HoaDonViewModel> dshd = list.Skip((page - 1) * Size).Take(Size).ToList();
            return (dshd, pages, page);
        }


        public (List<HoaDonViewModel> hd, int pages, int page) GetDSCD(int page)
        {
            List<HoaDonViewModel> list = new List<HoaDonViewModel>();
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "SELECT hd.MaHD, hd.MaKM, TenKH, DiaChiGH, TongTienTT, NgayLapHD, TinhTrangTT, TinhTrangHD, SoTienNhan, SoTienTra" +
                    " FROM hoadon hd, khachhang kh WHERE hd.MaKH = kh.MaKH and TinhTrangHD = 0 order by TinhTrangHD;";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new HoaDonViewModel()
                        {
                            MaHD = Convert.ToInt32(reader["MaHD"]),
                            MaKM = (reader["MaKM"] != DBNull.Value) ? Convert.ToInt32(reader["MaKM"]) : 0,
                            TenKH = reader["TenKH"].ToString(),
                            DiaChiGH = reader["DiaChiGH"].ToString(),
                            TongTienTT = Convert.ToInt32(reader["TongTienTT"]),
                            NgayLapHD = Convert.ToDateTime(reader["NgayLapHD"]),
                            TinhTrangTT = Convert.ToInt32(reader["TinhTrangTT"]),
                            TinhTrangHD = Convert.ToInt32(reader["TinhTrangHD"]),
                            SoTienNhan = Convert.ToInt32(reader["SoTienNhan"]),
                            SoTienTra = Convert.ToInt32(reader["SoTienTra"]),
                        });
                    }
                    reader.Close();
                }
                conn.Close();
            }
            int Size = 10;
            int pages = (int)Math.Ceiling((double)list.Count / Size);
            List<HoaDonViewModel> dshd = list.Skip((page - 1) * Size).Take(Size).ToList();
            return (dshd, pages, page);
        }

        public int ThemHD(HoaDonEditModel hd)
        {
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "INSERT INTO Hoadon(MaKH, MaKM, DiaChiGH, TongTienTT, NgayLapHD, TinhTrangHD, TinhTrangTT, SoTienNhan, SoTienTra) VALUES (@makh, @makm, @diachigh, @tongtientt, @ngaylaphd, @tinhtranghd, @tinhtrangtt, @sotiennhan, @sotientra)";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(str, conn);
                cmd.Parameters.AddWithValue("@makh", hd.MaKH);
                cmd.Parameters.AddWithValue("@makm", (object)hd.MaKM ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@diachigh", (object)hd.DiaChiGH ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@tongtientt", hd.TongTienTT);
                cmd.Parameters.AddWithValue("@ngaylaphd", hd.NgayLapHD);
                cmd.Parameters.AddWithValue("@tinhtranghd", hd.TinhTrangHD);
                cmd.Parameters.AddWithValue("@tinhtrangtt", hd.TinhTrangTT);
                cmd.Parameters.AddWithValue("@sotiennhan", (object)hd.SoTienNhan ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@sotientra", (object)hd.SoTienTra ?? DBNull.Value);
                return (cmd.ExecuteNonQuery());
            }
        }


        public HoaDonEditModel GetHD(int id)
        {
            HoaDonEditModel HD = new HoaDonEditModel();
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "SELECT * FROM HoaDon WHERE MaHD = @mahd";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(str, conn);
                cmd.Parameters.AddWithValue("@mahd", id);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        HD.MaHD = Convert.ToInt32(reader["MaHD"]);
                        HD.MaKH = Convert.ToInt32(reader["MaKH"]);
                        HD.MaKM = (reader["MaKM"] != DBNull.Value) ? Convert.ToInt32(reader["MaKM"]) : 0;
                        HD.NgayLapHD = Convert.ToDateTime(reader["NgayLapHD"]);
                        HD.SoTienNhan = (reader["SoTienNhan"] != DBNull.Value) ? Convert.ToInt32(reader["SoTienNhan"]) : 0;
                        HD.SoTienTra = (reader["SoTienTra"] != DBNull.Value) ? Convert.ToInt32(reader["SoTienTra"]) : 0;
                        HD.TinhTrangHD = Convert.ToInt32(reader["TinhTrangHD"]);
                        HD.TinhTrangTT = (reader["TinhTrangTT"] != DBNull.Value) ? Convert.ToInt32(reader["TinhTrangTT"]) : 0;
                        HD.TongTienTT = Convert.ToInt32(reader["TongTienTT"]);
                        HD.DiaChiGH = reader["DiaChiGH"]?.ToString() ?? "";
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return HD;
        }

        public int UpdateHD(HoaDonEditModel hd)
        {
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                if (hd.MaKM == 0)
                {
                    var str = "UPDATE HoaDon " +
                    "SET MaKH=@makh, MaKM=NULL, DiaChiGH=@diachigh, TongTienTT=@tongtientt, NgayLapHD=@ngaylaphd, TinhTrangHD=@tinhtranghd, TinhTrangTT=@tinhtrangtt, SoTienNhan=@sotiennhan, SoTienTra=@sotientra " +
                    "WHERE MaHD=@mahd";
                    Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(str, conn);
                    cmd.Parameters.AddWithValue("@makh", hd.MaKH);
                    cmd.Parameters.AddWithValue("@diachigh", (object)hd.DiaChiGH ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@tongtientt", hd.TongTienTT);
                    cmd.Parameters.AddWithValue("@ngaylaphd", hd.NgayLapHD);
                    cmd.Parameters.AddWithValue("@tinhtranghd", hd.TinhTrangHD);
                    cmd.Parameters.AddWithValue("@tinhtrangtt", (object)hd.TinhTrangTT ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@sotiennhan", (object)hd.SoTienNhan ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@sotientra", (object)hd.SoTienTra ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@mahd", hd.MaHD);
                    return (cmd.ExecuteNonQuery());
                }
                else
                {
                    var str = "UPDATE HoaDon " +
                    "SET MaKH=@makh, MaKM=@makm, DiaChiGH=@diachigh, TongTienTT=@tongtientt, NgayLapHD=@ngaylaphd, TinhTrangHD=@tinhtranghd, TinhTrangTT=@tinhtrangtt, SoTienNhan=@sotiennhan, SoTienTra=@sotientra " +
                    "WHERE MaHD=@mahd";
                    Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(str, conn);
                    cmd.Parameters.AddWithValue("@makh", hd.MaKH);
                    cmd.Parameters.AddWithValue("@makm", hd.MaKM);
                    cmd.Parameters.AddWithValue("@diachigh", (object)hd.DiaChiGH ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@tongtientt", hd.TongTienTT);
                    cmd.Parameters.AddWithValue("@ngaylaphd", hd.NgayLapHD);
                    cmd.Parameters.AddWithValue("@tinhtranghd", hd.TinhTrangHD);
                    cmd.Parameters.AddWithValue("@tinhtrangtt", (object)hd.TinhTrangTT ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@sotiennhan", (object)hd.SoTienNhan ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@sotientra", (object)hd.SoTienTra ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@mahd", hd.MaHD);
                    return (cmd.ExecuteNonQuery());
                }
            }
        }

        public List<CTHD> GetDSCTHD(int MAHD)
        {
            List<CTHD> list = new List<CTHD>();
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "select MAHD, CTHD.MATTSP, cthd.SOLUONG, TENSP, THANHTIEN from cthd join thongtinsp sp on cthd.mattsp = sp.mattsp where MAHD = @mahd";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(str, conn);
                cmd.Parameters.AddWithValue("mahd", MAHD);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new CTHD()
                        {
                            MaHD = Convert.ToInt32(reader["MAHD"]),
                            MaSP = Convert.ToInt32(reader["MATTSP"]),
                            SoLuong = Convert.ToInt32(reader["SOLUONG"]),
                            ThanhTien = Convert.ToInt32(reader["THANHTIEN"]),
                            TenSP = reader["TENSP"].ToString()
                        });
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return list;
        }

        public int InsertCTHD(CTHD ct)
        {
            List<int> list = new List<int>();
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "select mattsp from cthd where mahd = @mahd";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(str, conn);
                cmd.Parameters.AddWithValue("mahd", ct.MaHD);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(Convert.ToInt32(reader["mattsp"]));
                    }
                    reader.Close();
                }
                if(list.Contains(ct.MaSP))
                {
                    var sql = "update cthd set soluong = @sl where mahd = @mahd and mattsp = @masp";
                    Microsoft.Data.SqlClient.SqlCommand cmd1 = new Microsoft.Data.SqlClient.SqlCommand(sql, conn);
                    cmd1.Parameters.AddWithValue("sl", ct.SoLuong);
                    cmd1.Parameters.AddWithValue("mahd", ct.MaHD);
                    cmd1.Parameters.AddWithValue("masp", ct.MaSP);
                    return (cmd1.ExecuteNonQuery());
                }
                else
                {
                    var sql = "insert into cthd(mahd, mattsp, soluong) values(@mahd, @mattsp, @sl)";
                    Microsoft.Data.SqlClient.SqlCommand cmd1 = new Microsoft.Data.SqlClient.SqlCommand(sql, conn);
                    cmd1.Parameters.AddWithValue("sl", ct.SoLuong);
                    cmd1.Parameters.AddWithValue("mahd", ct.MaHD);
                    cmd1.Parameters.AddWithValue("mattsp", ct.MaSP);
                    return (cmd1.ExecuteNonQuery());
                }
            }
        }

        public int HuyHD(int mahd)
        {
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "update hoadon set tinhtranghd = -1 where mahd = @ma";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(str, conn);
                cmd.Parameters.AddWithValue("ma", mahd);
                return (cmd.ExecuteNonQuery());


            }
        }

        public int DuyetHD(int mahd)
        {
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var manv = 0;
                var sql = "select manv from nhanvien where LoaiNV ='Giao hàng'";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    manv = Convert.ToInt32(reader["manv"]);
                }
                var sql1 = "insert into giaohang values(@mahd, @manv, '0')";
                Microsoft.Data.SqlClient.SqlCommand cmd1 = new Microsoft.Data.SqlClient.SqlCommand(sql1, conn);
                cmd1.Parameters.AddWithValue("mahd", mahd);
                cmd1.Parameters.AddWithValue("manv", manv);
                cmd1.ExecuteNonQuery();
                var str = "update hoadon set tinhtranghd = 1 where mahd = @ma";
                Microsoft.Data.SqlClient.SqlCommand cmd2 = new Microsoft.Data.SqlClient.SqlCommand(str, conn);
                cmd2.Parameters.AddWithValue("ma", mahd);
                return cmd2.ExecuteNonQuery();
            }
        }

        public int DeleteHD(int mahd)
        {
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "delete from giaohang where MAHD = @mahd;" +
                    "delete from cthd where MAHD = @mahd;" +
                    "delete from hoadon where MAHD = @mahd;";
                Microsoft.Data.SqlClient.SqlCommand cmd1 = new Microsoft.Data.SqlClient.SqlCommand(str, conn);
                cmd1.Parameters.AddWithValue("mahd", mahd);
                return (cmd1.ExecuteNonQuery());
            }
        }

        public int DeleteCTHD(int mahd, int masp)
        {
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "delete from cthd where MAHD = @mahd and MATTSP = @masp;";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(str, conn);
                cmd.Parameters.AddWithValue("mahd", mahd);
                cmd.Parameters.AddWithValue("masp", masp);
                return (cmd.ExecuteNonQuery());
            }
        }

        //Giao hàng
        public (List<GiaoHang> gh, int pages, int page) GetDSGH(int page)
        {
            List<GiaoHang> list = new List<GiaoHang>();
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "SELECT gh.MaHD, hd.DiaChiGH, gh.MaNV, nv.TenNV, nv.SDT, gh.TinhTrangGH " +
                             "FROM hoadon hd " +
                             "JOIN giaohang gh ON hd.MaHD = gh.MaHD " +
                             "JOIN nhanvien nv ON gh.MaNV = nv.MaNV " +
                             "ORDER BY gh.TinhTrangGH;";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new GiaoHang()
                        {
                            MaHD = Convert.ToInt32(reader["MaHD"]),
                            DiaChiGH = reader["DiaChiGH"]?.ToString() ?? "",
                            MaNV = reader["MaNV"] != DBNull.Value ? Convert.ToInt32(reader["MaNV"]) : 0,
                            TenNV = reader["TenNV"]?.ToString() ?? "",
                            SDT = reader["SDT"]?.ToString() ?? "",
                            TinhTrangGH = Convert.ToInt32(reader["TinhTrangGH"])
                        });
                    }
                    reader.Close();
                }
                conn.Close();
            }
            int Size = 10;
            int pages = (int)Math.Ceiling((double)list.Count / Size);
            List<GiaoHang> dsgh = list.Skip((page - 1) * Size).Take(Size).ToList();
            return (dsgh, pages, page);
        }


        public int HoanThanhGH(int MAHD, int MANV, int stn)
        {
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                // Sử dụng transaction để đảm bảo tính nhất quán dữ liệu
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // Cập nhật số tiền nhận trong hóa đơn
                        var strHoaDon = "UPDATE hoadon SET SoTienNhan = @stn WHERE MaHD = @mahd;";
                        Microsoft.Data.SqlClient.SqlCommand cmdHoaDon = new Microsoft.Data.SqlClient.SqlCommand(strHoaDon, conn, transaction);
                        cmdHoaDon.Parameters.AddWithValue("@stn", stn);
                        cmdHoaDon.Parameters.AddWithValue("@mahd", MAHD);
                        cmdHoaDon.ExecuteNonQuery();

                        // Cập nhật tình trạng giao hàng
                        var strGiaoHang = "UPDATE giaohang SET TinhTrangGH = 1 WHERE MaHD = @mahd AND MaNV = @manv;";
                        Microsoft.Data.SqlClient.SqlCommand cmdGiaoHang = new Microsoft.Data.SqlClient.SqlCommand(strGiaoHang, conn, transaction);
                        cmdGiaoHang.Parameters.AddWithValue("@mahd", MAHD);
                        cmdGiaoHang.Parameters.AddWithValue("@manv", MANV);
                        int result = cmdGiaoHang.ExecuteNonQuery();

                        transaction.Commit();
                        return result;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public int DeleteGH(int mahd, int manv)
        {
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "DELETE FROM giaohang WHERE MaNV = @manv AND MaHD = @mahd";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(str, conn);
                cmd.Parameters.AddWithValue("@mahd", mahd);
                cmd.Parameters.AddWithValue("@manv", manv);
                return cmd.ExecuteNonQuery();
            }
        }

        public int HDChuaDuyet()
        {
            int sohd = 0;
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "SELECT COUNT(*) AS sohd FROM hoadon WHERE TinhTrangHD = 0";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        sohd = Convert.ToInt32(reader["sohd"]);
                    }
                }
            }
            return sohd;
        }

        public int GHChuaGiao()
        {
            int sogh = 0;
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "SELECT COUNT(*) AS sogh FROM giaohang WHERE TinhTrangGH = 0";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        sogh = Convert.ToInt32(reader["sogh"]);
                    }
                }
            }
            return sogh;
        }


        //Khách hàng
        public List<KhachHang> GetDSKH()
        {
            List<KhachHang> DSKH = new List<KhachHang>();
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "SELECT * FROM khachhang";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DSKH.Add(new KhachHang()
                        {
                            MaKH = Convert.ToInt32(reader["MaKH"]),
                            TenKH = reader["TenKH"].ToString(),
                            GioiTinh = reader["GioiTinh"].ToString(),
                            SDT = reader["SDT"].ToString(),
                            Email = reader["Email"].ToString(),
                            MatKhau = reader["MatKhau"].ToString(),
                            CMND = reader["CMND"].ToString(),
                            DiaChi = reader["DiaChi"].ToString(),
                            LoaiKH = reader["LoaiKH"].ToString(),
                        });
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return (DSKH);
        }

        public KhachHang GetKH(int id)
        {
            KhachHang KH = new KhachHang();
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "SELECT * FROM khachhang WHERE MaKH = @makh";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(str, conn);
                cmd.Parameters.AddWithValue("makh", id);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        KH.MaKH = Convert.ToInt32(reader["MaKH"]);
                        KH.TenKH = reader["TenKH"].ToString();
                        KH.SDT = reader["SDT"].ToString();
                        KH.Email = reader["Email"].ToString();
                        KH.GioiTinh = reader["GioiTinh"].ToString();
                        KH.CMND = reader["CMND"].ToString();
                        KH.DiaChi = reader["DiaChi"].ToString();
                        KH.MatKhau = reader["MatKhau"].ToString();
                        KH.LoaiKH = reader["LoaiKH"].ToString();
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return KH;
        }

        public (List<KhachHang> kh, int pages, int page) GetDSKH(int page)
        {
            List<KhachHang> DSKH = new List<KhachHang>();
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "SELECT * FROM khachhang";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DSKH.Add(new KhachHang()
                        {
                            MaKH = Convert.ToInt32(reader["MaKH"]),
                            TenKH = reader["TenKH"].ToString(),
                            GioiTinh = reader["GioiTinh"].ToString(),
                            SDT = reader["SDT"].ToString(),
                            Email = reader["Email"].ToString(),
                            MatKhau = reader["MatKhau"].ToString(),
                            CMND = reader["CMND"].ToString(),
                            DiaChi = reader["DiaChi"].ToString(),
                            LoaiKH = reader["LoaiKH"].ToString(),
                        });
                    }
                    reader.Close();
                }
                conn.Close();
            }
            int Size = 10;
            int pages = (int)Math.Ceiling((double)DSKH.Count / Size);
            List<KhachHang> dskh = DSKH.Skip((page - 1) * Size).Take(Size).ToList();
            return (dskh, pages, page);
        }

        public int DeleteKH(int id)
        {
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "delete from khachhang where MaKH=@makh";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(str, conn);
                cmd.Parameters.AddWithValue("makh", id);
                return (cmd.ExecuteNonQuery());
            }
        }

        public (List<NhanVien> nv, int pages, int page) GetDSNV(int page)
        {
            List<NhanVien> DSNV = new List<NhanVien>();
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "SELECT * FROM nhanvien";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DSNV.Add(new NhanVien()
                        {
                            MaNV = Convert.ToInt32(reader["MaNV"]),
                            TenNV = reader["TenNV"].ToString(),
                            NgayVL = Convert.ToDateTime(reader["NgayVL"]),
                            Luong = Convert.ToInt32(reader["Luong"]),
                            SDT = reader["SDT"].ToString(),
                            Email = reader["Email"].ToString(),
                            MatKhau = reader["MatKhau"].ToString(),
                            CMND = reader["CMND"].ToString(),
                            DiaChi = reader["DiaChi"].ToString(),
                            LoaiNV = reader["LoaiNV"].ToString(),
                        });
                    }
                    reader.Close();
                }
                conn.Close();
            }
            int Size = 10;
            int pages = (int)Math.Ceiling((double)DSNV.Count / Size);
            List<NhanVien> dsnv = DSNV.Skip((page - 1) * Size).Take(Size).ToList();
            return (dsnv, pages, page);
        }

        public List<SanPham> GetDSSP()
        {
            List<SanPham> DSSP = new List<SanPham>();
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "SELECT * FROM thongtinsp";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DSSP.Add(new SanPham()
                        {
                            MaTTSP = Convert.ToInt32(reader["MaTTSP"]),
                            TenSP = reader["TenSP"].ToString(),
                            HinhAnh = reader["HinhAnh"].ToString(),
                            MaLoaiSP = Convert.ToInt32(reader["MaLoaiSP"]),
                            MaTH = Convert.ToInt32(reader["MaTH"]),
                            Gia = Convert.ToInt32(reader["Gia"]),
                            GiaKM = Convert.ToInt32(reader["GiaKM"]),
                            SoLuong = reader["SoLuong"] != DBNull.Value ? Convert.ToInt32(reader["SoLuong"]) : 0,
                            MauSac = reader["MauSac"].ToString(),
                            KhoiLuong = reader["KhoiLuong"] != DBNull.Value ? Convert.ToSingle(reader["KhoiLuong"]) : 0.0f,
                            DoTuoi = reader["DoTuoi"].ToString(),
                            XuatXu = reader["XuatXu"].ToString(),
                            KichThuoc = reader["KichThuoc"].ToString(),
                            ThanhPhan = reader["ThanhPhan"].ToString(),
                            CongDung = reader["CongDung"].ToString(),
                            HuongDanSD = reader["HuongDanSD"].ToString()
                        });
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return DSSP;
        }


        //Nhân viên
        public int ThemNhanVien(NhanVien nv)
        {
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "INSERT INTO nhanvien (TenNV, NgayVL, Luong, SDT, Email, MatKhau, CMND, DiaChi, LoaiNV) " +
                          "VALUES (@tennv, @ngayvl, @luong, @sdt, @email, @matkhau, @cmnd, @diachi, @loainv)";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(str, conn);
                cmd.Parameters.AddWithValue("tennv", nv.TenNV);
                cmd.Parameters.AddWithValue("ngayvl", nv.NgayVL);
                cmd.Parameters.AddWithValue("luong", nv.Luong);
                cmd.Parameters.AddWithValue("sdt", nv.SDT);
                cmd.Parameters.AddWithValue("email", nv.Email);
                cmd.Parameters.AddWithValue("matkhau", "11111111");
                cmd.Parameters.AddWithValue("cmnd", nv.CMND);
                cmd.Parameters.AddWithValue("diachi", nv.DiaChi);
                cmd.Parameters.AddWithValue("loainv", nv.LoaiNV);
                return (cmd.ExecuteNonQuery());
            }
        }


        public NhanVien GetNV(int id)
        {
            NhanVien NV = new NhanVien();
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "SELECT * FROM nhanvien WHERE MaNV = @manv";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(str, conn);
                cmd.Parameters.AddWithValue("manv", id);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
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
                    reader.Close();
                }
                conn.Close();
            }
            return NV;
        }


        public int DeleteNV(int id)
        {
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "DELETE FROM giaohang WHERE MaNV=@manv;" +
                          "DELETE FROM phieunhap WHERE MaNV=@manv";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(str, conn);
                cmd.Parameters.AddWithValue("manv", id);
                cmd.ExecuteNonQuery();

                str = "DELETE FROM nhanvien WHERE MaNV=@manv";
                Microsoft.Data.SqlClient.SqlCommand cmd1 = new Microsoft.Data.SqlClient.SqlCommand(str, conn);
                cmd1.Parameters.AddWithValue("manv", id);
                return (cmd1.ExecuteNonQuery());
            }
        }


        public int UpdateNV(NhanVien nv)
        {
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "UPDATE nhanvien " +
                          "SET TenNV=@tennv, NgayVL=@ngayvl, Luong=@luong, SDT=@sdt, Email=@email, " +
                          "CMND=@cmnd, DiaChi=@diachi, LoaiNV=@loainv " +
                          "WHERE MaNV=@manv";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(str, conn);
                cmd.Parameters.AddWithValue("tennv", nv.TenNV);
                cmd.Parameters.AddWithValue("ngayvl", nv.NgayVL);
                cmd.Parameters.AddWithValue("luong", nv.Luong);
                cmd.Parameters.AddWithValue("sdt", nv.SDT);
                cmd.Parameters.AddWithValue("email", nv.Email);
                cmd.Parameters.AddWithValue("cmnd", nv.CMND);
                cmd.Parameters.AddWithValue("diachi", nv.DiaChi);
                cmd.Parameters.AddWithValue("loainv", nv.LoaiNV);
                cmd.Parameters.AddWithValue("manv", nv.MaNV);
                return (cmd.ExecuteNonQuery());
            }
        }


        ///Nhà cung cấp
        public List<NhaCungCap> GetNhaCungCaps()
        {
            List<NhaCungCap> list = new List<NhaCungCap>();
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "SELECT * FROM nhacc";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new NhaCungCap()
                        {
                            mancc = Convert.ToInt32(reader["MaNCC"]), 
                            tenncc = reader["TenNCC"].ToString(),
                            email = reader["Email"].ToString(),
                            sdt = reader["SDT"].ToString(),
                            diachi = reader["DiaChi"] == DBNull.Value ? null : reader["DiaChi"].ToString(), 
                            website = reader["Website"] == DBNull.Value ? null : reader["Website"].ToString(), 
                        });
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return list;
        }

        public int ThemNCC(NhaCungCap ncc)
        {
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                // Không chèn MaNCC vì đây là IDENTITY column
                var str = "INSERT INTO nhacc (TenNCC, Email, SDT, DiaChi, Website) " +
                          "VALUES (@TenNCC, @Email, @SDT, @DiaChi, @Website)";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(str, conn);
                // Không thêm MaNCC vì là IDENTITY
                cmd.Parameters.AddWithValue("TenNCC", ncc.tenncc);
                cmd.Parameters.AddWithValue("Email", ncc.email);
                cmd.Parameters.AddWithValue("SDT", ncc.sdt);
                cmd.Parameters.AddWithValue("DiaChi", ncc.diachi ?? (object)DBNull.Value); // Xử lý NULL
                cmd.Parameters.AddWithValue("Website", ncc.website ?? (object)DBNull.Value); // Xử lý NULL
                return (cmd.ExecuteNonQuery());
            }
        }

        public int XoaNCC(string id) // Thay đổi kiểu tham số từ string sang int
        {
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "DELETE FROM nhacc WHERE MaNCC = @MaNCC";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(str, conn);
                cmd.Parameters.AddWithValue("MaNCC", id);
                return (cmd.ExecuteNonQuery());
            }
        }

        public int SuaNCC(NhaCungCap ncc)
        {
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "UPDATE nhacc SET TenNCC = @TenNCC, Email = @Email, SDT = @SDT, " +
                          "DiaChi = @DiaChi, Website = @Website WHERE MaNCC = @MaNCC";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(str, conn);
                cmd.Parameters.AddWithValue("MaNCC", ncc.mancc);
                cmd.Parameters.AddWithValue("TenNCC", ncc.tenncc);
                cmd.Parameters.AddWithValue("Email", ncc.email);
                cmd.Parameters.AddWithValue("SDT", ncc.sdt);
                cmd.Parameters.AddWithValue("DiaChi", ncc.diachi ?? (object)DBNull.Value); // Xử lý NULL
                cmd.Parameters.AddWithValue("Website", ncc.website ?? (object)DBNull.Value); // Xử lý NULL
                return (cmd.ExecuteNonQuery());
            }
        }

        public NhaCungCap GetNhaCungCapTheoMa(int id)
        {
            NhaCungCap list = new NhaCungCap();
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "SELECT * FROM nhacc WHERE MaNCC = @id";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(str, conn);
                cmd.Parameters.AddWithValue("id", id);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.mancc = Convert.ToInt32(reader["MaNCC"]); // Chuyển đổi sang int
                        list.tenncc = reader["TenNCC"].ToString();
                        list.email = reader["Email"].ToString();
                        list.sdt = reader["SDT"].ToString();
                        list.diachi = reader["DiaChi"] == DBNull.Value ? null : reader["DiaChi"].ToString(); // Xử lý NULL
                        list.website = reader["Website"] == DBNull.Value ? null : reader["Website"].ToString(); // Xử lý NULL
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return list;
        }

        ///////Tin tức///////
        public List<TinTuc> GetTinTucs()
        {
            List<TinTuc> list = new List<TinTuc>();
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "SELECT * FROM tintuc";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new TinTuc()
                        {
                            matintuc = Convert.ToInt32(reader["MaTinTuc"]), // Chuyển đổi sang int
                            hinhbia = reader["HinhBia"] == DBNull.Value ? null : reader["HinhBia"].ToString(), // Xử lý NULL
                            tieude = reader["TieuDe"] == DBNull.Value ? null : reader["TieuDe"].ToString(), // Xử lý NULL
                            link = reader["Link"] == DBNull.Value ? null : reader["Link"].ToString(), // Xử lý NULL
                            noidung = reader["NoiDung"] == DBNull.Value ? null : reader["NoiDung"].ToString(), // Xử lý NULL
                            trangthai = Convert.ToInt32(reader["TrangThai"]),
                        });
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return list;
        }

        public TinTuc GetTinTucTheoMa(int id)
        {
            TinTuc list = new TinTuc();
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "SELECT * FROM tintuc WHERE MaTinTuc = @id";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(str, conn);
                cmd.Parameters.AddWithValue("id", id);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.matintuc = Convert.ToInt32(reader["MaTinTuc"]); // Chuyển đổi sang int
                        list.hinhbia = reader["HinhBia"] == DBNull.Value ? null : reader["HinhBia"].ToString(); // Xử lý NULL
                        list.tieude = reader["TieuDe"] == DBNull.Value ? null : reader["TieuDe"].ToString(); // Xử lý NULL
                        list.link = reader["Link"] == DBNull.Value ? null : reader["Link"].ToString(); // Xử lý NULL
                        list.noidung = reader["NoiDung"] == DBNull.Value ? null : reader["NoiDung"].ToString(); // Xử lý NULL
                        list.trangthai = Convert.ToInt32(reader["TrangThai"]);
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return list;
        }

        public int ThemTT(TinTuc tt)
        {
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                // Không chèn MaTinTuc vì đây là IDENTITY column
                var str = "INSERT INTO tintuc (HinhBia, TieuDe, NoiDung, Link, TrangThai) " +
                          "VALUES (@HinhBia, @TieuDe, @NoiDung, @Link, @TrangThai)";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(str, conn);
                // Không thêm MaTinTuc vì là IDENTITY
                cmd.Parameters.AddWithValue("HinhBia", tt.hinhbia ?? (object)DBNull.Value); // Xử lý NULL
                cmd.Parameters.AddWithValue("TieuDe", tt.tieude ?? (object)DBNull.Value); // Xử lý NULL
                cmd.Parameters.AddWithValue("NoiDung", tt.noidung ?? (object)DBNull.Value); // Xử lý NULL
                cmd.Parameters.AddWithValue("Link", tt.link ?? (object)DBNull.Value); // Xử lý NULL
                cmd.Parameters.AddWithValue("TrangThai", tt.trangthai);
                return (cmd.ExecuteNonQuery());
            }
        }

        public int XoaTT(string id) // Thay đổi kiểu tham số từ string sang int
        {
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "DELETE FROM tintuc WHERE MaTinTuc = @MaTinTuc";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(str, conn);
                cmd.Parameters.AddWithValue("MaTinTuc", id);
                return (cmd.ExecuteNonQuery());
            }
        }

        public int SuaTT(TinTuc tt)
        {
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "UPDATE tintuc SET HinhBia = @HinhBia, TieuDe = @TieuDe, Link = @Link, " +
                          "TrangThai = @TrangThai, NoiDung = @NoiDung WHERE MaTinTuc = @MaTinTuc";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(str, conn);
                cmd.Parameters.AddWithValue("MaTinTuc", tt.matintuc);
                cmd.Parameters.AddWithValue("HinhBia", tt.hinhbia ?? (object)DBNull.Value); // Xử lý NULL
                cmd.Parameters.AddWithValue("TieuDe", tt.tieude ?? (object)DBNull.Value); // Xử lý NULL
                cmd.Parameters.AddWithValue("NoiDung", tt.noidung ?? (object)DBNull.Value); // Xử lý NULL
                cmd.Parameters.AddWithValue("Link", tt.link ?? (object)DBNull.Value); // Xử lý NULL
                cmd.Parameters.AddWithValue("TrangThai", tt.trangthai);
                return (cmd.ExecuteNonQuery());
            }
        }




        //////Khuyến mãi

        public List<KhuyenMai> GetDSKM()
        {
            List<KhuyenMai> DSKM = new List<KhuyenMai>();
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "SELECT * FROM khuyenmai";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DSKM.Add(new KhuyenMai()
                        {
                            MaKM = Convert.ToInt32(reader["MaKM"]),
                            SoPTKM = Convert.ToByte(reader["SoPTKM"]), // Chuyển đổi sang tinyint (byte)
                            TuNgay = Convert.ToDateTime(reader["TuNgay"]),
                            DenNgay = Convert.ToDateTime(reader["DenNgay"]),
                            TTienToiThieu = reader["TTienToiThieu"] == DBNull.Value ?
                                null : (int?)Convert.ToInt32(reader["TTienToiThieu"]), // Xử lý NULL
                        });
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return (DSKM);
        }
        public List<KhuyenMai> GetKhuyenMais()
        {
            List<KhuyenMai> list = new List<KhuyenMai>();

            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "SELECT * FROM khuyenmai";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new KhuyenMai()
                        {
                            MaKM = Convert.ToInt32(reader["MaKM"]),
                            SoPTKM = Convert.ToByte(reader["SoPTKM"]), // Chuyển đổi sang tinyint (byte)
                            TuNgay = Convert.ToDateTime(reader["TuNgay"]),
                            DenNgay = Convert.ToDateTime(reader["DenNgay"]),
                            TTienToiThieu = reader["TTienToiThieu"] == DBNull.Value ?
                                null : (int?)Convert.ToInt32(reader["TTienToiThieu"]), // Xử lý NULL
                        });
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return list;
        }

        public int[] XoaKhuyenMai(string km)
        {
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();

                var queryKHUYENMAI = "DELETE FROM khuyenmai WHERE MaKM = @makm";
                Microsoft.Data.SqlClient.SqlCommand cmd1 = new Microsoft.Data.SqlClient.SqlCommand(queryKHUYENMAI, conn);
                cmd1.Parameters.AddWithValue("makm", km);
                int[] deleted = new int[1];
                deleted[0] = cmd1.ExecuteNonQuery();
                return deleted;
            }
        }

        public KhuyenMai ViewKhuyenMai(string Id) 
        {
            KhuyenMai km = new KhuyenMai();
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "SELECT * FROM khuyenmai WHERE MaKM = @makm";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(str, conn);
                cmd.Parameters.AddWithValue("makm", Id);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read()) // Kiểm tra xem có dữ liệu không
                    {
                        km.MaKM = Convert.ToInt32(reader["MaKM"]);
                        km.SoPTKM = Convert.ToByte(reader["SoPTKM"]); // Chuyển đổi sang tinyint (byte)
                        km.TuNgay = Convert.ToDateTime(reader["TuNgay"]);
                        km.DenNgay = Convert.ToDateTime(reader["DenNgay"]);
                        km.TTienToiThieu = reader["TTienToiThieu"] == DBNull.Value ?
                            null : (int?)Convert.ToInt32(reader["TTienToiThieu"]); // Xử lý NULL
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return km;
        }

        public int UpdateKhuyenMai(KhuyenMai km)
        {
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "UPDATE khuyenmai SET SoPTKM = @soptkm, TuNgay = @tungay, DenNgay = @denngay, " +
                          "TTienToiThieu = @ttientoithieu WHERE MaKM = @makm";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(str, conn);
                cmd.Parameters.AddWithValue("makm", km.MaKM);
                cmd.Parameters.AddWithValue("soptkm", km.SoPTKM);
                cmd.Parameters.AddWithValue("tungay", km.TuNgay);
                cmd.Parameters.AddWithValue("denngay", km.DenNgay);
                cmd.Parameters.AddWithValue("ttientoithieu", km.TTienToiThieu ?? (object)DBNull.Value); // Xử lý NULL
                return (cmd.ExecuteNonQuery());
            }
        }

        public int InsertKhuyenMai(KhuyenMai km)
        {
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                // Không chèn MaKM vì đây là IDENTITY column
                var str = "INSERT INTO khuyenmai (SoPTKM, TuNgay, DenNgay, TTienToiThieu) " +
                          "VALUES (@soptkm, @tungay, @denngay, @ttientoithieu)";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(str, conn);
                // Không thêm MaKM vì là IDENTITY
                cmd.Parameters.AddWithValue("soptkm", km.SoPTKM);
                cmd.Parameters.AddWithValue("tungay", km.TuNgay);
                cmd.Parameters.AddWithValue("denngay", km.DenNgay);
                cmd.Parameters.AddWithValue("ttientoithieu", km.TTienToiThieu ?? (object)DBNull.Value); // Xử lý NULL
                return (cmd.ExecuteNonQuery());
            }
        }



        //////Hổ trợ
        public List<HoTro> GetHoTros()
        {
            List<HoTro> list = new List<HoTro>();

            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                // Sử dụng INNER JOIN thay vì liệt kê các bảng trong FROM
                string str = "SELECT b.MABL, t.TENSP, k.TENKH, b.NOIDUNG " +
                             "FROM binhluan b " +
                             "INNER JOIN thongtinsp t ON b.MATTSP = t.MATTSP " +
                             "INNER JOIN khachhang k ON b.MAKH = k.MAKH";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new HoTro()
                        {
                            MABL = Convert.ToInt32(reader["MABL"]),
                            TENSP = reader["TENSP"].ToString(),
                            TENKH = reader["TENKH"].ToString(),
                            NOIDUNG = reader["NOIDUNG"].ToString(),
                        });
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return list;
        }

        public int[] XoaHoTro(string ht)
        {
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();

                var queryBINHLUAN = "DELETE FROM binhluan WHERE MABL = @mabl";
                Microsoft.Data.SqlClient.SqlCommand cmd1 = new Microsoft.Data.SqlClient.SqlCommand(queryBINHLUAN, conn);
                cmd1.Parameters.AddWithValue("@mabl", ht); // Thêm @ vào tên tham số
                int[] deleted = new int[1];
                deleted[0] = cmd1.ExecuteNonQuery();
                return deleted;
            }
        }

        public HoTro ViewHoTro(string Id)
        {
            HoTro ht = new HoTro();
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                // Sửa truy vấn để JOIN với bảng sản phẩm và khách hàng
                var str = @"
            SELECT bl.MABL, bl.MATTSP, bl.MAKH, bl.NOIDUNG, 
                   sp.TENSP, -- Lấy tên sản phẩm
                   kh.TENKH  -- Lấy tên khách hàng
            FROM binhluan bl
            LEFT JOIN thongtinsp sp ON bl.MATTSP = sp.MATTSP
            LEFT JOIN khachhang kh ON bl.MAKH = kh.MAKH
            WHERE bl.MABL = @mabl";

                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(str, conn);
                cmd.Parameters.AddWithValue("@mabl", Id);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        ht.MABL = Convert.ToInt32(reader["MABL"]);
                        ht.MATTSP = Convert.ToInt32(reader["MATTSP"]);
                        ht.MAKH = Convert.ToInt32(reader["MAKH"]);
                        ht.NOIDUNG = reader["NOIDUNG"] == DBNull.Value ? null : reader["NOIDUNG"].ToString();

                        // Thêm dòng code để gán giá trị cho TENSP và TENKH
                        ht.TENSP = reader["TENSP"] == DBNull.Value ? null : reader["TENSP"].ToString();
                        ht.TENKH = reader["TENKH"] == DBNull.Value ? null : reader["TENKH"].ToString();
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return ht;
        }




        ////Sản phẩm
        public (List<SanPham> sp, int pages, int page) GetSanPhams(int page)
        {
            List<SanPham> list = new List<SanPham>();

            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "select thongtinsp.*, loaisp.TenLoaiSP, thuonghieu.TenTH from thongtinsp, loaisp, thuonghieu where thongtinsp.maloaisp = loaisp.maloaisp and thongtinsp.math = thuonghieu.math\r\n";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new SanPham()
                        {
                            MaTTSP = Int32.Parse(reader["MaTTSP"].ToString()),
                            TenSP = reader["TenSP"].ToString(),
                            HinhAnh = reader["HinhAnh"].ToString(),
                            MaLoaiSP = Int32.Parse(reader["MaLoaiSP"].ToString()),
                            MaTH = Int32.Parse(reader["MaTH"].ToString()),
                            Gia = Int32.Parse(reader["Gia"].ToString()),
                            GiaKM = Int32.Parse(reader["GiaKM"].ToString()),
                            SoLuong = Int32.Parse(reader["SoLuong"].ToString()),
                            MauSac = reader["MauSac"].ToString(),
                            KhoiLuong = reader.IsDBNull(reader.GetOrdinal("KhoiLuong")) ? 0 : float.Parse(reader["KhoiLuong"].ToString()),
                            DoTuoi = reader.IsDBNull(reader.GetOrdinal("DoTuoi")) ? null : reader["DoTuoi"].ToString(),
                            XuatXu = reader.IsDBNull(reader.GetOrdinal("XuatXu")) ? null : reader["XuatXu"].ToString(),
                            KichThuoc = reader.IsDBNull(reader.GetOrdinal("KichThuoc")) ? null : reader["KichThuoc"].ToString(),
                            ThanhPhan = reader.IsDBNull(reader.GetOrdinal("ThanhPhan")) ? null : reader["ThanhPhan"].ToString(),
                            CongDung = reader.IsDBNull(reader.GetOrdinal("CongDung")) ? null : reader["CongDung"].ToString(),
                            HuongDanSD = reader.IsDBNull(reader.GetOrdinal("HuongDanSD")) ? null : reader["HuongDanSD"].ToString(),
                            TenLoaiSP = reader["TenLoaiSP"].ToString(),
                            TenTH = reader["TenTH"].ToString()
                        });
                    }
                    reader.Close();
                }
                conn.Close();
            }
            int Size = 8;
            int pages = (int)Math.Ceiling((double)list.Count / Size);
            List<SanPham> dssp = list.Skip((page - 1) * Size).Take(Size).ToList();
            return (dssp, pages, page);
        }

        public int[] XoaSanPham(string mattsp)
        {
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();

                var queryBINHLUAN = "delete from binhluan where MaTTSP=@mattsp";
                Microsoft.Data.SqlClient.SqlCommand cmd1 = new Microsoft.Data.SqlClient.SqlCommand(queryBINHLUAN, conn);
                cmd1.Parameters.AddWithValue("mattsp", mattsp);

                var queryGIOHANG = "delete from giohang where MaTTSP=@mattsp";
                Microsoft.Data.SqlClient.SqlCommand cmd3 = new Microsoft.Data.SqlClient.SqlCommand(queryGIOHANG, conn);
                cmd3.Parameters.AddWithValue("mattsp", mattsp);

                var queryTHONGTINSP = "delete from thongtinsp where MaTTSP=@mattsp";
                Microsoft.Data.SqlClient.SqlCommand cmd5 = new Microsoft.Data.SqlClient.SqlCommand(queryTHONGTINSP, conn);
                cmd5.Parameters.AddWithValue("mattsp", mattsp);

                var queryCTHD = "delete from cthd where MaTTSP=@mattsp";
                Microsoft.Data.SqlClient.SqlCommand cmd6 = new Microsoft.Data.SqlClient.SqlCommand(queryCTHD, conn);
                cmd6.Parameters.AddWithValue("mattsp", mattsp);

                var queryCTPN = "delete from ctpn where MaTTSP=@mattsp";
                Microsoft.Data.SqlClient.SqlCommand cmd7 = new Microsoft.Data.SqlClient.SqlCommand(queryCTPN, conn);
                cmd7.Parameters.AddWithValue("mattsp", mattsp);

                var queryGIAOHANG = "delete from giaohang where MaHD IN (select distinct MaHD from cthd where cthd.MaTTSP=@mattsp)";
                Microsoft.Data.SqlClient.SqlCommand cmd8 = new Microsoft.Data.SqlClient.SqlCommand(queryGIAOHANG, conn);
                cmd8.Parameters.AddWithValue("mattsp", mattsp);

                var queryHOADON = "delete from hoadon where MaHD IN (select distinct MAHD from cthd where cthd.MaTTSP=@mattsp)";
                Microsoft.Data.SqlClient.SqlCommand cmd9 = new Microsoft.Data.SqlClient.SqlCommand(queryHOADON, conn);
                cmd9.Parameters.AddWithValue("mattsp", mattsp);

                var queryPHIEUNHAP = "delete from phieunhap where MaPN IN (select distinct MaPN from ctpn where ctpn.MaTTSP=@mattsp)";
                Microsoft.Data.SqlClient.SqlCommand cmd10 = new Microsoft.Data.SqlClient.SqlCommand(queryPHIEUNHAP, conn);
                cmd10.Parameters.AddWithValue("mattsp", mattsp);

                int[] deleted = new int[10];
                deleted[0] = cmd1.ExecuteNonQuery();
                deleted[2] = cmd3.ExecuteNonQuery();
                deleted[4] = cmd5.ExecuteNonQuery();
                deleted[5] = cmd6.ExecuteNonQuery();
                deleted[6] = cmd7.ExecuteNonQuery();
                deleted[7] = cmd8.ExecuteNonQuery();
                deleted[8] = cmd9.ExecuteNonQuery();
                deleted[9] = cmd10.ExecuteNonQuery();
                return deleted;
            }
        }

        public SanPham ViewSanPham(string Id)
        {
            SanPham ttsp = new SanPham();
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "select * from thongtinsp where MaTTSP = @mattsp";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(str, conn);
                cmd.Parameters.AddWithValue("mattsp", Id);
                using (var reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    ttsp.MaTTSP = Int32.Parse(reader["MaTTSP"].ToString());
                    ttsp.TenSP = reader["TenSP"].ToString();
                    ttsp.HinhAnh = reader["HinhAnh"].ToString();
                    ttsp.MaLoaiSP = Int32.Parse(reader["MaLoaiSP"].ToString());
                    ttsp.MaTH = Int32.Parse(reader["MaTH"].ToString());
                    ttsp.Gia = Int32.Parse(reader["Gia"].ToString());
                    ttsp.GiaKM = Int32.Parse(reader["GiaKM"].ToString());
                    ttsp.SoLuong = Int32.Parse(reader["SoLuong"].ToString());
                    ttsp.MauSac = reader["MauSac"].ToString();
                    ttsp.KhoiLuong = reader.IsDBNull(reader.GetOrdinal("KhoiLuong")) ? 0 : float.Parse(reader["KhoiLuong"].ToString());
                    ttsp.DoTuoi = reader.IsDBNull(reader.GetOrdinal("DoTuoi")) ? null : reader["DoTuoi"].ToString();
                    ttsp.XuatXu = reader.IsDBNull(reader.GetOrdinal("XuatXu")) ? null : reader["XuatXu"].ToString();
                    ttsp.KichThuoc = reader.IsDBNull(reader.GetOrdinal("KichThuoc")) ? null : reader["KichThuoc"].ToString();
                    ttsp.ThanhPhan = reader.IsDBNull(reader.GetOrdinal("ThanhPhan")) ? null : reader["ThanhPhan"].ToString();
                    ttsp.CongDung = reader.IsDBNull(reader.GetOrdinal("CongDung")) ? null : reader["CongDung"].ToString();
                    ttsp.HuongDanSD = reader.IsDBNull(reader.GetOrdinal("HuongDanSD")) ? null : reader["HuongDanSD"].ToString();
                }
                conn.Close();
            }
            return (ttsp);
        }

        public int UpdateSanPham(SanPham ttsp)
        {
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "update thongtinsp set TenSP=@tensp, HinhAnh=@hinhanh, MaLoaiSP=@maloaisp, MaTH=@math, Gia=@gia, GiaKM=@giakm, SoLuong=@soluong, MauSac=@mausac, KhoiLuong=@khoiluong, DoTuoi=@dotuoi, XuatXu=@xuatxu, KichThuoc=@kichthuoc, ThanhPhan=@thanhphan, CongDung=@congdung, HuongDanSD=@huongdansd where MaTTSP=@mattsp";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(str, conn);
                cmd.Parameters.AddWithValue("mattsp", ttsp.MaTTSP);
                cmd.Parameters.AddWithValue("tensp", ttsp.TenSP);
                cmd.Parameters.AddWithValue("hinhanh", ttsp.HinhAnh);
                cmd.Parameters.AddWithValue("maloaisp", ttsp.MaLoaiSP);
                cmd.Parameters.AddWithValue("math", ttsp.MaTH);
                cmd.Parameters.AddWithValue("gia", ttsp.Gia);
                cmd.Parameters.AddWithValue("giakm", ttsp.GiaKM);
                cmd.Parameters.AddWithValue("soluong", ttsp.SoLuong);
                cmd.Parameters.AddWithValue("mausac", ttsp.MauSac);
                cmd.Parameters.AddWithValue("khoiluong", ttsp.KhoiLuong);
                cmd.Parameters.AddWithValue("dotuoi", ttsp.DoTuoi ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("xuatxu", ttsp.XuatXu ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("kichthuoc", ttsp.KichThuoc ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("thanhphan", ttsp.ThanhPhan ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("congdung", ttsp.CongDung ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("huongdansd", ttsp.HuongDanSD ?? (object)DBNull.Value);
                return (cmd.ExecuteNonQuery());
            }
        }

        public int InsertSanPham(SanPham ttsp)
        {
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();

                // Lấy tên loại sản phẩm từ bảng loaisp
                string tenLoaiSP = "";
                string queryGetTenLoaiSP = "SELECT TenLoaiSP FROM loaisp WHERE MaLoaiSP = @maloaisp";
                using (var cmdGetTenLoai = new Microsoft.Data.SqlClient.SqlCommand(queryGetTenLoaiSP, conn))
                {
                    cmdGetTenLoai.Parameters.AddWithValue("@maloaisp", ttsp.MaLoaiSP);
                    var result = cmdGetTenLoai.ExecuteScalar();
                    if (result != null)
                        tenLoaiSP = result.ToString();
                }

                // Lấy tên thương hiệu từ bảng thuonghieu
                string tenTH = "";
                string queryGetTenTH = "SELECT TenTH FROM thuonghieu WHERE MaTH = @math";
                using (var cmdGetTenTH = new Microsoft.Data.SqlClient.SqlCommand(queryGetTenTH, conn))
                {
                    cmdGetTenTH.Parameters.AddWithValue("@math", ttsp.MaTH);
                    var result = cmdGetTenTH.ExecuteScalar();
                    if (result != null)
                        tenTH = result.ToString();
                }

                // Câu lệnh INSERT với các cột mới
                var str = @"INSERT INTO thongtinsp 
                  (TenSP, HinhAnh, MaLoaiSP, MaTH, TenLoaiSP, TenTH, Gia, GiaKM, SoLuong, 
                   MauSac, KhoiLuong, DoTuoi, XuatXu, KichThuoc, ThanhPhan, CongDung, HuongDanSD) 
                  VALUES
                  (@tensp, @hinhanh, @maloaisp, @math, @tenloaisp, @tenth, @gia, @giakm, @soluong, 
                   @mausac, @khoiluong, @dotuoi, @xuatxu, @kichthuoc, @thanhphan, @congdung, @huongdansd)";

                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(str, conn);
                cmd.Parameters.AddWithValue("@tensp", ttsp.TenSP);
                cmd.Parameters.AddWithValue("@hinhanh", ttsp.HinhAnh);
                cmd.Parameters.AddWithValue("@maloaisp", ttsp.MaLoaiSP);
                cmd.Parameters.AddWithValue("@math", ttsp.MaTH);
                cmd.Parameters.AddWithValue("@tenloaisp", tenLoaiSP);  // Thêm tham số mới
                cmd.Parameters.AddWithValue("@tenth", tenTH);          // Thêm tham số mới
                cmd.Parameters.AddWithValue("@gia", ttsp.Gia);
                cmd.Parameters.AddWithValue("@giakm", ttsp.GiaKM);
                cmd.Parameters.AddWithValue("@soluong", ttsp.SoLuong);
                cmd.Parameters.AddWithValue("@mausac", ttsp.MauSac);
                cmd.Parameters.AddWithValue("@khoiluong", ttsp.KhoiLuong);
                cmd.Parameters.AddWithValue("@dotuoi", ttsp.DoTuoi ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@xuatxu", ttsp.XuatXu ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@kichthuoc", ttsp.KichThuoc ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@thanhphan", ttsp.ThanhPhan ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@congdung", ttsp.CongDung ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@huongdansd", ttsp.HuongDanSD ?? (object)DBNull.Value);

                return cmd.ExecuteNonQuery();
            }
        }


        public (List<SanPham> sp, int page, int pages) FindSanPham(string ten, int page = 1, int pageSize = 8)
        {
            List<SanPham> list = new List<SanPham>();
            int totalItems = 0;

            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();

                // Đếm tổng số sản phẩm phù hợp với từ khóa
                string countQuery = "SELECT COUNT(*) FROM thongtinsp " +
                                   "JOIN loaisp ON thongtinsp.MaLoaiSP = loaisp.MaLoaiSP " +
                                   "JOIN thuonghieu ON thongtinsp.MaTH = thuonghieu.MaTH " +
                                   "WHERE thongtinsp.TenSP LIKE @tensp";

                Microsoft.Data.SqlClient.SqlCommand countCmd = new Microsoft.Data.SqlClient.SqlCommand(countQuery, conn);
                countCmd.Parameters.AddWithValue("tensp", "%" + ten + "%");

                totalItems = (int)countCmd.ExecuteScalar();

                // Tính toán số trang
                int pages = (int)Math.Ceiling((double)totalItems / pageSize);
                page = Math.Max(1, Math.Min(page, Math.Max(1, pages)));

                // Truy vấn lấy sản phẩm cho trang hiện tại
                string query = "SELECT thongtinsp.*, loaisp.TenLoaiSP, thuonghieu.TenTH " +
                              "FROM thongtinsp " +
                              "JOIN loaisp ON thongtinsp.MaLoaiSP = loaisp.MaLoaiSP " +
                              "JOIN thuonghieu ON thongtinsp.MaTH = thuonghieu.MaTH " +
                              "WHERE thongtinsp.TenSP LIKE @tensp " +
                              "ORDER BY thongtinsp.MaTTSP " +
                              "OFFSET @offset ROWS FETCH NEXT @pageSize ROWS ONLY";

                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("tensp", "%" + ten + "%");
                cmd.Parameters.AddWithValue("offset", (page - 1) * pageSize);
                cmd.Parameters.AddWithValue("pageSize", pageSize);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new SanPham()
                        {
                            MaTTSP = Int32.Parse(reader["MaTTSP"].ToString()),
                            TenSP = reader["TenSP"].ToString(),
                            HinhAnh = reader["HinhAnh"].ToString(),
                            MaLoaiSP = Int32.Parse(reader["MaLoaiSP"].ToString()),
                            MaTH = Int32.Parse(reader["MaTH"].ToString()),
                            Gia = Int32.Parse(reader["Gia"].ToString()),
                            GiaKM = Int32.Parse(reader["GiaKM"].ToString()),
                            SoLuong = Int32.Parse(reader["SoLuong"].ToString()),
                            MauSac = reader["MauSac"].ToString(),
                            KhoiLuong = reader.IsDBNull(reader.GetOrdinal("KhoiLuong")) ? 0 : float.Parse(reader["KhoiLuong"].ToString()),
                            DoTuoi = reader.IsDBNull(reader.GetOrdinal("DoTuoi")) ? null : reader["DoTuoi"].ToString(),
                            XuatXu = reader.IsDBNull(reader.GetOrdinal("XuatXu")) ? null : reader["XuatXu"].ToString(),
                            KichThuoc = reader.IsDBNull(reader.GetOrdinal("KichThuoc")) ? null : reader["KichThuoc"].ToString(),
                            ThanhPhan = reader.IsDBNull(reader.GetOrdinal("ThanhPhan")) ? null : reader["ThanhPhan"].ToString(),
                            CongDung = reader.IsDBNull(reader.GetOrdinal("CongDung")) ? null : reader["CongDung"].ToString(),
                            HuongDanSD = reader.IsDBNull(reader.GetOrdinal("HuongDanSD")) ? null : reader["HuongDanSD"].ToString(),
                            TenLoaiSP = reader["TenLoaiSP"].ToString(),
                            TenTH = reader["TenTH"].ToString()
                        });
                    }
                }
                conn.Close();
            }

            return (list, page, (int)Math.Ceiling((double)totalItems / pageSize));
        }




        public List<LoaiSanPham> GetLoaiSanPhams()
        {
            List<LoaiSanPham> list = new List<LoaiSanPham>();

            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "select * from loaisp";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new LoaiSanPham()
                        {
                            MALOAISP = Int32.Parse(reader["MaLoaiSP"].ToString()),
                            TENLOAISP = reader["TenLoaiSP"].ToString(),
                        });
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return list;
        }

        public List<ThuongHieu> GetThuongHieus()
        {
            List<ThuongHieu> list = new List<ThuongHieu>();

            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "select * from thuonghieu";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new ThuongHieu()
                        {
                            MATH = Int32.Parse(reader["MaTH"].ToString()),
                            TENTH = reader["TenTH"].ToString(),
                        });
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return list;
        }

        public int InsertLoaiSanPham(string TenLoaiSP)
        {
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "insert into loaisp (TenLoaiSP) values(@tenloaisp)";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(str, conn);
                cmd.Parameters.AddWithValue("tenloaisp", TenLoaiSP);
                return (cmd.ExecuteNonQuery());
            }
        }

        public int InsertThuongHieu(ThuongHieu th)
        {
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "insert into thuonghieu (TenTH) values(@tenth)";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(str, conn);
                cmd.Parameters.AddWithValue("tenth", th.TENTH);
                return (cmd.ExecuteNonQuery());
            }
        }

        public int[] XoaLoaiSanPham(string lsp)
        {
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var queryLOAISANPHAM = "delete from loaisp where TenLoaiSP=@tenloaisp";
                Microsoft.Data.SqlClient.SqlCommand cmd1 = new Microsoft.Data.SqlClient.SqlCommand(queryLOAISANPHAM, conn);
                cmd1.Parameters.AddWithValue("tenloaisp", lsp);
                int[] deleted = new int[1];
                deleted[0] = cmd1.ExecuteNonQuery();
                return deleted;
            }
        }

        public int[] XoaThuongHieu(string th)
        {
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var queryTHUONGHIEU = "delete from thuonghieu where TenTH=@tenth";
                Microsoft.Data.SqlClient.SqlCommand cmd1 = new Microsoft.Data.SqlClient.SqlCommand(queryTHUONGHIEU, conn);
                cmd1.Parameters.AddWithValue("tenth", th);
                int[] deleted = new int[1];
                deleted[0] = cmd1.ExecuteNonQuery();
                return deleted;
            }
        }


        ////////Phiếu nhập
        public List<CTPN> GetDSCTPN(int MAPN)
        {
            List<CTPN> list = new List<CTPN>();
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                // Điều chỉnh câu truy vấn để phù hợp với cấu trúc bảng ctpn mới
                var str = "SELECT MaPN, ctpn.MaTTSP, ctpn.SoLuong, TENSP, GiaNhap, ThanhTien FROM ctpn JOIN thongtinsp sp ON ctpn.MaTTSP = sp.MaTTSP WHERE MaPN = @mapn";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(str, conn);
                cmd.Parameters.AddWithValue("mapn", MAPN);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new CTPN()
                        {
                            MAPN = Convert.ToInt32(reader["MaPN"]),
                            MATTSP = Convert.ToInt32(reader["MaTTSP"]),
                            SOLUONG = Convert.ToInt32(reader["SoLuong"]),
                            GIANHAP = Convert.ToInt32(reader["GiaNhap"]),
                            THANHTIEN = reader["ThanhTien"] != DBNull.Value ? Convert.ToInt32(reader["ThanhTien"]) : 0,
                            TENSP = reader["TENSP"].ToString()
                        });
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return list;
        }

        public int InsertCTPN(CTPN ct)
        {
            List<int> list = new List<int>();
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "SELECT MaTTSP FROM ctpn WHERE MaPN = @mapn AND MaTTSP = @mattsp";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(str, conn);
                cmd.Parameters.AddWithValue("mapn", ct.MAPN);
                cmd.Parameters.AddWithValue("mattsp", ct.MATTSP);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(Convert.ToInt32(reader["MaTTSP"]));
                    }
                    reader.Close();
                }

                // Tính ThanhTien nếu chưa được thiết lập
                int thanhTien = ct.THANHTIEN;
                if (thanhTien == 0)
                {
                    thanhTien = ct.SOLUONG * ct.GIANHAP;
                }

                if (list.Count() > 0)
                {
                    // Cập nhật bản ghi hiện có
                    var sql = "UPDATE ctpn SET SoLuong = @sl, GiaNhap = @gn, ThanhTien = @tt WHERE MaPN = @mapn AND MaTTSP = @mattsp";
                    Microsoft.Data.SqlClient.SqlCommand cmd1 = new Microsoft.Data.SqlClient.SqlCommand(sql, conn);
                    cmd1.Parameters.AddWithValue("sl", ct.SOLUONG);
                    cmd1.Parameters.AddWithValue("gn", ct.GIANHAP);
                    cmd1.Parameters.AddWithValue("tt", thanhTien);
                    cmd1.Parameters.AddWithValue("mapn", ct.MAPN);
                    cmd1.Parameters.AddWithValue("mattsp", ct.MATTSP);
                    return (cmd1.ExecuteNonQuery());
                }
                else
                {
                    // Thêm bản ghi mới
                    var sql = "INSERT INTO ctpn(MaPN, MaTTSP, SoLuong, GiaNhap, ThanhTien) VALUES(@mapn, @mattsp, @sl, @gn, @tt)";
                    Microsoft.Data.SqlClient.SqlCommand cmd1 = new Microsoft.Data.SqlClient.SqlCommand(sql, conn);
                    cmd1.Parameters.AddWithValue("sl", ct.SOLUONG);
                    cmd1.Parameters.AddWithValue("gn", ct.GIANHAP);
                    cmd1.Parameters.AddWithValue("tt", thanhTien);
                    cmd1.Parameters.AddWithValue("mapn", ct.MAPN);
                    cmd1.Parameters.AddWithValue("mattsp", ct.MATTSP);
                    return (cmd1.ExecuteNonQuery());
                }
            }
        }

        public int DeleteCTPN(int mapn, int mattsp)
        {
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "DELETE FROM ctpn WHERE MaPN = @mapn AND MaTTSP = @masp";
                Microsoft.Data.SqlClient.SqlCommand cmd1 = new Microsoft.Data.SqlClient.SqlCommand(str, conn);
                cmd1.Parameters.AddWithValue("mapn", mapn);
                cmd1.Parameters.AddWithValue("masp", mattsp);

                return (cmd1.ExecuteNonQuery());
            }
        }


        /////Nhập hàng
        ///
        public List<NhapHang> GetNhapHangs()
        {
            List<NhapHang> list = new List<NhapHang>();

            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                // Điều chỉnh câu truy vấn để phù hợp với tên cột chính xác
                string str = "SELECT * FROM phieunhap, nhacc, nhanvien WHERE phieunhap.MaNCC = nhacc.MaNCC AND phieunhap.MaNV = nhanvien.MaNV";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new NhapHang()
                        {
                            MAPN = Convert.ToInt32(reader["MaPN"]),
                            TONGTIENTT = Convert.ToInt32(reader["TongTienTT"]),
                            NGAYLAPPN = Convert.ToDateTime(reader["NgayLapPN"]),
                            TINHTRANGTT = Convert.ToInt32(reader["TinhTrangTT"]),
                            TENNCC = reader["TENNCC"].ToString(),
                            TENNV = reader["TENNV"].ToString(),
                        });
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return list;
        }

        public int XoaNhapHang(string nh)
        {
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                // Điều chỉnh câu truy vấn để phù hợp với tên cột chính xác
                var queryPHIEUNHAP = "DELETE FROM ctpn WHERE MaPN = @mapn;" +
                    "DELETE FROM phieunhap WHERE MaPN = @mapn";
                Microsoft.Data.SqlClient.SqlCommand cmd1 = new Microsoft.Data.SqlClient.SqlCommand(queryPHIEUNHAP, conn);
                cmd1.Parameters.AddWithValue("mapn", nh);
                return cmd1.ExecuteNonQuery();
            }
        }

        public NhapHang ViewNhapHang(string Id)
        {
            NhapHang nh = new NhapHang();
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                // Điều chỉnh câu truy vấn để phù hợp với tên cột chính xác
                var str = "SELECT * FROM phieunhap WHERE MaPN = @mapn";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(str, conn);
                cmd.Parameters.AddWithValue("mapn", Id);
                using (var reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    nh.MAPN = Convert.ToInt32(reader["MaPN"]);
                    nh.TONGTIENTT = Convert.ToInt32(reader["TongTienTT"]);
                    nh.NGAYLAPPN = Convert.ToDateTime(reader["NgayLapPN"]);
                    nh.TINHTRANGTT = Convert.ToInt32(reader["TinhTrangTT"]);
                    nh.MANCC = Convert.ToInt32(reader["MaNCC"]);
                    nh.MANV = Convert.ToInt32(reader["MaNV"]);
                }
                conn.Close();
            }
            return (nh);
        }

        public int UpdateNhapHang(NhapHang nh)
        {
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                // Điều chỉnh câu truy vấn để phù hợp với tên cột chính xác
                var str = "UPDATE phieunhap SET TinhTrangTT = @tinhtrangtt, MaNCC = @mancc, MaNV = @manv WHERE MaPN = @mapn";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(str, conn);
                cmd.Parameters.AddWithValue("tinhtrangtt", nh.TINHTRANGTT);
                cmd.Parameters.AddWithValue("mancc", nh.MANCC);
                cmd.Parameters.AddWithValue("manv", nh.MANV);
                cmd.Parameters.AddWithValue("mapn", nh.MAPN);
                return (cmd.ExecuteNonQuery());
            }
        }

        public int InsertNhapHang(NhapHang nh)
        {
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                // Điều chỉnh câu truy vấn để phù hợp với cấu trúc bảng phieunhap có IDENTITY
                var str = "INSERT INTO phieunhap (TongTienTT, NgayLapPN, TinhTrangTT, MaNCC, MaNV) VALUES (@tongtientt, @ngaylappn, @tinhtrangtt, @mancc, @manv); SELECT SCOPE_IDENTITY();";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(str, conn);
                cmd.Parameters.AddWithValue("tongtientt", nh.TONGTIENTT);
                cmd.Parameters.AddWithValue("ngaylappn", nh.NGAYLAPPN);
                cmd.Parameters.AddWithValue("tinhtrangtt", nh.TINHTRANGTT);
                cmd.Parameters.AddWithValue("mancc", nh.MANCC);
                cmd.Parameters.AddWithValue("manv", nh.MANV);

                // Trả về ID mới được tạo
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public List<CTPN> GetCTPNs()
        {
            List<CTPN> list = new List<CTPN>();

            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                // Điều chỉnh câu truy vấn để phù hợp với tên cột chính xác
                string str = "SELECT * FROM ctpn JOIN thongtinsp ON thongtinsp.MaTTSP = ctpn.MaTTSP";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new CTPN()
                        {
                            MAPN = Convert.ToInt32(reader["MaPN"]),
                            MATTSP = Convert.ToInt32(reader["MaTTSP"]),
                            TENSP = reader["TenSP"].ToString(),
                            GIANHAP = Convert.ToInt32(reader["GiaNhap"]),
                            SOLUONG = Convert.ToInt32(reader["SoLuong"]),
                            THANHTIEN = reader["ThanhTien"] != DBNull.Value ? Convert.ToInt32(reader["ThanhTien"]) : 0
                        });
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return list;
        }

        public int GetTotalProductCount()
        {
            int totalProducts = 0;

            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                // Truy vấn đếm tổng số sản phẩm
                string countQuery = "SELECT COUNT(*) FROM thongtinsp";
                Microsoft.Data.SqlClient.SqlCommand countCmd = new Microsoft.Data.SqlClient.SqlCommand(countQuery, conn);
                totalProducts = Convert.ToInt32(countCmd.ExecuteScalar());

                conn.Close();
            }

            return totalProducts;
        }

        public List<NhanVien> GetNhanViens()
        {
            List<NhanVien> DSNV = new List<NhanVien>();
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "SELECT * FROM nhanvien";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DSNV.Add(new NhanVien()
                        {
                            MaNV = Convert.ToInt32(reader["MaNV"]),
                            TenNV = reader["TenNV"].ToString(),
                            NgayVL = Convert.ToDateTime(reader["NgayVL"]),
                            Luong = Convert.ToInt32(reader["Luong"]),
                            SDT = reader["SDT"].ToString(),
                            Email = reader["Email"].ToString(),
                            MatKhau = reader["MatKhau"].ToString(),
                            CMND = reader["CMND"].ToString(),
                            DiaChi = reader["DiaChi"].ToString(),
                            LoaiNV = reader["LoaiNV"].ToString(),
                        });
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return DSNV;
        }

        public List<SanPham> GetSanPhams()
        {
            List<SanPham> DSSP = new List<SanPham>();

            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "select thongtinsp.*, loaisp.TenLoaiSP, thuonghieu.TenTH from thongtinsp, loaisp, thuonghieu where thongtinsp.maloaisp = loaisp.maloaisp and thongtinsp.math = thuonghieu.math\r\n";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DSSP.Add(new SanPham()
                        {
                            MaTTSP = Int32.Parse(reader["MaTTSP"].ToString()),
                            TenSP = reader["TenSP"].ToString(),
                            HinhAnh = reader["HinhAnh"].ToString(),
                            MaLoaiSP = Int32.Parse(reader["MaLoaiSP"].ToString()),
                            MaTH = Int32.Parse(reader["MaTH"].ToString()),
                            Gia = Int32.Parse(reader["Gia"].ToString()),
                            GiaKM = Int32.Parse(reader["GiaKM"].ToString()),
                            SoLuong = Int32.Parse(reader["SoLuong"].ToString()),
                            MauSac = reader["MauSac"].ToString(),
                            KhoiLuong = reader.IsDBNull(reader.GetOrdinal("KhoiLuong")) ? 0 : float.Parse(reader["KhoiLuong"].ToString()),
                            DoTuoi = reader.IsDBNull(reader.GetOrdinal("DoTuoi")) ? null : reader["DoTuoi"].ToString(),
                            XuatXu = reader.IsDBNull(reader.GetOrdinal("XuatXu")) ? null : reader["XuatXu"].ToString(),
                            KichThuoc = reader.IsDBNull(reader.GetOrdinal("KichThuoc")) ? null : reader["KichThuoc"].ToString(),
                            ThanhPhan = reader.IsDBNull(reader.GetOrdinal("ThanhPhan")) ? null : reader["ThanhPhan"].ToString(),
                            CongDung = reader.IsDBNull(reader.GetOrdinal("CongDung")) ? null : reader["CongDung"].ToString(),
                            HuongDanSD = reader.IsDBNull(reader.GetOrdinal("HuongDanSD")) ? null : reader["HuongDanSD"].ToString(),
                            TenLoaiSP = reader["TenLoaiSP"].ToString(),
                            TenTH = reader["TenTH"].ToString()
                        });
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return DSSP; 
        }
        public (List<HoaDonViewModel> hd, int pages, int page) GetDSHD(int page, string searchString = null)
        {
            List<HoaDonViewModel> list = new List<HoaDonViewModel>();
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "SELECT * FROM hoadon hd, khachhang kh WHERE hd.MaKH = kh.MaKH";

                // Kiểm tra nếu có chuỗi tìm kiếm
                if (!string.IsNullOrEmpty(searchString))
                {
                    // Kiểm tra xem searchString có phải là số (mã hóa đơn) không
                    if (int.TryParse(searchString, out int maHD))
                    {
                        // Tìm kiếm theo mã hóa đơn
                        str += " AND hd.MaHD = @MaHD";
                    }
                    else
                    {
                        // Tìm kiếm theo tên khách hàng
                        str += " AND kh.TenKH LIKE @TenKH";
                    }
                }

                str += " ORDER BY TinhTrangHD;";

                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(str, conn);

                // Thêm tham số nếu có tìm kiếm
                if (!string.IsNullOrEmpty(searchString))
                {
                    if (int.TryParse(searchString, out int maHD))
                    {
                        cmd.Parameters.AddWithValue("@MaHD", maHD);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@TenKH", "%" + searchString + "%");
                    }
                }

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new HoaDonViewModel()
                        {
                            MaHD = Convert.ToInt32(reader["MaHD"]),
                            MaKM = (reader["MaKM"] != DBNull.Value) ? Convert.ToInt32(reader["MaKM"]) : 0,
                            TenKH = reader["TenKH"].ToString(),
                            DiaChiGH = reader["DiaChiGH"].ToString(),
                            TongTienTT = Convert.ToInt32(reader["TongTienTT"]),
                            NgayLapHD = Convert.ToDateTime(reader["NgayLapHD"]),
                            TinhTrangTT = Convert.ToInt32(reader["TinhTrangTT"]),
                            TinhTrangHD = Convert.ToInt32(reader["TinhTrangHD"]),
                            SoTienNhan = Convert.ToInt32(reader["SoTienNhan"]),
                            SoTienTra = Convert.ToInt32(reader["SoTienTra"]),
                        });
                    }
                    reader.Close();
                }
                conn.Close();
            }

            int Size = 10;
            int pages = (int)Math.Ceiling((double)list.Count / Size);

            // Điều chỉnh trang hiện tại nếu cần
            if (page < 1) page = 1;
            if (page > pages && pages > 0) page = pages;

            List<HoaDonViewModel> dshd = list.Skip((page - 1) * Size).Take(Size).ToList();
            return (dshd, pages, page);
        }

        public (List<KhachHang> kh, int pages, int page) GetDSKH(int page, string searchString = null)
        {
            List<KhachHang> list = new List<KhachHang>();
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "SELECT * FROM khachhang";

                // Thêm điều kiện tìm kiếm nếu có
                if (!string.IsNullOrEmpty(searchString))
                {
                    str += " WHERE TenKH LIKE @TenKH";
                }

                str += " ORDER BY MaKH";

                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(str, conn);

                // Thêm tham số tìm kiếm nếu có
                if (!string.IsNullOrEmpty(searchString))
                {
                    cmd.Parameters.AddWithValue("@TenKH", "%" + searchString + "%");
                }

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new KhachHang()
                        {
                            MaKH = Convert.ToInt32(reader["MaKH"]),
                            TenKH = reader["TenKH"].ToString(),
                            GioiTinh = reader["GioiTinh"].ToString(),
                            SDT = reader["SDT"].ToString(),
                            Email = reader["Email"].ToString(),
                            MatKhau = reader["MatKhau"].ToString(),
                            CMND = reader["CMND"].ToString(),
                            DiaChi = reader["DiaChi"].ToString(),
                            LoaiKH = reader["LoaiKH"].ToString(),
                        });
                    }
                    reader.Close();
                }
                conn.Close();
            }

            int Size = 10; // Số lượng khách hàng trên mỗi trang
            int pages = (int)Math.Ceiling((double)list.Count / Size);

            // Điều chỉnh trang hiện tại nếu cần
            if (page < 1) page = 1;
            if (page > pages && pages > 0) page = pages;

            List<KhachHang> dskh = list.Skip((page - 1) * Size).Take(Size).ToList();
            return (dskh, pages, page);
        }

    }
}
